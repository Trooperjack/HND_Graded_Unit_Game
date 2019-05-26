using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

	public PlayerController playerHealth;
	public GameObject LightRifleman;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;
	
	public GameObject[] enemiesSpawned;
	public int enemyCap;
	
	
	void Start () {
		enemyCap = 10;
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	void Update() 
	{
		enemiesSpawned = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	void Spawn () {
		if (playerHealth.isDead == true || enemiesSpawned.Length >= enemyCap)
		{
			return;
		}
		
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		
		Instantiate (LightRifleman, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		
		Debug.Log("Enemies: " + enemiesSpawned.Length);
	}
}
