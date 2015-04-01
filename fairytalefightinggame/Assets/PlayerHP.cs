using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHP : MonoBehaviour {

	private static float healthBarDestroyDelay = 0.0f;

	public float startingHealth = 100.0f;
	public uint startingStocks = 2;
	private float health;
	private uint stocks;

	public Slider[] healthBars;

	// Use this for initialization
	void Start () 
	{
		health = startingHealth;
		stocks = startingStocks;
	}

	void Update()
	{
		if ( health == 0 )
		{
			PlayerDie();
		}
	}

	void FlatDamage( float damage )
	{
		health -= damage ;

		if ( health < 0 )
		{
			health = 0;
		}
		else if ( health > startingHealth )
		{
			health = startingHealth;
		}
	}

	void PercentDamage( float damage )
	{
		health -= startingHealth * Mathf.Clamp01( damage );

		if ( health < 0 )
		{
			health = 0;
		}
		else if ( health > startingHealth )
		{
			health = startingHealth;
		}
	}

	void PlayerDie()
	{
		stocks--;
		Destroy( healthBars[stocks], healthBarDestroyDelay );
		health = startingHealth;

		// play a death animation and respawn here

		if ( stocks == 0 )
		{
			// trigger the end of the game here
		}
	}
}
