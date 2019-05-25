using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	public int startingHealth = 100;
	public int maxHealth = 100;
	public int currentHealth;
	public bool isDead;
	
	public int damage = 38;
	
	public int startingMagazine = 10;
	public int maxMagazine = 10;
	public int currentMagazine;
	public int reloadTime = 3;
	
	public int score = 100;
	public int dropChance = 20;
	
	//private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;
    private int shotsFired;

	public Transform playerTransform;
	PlayerController player;
	NavMeshAgent nav;
	
	void Awake () {
		
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		player = playerTransform.GetComponent<PlayerController>();
		nav = GetComponent<NavMeshAgent>();
		
		isDead = false;
        currentHealth = startingHealth;
        currentMagazine = startingMagazine;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isDead && player.isDead == false)
		{
			nav.SetDestination(playerTransform.position);
		}
		else
		{
			nav.enabled = false;
		}
		
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
