using UnityEngine;
using System.Collections;

public class MoveControl : MonoBehaviour {

	private bool jump = false;
	private bool jump_button_down = false;
	private bool crouch = false;
	private bool stand = false;
	private bool grounded = true;
	private bool jumping = false;
	private bool ducking = false;
	private Rigidbody2D body;
	private Animator anim;

	public string Vertical;
	public string Horizontal;
	
	public float jumpforce;
	public float speed;
	public float direction;
	private float oldDirection;

	public float originalspeed;

	void Start() {

		if (transform.position.x > 1) { //if you spawn on the right side of the map flip transform (dumb fix but whatevs)
			gameObject.transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, 1f);
		}
		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		oldDirection = transform.localScale.x / Mathf.Abs (transform.localScale.x);
		originalspeed = speed;
	}

	// Each frame - read controller movement input 
	void Update() {

		if ( Input.GetAxis( Vertical ) > 0f  )
		{
			// start a jump only on the first frame of jump_button_down
			if ( grounded && !jumping && !jump_button_down )
			{
					jump = true;
			}
			jump_button_down = true;

			// we can't be crouched while holding up
			crouch = false;
			if ( ducking )
			{
				stand = true;
			}	
		}
		else 
		{
			// this has a slight issue where you can actually double-tap so fast it doesn't register
			// I doubt the same issue occured with Input.GetButtonUp because it reads OS messages :(
			jump_button_down = false;

			// holding down means crouch
			if ( Input.GetAxis( Vertical ) < 0f )
			{
				crouch = true;
			}
			else // if ( Input.GetAxis( Vertical ) == 0f )
			{
				// stop crouching as soon as we let go
				crouch = false;
				if ( ducking )
				{
					stand = true;
				}
			} 
		}

		float h = Input.GetAxis( Horizontal );
		direction = h < 0f ? -1f : h > 0f ? 1f : 0f;
	}

	// Steady rate - update physics movement
	void FixedUpdate() {


		// todo - h_vel in air should maybe be handled differently
		//        i.e. fast characters don't move super fast in midair
		//        maybe set a constant horizontal speed for all chars?
		float h_vel = direction * speed;
		float v_vel = body.velocity.y;

		body.velocity = new Vector2( h_vel, v_vel );
		anim.SetFloat( "Speed", Mathf.Abs( h_vel ));

		if (oldDirection != direction && direction != 0){
			Vector3 tempScale = transform.localScale;
			tempScale.x *= -1;
			transform.localScale = tempScale;
		}
		if (direction != 0) {oldDirection = direction;}

		if ( jump )
		{
			speed = originalspeed;
			// check that we're still grounded
			if ( grounded )
			{
				jump = false;
				crouch = false;
				ducking = false;
				jumping = true; // we could just set grounded = false here
				                // but this might cause bugs if we fail to jump
				Jump();
			}
		}
		else if ( crouch )
		{
			speed=0; //can't move while crouching
			// check that we're on the ground
			// don't let us crouch while jumping before we leave the ground
			if ( grounded && !jumping )
			{
				crouch = false;
				ducking = true;
				Crouch();
			}
		}
		else if ( stand )
		{
			speed = originalspeed;
			if ( ducking )
			{
				stand = false;
				ducking = false;
				StandUp();
			}		
		}
	}

	void OnCollisionEnter2D( Collision2D collision )
	{
		if ( collision.gameObject.tag == "Ground" )
		{
			grounded = true;
			jumping = false;
			anim.SetBool( "Jump", false );
		}
	}

	void OnCollisionExit2D( Collision2D collision )
	{
		if ( collision.gameObject.tag == "Ground" )
		{
			grounded = false;
		}
	}

	void Jump()
	{
		anim.SetBool( "Jump", true );

		body.AddForce( new Vector2( 0f, jumpforce ), ForceMode2D.Impulse );
	}

	void Crouch()
	{
		anim.SetBool( "Crouch", true );
	}

	void StandUp()
	{
		anim.SetBool( "Crouch", false );
	}

}
