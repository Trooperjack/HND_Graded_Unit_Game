using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakCannonController : MonoBehaviour {

	public GameObject levelManagerObject;
	LevelManager lvlman;

	void Start()
	{
		levelManagerObject = GameObject.FindGameObjectWithTag("Level Manager");
		lvlman = levelManagerObject.GetComponent<LevelManager>();
	}

	public int currentHealth = 4000;
    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
			lvlman.FlakCannonsKilled++;
            Destroy(gameObject);
        }
    }
}
