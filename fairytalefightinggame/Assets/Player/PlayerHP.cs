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

	private bool dead = false;

	// Use this for initialization
	void Start () 
	{
		health = startingHealth;
		stocks = startingStocks;

		foreach ( Slider slider in healthBars )
		{
			slider.maxValue = startingHealth;
			slider.minValue = 0f;
			slider.value = slider.maxValue;
		}
	}

	void Update()
	{
		if ( health <= 0f )
		{
			if ( !dead )
			{
				PlayerDying();
			}
			else
			{
				PlayerDead();
			}		
		}

		if ( stocks > 0)
		{
			Slider slider = healthBars[stocks-1];
			slider.value = health;
		}
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
		Destroy( healthBars[stocks], healthBarDestroyDelay );
		health = startingHealth;

		// start playing a death animation and respawn here

		if ( stocks == 0 )
		{
			// trigger the end of the game here
		}
	}

	void PlayerDead()
	{
		// nothing here yet, could do some animation for the other player?
	}
}
