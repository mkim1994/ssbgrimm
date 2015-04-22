using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHP : MonoBehaviour {

	public static float respawn_delay = 0f;

	public float startingHealth = 100f;
	public float health = 100f;
	public uint stocks = 2;

	public Slider hpbar;

	private bool dead = false;
	private FightControl control;
	private Animator anim;

	void Start()
	{
	}

	// Use this for initialization
	public void Init ( Animator animator, FightControl fightcontrol ) 
	{
		hpbar.maxValue = startingHealth;
		hpbar.minValue = 0f;
		hpbar.value = startingHealth;

		control = fightcontrol;
		anim = animator;
	}

	void Update()
	{
		if ( health <= 0f )
		{
			if ( !dead )
			{
				PlayerDying();
			}		
		}

		hpbar.value = health;
	}

	public void FlatDamage( float damage )
	{
		if (anim.GetBool ("Block")) {
			control.sheild -= damage/2;
			Debug.Log("blocked");
		}
		else{
			health -= damage ;
			Debug.Log("not blocked");
		}

		if ( health < 0f )
		{
			health = 0f;
		}
		else if ( health > startingHealth )
		{
			health = startingHealth;
		}
	}

	public void PercentDamage( float damage )
	{
		float loss = startingHealth * Mathf.Clamp01 (damage);
		if (anim.GetBool ("Block")) {
			control.sheild -= loss/2;
		}
		else {health -= damage;}

		if ( health < 0f )
		{
			health = 0f;
		}
		else if ( health > startingHealth )
		{
			health = startingHealth;
		}
	}

	void PlayerDying()
	{
		dead = true;
		stocks--;
		// start playing a death animation and respawn here
		DestroyApple();

		if ( stocks == 0 )
		{
			// TODO - trigger the end of the game here
			Debug.Log( "Player Died! Game over!" );
			Application.LoadLevel( "MainMenu" );
		}
		else
		{
			Invoke( "PlayerDead", respawn_delay );
		}
	}

	void PlayerDead()
	{
		health = startingHealth;
		// respawn the player here
		// could just be triggering an animation, or spawning a whole new avatar
		// could be swapping out to a different character for tag team!
		dead = false;
	}

	void DestroyApple()
	{
		foreach (Transform child in hpbar.transform.parent)
		{
			if ( child.tag == "Apple" )
			{
				Destroy( child.gameObject );
				break;
			}
		}
	}
}
