using UnityEngine;
using System.Collections;

public class BambooUlt : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		float delay = Random.Range(0, 6);
		if (delay != 5) {
			float randValue = Random.value;
			if (randValue < .35f) // 45% of the time
			{
				delay = 5;
			}
		}
		delay = delay / 2;
		Invoke ("Go", delay);
	}
	
	void Go () {
		int force = Random.Range(3500, 4500);
		Rigidbody2D body = gameObject.GetComponent<Rigidbody2D> ();
		body.WakeUp ();
		body.AddForce(new Vector2(0f,force));
		Invoke ("Kill", 3);
	}

	void Kill (){
		Destroy (gameObject);
	}
}
