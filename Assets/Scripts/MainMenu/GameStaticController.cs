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
	
	public int highScore;
	public int gamesWon;
	public int gamesLost;
	public int totalKills;
	
	
	public AudioSource gameMusic;
	public AudioClip menuFile;
	public AudioClip gameFile;
	
	
	void Awake () {
		DontDestroyOnLoad(gameObject);
				
		GlobalScore = 0;
		GlobalGameResult = "";
		playerName = "";
		
		selectedDiff = "normal";
		enemyModifier = 0;
		scoreMultiplier = 1;
		extraEnemies = 0;
		
		gameMusic.loop = true;
		gameMusic.clip = menuFile;
		gameMusic.volume = 0.8f;
		gameMusic.Play();
		
		highScore = 0;
		gamesWon = 0;
		gamesLost = 0;
		totalKills = 0;
		
		if (PlayerPrefs.HasKey("highScore"))
		{
			highScore = PlayerPrefs.GetInt("highScore");
			gamesWon = PlayerPrefs.GetInt("gamesWon");
			gamesLost = PlayerPrefs.GetInt("gamesLost");
			totalKills = PlayerPrefs.GetInt("totalKills");
		}
		else
		{
			Debug.Log("No save located");
		}
		
	}
	
}
