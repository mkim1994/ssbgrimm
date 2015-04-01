using UnityEngine;
using System.Collections;

public class LoadCharacter : MonoBehaviour {

	public int character;

	public GameObject[] prefabs;

	// Use this for initialization
	void Start () {
	
		// todo - find out how to pass values from character select
		//        see Object.DontDestroyOnLoad()

		// spawn a copy of the prefab for my character
		GameObject myCharacter = (GameObject)Instantiate( prefabs[character], transform.position, Quaternion.identity );
		// attach my character to me (it will now have my position)
		myCharacter.transform.parent = transform;
	}
}
