using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //https://www.mvcode.com/lessons/first-person-camera-and-controller-jamie
    //https://unity3d.com/learn/tutorials/projects/lets-try-assignments/lets-try-shooting-raycasts-article

    //Public variables
    public float walkSpeed;
    public Transform gunTip;

    public int gunDamage = 43;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;

    public int startingHealth = 100;
    public int maxHealth = 100;
    public int currentHealth;
    public bool isDead;

    public int startingMagazine = 10;
    public int maxMagazine = 10;
    public int currentMagazine;
    public int startingAmmo = 60;
    public int maxAmmo = 60;
    public int currentAmmo;
    public int bulletsRemaining;
    public bool isReloading;
    public bool isEmpty;
    public bool canFire;

    //Private variables
    Rigidbody rb;
    Vector3 moveDirection;
    CapsuleCollider col;
    Camera cam;
    LineRenderer bulletLine;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;
    private int shotsFired;

    Text messageText;
    Text shootingText;
    Text healthText;
    Text magazineText;
    Text ammoText;
    Text reloadText;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        bulletLine = GetComponent<LineRenderer>();

        cam = Camera.main;
        //cam = GetComponentInParent<Camera>();

        messageText = GameObject.Find("Canvas/MessageText").GetComponent<Text>();
        shootingText = GameObject.Find("Canvas/ShootingText").GetComponent<Text>();
        healthText = GameObject.Find("Canvas/HealthText").GetComponent<Text>();
        magazineText = GameObject.Find("Canvas/MagazineText").GetComponent<Text>();
        ammoText = GameObject.Find("Canvas/AmmoText").GetComponent<Text>();
        reloadText = GameObject.Find("Canvas/ReloadText").GetComponent<Text>();

        isDead = false;
        shotsFired = 0;
        currentHealth = startingHealth;
        currentMagazine = startingMagazine;
        currentAmmo = startingAmmo;
    }



    void Update () {
        CheckInteraction();
        CheckReloadText();
        Shoot();
        
        shootingText.text = "" + shotsFired;
        healthText.text = "" + currentHealth;
        magazineText.text = "" + currentMagazine;
        ammoText.text = "/ " + currentAmmo;

        if (currentMagazine != 0 && isReloading == false)
        {
            canFire = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentMagazine < maxMagazine && currentAmmo > 0)
        {
            isReloading = true;
            StartCoroutine(ReloadDelay());
        }

        if (currentMagazine < 0)
        {
            currentMagazine = 0;
            canFire = false;
        }
        if (currentAmmo < 0)
        {
            currentAmmo = 0;
        }
        if (currentAmmo > 0)
        {
            isEmpty = false;
        }
        if (currentAmmo <= 0 && currentMagazine <= 0)
        {
            isEmpty = true;
        }
        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        if (currentMagazine > maxMagazine)
        {
            currentMagazine = maxMagazine;
        }

        if (currentHealth < 0)
        {
            currentHealth = 0;
            isDead = true;
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Vector3 lineOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Debug.DrawRay(lineOrigin, cam.transform.forward * 1000f, Color.green);

        //Get directional input from the user
        float horizontalMovement = 0;
        float verticalMovement = 0;

        if (CanMove(transform.right * Input.GetAxisRaw("Horizontal")))
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
        }

        if (CanMove(transform.forward * Input.GetAxisRaw("Vertical")))
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
        }
    }




    void Shoot()
    {

        if (Input.GetButton("Fire1") && Time.time > nextFire && canFire == true && !isReloading && currentMagazine > 0)
        {
            shotsFired++;
            currentMagazine--;
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;
            bulletLine.SetPosition(0, gunTip.transform.position);

            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, weaponRange))
            {
                bulletLine.SetPosition(1, hit.point);
                ShootableBox health = hit.collider.GetComponent<ShootableBox>();
				EnemyController enemyHealth = hit.collider.GetComponent<EnemyController>();
				
                if (health != null)
                {
                    health.Damage(gunDamage);
                }

				if (enemyHealth != null)
                {
                    enemyHealth.Damage(gunDamage);
                }
				
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }

            }
            else
            {
                bulletLine.SetPosition(1, rayOrigin + (cam.transform.forward * weaponRange));
            }
        }

    }





    void CheckReloadText()
    {

        reloadText.text = "";

        if(currentMagazine <= 0 && !isReloading && !isEmpty)
        {
            reloadText.text = "Press R to Reload Weapon";
        }
        else
        {
            if (isReloading == true)
            {
                reloadText.text = "Reloading...";
            }
            else
            {
                if (isEmpty == true && !isReloading)
                {
                    reloadText.text = "No Ammo";
                }
            }
        }

    }



    void OnTriggerEnter(Collider other)
    {
        //Ammo Pickups
        //Light Ammo
        if (other.gameObject.CompareTag("LightAmmoPickup") && currentAmmo < maxAmmo)
        {
            other.gameObject.SetActive(false);
            float a;
            int b;
            a = currentAmmo + ((maxAmmo / 100) * 20);
            b = Mathf.RoundToInt(a);
            currentAmmo = currentAmmo + b;
        }
        //Medium Ammo
        if (other.gameObject.CompareTag("MediumAmmoPickup") && currentAmmo < maxAmmo)
        {
            other.gameObject.SetActive(false);
            float a;
            int b;
            a = currentAmmo + ((maxAmmo / 100) * 40);
            b = Mathf.RoundToInt(a);
            currentAmmo = currentAmmo + b;
        }
        //Large Ammo
        if (other.gameObject.CompareTag("LargeAmmoPickup") && currentAmmo < maxAmmo)
        {
            other.gameObject.SetActive(false);
            float a;
            int b;
            a = currentAmmo + ((maxAmmo / 100) * 60);
            b = Mathf.RoundToInt(a);
            currentAmmo = currentAmmo + b;
        }

        //Health Pickups
        //Light Health
        if (other.gameObject.CompareTag("LightHealthPickup") && currentHealth < maxHealth)
        {
            other.gameObject.SetActive(false);
            float a;
            int b;
            a = currentHealth + ((maxHealth / 100) * 20);
            b = Mathf.RoundToInt(a);
            currentHealth = currentHealth + b;
        }
        //Medium Health
        if (other.gameObject.CompareTag("MediumHealthPickup") && currentHealth < maxHealth)
        {
            other.gameObject.SetActive(false);
            float a;
            int b;
            a = currentHealth + ((maxHealth / 100) * 50);
            b = Mathf.RoundToInt(a);
            currentHealth = currentHealth + b;
        }
        //Large Health
        if (other.gameObject.CompareTag("LargeHealthPickup") && currentHealth < maxHealth)
        {
            other.gameObject.SetActive(false);
            float a;
            int b;
            a = currentHealth + ((maxHealth / 100) * 80);
            b = Mathf.RoundToInt(a);
            currentHealth = currentHealth + b;
        }
    }



    private IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(3);
        if (currentAmmo > maxMagazine - currentMagazine)
        {
            bulletsRemaining = maxMagazine - currentMagazine;
            currentMagazine = maxMagazine;
            currentAmmo = currentAmmo - bulletsRemaining;
        }
        else
        {
            bulletsRemaining = maxMagazine - currentMagazine;
            currentMagazine = currentMagazine + currentAmmo;
            currentAmmo = 0;
        }
        isReloading = false;
        canFire = true;
    }



    private IEnumerator ShotEffect()
    {
        bulletLine.enabled = true;

        yield return shotDuration;

        bulletLine.enabled = false;

    }



}
