using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawn : MonoBehaviour {

	public GameObject lightAmmo;
	public GameObject medAmmo;
	public GameObject largeAmmo;
	
	public GameObject lightHealth;
	public GameObject medHealth;
	public GameObject largeHealth;

	private bool forceHealth;
	
	void Awake () {
		
		forceHealth = true;
		
		int ammo;
		int health;
		
		int a;
		a = Random.Range(1,3);
		
		if (a == 1 && forceHealth == false)
		{
			ammo = Random.Range(1,4);
			if (ammo == 1)
			{
				Instantiate(lightAmmo, transform.position, transform.rotation);
			}
			if (ammo == 2)
			{
				Instantiate(medAmmo, transform.position, transform.rotation);
			}
			if (ammo == 3)
			{
				Instantiate(largeAmmo, transform.position, transform.rotation);
			}
		}
		
		if (a == 2 || forceHealth == true)
		{
			health = Random.Range(1,4);
			if (health == 1)
			{
				Instantiate(lightHealth, transform.position, transform.rotation);
			}
			if (health == 2)
			{
				Instantiate(medHealth, transform.position, transform.rotation);
			}
			if (health == 3)
			{
				Instantiate(largeHealth, transform.position, transform.rotation);
			}
		}
		
	}
	
	
}