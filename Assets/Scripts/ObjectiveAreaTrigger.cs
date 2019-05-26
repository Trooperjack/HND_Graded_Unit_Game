using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveAreaTrigger : MonoBehaviour {

	public GameObject LevelManagerObject;
	LevelManager levelManager;

	public float healthTime = 60f;
	public float areaTimer = 300f;
	public bool isInside;
	public bool isActive;
	public bool isCaptured;
	public bool isDefended;
	
	
	void Awake () {
		
		LevelManagerObject = GameObject.FindGameObjectWithTag("Level Manager");
		levelManager = LevelManagerObject.GetComponent<LevelManager>();
		
		isInside = false;
		isActive = true;
		isCaptured = false;
		isDefended = false;
		
	}
	
	
	void Update () {
		
		if (isInside == true && isActive == true && isCaptured == false && isDefended == false)
		{
			startHealthTimer();
		}
		
		if (isActive == true && isCaptured == false && isDefended == false)
		{
			startAreaTimer();
		}
		
	}
	
	
	
	void OnTriggerStay(Collider other)
	{
		
		if (other.gameObject.CompareTag("Enemy") && isActive == true)
		{
			isInside = true;
		}
		else
		{
			isInside = false;
		}
		
	}
	
	
	
	
	void startHealthTimer()
	{
		healthTime -= Time.deltaTime;
		//Debug.Log("Objective Health Left: " + healthTime.ToString());
		if (healthTime < 0)
		{
			Debug.Log("OBJECTIVE CAPTURED BY THE ENEMY!");
			isCaptured = true;
			isActive = false;
			levelManager.onArea1Complete();
			Destroy(gameObject);
		}
	}
	
	
	void startAreaTimer()
	{
		areaTimer -= Time.deltaTime;
		//Debug.Log("Area Time Left: " + areaTimer.ToString());
		if (areaTimer < 0)
		{
			Debug.Log("OBJECTIVE SUCCESSFULLY DEFENDED!");
			isDefended = true;
			isActive = false;
		}
	}
	
	
}
