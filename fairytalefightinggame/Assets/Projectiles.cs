using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour {

	public Rigidbody2D heart;
	private GameObject clone;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Y)) {
			Invoke("Fire", 0.25f);
		}
	}

	void Fire() {
		Rigidbody2D g = Instantiate (heart,
		                             transform.position + (new Vector3 (0.45f * transform.localScale.x,
		                                                                -0.317f,
		                                                                0f)),
		                             transform.rotation) as Rigidbody2D;
		clone = g.gameObject;
		clone.SetActive(true);
		g.velocity = transform.TransformDirection(new Vector3 (5 * transform.localScale.x, 0, 0));
		Destroy (clone, 1.5f);
	}
}
