using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject playerObject;
	PlayerController player;
	
	public GameObject enemyManager;
	EnemySpawning enemySpawningScript;
	
	public GameObject[] DefendArea;
	
	public GameObject SniperGroupA;
	public GameObject SniperGroupB;
	public GameObject SniperGroupC;
	public GameObject[] SniperSpawns;
	
	public bool defendAreaObjectiveActive;
	
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
	
	private float startDelay = 1f;
	
	
	void Awake()
	{
		SniperGroupA = GameObject.FindGameObjectWithTag("SniperSpawnsA");
		SniperGroupB = GameObject.FindGameObjectWithTag("SniperSpawnsB");
		SniperGroupC = GameObject.FindGameObjectWithTag("SniperSpawnsC");
		
		playerObject = GameObject.FindGameObjectWithTag("Player");
		player = playerObject.GetComponent<PlayerController>();
		
		enemyManager = GameObject.FindGameObjectWithTag("Enemy Manager");
		enemySpawningScript = enemyManager.GetComponent<EnemySpawning>();
		
		defendAreaObjectiveActive = false;
		
		DefendAreaLocated = false; //1
		KillSnipersLocated = false; //2
		FlakCannonsLocated = false; //3
		Area1Done = false;
		Area2Done = false;
		Area3Done = false;
		
		area1 = Random.Range(1,4);
		area2 = Random.Range(1,4);
		area3 = Random.Range(1,4);
	}
	
	
	void DelayedStart () {
		
		Destroy(AreaDoors[0]);
		
		while(Area1Done == false)
		{
			if (area1 == 1  && DefendAreaLocated == false)
			{
				Destroy(DefendArea[1]);
				Destroy(DefendArea[2]);
				Destroy(SniperGroupA);
				Area1Done = true;
				DefendAreaLocated = true;
				defendAreaObjectiveActive = true;
			}
			else if (area1 == 2 && KillSnipersLocated == false)
			{
				Destroy(DefendArea[0]);
				Destroy(SniperGroupB);
				Destroy(SniperGroupC);
				Area1Done = true;
				KillSnipersLocated = true;
				enemySpawningScript.ActivateSnipers();
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
		
	}
	
	
	
	
	public void onArea1Complete()
	{
		Destroy(AreaDoors[1]);
		enemySpawningScript.DeactivateSnipers();
		defendAreaObjectiveActive = false;
		
		while(Area2Done == false)
		{
			if (area2 == 1 && DefendAreaLocated == false)
			{
				Destroy(DefendArea[0]);
				Destroy(DefendArea[2]);
				Area2Done = true;
			}
			else
			{
				//area2 = Random.Range(1,4);
				Area2Done = true;
			}
		}
		
	}
	
}
