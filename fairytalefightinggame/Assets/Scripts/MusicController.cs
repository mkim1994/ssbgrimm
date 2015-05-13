﻿using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	// Use this for initialization


	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
		
		GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
