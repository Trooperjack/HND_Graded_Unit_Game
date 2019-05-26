using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySniperController : MonoBehaviour {

	public int startingHealth = 100;
	public int maxHealth = 100;
	public int currentHealth;
	public bool isDead;
	
	public int damage = 50;
	public float fireRate = .5f;
	
	public int startingMagazine = 10;
	public int maxMagazine = 10;
	public int currentMagazine;
	public int reloadTime = 3;
	public bool isReloading;
    public bool canFire;
	
	public int score = 100;
		
	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;

	public GameObject projectile;
	public Transform playerTransform;
	public Transform triggerTarget;
	PlayerController player;
	
	public GameObject enemyManager;
	EnemySpawning enemyScript;
	
	void Awake () {
		
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		player = playerTransform.GetComponent<PlayerController>();
		
		enemyManager = GameObject.FindGameObjectWithTag("Enemy Manager");
		enemyScript = enemyManager.GetComponent<EnemySpawning>();
		
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
			enemyScript.OnSniperDeath();
			Destroy(gameObject);
        }
    }
	
}
