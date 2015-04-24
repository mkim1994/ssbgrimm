using UnityEngine;
using System.Collections;

public class MenuButtons : MonoBehaviour {

	public void LoadCharacterSelect()
	{
		Application.LoadLevel("CharacterSelect");
	}

	public void ShowControls()
	{
		// todo - figure out world camera, pan to page with controls
		// easy out - load another level for controls
		Application.LoadLevel ("ControlScene");
	
	}

	public void ShowCredits()
	{
		// todo - figure out world camera, pan to page with credits
		// easy out - load another level for controls
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
