using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardButton : MonoBehaviour {

	public GameStaticController gameController;
	public Button buttonObject;
	
	void Start () {
		gameController = GameObject.Find("GameStaticController").GetComponent<GameStaticController>();
		buttonObject = GameObject.Find("HardButton").GetComponent<Button>();
	}
	
	void Update () {
		
		if (gameController.selectedDiff == "hard")
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
		gameController.selectedDiff = "hard";
		gameController.enemyModifier = 10;
		gameController.scoreMultiplier = 2;
		gameController.extraEnemies = 2;
	}
	
}
