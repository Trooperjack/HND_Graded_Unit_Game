using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour {

	public float range;
	
	void Awake()
	{
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
	
	void OnCollisionEnter()
	{
		Destroy(gameObject);
	}
	
}
