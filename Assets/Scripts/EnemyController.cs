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
	
	public GameObject lightAmmo;
	public GameObject medAmmo;
	public GameObject largeAmmo;
	
	public GameObject lightHealth;
	public GameObject medHealth;
	public GameObject largeHealth;
	
	//private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;
    private int shotsFired;

	public Transform playerTransform;
	public Transform triggerTarget;
	PlayerController player;
	NavMeshAgent nav;
	
	void Awake () {
		
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		player = playerTransform.GetComponent<PlayerController>();
		nav = GetComponent<NavMeshAgent>();
		
		triggerTarget = GameObject.FindGameObjectWithTag("Trigger Target").transform;
		
		isDead = false;
        currentHealth = startingHealth;
        currentMagazine = startingMagazine;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isDead && player.isDead == false)
		{
			nav.SetDestination(triggerTarget.position);
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
			randomPickup();
			Destroy(gameObject);
        }
    }
	
	
	void randomPickup()
	{
		
		int ammo;
		int a;
		a = Random.Range(0,100);
		
		if (a <= dropChance)
		{
			ammo = Random.Range(0,100);
			if (ammo >= 0 && ammo < 50)
			{
				Instantiate(lightAmmo, transform.position, transform.rotation);
			}
			if (ammo >= 50 && ammo < 80)
			{
				Instantiate(medAmmo, transform.position, transform.rotation);
			}
			if (ammo >= 80 && ammo < 95)
			{
				Instantiate(largeAmmo, transform.position, transform.rotation);
			}
			if (ammo >= 95 && ammo < 100)
			{
				Instantiate(lightHealth, transform.position, transform.rotation);
			}
		}
		
		
	}
	
}
