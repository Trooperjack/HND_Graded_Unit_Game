﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public GameStaticController gameController;
	
    //Public variables
    public float walkSpeed;
    
    public int startingHealth = 100;
    public int maxHealth = 100;
    public int currentHealth;
    public bool isDead;
	public bool godMode = false;
	
	public GameObject wep1;
	public GameObject wep2;
	public GameObject wep3;
	WeaponStats gara;
	WeaponStats tommy;
	WeaponStats panz;
	
    //Private variables
    Rigidbody rb;
    Vector3 moveDirection;
    CapsuleCollider col;
    Camera cam;

    Text messageText;
    Text healthText;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

		gameController = GameObject.Find("GameStaticController").GetComponent<GameStaticController>();
		
		wep1 = GameObject.FindGameObjectWithTag("M1Garand");
		wep2 = GameObject.FindGameObjectWithTag("M1A1Thompson");
		wep3 = GameObject.FindGameObjectWithTag("Panzerschreck");
		gara = wep1.GetComponent<WeaponStats>();
		tommy = wep2.GetComponent<WeaponStats>();
		panz = wep3.GetComponent<WeaponStats>();
		
        cam = Camera.main;
        //cam = GetComponentInParent<Camera>();

        messageText = GameObject.Find("Canvas/MessageText").GetComponent<Text>();
        healthText = GameObject.Find("Canvas/HealthText").GetComponent<Text>();
		
        isDead = false;
        currentHealth = startingHealth;
    }



    void Update () {
		
		if (!isDead)
		{
			CheckInteraction();
		}
        
        healthText.text = "" + currentHealth;
        

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Vector3 lineOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Debug.DrawRay(lineOrigin, cam.transform.forward * 1000f, Color.green);

        //Get directional input from the user
        float horizontalMovement = 0;
        float verticalMovement = 0;

        if (CanMove(transform.right * Input.GetAxisRaw("Horizontal")) && !isDead)
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
        }

        if (CanMove(transform.forward * Input.GetAxisRaw("Vertical")) && !isDead)
        {
            verticalMovement = Input.GetAxisRaw("Vertical");
        }

        moveDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
    }

    void FixedUpdate()
    {
        //Call the move function
        Move();
    }



    void Move()
    {
        Vector3 yVelFix = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = moveDirection * walkSpeed * Time.deltaTime;
        rb.velocity += yVelFix;
    }


    bool CanMove(Vector3 dir)
    {
        float distanceToPoints = col.height / 2 - col.radius;

        Vector3 point1 = transform.position + col.center + Vector3.up * distanceToPoints;
        Vector3 point2 = transform.position + col.center - Vector3.up * distanceToPoints;

        float radius = col.radius * 0.95f;
        float castDistance = 0.5f;

        RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, radius, dir, castDistance);

        foreach (RaycastHit objectHit in hits)
        {
            if (objectHit.transform.tag == "Wall")
            {
                return false;
            }
        }

        return true;
    }



    void CheckInteraction()
    {
        messageText.text = "";

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 4f))
        {
            if (hit.transform.tag == "Door")
            {
                messageText.text = "Press E to open";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.gameObject.GetComponent<doorOpen>().enabled = true;
                }
            }
			
			if (hit.transform.tag == "Ammo Box")
			{
				messageText.text = "Press E to refill Ammo";
				if (Input.GetKeyDown(KeyCode.E))
                {
					gara.currentAmmo = gara.maxAmmo;
					tommy.currentAmmo = tommy.maxAmmo;
					panz.currentAmmo = panz.maxAmmo;
					Debug.Log("AMMO GET");
                }
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {		
        //Ammo Pickups
        //Light Ammo
        if (other.gameObject.CompareTag("LightAmmoPickup"))
        {
			Destroy(other.gameObject);
			int chance = 20;
			gara.getAmmo(chance);
			tommy.getAmmo(chance);
			panz.getAmmo(chance);
        }
        //Medium Ammo
        if (other.gameObject.CompareTag("MediumAmmoPickup"))
        {
			Destroy(other.gameObject);
			int chance = 20;
			gara.getAmmo(chance);
			tommy.getAmmo(chance);
			panz.getAmmo(chance);
        }
        //Large Ammo
        if (other.gameObject.CompareTag("LargeAmmoPickup"))
        {
			Destroy(other.gameObject);
			int chance = 20;
			gara.getAmmo(chance);
			tommy.getAmmo(chance);
			panz.getAmmo(chance);
        }

        //Health Pickups
        //Light Health
        if (other.gameObject.CompareTag("LightHealthPickup") && currentHealth < maxHealth)
        {
			Destroy(other.gameObject);
            float a;
            int b;
            a = currentHealth + ((maxHealth / 100) * 20);
            b = Mathf.RoundToInt(a);
            currentHealth = currentHealth + b;
        }
        //Medium Health
        if (other.gameObject.CompareTag("MediumHealthPickup") && currentHealth < maxHealth)
        {
			Destroy(other.gameObject);
            float a;
            int b;
            a = currentHealth + ((maxHealth / 100) * 50);
            b = Mathf.RoundToInt(a);
            currentHealth = currentHealth + b;
        }
        //Large Health
        if (other.gameObject.CompareTag("LargeHealthPickup") && currentHealth < maxHealth)
        {
			Destroy(other.gameObject);
            float a;
            int b;
            a = currentHealth + ((maxHealth / 100) * 80);
            b = Mathf.RoundToInt(a);
            currentHealth = currentHealth + b;
        }
    }

	
	//On any Physicall Object Hits the player
	void OnCollisionEnter(Collision other)
	{
		//Damaged
		if (other.gameObject.CompareTag("Projectile") && !isDead)
		{
			//Debug.Log("IS HIT");
		}
	}
	

	//When the player is damaged
	public void onDamaged(int damageAmount)
	{
		if (!godMode)
		{
			currentHealth = currentHealth - damageAmount;
			if (currentHealth <= 0)
			{
				isDead = true;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				gameController.gameMusic.Stop();
				gameController.gameMusic.loop = true;
				gameController.gameMusic.clip = gameController.menuFile;
				gameController.gameMusic.volume = 0.8f;
				gameController.gameMusic.Play();
				gameController.gamesLost++;
				PlayerPrefs.SetInt("gamesLost", gameController.gamesLost);
				if (gameController.GlobalScore > gameController.highScore);
				{
					PlayerPrefs.SetInt("highScore", gameController.GlobalScore);
				}
				gameController.GlobalGameResult = "lost - death";
				SceneManager.LoadScene("Results");
			}
		}

	}
	

}
