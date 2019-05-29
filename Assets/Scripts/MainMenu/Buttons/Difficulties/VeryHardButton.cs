using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VeryHardButton : MonoBehaviour {

	public GameStaticController gameController;
	public Button buttonObject;
	
	void Start () {
		gameController = GameObject.Find("GameStaticController").GetComponent<GameStaticController>();
		buttonObject = GameObject.Find("VeryHardButton").GetComponent<Button>();
	}
	
	void Update () {
		
		if (gameController.selectedDiff == "veryHard")
		{
			buttonObject.interactable = false;
		}
		else
		{
			buttonObject.interactable = true;
		}
		
	}
	
	public void Pressed()
	{
		gameController.selectedDiff = "veryHard";
		gameController.enemyModifier = 25;
		gameController.scoreMultiplier = 5;
		gameController.extraEnemies = 4;
	}
	
}
