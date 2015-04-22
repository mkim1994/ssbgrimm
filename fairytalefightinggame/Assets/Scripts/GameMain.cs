using UnityEngine;
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
	private int randomBG = 0;
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

		// TODO- Actual char select
		players[0].characterID = 0;
		players[1].characterID = 1;
	}

	// use this from buttons in the character select scene
	public void SetCharacterForPlayer( int playerID, int characterID )
	{
		players[playerID].characterID = characterID;
	}

	public void BeginFight()
	{
		// todo - we should play a countdown here
		Application.LoadLevel( "FightStage" );
	}

	void OnLevelWasLoaded( int level )
	{
		// the fight stage - we just called BeginFight()
		if ( Application.loadedLevelName == "FightStage" )
		{
			// display the previously selected background
			RawImage bg = GameObject.FindWithTag("Background").GetComponent<RawImage>();
			bg.texture = backgrounds[randomBG];

			Debug.Log("SPAWNING");

			foreach (GameObject chr in GameObject.FindGameObjectsWithTag("Player")){
				Destroy(chr); //dont duplicate characters
			}

			// spawn the players; get ready to fight!
			for ( int i = 0; i < players.Length; i++ )
			{
				PlayerInfo player = players[i];

				player.playerObject = (GameObject)Instantiate( playerPrefab, new Vector3( spawnPoints[i].x , spawnPoints[i].y, 0f ), Quaternion.identity);

				PlayerHP playerHP = player.playerObject.GetComponent<PlayerHP>();
				// TODO - make hpbars not suck
				playerHP.hpbar = ((i == 0) ? GameObject.FindWithTag("HP1") : GameObject.FindWithTag("HP2")).GetComponent<Slider>();		
				
				CharacterAvatar playerAvatar = player.playerObject.GetComponent<CharacterAvatar>();	
				playerAvatar.SpawnCharacter( player.characterID );
				playerAvatar.InitControls( i );

				FightControl fc = player.playerObject.GetComponent<CharacterAvatar>().myCharacter.GetComponent<FightControl>();
				fc.apple = GameObject.FindWithTag( i == 0 ? "Ult1" : "Ult2" ).GetComponent<Animator>();
				Animator anim = player.playerObject.GetComponent<CharacterAvatar>().myCharacter.GetComponent<Animator>();
				playerHP.Init( anim, fc );			
			}
		}
		else if ( Application.loadedLevelName == "CharacterSelect" )
		{
			// choose a random background image
			randomBG = (int)(Random.value * backgrounds.Length);
			// display the background in character select
			RawImage bg = GameObject.FindWithTag("Menu").GetComponent<RawImage>();
			bg.texture = backgrounds[randomBG];
		}
		else if ( Application.loadedLevelName == "MainMenu" )
		{
			// TODO
		}
	}
}
