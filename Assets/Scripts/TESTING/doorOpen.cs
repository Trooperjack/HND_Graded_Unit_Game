using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : MonoBehaviour {


    float startTime;
    bool isOpened = false;


    //When the Script is enabled
	void OnEnable () {
        //If isOpened == true, then set it to false and vice-versa
        isOpened = !isOpened;

        startTime = Time.time;
	}
	
	
	void Update () {
		
        //If the door was opened...
        if (isOpened)
        {
            //...start closing it at a rate of -90 degrees per second
            transform.Rotate(transform.up, -90 * Time.deltaTime);
        }
        else
        {
            //...otherwise open it at a rate of 90 degrees per second
            transform.Rotate(transform.up, 90 * Time.deltaTime);
        }


        //If it has been 1 second since we started
        if (Time.time - startTime > 1f)
        {
            //De-enable the script
            enabled = false;
        }


	}
}
