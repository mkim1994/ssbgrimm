using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour {

	public Rigidbody2D heart;
	public Rigidbody2D ultHeart;
	public GameObject bambooos;
	private GameObject clone;
	private string special;
	private string ultimate;

	// Use this for initialization
	void Start () {
		FightControl f = GetComponent<FightControl> ();
		special = f.SpecialButton;
		ultimate = f.UltimateButton;
	}

	void Fire() {
		Debug.Log ("fire!");
		Rigidbody2D g = Instantiate (heart,
		                             transform.position + (new Vector3 (0.45f * transform.localScale.x,
		                                                                -0.317f,
		                                                                0f)),
		                             transform.rotation) as Rigidbody2D;
		clone = g.gameObject;
		AttackHit ah = clone.GetComponent<AttackHit> ();
		GameObject po = heart.gameObject.transform.parent.gameObject;
		ah.anim = po.GetComponent<Animator> ();
		ah.fc = po.GetComponent<FightControl> ();
		ah.parent = po.GetComponent<Collider2D> ();
		Debug.Log (po.name);
		clone.SetActive(true);
		//clone.transform.parent = heart.transform.parent;
		g.velocity = transform.TransformDirection(new Vector3 (5 * transform.localScale.x, 0, 0));
		Destroy (clone, 1.5f);
	}
	void FGUlt() {
		Rigidbody2D h = Instantiate (ultHeart,
		                             transform.position + (new Vector3 (0.45f * transform.localScale.x,
		                                   -0.317f,
		                                   0f)),
		                             transform.rotation) as Rigidbody2D;
		clone = h.gameObject;
		string tg = gameObject.GetComponent<FightControl> ().AttackButton;
		tg = (tg [tg.Length - 1]).ToString(); //extract last char (player number)
		tg = "Player" + tg + "Ult";
		clone.tag = tg;
		clone.SetActive(true);
		h.velocity = transform.TransformDirection(new Vector3 (3.5f * transform.localScale.x, 0, 0));
		Destroy (clone, 15.0f);
	}
	void BCUlt() {
		GameObject clone = Instantiate (bambooos);

		foreach (Transform child in clone.transform) {
			Physics2D.IgnoreCollision( gameObject.GetComponent<Collider2D>(), child.gameObject.GetComponent<Collider2D>(), true );
		}

		string tg = gameObject.GetComponent<FightControl> ().AttackButton;
		tg = (tg [tg.Length - 1]).ToString(); //extract last char (player number)
		tg = "Player" + tg + "Ult";
		clone.tag = tg;
		clone.SetActive(true);
		Destroy (clone, 10.0f);
	}
}
