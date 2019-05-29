using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalButton : MonoBehaviour {

	public GameStaticController gameController;
	public Button buttonObject;
	
	void Start () {
		gameController = GameObject.Find("GameStaticController").GetComponent<GameStaticController>();
		buttonObject = GameObject.Find("NormalButton").GetComponent<Button>();
	}
	
	void Update () {
		
		if (gameController.selectedDiff == "normal")
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
		gameController.selectedDiff = "normal";
		gameController.enemyModifier = 0;
		gameController.scoreMultiplier = 1;
		gameController.extraEnemies = 0;
	}
	
}
