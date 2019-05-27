using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {

	//https://www.youtube.com/watch?v=Dn_BUIVdAPg

	public int selectedWeapon = 0;
	public bool isReloading;

	// Use this for initialization
	void Start () {
		SelectWeapon();
		isReloading = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		int previousSelectedWeapon = selectedWeapon;
		
		if (Input.GetKeyDown(KeyCode.Alpha1) && isReloading == false)
		{
			selectedWeapon = 0;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && isReloading == false)
		{
			selectedWeapon = 1;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3 && isReloading == false)
		{
			selectedWeapon = 2;
		}
		if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4 && isReloading == false)
		{
			selectedWeapon = 3;
		}
		
		if (previousSelectedWeapon != selectedWeapon)
		{
			SelectWeapon();
		}
		
	}
	
	
	
	void SelectWeapon()
	{
		int i = 0;
		foreach (Transform weapon in transform)
		{
			if (i == selectedWeapon)
			{
				weapon.gameObject.SetActive(true);
			}
			else
			{
				weapon.gameObject.SetActive(false);
			}
			i++;
		}
	}
	
	
}
