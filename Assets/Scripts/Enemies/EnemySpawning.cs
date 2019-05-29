using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

	public PlayerController playerHealth;
	public GameObject LightRifleman;
	public GameObject Sniper;
	public float spawnTime = 3f;
	public Transform[] spawnPointsA;
	public Transform[] spawnPointsB;
	public Transform[] spawnPointsC;
	public Transform[] sniperSpawnPointsA;
	public Transform[] sniperSpawnPointsB;
	public Transform[] sniperSpawnPointsC;
	
	public GameObject[] snipersSpawned;
	public GameObject[] enemiesSpawned;
	public int enemyCap;
	public int sniperCap;
	public int sniperDeaths;
	public bool snipersActive;
	public bool snipersCompleted;
	public bool ifSnipersA;
	public bool ifSnipersB;
	public bool ifSnipersC;
	public bool ifSpawningA;
	public bool ifSpawningB;
	public bool ifSpawningC;
	
	public GameStaticController gameController;
	
	
	void Start () {
		
		gameController = GameObject.Find("GameStaticController").GetComponent<GameStaticController>();
		
		snipersCompleted = false;
		snipersActive = false;
		sniperDeaths = 0;
		enemyCap = 10 + gameController.extraEnemies;
		sniperCap = 1;
		ifSnipersA = false;
		ifSnipersB = false;
		ifSnipersC = false;
		ifSpawningA = false;
		ifSpawningB = false;
		ifSpawningC = false;
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	void Update() 
	{
		enemiesSpawned = GameObject.FindGameObjectsWithTag("Enemy");
		snipersSpawned = GameObject.FindGameObjectsWithTag("Sniper");
		
		if (sniperDeaths >= 6 && snipersActive == true)
		{
			snipersActive = false;
			ifSnipersA = false;
			ifSnipersB = false;
			ifSnipersC = false;
			snipersCompleted = true;
		}
		
	}
	
	void Spawn () {
		if (playerHealth.isDead == true || enemiesSpawned.Length >= enemyCap)
		{
			return;
		}
		
		if (ifSpawningA == true)
		{
			int spawnPointIndex = Random.Range (0, spawnPointsA.Length);
			Instantiate (LightRifleman, spawnPointsA[spawnPointIndex].position, spawnPointsA[spawnPointIndex].rotation);
		}
		if (ifSpawningB == true)
		{
			int spawnPointIndex = Random.Range (0, spawnPointsB.Length);
			Instantiate (LightRifleman, spawnPointsB[spawnPointIndex].position, spawnPointsB[spawnPointIndex].rotation);
		}
		if (ifSpawningC == true)
		{
			int spawnPointIndex = Random.Range (0, spawnPointsC.Length);
			Instantiate (LightRifleman, spawnPointsC[spawnPointIndex].position, spawnPointsC[spawnPointIndex].rotation);
		}
		
		if (snipersActive == true && snipersSpawned.Length < sniperCap)
		{
			if (ifSnipersA == true)
			{
				int sniperSpawnPointIndex = Random.Range (0, sniperSpawnPointsA.Length);
				Instantiate (Sniper, sniperSpawnPointsA[sniperSpawnPointIndex].position, sniperSpawnPointsA[sniperSpawnPointIndex].rotation);
			}
			if (ifSnipersB == true)
			{
				int sniperSpawnPointIndex = Random.Range (0, sniperSpawnPointsB.Length);
				Instantiate (Sniper, sniperSpawnPointsB[sniperSpawnPointIndex].position, sniperSpawnPointsB[sniperSpawnPointIndex].rotation);
			}
			if (ifSnipersC == true)
			{
				int sniperSpawnPointIndex = Random.Range (0, sniperSpawnPointsC.Length);
				Instantiate (Sniper, sniperSpawnPointsC[sniperSpawnPointIndex].position, sniperSpawnPointsC[sniperSpawnPointIndex].rotation);
			}
		}
		
		Debug.Log("Enemies: " + enemiesSpawned.Length);
		Debug.Log("Snipers: " + snipersSpawned.Length);
	}
	
	
	
	public void OnSniperDeath()
	{
		sniperDeaths++;
	}
	
	public void SniperA()
	{
		ifSnipersA = true;
		snipersActive = true;
	}
	public void SniperB()
	{
		ifSnipersB = true;
		snipersActive = true;
	}
	public void SniperC()
	{
		ifSnipersC = true;
		snipersActive = true;
	}
}
