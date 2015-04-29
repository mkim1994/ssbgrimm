using UnityEngine;
using System.Collections;

public class SortingLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<ParticleRenderer>().sortingLayerName = "Hud"; //make it actually show up and not be behind background
		gameObject.GetComponent<ParticleEmitter> ().emit = false; //start off

	}

}
