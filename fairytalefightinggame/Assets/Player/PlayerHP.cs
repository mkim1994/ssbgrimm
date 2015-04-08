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

	// Use this for initialization
	void Start () 
	{
		hpbar.maxValue = startingHealth;
		hpbar.minValue = 0f;
		hpbar.value = startingHealth;
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
		health -= damage ;

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
		health -= startingHealth * Mathf.Clamp01( damage );

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
}
