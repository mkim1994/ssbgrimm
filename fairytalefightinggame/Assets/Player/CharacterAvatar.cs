using UnityEngine;
using System.Collections;

public class CharacterAvatar : MonoBehaviour {

	public GameObject[] prefabs;
	private GameObject myCharacter;

	private static string[,] buttons = { {"Vertical1", "Horizontal1", "Block1", "Attack1", "Special1", "Ultimate1" },
	                                     {"Vertical2", "Horizontal2", "Block2", "Attack2", "Special2", "Ultimate2" } };

	// set up this character to be controlled by a player
	public void InitControls( int playerID ) {

		MoveControl mc = myCharacter.GetComponent<MoveControl>();
		mc.Vertical = buttons[playerID, 0];
		mc.Horizontal = buttons[playerID, 1];

		FightControl fc = myCharacter.GetComponent<FightControl>();
		fc.BlockButton = buttons[playerID, 2];
		fc.AttackButton = buttons[playerID, 3];
		fc.SpecialButton = buttons[playerID, 4];
		fc.UltimateButton = buttons[playerID, 5];
	}

	// swap to a new character
	public void SpawnCharacter( int characterID )
	{
		myCharacter = (GameObject)Instantiate( prefabs[characterID], transform.position, Quaternion.identity );
		myCharacter.transform.parent = transform;

		foreach ( GameObject avatar in GameObject.FindGameObjectsWithTag("Avatar") )
		{
			Physics2D.IgnoreCollision( myCharacter.GetComponent<Collider2D>(), avatar.GetComponent<Collider2D>(), true );
		}
	}
}
