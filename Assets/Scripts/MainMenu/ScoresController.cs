using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoresController : MonoBehaviour {

	public GameStaticController gameController;

	Text highScoreText;
	
	
	
	void Start () {
		
		gameController = GameObject.Find("GameStaticController").GetComponent<GameStaticController>();
		
		highScoreText = GameObject.Find("Canvas/Main Screen/HighScoreText").GetComponent<Text>();
		
		highScoreText.text = "" + gameController.highScore;
		
	}
	
}
