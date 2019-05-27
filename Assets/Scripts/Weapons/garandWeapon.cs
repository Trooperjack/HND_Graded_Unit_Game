﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class garandWeapon : MonoBehaviour {

	//https://www.youtube.com/watch?v=THnivyG0Mvo

	public Transform gunTip;
	
	
	public string weaponName = "M1 Garand";
	public int gunDamage = 65;
	private int startingMagazine;
    public int maxMagazine = 8;
    private int currentMagazine;
	private int startingAmmo;
    public int maxAmmo = 32;
    private int currentAmmo;
	public int reloadSpeed = 3;
    public float fireRate = .2f;
    public float weaponRange = 100f;
    public float hitForce = 100f;
	//public float switchSpeed = 0.25f;
    private int bulletsRemaining;
    public bool isReloading;
    public bool isEmpty;
    public bool canFire;
	
	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;
    private int shotsFired;
	
	Camera cam;
	LineRenderer bulletLine;
	
	Text magazineText;
    Text ammoText;
    Text reloadText;
	Text weaponText;
	
	void Awake()
	{
		cam = Camera.main;
		bulletLine = GetComponent<LineRenderer>();
		magazineText = GameObject.Find("Canvas/MagazineText").GetComponent<Text>();
        ammoText = GameObject.Find("Canvas/AmmoText").GetComponent<Text>();
        reloadText = GameObject.Find("Canvas/ReloadText").GetComponent<Text>();
		weaponText = GameObject.Find("Canvas/WeaponText").GetComponent<Text>();
		
		startingMagazine = maxMagazine;
		startingAmmo = maxAmmo;
        currentMagazine = startingMagazine;
        currentAmmo = startingAmmo;
	}
	
	// Update is called once per frame
	void Update () {
		
		CheckReloadText();
		
		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
		
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
		
		magazineText.text = "" + currentMagazine;
        ammoText.text = "/ " + currentAmmo;
		weaponText.text = weaponName;
		
	}
	
	
	void Shoot()
    {

        if (Time.time > nextFire && canFire == true && !isReloading && currentMagazine > 0)
        {
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
				EnemySniperController enemySniperHealth = hit.collider.GetComponent<EnemySniperController>();
				
                if (health != null)
                {
                    health.Damage(gunDamage);
                }

				if (enemyHealth != null)
                {
                    enemyHealth.Damage(gunDamage);
                }
				
				if (enemySniperHealth != null)
                {
                    enemySniperHealth.Damage(gunDamage);
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
	
	
	//Reload player's weapon
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
