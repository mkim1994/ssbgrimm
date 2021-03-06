﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMain : MonoBehaviour {

	private class PlayerInfo {

		public int characterID = 0;
		public GameObject playerObject = null;

	}

	private PlayerInfo[] players = null;
	public int numplayers = 2;
	public Texture2D[] backgrounds;
	private int randomBG = 0;
	public Vector2[] spawnPoints;
	public GameObject playerPrefab;

	public AudioClip fight;

    //public bool musicPlaying;
    public bool shouldDestroy = false;

    public Sprite[] countdownSprites;
    public Sprite koSprite;
    public Sprite[] victorySprites;
    public Image overlay;
    private Image fullOverlay;
    private GoBack goback = null;

   /* private void StartMusic()
    {
        if ( !musicPlaying )
        {
				
            GetComponent<AudioSource>().Play();
            musicPlaying = true;
        }
    }*/

	void Start()
	{
		// this should live until we quit the game, or go back to main menu
		DontDestroyOnLoad(transform.gameObject);
        //StartMusic();
        fullOverlay = GetComponent<Image>();
        fullOverlay.enabled = false;
        overlay.enabled = false;
        goback = GetComponent<GoBack>();

	}

	// use this from buttons in the character select scene
	public void SetCharacterForPlayer( int playerID, int characterID )
	{
		players[playerID].characterID = characterID;
	}

	private IEnumerator Countdown()
	{
		overlay.enabled = true;
		Debug.Log("3");
		overlay.sprite = countdownSprites[3];
		yield return new WaitForSeconds(1);
		Debug.Log("2");
		overlay.sprite = countdownSprites[2];
		yield return new WaitForSeconds(1);
		Debug.Log("1");
		overlay.sprite = countdownSprites[1];
		yield return new WaitForSeconds(1);
		Debug.Log("FIGHT!");
		gameObject.GetComponent<AudioSource> ().PlayOneShot (fight);
		overlay.sprite = countdownSprites[0];
		yield return new WaitForSeconds(1);
		overlay.enabled = false;

		MoveControl[] mControls = FindObjectsOfType<MoveControl>();
		for (int i = 0; i < mControls.Length; i++){
			mControls[i].enabled = true;
		}
		FightControl[] fControls = FindObjectsOfType<FightControl>();
		for (int i = 0; i < fControls.Length; i++) {
			fControls [i].enabled = true;
		}
	}

	public void DisplayKO( int deadID )
	{
		overlay.sprite = koSprite;
		overlay.enabled = true;

		int killerID = deadID == 0 ? 1 : 0;

		GameObject killer = players[killerID].playerObject.GetComponent<CharacterAvatar>().myCharacter;
		killer.GetComponent<Animator>().SetTrigger("Winner");
		FightControl disabled = killer.GetComponent<FightControl>();
		disabled.enabled = false;
		StartCoroutine(ClearSprite(disabled));
	}

	private IEnumerator ClearSprite( FightControl toEnable )
	{
		yield return new WaitForSeconds(3); // MUST MATCH RESPAWN DELAY
		overlay.enabled = false;
		toEnable.gameObject.GetComponent<Animator>().SetTrigger("Respawn");
		toEnable.enabled = true;
	}

	public void BeginFight()
	{
		overlay.enabled = false;
		overlay.sprite = null;
		Application.LoadLevel( "FightStage" );
	}

	void Dissable(){
		MoveControl[] mControls = FindObjectsOfType<MoveControl>();
		for (int i = 0; i < mControls.Length; i++){
			mControls[i].enabled = false; //dissable all movecontrols
		}
		FightControl[] fControls = FindObjectsOfType<FightControl>();
		for (int i = 0; i < fControls.Length; i++){
			fControls[i].enabled = false; //dissable all fight controls
		}
	}

	void OnLevelWasLoaded( int level )
	{
		// the fight stage - we just called BeginFight()
		if ( Application.loadedLevelName == "FightStage" )
		{

			StartCoroutine("Countdown");

			goback.enabled = false;

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
				GameObject hud = ((i == 0) ? GameObject.FindWithTag("HP1") : GameObject.FindWithTag("HP2"));
				playerHP.hpbar = hud.GetComponent<Slider>();
				playerHP.gems = new RawImage[2];

				foreach ( Transform child in hud.transform.parent )
				{
					if ( child.gameObject.tag == "Gem1" )
					{
						playerHP.gems[0] = child.gameObject.GetComponent<RawImage>();
					}
					else if ( child.gameObject.tag == "Gem2" )
					{
						playerHP.gems[1] = child.gameObject.GetComponent<RawImage>();
					}

				}		
				
				CharacterAvatar playerAvatar = player.playerObject.GetComponent<CharacterAvatar>();	
				playerAvatar.SpawnCharacter( player.characterID );
				playerAvatar.InitControls( i );

				GameObject playerCharacter = playerAvatar.myCharacter;
				if ( i > 0 && player.characterID == players[i-1].characterID )
				{
					playerCharacter.GetComponent<SpriteRenderer>().color = new Color( 0.6f, 0.6f, 0.6f, 1.0f );
				}

				FightControl fc = playerCharacter.GetComponent<FightControl>();
				fc.apple = GameObject.FindWithTag( i == 0 ? "Ult1" : "Ult2" );
				fc.InitUltimate();
				Animator anim = player.playerObject.GetComponent<CharacterAvatar>().myCharacter.GetComponent<Animator>();
				playerHP.Init( anim, fc );	
				playerHP.playerID = i;	
			}
			Invoke("Dissable",0.01f);

			shouldDestroy = true;
		}
		else if ( Application.loadedLevelName == "CharacterSelect" )
		{	
			goback.enabled = true;
			overlay.enabled = false;
			fullOverlay.enabled = false;

			// choose a random background image
			randomBG = (int)(Random.value * backgrounds.Length);
			// display the background in character select
			//RawImage bg = GameObject.FindWithTag("Menu").GetComponent<RawImage>();
			//bg.texture = backgrounds[randomBG];

			// set up character select
			if (players == null)
			{
				players = new PlayerInfo[numplayers];
				for ( int i = 0; i < numplayers; i++ )
				{
					players[i] = new PlayerInfo();
				}
			}		
		}
		else if ( Application.loadedLevelName == "MainMenu" )
		{
			if ( goback ) goback.enabled = true;
			if ( shouldDestroy )
				GameObject.Destroy(this.transform.gameObject);
		}
	}

	public void GameOver( int loserID )
	{
		for ( int i = 0; i < numplayers; i++ )
		{
			GameObject avatar = players[i].playerObject.GetComponent<CharacterAvatar>().myCharacter;
			avatar.GetComponent<FightControl>().enabled = false;

			if ( i == loserID )
			{
				// loser is already in the 'dead' animation state, set by playerHP
			}
			else
			{
				// everybody else wins
				avatar.GetComponent<Animator>().SetTrigger("Winner");

				// but only the last winner gets the victory screen :)
				fullOverlay.sprite = victorySprites[i];
			}
		}
		// display winner icon
		fullOverlay.enabled = true;
		goback.enabled = true;

		Invoke("Restart", 6);
	}

	private void Restart()
	{
		Application.LoadLevel("CharacterSelect");
	}
}
