﻿using UnityEngine;
using System.Collections;

public class BambooUlt : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		float delay = Random.Range(0, 10);
		delay = delay / 2;
		Invoke ("Go", delay);
	}
	
	void Go () {
		int force = Random.Range(4500, 5500);
		Rigidbody2D body = gameObject.GetComponent<Rigidbody2D> ();
		body.WakeUp ();
		body.AddForce(new Vector2(0f,force));
		Invoke ("Kill", 3);
	}

	void Kill (){
		Destroy (gameObject);
	}
}
