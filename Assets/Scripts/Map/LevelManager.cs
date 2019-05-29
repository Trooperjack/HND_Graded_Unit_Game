using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public GameObject playerObject;
	PlayerController player;
	
	public GameObject enemyManager;
	EnemySpawning enemySpawningScript;
	
	public GameObject objectiveAreaObject;
	ObjectiveAreaTrigger objArea;
	
	public GameObject FlakCannon;
	
	public GameObject[] DefendArea;
	
	public GameObject SniperGroupA;
	public GameObject SniperGroupB;
	public GameObject SniperGroupC;
	public GameObject[] SniperSpawns;
	
	public GameObject FlakGroupA;
	public GameObject FlakGroupB;
	public GameObject FlakGroupC;
	public Transform[] FlakSpawnPointsA;
	public Transform[] FlakSpawnPointsB;
	public Transform[] FlakSpawnPointsC;
	
	public bool defendAreaObjectiveActive;
	public bool KillSnipersObjectiveActive;
	public bool FlakCannonsObjectiveActive;
	public bool doseAreaDefendExist;
	
	public GameObject[] AreaDoors;

	public bool DefendAreaLocated;
	public bool KillSnipersLocated;
	public bool FlakCannonsLocated;
	public bool Area1Done;
	public bool Area2Done;
	public bool Area3Done;
	
	public int area1;
	public int area2;
	public int area3;
	
	public int ObjectivesCompleted;
	public bool EnemySnipersDone;
	public int FlakCannonsAmount;
	public int FlakCannonsKilled;
	public bool FlakCannonsDone;
	
	private float startDelay = 1f;
	public int objectiveReward = 500;
	
	Text objectiveText;
	Text objAmountText;
	Text scoreText;
	public GameStaticController gameController;
	
	
	void Awake()
	{
		SniperGroupA = GameObject.FindGameObjectWithTag("SniperSpawnsA");
		SniperGroupB = GameObject.FindGameObjectWithTag("SniperSpawnsB");
		SniperGroupC = GameObject.FindGameObjectWithTag("SniperSpawnsC");
		
		FlakGroupA = GameObject.FindGameObjectWithTag("FlakSpawnsA");
		FlakGroupB = GameObject.FindGameObjectWithTag("FlakSpawnsB");
		FlakGroupC = GameObject.FindGameObjectWithTag("FlakSpawnsC");
		
		playerObject = GameObject.FindGameObjectWithTag("Player");
		player = playerObject.GetComponent<PlayerController>();
		
		enemyManager = GameObject.FindGameObjectWithTag("Enemy Manager");
		enemySpawningScript = enemyManager.GetComponent<EnemySpawning>();
		
		objectiveText = GameObject.Find("Canvas/ObjectiveText").GetComponent<Text>();
		objAmountText = GameObject.Find("Canvas/objAmountText").GetComponent<Text>();
		scoreText = GameObject.Find("Canvas/scoreText").GetComponent<Text>();
		gameController = GameObject.Find("GameStaticController").GetComponent<GameStaticController>();
		
		defendAreaObjectiveActive = false;
		KillSnipersObjectiveActive = false;
		FlakCannonsObjectiveActive = false;
		doseAreaDefendExist = false;
		
		DefendAreaLocated = false; //1
		KillSnipersLocated = false; //2
		FlakCannonsLocated = false; //3
		Area1Done = false;
		Area2Done = false;
		Area3Done = false;
		
		//objArea.isActive = false;
		ObjectivesCompleted = 0;
		EnemySnipersDone = false;
		FlakCannonsAmount = 1;
		FlakCannonsKilled = 0;
		FlakCannonsDone = false;
		
		objectiveReward = objectiveReward * gameController.scoreMultiplier;
		gameController.GlobalScore = 0;
		
		area1 = Random.Range(1,4);
		area2 = Random.Range(1,4);
		area3 = Random.Range(1,4);
		
		objectiveText.text = "";
		objAmountText.text = "";
		
		gameController.gameMusic.Stop();
		gameController.gameMusic.loop = true;
		gameController.gameMusic.clip = gameController.gameFile;
		gameController.gameMusic.volume = 0.8f;
		gameController.gameMusic.Play();
	}
	
	
	void DelayedStart () {
		
		Destroy(AreaDoors[0]);
		enemySpawningScript.ifSpawningA = true;
		
		while(Area1Done == false)
		{
			if (area1 == 1  && DefendAreaLocated == false)
			{
				Destroy(DefendArea[1]);
				Destroy(DefendArea[2]);
				Destroy(SniperGroupA);
				Destroy(FlakGroupA);
				Area1Done = true;
				DefendAreaLocated = true;
				defendAreaObjectiveActive = true;
				doseAreaDefendExist = GameObject.FindGameObjectWithTag("DefendArea1");
				if (doseAreaDefendExist == true)
				{
					objectiveAreaObject = GameObject.FindGameObjectWithTag("DefendArea1");
					objArea = objectiveAreaObject.GetComponent<ObjectiveAreaTrigger>();
					objArea.isActive = true;
				}
				objectiveText.text = "Defend the area!";
			}
			else if (area1 == 2 && KillSnipersLocated == false)
			{
				Destroy(DefendArea[0]);
				Destroy(SniperGroupB);
				Destroy(SniperGroupC);
				Destroy(FlakGroupA);
				Area1Done = true;
				KillSnipersLocated = true;
				KillSnipersObjectiveActive = true;
				enemySpawningScript.SniperA();
				objectiveText.text = "Find & Kill the Snipers!";
			}
			else if (area1 == 3 && FlakCannonsLocated == false)
			{
				Destroy(DefendArea[0]);
				Destroy(SniperGroupA);
				Area1Done = true;
				FlakCannonsLocated = true;
				FlakCannonsObjectiveActive = true;
				SpawnFlakCannonsA();
				objectiveText.text = "Find & Destroy the Flak Cannons!";
			}
			else
			{
				area1 = Random.Range(1,4);
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		startDelay -= Time.deltaTime;
		if (startDelay < 0)
		{
			DelayedStart();
		}
		
		if (enemySpawningScript.snipersCompleted == true && EnemySnipersDone == false)
		{
			EnemySnipersDone = true;
			onAreaCompleteX();
		}
		
		if (FlakCannonsKilled == FlakCannonsAmount && FlakCannonsDone == false)
		{
			FlakCannonsDone = true;
			onAreaCompleteX();
		}
		
		if (defendAreaObjectiveActive == true)
		{
			if (objArea.isActive == true)
			{
				objAmountText.text = "Defend for: " + objArea.areaTimer + " Seconds!";
			}
		}
		
		if (KillSnipersObjectiveActive == true)
		{
			objAmountText.text = "" + enemySpawningScript.sniperDeaths + " / 6";
		}
		
		if (FlakCannonsObjectiveActive == true)
		{
			objAmountText.text = "" + FlakCannonsKilled + " / " + FlakCannonsAmount;
		}
		
		if (Input.GetKeyDown(KeyCode.P))
		{
			SceneManager.LoadScene("MainMenu");
		}
		
		scoreText.text = "Score: " + gameController.GlobalScore;
		
	}
	
	
	
	public void onAreaCompleteX()
	{
		ObjectivesCompleted++;
		if (ObjectivesCompleted == 1)
		{
			gameController.GlobalScore = gameController.GlobalScore + objectiveReward;
			onArea1Complete();
		}
		if (ObjectivesCompleted == 2)
		{
			gameController.GlobalScore = gameController.GlobalScore + objectiveReward;
			onArea2Complete();
		}
		if (ObjectivesCompleted == 3)
		{
			gameController.GlobalScore = gameController.GlobalScore + objectiveReward;
			WinGame();
		}
	}
	
	
	
	public void onArea1Complete()
	{
		Destroy(AreaDoors[1]);
		//objArea.isActive = false;
		enemySpawningScript.snipersActive = false;
		defendAreaObjectiveActive = false;
		KillSnipersObjectiveActive = false;
		FlakCannonsObjectiveActive = false;
		enemySpawningScript.ifSpawningA = false;
		enemySpawningScript.ifSpawningB = true;
		StartCoroutine(Area2Delay());
	}
	
	
	public void onArea2Complete()
	{
		Destroy(AreaDoors[2]);
		//objArea.isActive = false;
		enemySpawningScript.snipersActive = false;
		defendAreaObjectiveActive = false;
		KillSnipersObjectiveActive = false;
		FlakCannonsObjectiveActive = false;
		enemySpawningScript.ifSpawningB = false;
		enemySpawningScript.ifSpawningC = true;
		StartCoroutine(Area3Delay());
	}
	
	
	
	
	private IEnumerator Area2Delay()
	{
		yield return new WaitForSeconds(1);
		BuildArea2();
	}
	private IEnumerator Area3Delay()
	{
		yield return new WaitForSeconds(1);
		BuildArea3();
	}
	
	public void BuildArea2()
	{
		while(Area2Done == false)
		{
			if (area2 == 1 && DefendAreaLocated == false)
			{
				Destroy(DefendArea[0]);
				Destroy(DefendArea[2]);
				Destroy(SniperGroupB);
				Destroy(FlakGroupB);
				Area2Done = true;
				DefendAreaLocated = true;
				defendAreaObjectiveActive = true;
				doseAreaDefendExist = GameObject.FindGameObjectWithTag("DefendArea2");
				if (doseAreaDefendExist == true)
				{
					objectiveAreaObject = GameObject.FindGameObjectWithTag("DefendArea2");
					objArea = objectiveAreaObject.GetComponent<ObjectiveAreaTrigger>();
					objArea.isActive = true;
				}
				objectiveText.text = "Defend the area!";
			}
			else if (area2 == 2 && KillSnipersLocated == false)
			{
				Destroy(DefendArea[1]);
				Destroy(SniperGroupA);
				Destroy(SniperGroupC);
				Destroy(FlakGroupB);
				Area2Done = true;
				KillSnipersLocated = true;
				KillSnipersObjectiveActive = true;
				enemySpawningScript.SniperB();
				objectiveText.text = "Find & Kill the Snipers!";
			}
			else if (area2 == 3 && FlakCannonsLocated == false)
			{
				Destroy(DefendArea[1]);
				Destroy(SniperGroupB);
				Area2Done = true;
				FlakCannonsLocated = true;
				FlakCannonsObjectiveActive = true;
				SpawnFlakCannonsB();
				objectiveText.text = "Find & Destroy the Flak Cannons!";
			}
			else
			{
				area2 = Random.Range(1,4);
				//Area2Done = true;
			}
		}
	}
	
	public void BuildArea3()
	{
		while(Area3Done == false)
		{
			if (area3 == 1 && DefendAreaLocated == false)
			{
				Destroy(DefendArea[0]);
				Destroy(DefendArea[1]);
				Destroy(SniperGroupC);
				Destroy(FlakGroupC);
				Area3Done = true;
				DefendAreaLocated = true;
				defendAreaObjectiveActive = true;
				doseAreaDefendExist = GameObject.FindGameObjectWithTag("DefendArea3");
				if (doseAreaDefendExist == true)
				{
					objectiveAreaObject = GameObject.FindGameObjectWithTag("DefendArea3");
					objArea = objectiveAreaObject.GetComponent<ObjectiveAreaTrigger>();
					objArea.isActive = true;
				}
				objectiveText.text = "Defend the area!";
			}
			else if (area3 == 2 && KillSnipersLocated == false)
			{
				Destroy(DefendArea[2]);
				Destroy(SniperGroupA);
				Destroy(SniperGroupB);
				Destroy(FlakGroupC);
				Area3Done = true;
				KillSnipersLocated = true;
				KillSnipersObjectiveActive = true;
				enemySpawningScript.SniperC();
				objectiveText.text = "Find & Kill the Snipers!";
			}
			else if (area3 == 3 && FlakCannonsLocated == false)
			{
				Destroy(DefendArea[2]);
				Destroy(SniperGroupC);
				Area3Done = true;
				FlakCannonsLocated = true;
				FlakCannonsObjectiveActive = true;
				SpawnFlakCannonsC();
				objectiveText.text = "Find & Destroy the Flak Cannons!";
			}
			else
			{
				area3 = Random.Range(1,4);
				//Area3Done = true;
			}
		}
	}
	
	
	
	
	
	public void SpawnFlakCannonsA()
	{
		int cannonsSpawned = 0;
		while(cannonsSpawned < FlakCannonsAmount)
		{
			int spawnPointIndex = Random.Range (0, FlakSpawnPointsA.Length);
			Instantiate (FlakCannon, FlakSpawnPointsA[spawnPointIndex].position, FlakSpawnPointsA[spawnPointIndex].rotation);
			//FlakSpawnPointsA[spawnPointIndex].SetActive(false);
			cannonsSpawned++;
		}
	}
	public void SpawnFlakCannonsB()
	{
		int cannonsSpawned = 0;
		while(cannonsSpawned < FlakCannonsAmount)
		{
			int spawnPointIndex = Random.Range (0, FlakSpawnPointsB.Length);
			Instantiate (FlakCannon, FlakSpawnPointsB[spawnPointIndex].position, FlakSpawnPointsB[spawnPointIndex].rotation);
			//FlakSpawnPointsA[spawnPointIndex].SetActive(false);
			cannonsSpawned++;
		}
	}
	public void SpawnFlakCannonsC()
	{
		int cannonsSpawned = 0;
		while(cannonsSpawned < FlakCannonsAmount)
		{
			int spawnPointIndex = Random.Range (0, FlakSpawnPointsC.Length);
			Instantiate (FlakCannon, FlakSpawnPointsC[spawnPointIndex].position, FlakSpawnPointsC[spawnPointIndex].rotation);
			//FlakSpawnPointsA[spawnPointIndex].SetActive(false);
			cannonsSpawned++;
		}
	}
	
	
	
	
	
	public void WinGame()
	{
		enemySpawningScript.snipersActive = false;
		defendAreaObjectiveActive = false;
		KillSnipersObjectiveActive = false;
		FlakCannonsObjectiveActive = false;
		objectiveText.text = "YOU WON!";
		objAmountText.text = "";
		gameController.GlobalGameResult = "win";
		Debug.Log("PLAYER WINS GAME");
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
		gameController.gameMusic.Stop();
		gameController.gameMusic.loop = true;
		gameController.gameMusic.clip = gameController.menuFile;
		gameController.gameMusic.volume = 0.8f;
		gameController.gameMusic.Play();
		gameController.gamesWon++;
		PlayerPrefs.SetInt("gamesWon", gameController.gamesWon);
		if (gameController.GlobalScore > gameController.highScore);
		{
			PlayerPrefs.SetInt("highScore", gameController.GlobalScore);
		}
		SceneManager.LoadScene("Results");
	}
	
	public void LoseGame(int loseCondition)
	{
		enemySpawningScript.snipersActive = false;
		defendAreaObjectiveActive = false;
		KillSnipersObjectiveActive = false;
		FlakCannonsObjectiveActive = false;
		if (loseCondition == 1)
		{
			gameController.GlobalGameResult = "lost - defend";
			Debug.Log("PLAYER LOST: FAILED TO DEFEND AREA");
		}
		if (loseCondition == 2)
		{
			gameController.GlobalGameResult = "lost - snipers";
			Debug.Log("PLAYER LOST: FAILED TO KILL SNIPERS");
		}
		if (loseCondition == 3)
		{
			gameController.GlobalGameResult = "lost - flak";
			Debug.Log("PLAYER LOST: FAILED TO DESTROY FLAK CANNONS");
		}
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
		gameController.gameMusic.Stop();
		gameController.gameMusic.loop = true;
		gameController.gameMusic.clip = gameController.menuFile;
		gameController.gameMusic.volume = 0.8f;
		gameController.gameMusic.Play();
		gameController.gamesLost++;
		PlayerPrefs.SetInt("gamesLost", gameController.gamesLost);
		if (gameController.GlobalScore > gameController.highScore);
		{
			PlayerPrefs.SetInt("highScore", gameController.GlobalScore);
		}
		SceneManager.LoadScene("Results");
	}
	
}
