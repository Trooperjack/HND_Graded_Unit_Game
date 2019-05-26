using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour {

	public float range;
	public int damage;
	
	public GameObject player;
	PlayerController playerScript;
	
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerScript = player.GetComponent<PlayerController>();
		range = 5f;
	}
	
	void Update()
	{
		range -= Time.deltaTime;
		if (range < 0)
		{
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (playerScript != null)
			{
				playerScript.onDamaged(damage);
			}
		}
		Destroy(gameObject);
	}
	
	public void setDamage(int damageAmount)
	{
		damage = damageAmount;
	}
	
}
