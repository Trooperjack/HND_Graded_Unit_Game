using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject playerObject;
	PlayerController player;
	
	
	public GameObject[] AreaDoors;

	
	
	
	
	void Awake () {
		
		playerObject = GameObject.FindGameObjectWithTag("Player");
		player = playerObject.GetComponent<PlayerController>();
		
		Destroy(AreaDoors[0]);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
