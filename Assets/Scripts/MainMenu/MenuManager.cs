using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
	
	GameObject mainScreen;
	GameObject newGameScreen;
	
	void Start () {
		
		mainScreen = GameObject.Find("Canvas/Main Screen");
		newGameScreen = GameObject.Find("Canvas/New Game Screen");
		
		mainScreen.GetComponent<Renderer>().enabled = true;
		newGameScreen.GetComponent<Renderer>().enabled = false;
		
	}
	
}
