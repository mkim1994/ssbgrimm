﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMain : MonoBehaviour {

	private class PlayerInfo {

		public int characterID = 0;
		public GameObject playerObject = null;

	}

	private PlayerInfo[] players;
	public int numplayers = 2;
	public Texture2D[] backgrounds;
	public Vector2[] spawnPoints;
	public GameObject playerPrefab;

	void Start()
	{
		// this should live until we quit the game
		DontDestroyOnLoad(transform.gameObject);

		players = new PlayerInfo[numplayers];
		for ( int i = 0; i < numplayers; i++ )
		{
			players[i] = new PlayerInfo();
		}

		// TODO- MAKE BUTTONS WORK, PUT IN CHAR SELECT
		BeginFight();
	}

	void SetRandomBackground()
	{
		GameObject background = GameObject.FindWithTag( "Background" );
		RawImage image = background.GetComponent<RawImage>();
		image.texture = backgrounds[(int)(Random.value * backgrounds.Length)];
	}

	// use this from buttons in the character select scene
	void SetCharacterForPlayer( int playerID, int characterID )
	{
		players[playerID].characterID = characterID;
	}

	public void BeginFight()
	{
		Application.LoadLevel( "FightStage" );
	}

	void OnLevelWasLoaded( int level )
	{
		// the fight stage - we just called BeginFight()
		if ( Application.loadedLevelName == "FightStage" )
		{
			// set up the background
			SetRandomBackground();

			// spawn the players; get ready to fight!
			for ( int i = 0; i < players.Length; i++ )
			{
				PlayerInfo player = players[i];
				player.playerObject = (GameObject)Instantiate( playerPrefab, new Vector3( spawnPoints[i].x , spawnPoints[i].y, 0f ), Quaternion.identity );

				PlayerHP playerHP = player.playerObject.GetComponent<PlayerHP>();
				// TODO - make hpbars not suck
				playerHP.hpbar = ((i == 0) ? GameObject.FindWithTag("HP1") : GameObject.FindWithTag("HP2")).GetComponent<Slider>();				
				
				CharacterAvatar playerAvatar = player.playerObject.GetComponent<CharacterAvatar>();			
				playerAvatar.SpawnCharacter( player.characterID );
				playerAvatar.InitControls( i );			
			}
		}
		else if ( Application.loadedLevelName == "CharacterSelect" )
		{
			// TODO
		}
		else if ( Application.loadedLevelName == "MainMenu" )
		{
			// TODO
		}
	}

}
