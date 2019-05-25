using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

	public PlayerController playerHealth;
	public GameObject LightRifleman;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;
	
	
	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	
	void Spawn () {
		if (playerHealth.isDead == true)
		{
			return;
		}
		
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		
		Instantiate (LightRifleman, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		
	}
}
