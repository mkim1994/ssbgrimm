using UnityEngine;
using System.Collections;

public class MoveControl : MonoBehaviour {

	private bool jump = false;
	private bool crouch = false;
	private bool stand = false;
	private bool grounded = true;
	private bool ducking = true;
	private Rigidbody2D body;
	private Animator anim;

	public string UpButton;
	public string DownButton;
	public string RightButton;
	public string LeftButton;
	
	public float jumpforce;
	public float speed;
	public float direction;
	private float oldDirection;

	void Start() {

		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		oldDirection = direction;
	}

	// Each frame - read controller movement input 
	void Update() {

		if ( grounded && Input.GetButtonDown( UpButton ) )
		{
			jump = true;
		}
		else if ( grounded && Input.GetButtonDown( DownButton ) )
		{
			crouch = true;
		}
		else if ( ducking && Input.GetButtonUp( DownButton ) )
		{
			stand = true;
		}

		if ( Input.GetButton( RightButton ) )
		{
			direction = 1.0f;
		}
		else if ( Input.GetButton( LeftButton ) )
		{
			direction = -1.0f;
		}
		else
		{
			direction = 0.0f;
		}
	}

	// Steady rate - update physics movement
	void FixedUpdate() {

		// todo - h_vel in air should maybe be handled differently
		//        i.e. fast characters don't move super fast in midair
		//        maybe set a constant horizontal speed for all chars?
		float h_vel = direction * speed;
		float v_vel = body.velocity.y;

		body.velocity = new Vector2( h_vel, v_vel );
		anim.SetFloat( "Speed", Mathf.Abs( h_vel ) );

		if (oldDirection != direction && direction != 0){
			Vector3 tempScale = transform.localScale;
			tempScale.x *= -1;
			transform.localScale = tempScale;
		}
		if (direction != 0) {oldDirection = direction;}

		if ( jump )
		{
			// check that we're still grounded - in case we are launched up
			if ( grounded )
			{
				jump = false;
				Jump();
			}
		}
		else if ( crouch )
		{
			// check that we're still grounded - in case we are launched up
			if ( grounded )
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
