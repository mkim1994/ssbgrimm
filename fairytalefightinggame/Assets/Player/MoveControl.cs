using UnityEngine;
using System.Collections;

public class MoveControl : MonoBehaviour {

	private bool jump = false;
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

	void Start() {

		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		oldDirection = transform.localScale.x / Mathf.Abs (transform.localScale.x);
	}

	// Each frame - read controller movement input 
	void Update() {

		if ( grounded && !jumping && Input.GetAxis( Vertical ) > 0f )
		{
			jump = true;
		}
		else if ( !crouch && Input.GetAxis( Vertical ) < 0f )
		{
			crouch = true;
		}
		else if ( Input.GetAxis( Vertical ) >= 0f )
		{
			crouch = false;

			if ( ducking )
			{
				stand = true;
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
			jump = false;

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
