using UnityEngine;
using System.Collections;

public class AttackHit : MonoBehaviour {

	public bool isPercent;
	public float damage;

	public float knockbackForce;
	public Vector2 knockbackDirection;

	void Start()
	{
		knockbackDirection.Normalize();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if ( other.tag == "Player" )
		{
			// apply knockback
			Rigidbody2D otherBody = other.GetComponent<Rigidbody2D>();
			otherBody.AddForce( knockbackForce * knockbackDirection, ForceMode2D.Impulse );


			// deal damage to the other player
			PlayerHP otherHP = other.GetComponentInParent<PlayerHP>();
			if ( isPercent )
			{
				otherHP.PercentDamage( damage );
			}
			else
			{
				otherHP.FlatDamage( damage );
			}
		}
		else if ( other.tag == "Attack" )
		{
			// here we can cancel attacks if they bounce
		}
	}
}
