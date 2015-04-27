using UnityEngine;
using System.Collections;

public class GoBack : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetButtonDown("Block1") || Input.GetButtonDown("Block2"))
		{
			GetComponent<GameMain>().shouldDestroy = true;
			Application.LoadLevel("MainMenu");
		}

	}
}
