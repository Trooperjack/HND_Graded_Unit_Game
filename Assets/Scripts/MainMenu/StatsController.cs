using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour {

	public GameStaticController gameController;

	Text gamesWonText;
	Text gamesLostText;
	Text totalKillsText;
	
	
	
	void Start () {
		
		gameController = GameObject.Find("GameStaticController").GetComponent<GameStaticController>();
		
		gamesWonText = GameObject.Find("Canvas/Main Screen/GamesWonText").GetComponent<Text>();
		gamesLostText = GameObject.Find("Canvas/Main Screen/GamesLostText").GetComponent<Text>();
		totalKillsText = GameObject.Find("Canvas/Main Screen/TotalKillsText").GetComponent<Text>();
		
		gamesWonText.text = "Games Won: " + gameController.gamesWon;
		gamesLostText.text = "Games Lost: " + gameController.gamesLost;
		totalKillsText.text = "Total Kills: " + gameController.totalKills;
		
	}
	
}
