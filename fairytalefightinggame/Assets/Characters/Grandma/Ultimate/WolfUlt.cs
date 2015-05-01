using UnityEngine;
using System.Collections;

public class WolfUlt : MonoBehaviour {

	public float dammage;
	private Rigidbody2D body;
	private bool moving;
	private float direction;
	private Animator anim;
	private MoveControl mControl;

	void Start(){
		body = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		mControl = gameObject.GetComponent<MoveControl> ();
	}

	void BBGUlt(){
		direction = gameObject.transform.localScale.x;
		moving = true;
		Physics2D.IgnoreCollision( GameObject.FindGameObjectsWithTag("Avatar")[0].GetComponent<Collider2D>(),
		                          GameObject.FindGameObjectsWithTag("Avatar")[1].GetComponent<Collider2D>(), false );
	}

	void Update() {
		if (moving) {
			mControl.enabled = false;
			body.AddForce(new Vector2(direction * 5000f,0f)); //move
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (moving) { //if using ult
			GameObject other = coll.collider.gameObject;
			Debug.Log (other.tag);
			if (other.tag == "Avatar") { //hit player
				Debug.Log ("Rarr!");
				moving = false;
				PlayerHP otherHP = other.GetComponentInParent<PlayerHP> ();
				otherHP.FlatDamage (dammage);
				anim.SetTrigger ("UltimateEnd"); //end ult anim
				ParticleEmitter[] emitters = gameObject.GetComponentsInChildren<ParticleEmitter>();
				for (int i = 0; i < emitters.Length; i++){
					emitters[i].Emit();
				}
				Invoke("EndUlt",0.5f);
			} else if (other.tag == "Wall") { //hit wall
				moving = false;
				anim.SetTrigger ("UltimateEnd"); //end ult anim
				Invoke("EndUlt",0.5f);
			}
		}
	}

	void EndUlt(){
		mControl.enabled = true;
		Physics2D.IgnoreCollision( GameObject.FindGameObjectsWithTag("Avatar")[0].GetComponent<Collider2D>(),
		                          GameObject.FindGameObjectsWithTag("Avatar")[1].GetComponent<Collider2D>(), true );
	}
}
