using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

	public PlayerController playerHealth;
	public GameObject LightRifleman;
	public GameObject Sniper;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;
	public Transform[] sniperSpawnPoints;
	
	public GameObject[] snipersSpawned;
	public GameObject[] enemiesSpawned;
	public int enemyCap;
	public int sniperCap;
	public int sniperDeaths;
	public bool snipersActive;
	
	
	void Start () {
		snipersActive = false;
		sniperDeaths = 0;
		enemyCap = 10;
		sniperCap = 1;
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	void Update() 
	{
		enemiesSpawned = GameObject.FindGameObjectsWithTag("Enemy");
		snipersSpawned = GameObject.FindGameObjectsWithTag("Sniper");
		
		if (sniperDeaths >= 6)
		{
			snipersActive = false;
		}
	}
	
	void Spawn () {
		if (playerHealth.isDead == true || enemiesSpawned.Length >= enemyCap)
		{
			return;
		}
		
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		
		Instantiate (LightRifleman, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		
		if (snipersActive == true && snipersSpawned.Length < sniperCap)
		{
			int sniperSpawnPointIndex = Random.Range (0, sniperSpawnPoints.Length);
			Instantiate (Sniper, sniperSpawnPoints[sniperSpawnPointIndex].position, sniperSpawnPoints[sniperSpawnPointIndex].rotation);
		}
		
		Debug.Log("Enemies: " + enemiesSpawned.Length);
		Debug.Log("Snipers: " + snipersSpawned.Length);
	}
	
	public void ActivateSnipers()
	{
		snipersActive = true;
	}
	
	public void DeactivateSnipers()
	{
		snipersActive = false;
	}
	
	public void OnSniperDeath()
	{
		sniperDeaths++;
	}
}
