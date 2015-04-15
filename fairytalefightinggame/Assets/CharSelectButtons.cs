using UnityEngine;
using System.Collections;

public class CharSelectButtons : MonoBehaviour {

	public GameMain main = null;

	private int playerID = 0;

	// Use this for initialization
	void Start () {
		main = GameObject.FindWithTag("GameController").GetComponent<GameMain>();
	}
	
	public void Select( int id )
	{
		main.SetCharacterForPlayer( playerID, id );
		playerID++;

		if ( playerID >= main.numplayers )
		{
			main.BeginFight();
		}
	}

}
