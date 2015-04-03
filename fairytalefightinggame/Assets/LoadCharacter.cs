using UnityEngine;
using System.Collections;

public class LoadCharacter : MonoBehaviour {

	public GameObject[] prefabs;
	private GameObject myCharacter;

	public string[] buttons;

	// Use this for initialization
	void Start () {
	
		// todo - find out how to pass values from character select
		//        see Object.DontDestroyOnLoad()

		// spawn a copy of the prefab for my character
		Load( 0 );

		// set up this character to be controlled by me

		MoveControl mc = myCharacter.GetComponent<MoveControl>();
		mc.LeftButton = buttons[0];
		mc.RightButton = buttons[1];
		mc.UpButton = buttons[2];
		mc.DownButton = buttons[3];

		FightControl fc = myCharacter.GetComponent<FightControl>();
		fc.BlockButton = buttons[4];
		fc.AttackButton = buttons[5];
		fc.SpecialButton = buttons[6];
		fc.UltimateButton = buttons[7];
	}

	void Load( int index )
	{
		if ( myCharacter )
		{
			Destroy( myCharacter );
		}
		myCharacter = (GameObject)Instantiate( prefabs[index], transform.position, Quaternion.identity );
		myCharacter.transform.parent = transform;
	}
}
