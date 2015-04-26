using UnityEngine;
using System.Collections;

public class Polymorph : MonoBehaviour {

	public Sprite pumpkin;
	private float duration;
	private GameObject polyPlayer;

	void unPoly(){
		if (duration >= 3.5f) { //unpolymorph
			polyPlayer.GetComponent<MoveControl> ().enabled = true;
			polyPlayer.GetComponent<FightControl> ().enabled = true;
			Animator anim = polyPlayer.GetComponent<Animator> ();
			anim.enabled = true;
			anim.Play ("Run");
			anim.Play ("Idle");
		} else {
			Rigidbody2D body = polyPlayer.GetComponent<Rigidbody2D>();
			body.velocity = new Vector2(-body.velocity.x,0f); //turn around
			polyPlayer.transform.localScale = new Vector2(-polyPlayer.transform.localScale.x,1f);//flip
			Invoke ("unPoly", 1.0f);
			duration += 1.0f;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Avatar") {
			GameObject player = other.gameObject;
			player.GetComponent<FightControl>().enabled = false;
			player.GetComponent<MoveControl>().enabled = false;
			player.GetComponent<Animator>().enabled = false;
			player.GetComponent<SpriteRenderer>().sprite = pumpkin;
			player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f,0f);
			polyPlayer = player;
			Invoke("unPoly", 0.5f);
			//Destroy(gameObject);
		}
	}

}
