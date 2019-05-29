using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameController : MonoBehaviour {

	public GameStaticController gameController;
	public InputField iField;
	public string name;
	
	void Start () {
		
		gameController = GameObject.Find("GameStaticController").GetComponent<GameStaticController>();
		
	}
	
	public void ApplyName()
	{
		name = iField.text;
		gameController.playerName = name;
	}
	
}
