using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject playerObject;
	PlayerController player;
	
	public GameObject[] DefendArea;
	
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
	
	
	void Awake () {
		
		playerObject = GameObject.FindGameObjectWithTag("Player");
		player = playerObject.GetComponent<PlayerController>();
		
		Destroy(AreaDoors[0]);
		
		DefendAreaLocated = false; //1
		KillSnipersLocated = false; //2
		FlakCannonsLocated = false; //3
		Area1Done = false;
		Area2Done = false;
		Area3Done = false;
		
		area1 = Random.Range(1,4);
		area2 = Random.Range(1,4);
		area3 = Random.Range(1,4);
		
		while(Area1Done == false)
		{
			if (area1 == 1  && DefendAreaLocated == false)
			{
				Destroy(DefendArea[1]);
				Destroy(DefendArea[2]);
				Area1Done = true;
				DefendAreaLocated = true;
			}
			else
			{
				area1 = Random.Range(1,4);
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	public void onArea1Complete()
	{
		Destroy(AreaDoors[1]);
		
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
