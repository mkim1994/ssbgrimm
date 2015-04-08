using UnityEngine;
using System.Collections;

public class CharacterAvatar : MonoBehaviour {

	public GameObject[] prefabs;
	private GameObject myCharacter;

	private static string[,] buttons = { {"Left1", "Right1", "Up1", "Down1", "Block1", "Attack1", "Special1", "Ultimate1" },
	                                     {"Left2", "Right2", "Up2", "Down2", "Block2", "Attack2", "Special2", "Ultimate2" } };

	// set up this character to be controlled by a player
	public void InitControls( int playerID ) {

		MoveControl mc = myCharacter.GetComponent<MoveControl>();
		mc.LeftButton = buttons[playerID, 0];
		mc.RightButton = buttons[playerID, 1];
		mc.UpButton = buttons[playerID, 2];
		mc.DownButton = buttons[playerID, 3];

		FightControl fc = myCharacter.GetComponent<FightControl>();
		fc.BlockButton = buttons[playerID, 4];
		fc.AttackButton = buttons[playerID, 5];
		fc.SpecialButton = buttons[playerID, 6];
		fc.UltimateButton = buttons[playerID, 7];
	}

	// swap to a new character
	public void SpawnCharacter( int characterID )
	{
		if ( myCharacter )
		{
			Destroy( myCharacter );
		}
		myCharacter = (GameObject)Instantiate( prefabs[characterID], transform.position, Quaternion.identity );
		myCharacter.transform.parent = transform;
	}
}
