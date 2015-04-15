using UnityEngine;
using UnityEngine.EventSystems;
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
		else
		{
			SwitchController();
		}
	}

	void SwitchController()
	{
		StandaloneInputModule[] modules = GetComponents<StandaloneInputModule>();
		modules[playerID - 1].enabled = false;
		modules[playerID].enabled = true;
	}

}
