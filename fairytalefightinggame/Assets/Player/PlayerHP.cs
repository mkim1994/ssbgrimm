using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHP : MonoBehaviour {

	public int playerID;

	public static float respawn_delay = 3f;

	public float startingHealth = 100f;
	public float health = 100f;
	public uint stocks = 2;

	public RawImage[] gems;
	public Slider hpbar;
	public Texture empty_stock;

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
		Debug.Log("Dying");
		dead = true;
		stocks--;
		DestroyGem();

		GameObject avatar = GetComponent<CharacterAvatar>().myCharacter;
		avatar.GetComponent<FightControl>().enabled = false;
		avatar.GetComponent<MoveControl>().enabled = false;
		avatar.GetComponent<Animator>().SetTrigger("Loser");

		GameMain main = GameObject.FindWithTag("GameController").GetComponent<GameMain>();

		if ( stocks == 0 )
		{
			Debug.Log( "Player Died! Game over!" );
			main.GameOver( playerID );
		}
		else
		{
			main.DisplayKO( playerID );
			Invoke( "PlayerDead", respawn_delay );
		}
	}

	void PlayerDead()
	{
		health = startingHealth;
		GameObject avatar = GetComponent<CharacterAvatar>().myCharacter;
		avatar.GetComponent<Animator>().SetTrigger("Respawn");
		avatar.GetComponent<FightControl>().enabled = true;
		avatar.GetComponent<MoveControl>().enabled = true;
		dead = false;
	}

	void DestroyGem()
	{
		gems[stocks].texture = empty_stock;
	}
}
