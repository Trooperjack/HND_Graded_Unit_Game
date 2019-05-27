using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	public int startingHealth = 100;
	public int maxHealth = 100;
	public int currentHealth;
	public bool isDead;
	
	public int damage = 24;
	public float fireRate = .25f;
	
	public int startingMagazine = 10;
	public int maxMagazine = 10;
	public int currentMagazine;
	public int reloadTime = 3;
	public bool isReloading;
    public bool canFire;
	
	public int score = 100;
	public int dropChance = 20;
	
	public GameObject lightAmmo;
	public GameObject medAmmo;
	public GameObject largeAmmo;
	
	public GameObject lightHealth;
	public GameObject medHealth;
	public GameObject largeHealth;
	
	public GameObject LevelManagerObject;
	LevelManager levelManager;
	
	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;

	public GameObject projectile;
	public Transform playerTransform;
	public Transform triggerTarget;
	PlayerController player;
	NavMeshAgent nav;
	
	void Awake () {
		
		LevelManagerObject = GameObject.FindGameObjectWithTag("Level Manager");
		levelManager = LevelManagerObject.GetComponent<LevelManager>();
		
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		player = playerTransform.GetComponent<PlayerController>();
		nav = GetComponent<NavMeshAgent>();
		
		//triggerTarget = GameObject.FindGameObjectWithTag("Trigger Target").transform;
		
		isDead = false;
        currentHealth = startingHealth;
        currentMagazine = startingMagazine;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (playerTransform != null)
		{
			transform.LookAt(playerTransform);
		}
		
		Shoot();
		
		if (currentMagazine != 0 && isReloading == false)
        {
            canFire = true;
        }

        if (!isReloading && currentMagazine < maxMagazine)
        {
            isReloading = true;
            StartCoroutine(ReloadDelay());
        }

        if (currentMagazine < 0)
        {
            currentMagazine = 0;
            canFire = false;
        }
		
		
		bool doseDefendAreaExist = GameObject.FindGameObjectWithTag("Trigger Target");
		
		if (!isDead && player.isDead == false)
		{
			if (doseDefendAreaExist == true && levelManager.defendAreaObjectiveActive == true)
			{
				triggerTarget = GameObject.FindGameObjectWithTag("Trigger Target").transform;
				nav.SetDestination(triggerTarget.position);
			}
			else
			{
				nav.SetDestination(playerTransform.position);
			}
		}
		else
		{
			nav.enabled = false;
		}
		
		
	}
	
	
	
	
	void Shoot()
	{
		if (Time.time > nextFire && canFire == true && !isReloading && currentMagazine > 0)
		{
			currentMagazine--;
			nextFire = Time.time + fireRate;
			GameObject bullet = Instantiate(projectile, transform.position + transform.forward * 2, Quaternion.identity) as GameObject;
			Physics.IgnoreCollision(gameObject.GetComponent<CapsuleCollider>(),bullet.GetComponent<SphereCollider>());
			BulletProjectile bulletScript = bullet.GetComponent<BulletProjectile>();
			if (bulletScript != null)
			{
				bulletScript.setDamage(damage);
			}
			bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
		}
	}
	
	private IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(3);
		currentMagazine = maxMagazine;
        isReloading = false;
        canFire = true;
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
