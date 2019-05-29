using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsController : MonoBehaviour {

	public GameStaticController gameController;
	Text resultText;
	Text scoreText;
	public GameObject winPanel;
	public GameObject losePanel;
	
	
	void Awake () {
		
		gameController = GameObject.Find("GameStaticController").GetComponent<GameStaticController>();
		resultText = GameObject.Find("Canvas/Main Screen/ResultText").GetComponent<Text>();
		scoreText = GameObject.Find("Canvas/Main Screen/ScoreText").GetComponent<Text>();
		
		winPanel.SetActive(false);
		losePanel.SetActive(false);
		
		if(gameController.GlobalGameResult == "win")
		{
			winPanel.SetActive(true);
			resultText.text = "After fierce battling, " + gameController.playerName + " has successfully defended Itter Castle from the Waffen SS assault! A Time Portal has appeared close to " + gameController.playerName + " and without a second thought, jumped through and returned to the present.";
		}
		
		if(gameController.GlobalGameResult == "lost - defend")
		{
			losePanel.SetActive(true);
			resultText.text = "The Waffen SS has secured a critical position of the castle and effortlessly wiped out any remaining resistance defending Itter Castle. " + gameController.playerName + " has failed to defend the Castle and in turn, has caused a time paradox and will never return home.";
		}
		
		if(gameController.GlobalGameResult == "lost - snipers")
		{
			losePanel.SetActive(true);
			resultText.text = "The Waffen SS Snipers has managed to kill off key targets which has help the main force to easily take control of Itter Castle and wiped out any remaining resistance defending Itter Castle. " + gameController.playerName + " has failed to defend the Castle and in turn, has caused a time paradox and will never return home.";
		}
		
		if(gameController.GlobalGameResult == "lost - flak")
		{
			losePanel.SetActive(true);
			resultText.text = "The Flak 88 Cannons has managed to blow out key structure points of Itter Castle to allow the main force to easily assault and take control of Itter Castle and wiped out any remaining resistance defending Itter Castle. " + gameController.playerName + " has failed to defend the Castle and in turn, has caused a time paradox and will never return home.";
		}
		
		scoreText.text = "Score: " + gameController.GlobalScore;
	}
	
	
}
