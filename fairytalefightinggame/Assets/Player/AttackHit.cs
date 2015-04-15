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
		Debug.Log( "A Hit! A Hit I Say!" );
		if ( other.tag == "Avatar" )
		{
			// apply knockback
			Rigidbody2D otherBody = other.GetComponent<Rigidbody2D>();
			float sign = otherBody.transform.position.x > transform.position.x ? 1f : -1f;
			otherBody.AddForce( sign * knockbackForce * knockbackDirection, ForceMode2D.Impulse );


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
		//else if ( other.tag == "Attack" )
		{
			// here we can cancel attacks if they bounce
			// actually this won't work, since trigger events are only sent if
			// at least one of the colliders has a rigidbody attached :/
			// I guess we could put kinematic rigidbodies on all hitboxes? seems wasteful
		
			// unity forums also says collider will reference the parent when the parent has
			// a rigidbody attached but the collider doesn't? so maybe it will work but then
			// we can't distinguish them? correct approach seems to be rigidbodies on all hitboxes
		}
	}
}
