using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //https://www.mvcode.com/lessons/first-person-camera-and-controller-jamie
    //https://unity3d.com/learn/tutorials/projects/lets-try-assignments/lets-try-shooting-raycasts-article

    //Public variables
    public float walkSpeed;
    
    public int startingHealth = 100;
    public int maxHealth = 100;
    public int currentHealth;
    public bool isDead;

	
	//WEAPONS STATS
	//M1 GARAND
	public string garandName = "M1 Garand";
	public int garandDamage = 65;
	public int garandMaxMagazine = 8;
	public int garandMaxAmmo = 32;
	public int garandReload = 3;
	public float garandFireRate = 0.2f;
	
	//M1A1 THOMPSON
	public string tommyName = "M1A1 Thompson";
	public int tommyDamage = 43;
	public int tommyMaxMagazine = 30;
	public int tommyMaxAmmo = 120;
	public int tommyReload = 3;
	public float tommyFireRate = 0.133f;
	
	//PANZERSCHRECK
	public string rocketName = "Panzerschreck";
	public int rocketDamage = 2000;
	public int rocketMaxMagazine = 1;
	public int rocketMaxAmmo = 3;
	public int rocketReload = 5;
	public float rocketFireRate = 0.1f;
	
	
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
            float a;
            int b;
            //a = currentAmmo + ((maxAmmo / 100) * 20);
            //b = Mathf.RoundToInt(a);
            //currentAmmo = currentAmmo + b;
        }
        //Medium Ammo
        if (other.gameObject.CompareTag("MediumAmmoPickup"))
        {
			Destroy(other.gameObject);
            float a;
            int b;
            //a = currentAmmo + ((maxAmmo / 100) * 40);
            //b = Mathf.RoundToInt(a);
            //currentAmmo = currentAmmo + b;
        }
        //Large Ammo
        if (other.gameObject.CompareTag("LargeAmmoPickup"))
        {
			Destroy(other.gameObject);
            float a;
            int b;
            //a = currentAmmo + ((maxAmmo / 100) * 60);
            //b = Mathf.RoundToInt(a);
            //currentAmmo = currentAmmo + b;
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
			Debug.Log("IS HIT");
		}
	}
	

	//When the player is damaged
	public void onDamaged(int damageAmount)
	{
		currentHealth = currentHealth - damageAmount;
		if (currentHealth <= 0)
		{
			isDead = true;
		}

	}
	

}
