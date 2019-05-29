using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStaticController : MonoBehaviour {

	public int GlobalScore;
	public string GlobalGameResult;
	public string playerName;
	
	public string selectedDiff;
	public int enemyModifier;
	public int scoreMultiplier;
	public int extraEnemies;
	
	
	void Awake () {
		DontDestroyOnLoad(gameObject);
		
		GlobalScore = 0;
		GlobalGameResult = "";
		playerName = "";
		
		selectedDiff = "normal";
		enemyModifier = 0;
		scoreMultiplier = 1;
		extraEnemies = 0;
	}
	
}
