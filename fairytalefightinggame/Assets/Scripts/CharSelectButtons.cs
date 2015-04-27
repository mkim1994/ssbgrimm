using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class CharSelectButtons : MonoBehaviour {

	public Sprite[] indicators;

	public GameMain main = null;
	private Image indicatorPlayer = null;

	private int playerID = 0;

	// Use this for initialization
	void Start () {
		main = GameObject.FindWithTag("GameController").GetComponent<GameMain>();
		indicatorPlayer = GameObject.FindWithTag("CharacterSelect").GetComponent<Image>();
		indicatorPlayer.sprite = indicators[0];
	}
	
	public void Select( int id )
	{
		GameObject.FindWithTag("GameController").GetComponent<GameMain>().SetCharacterForPlayer( playerID, id );
		playerID++;

		if ( playerID >= main.numplayers )
		{
			playerID = 0;
			GameObject.FindWithTag("GameController").GetComponent<GameMain>().BeginFight();
		}
		else
		{
			indicatorPlayer.sprite = indicators[playerID];
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
