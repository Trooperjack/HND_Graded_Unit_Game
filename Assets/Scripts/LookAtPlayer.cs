using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

	public GameObject waypointObject;
	public GameObject player;
	
	void Start () {
		
		player = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	
	void Update () {
		
		transform.LookAt(player.transform);
		
	}
}
