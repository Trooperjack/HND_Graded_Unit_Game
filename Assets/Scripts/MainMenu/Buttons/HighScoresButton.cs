﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HighScoresButton : MonoBehaviour {


	public void Pressed()
	{
		SceneManager.LoadScene("HighScores");
	}
	
	
}
