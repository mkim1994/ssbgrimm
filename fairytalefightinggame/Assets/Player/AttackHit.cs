using UnityEngine;
using System.Collections;

public class AttackHit : MonoBehaviour {

	public bool isPercent;
	public float damage;
	public float ultcharge;

	public Animator anim;
	public FightControl fc;
	public Collider2D parent;

	public float knockbackForce;
	public Vector2 knockbackDirection;

	void Start()
	{
		knockbackDirection.Normalize();

		//This is a projectile that has its info created in Projectiles.cs
		if (this.gameObject.transform.parent == null)
			return;

		anim = GetComponentInParent<Animator>();
		fc = GetComponentInParent<FightControl>();
		Transform po = this.gameObject.transform.parent;
		parent = po.gameObject.GetComponent<Collider2D> ();
		Debug.Log (parent);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other == parent) {
			Debug.Log ("Stop hitting yourself!");
			return;
		}
		//Debug.Log (other.name);
		Debug.Log( "A Hit! A Hit I Say!" );
		if ( other.tag == "Avatar" )
		{
			string charNum = other.gameObject.GetComponent<FightControl> ().AttackButton;
			charNum = (charNum [charNum.Length - 1]).ToString(); //extract last char (player number)
			charNum = "Player" + charNum + "Ult";

				// apply knockback

				gameObject.GetComponentInChildren<ParticleEmitter>().Emit();

				Rigidbody2D otherBody = other.GetComponent<Rigidbody2D>();
				float sign = otherBody.transform.position.x > transform.position.x ? 1f : -1f;
				Vector2 knockback = new Vector2(knockbackDirection.x * sign * knockbackForce, knockbackDirection.y * knockbackForce); //flip only x direction
				otherBody.AddForce( knockback, ForceMode2D.Impulse );
				other.GetComponent<Animator>().SetTrigger("Hit");

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

				if(ultcharge != 0.0f)
				{
					fc.ChargeUltimate( ultcharge );
				}
		}
		else if ( other.tag == "Attack" )
		{
			// here we can cancel attacks if they bounce
			// actually this won't work, since trigger events are only sent if
			// at least one of the colliders has a rigidbody attached :/
			// I guess we could put kinematic rigidbodies on all hitboxes? seems wasteful
		
			// unity forums also says collider will reference the parent when the parent has
			// a rigidbody attached but the collider doesn't? so maybe it will work but then
			// we can't distinguish them? correct approach seems to be rigidbodies on all hitboxes
			Debug.Log("Clang!");
			anim.SetTrigger("Cancel");
		}
		if (this.gameObject.name == "Heart(Clone)") {
			Debug.Log ("Projectile out");
			Destroy (this.gameObject);
		}
	}
}
