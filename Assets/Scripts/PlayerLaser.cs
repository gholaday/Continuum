using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {

	public float cooldown = 180f;

	public bool canFire = false;

	private float timer;


	// Use this for initialization
	void Start () {
	
		timer = cooldown;
	}
	
	// Update is called once per frame
	void Update () {

		if(canFire == false)
		{
			timer -= Time.deltaTime;
		}
	
		if(timer <= 0)
		{
			timer = 0;
			canFire = true;
		}

		if((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && canFire)
		{
			//Fire the laser
			FireLaser();
		}

	}

	void FireLaser()
	{
		canFire = false;
		timer = cooldown;
		//Instantiate the laser prefab
	}
}
