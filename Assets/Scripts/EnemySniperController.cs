using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySniperController : MonoBehaviour {

	public int startingHealth = 100;
	public int maxHealth = 100;
	public int currentHealth;
	public bool isDead;
	
	public int damage = 45;
	
	public int startingMagazine = 5;
	public int maxMagazine = 5;
	public int currentMagazine;
	public int reloadTime = 3;
	
	public int score = 250;
	
	//private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;
    private int shotsFired;

	public Transform playerTransform;
	public Transform triggerTarget;
	PlayerController player;
	
	void Awake () {
		
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		player = playerTransform.GetComponent<PlayerController>();
		
		//triggerTarget = GameObject.FindGameObjectWithTag("Trigger Target").transform;
		
		isDead = false;
        currentHealth = startingHealth;
        currentMagazine = startingMagazine;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
	}
	
	
	public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
			isDead = true;
			Destroy(gameObject);
        }
    }
	
	
}
