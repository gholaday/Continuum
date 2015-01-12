using UnityEngine;
using System.Collections;

public class shoot: MonoBehaviour {
	
	public GameObject bullet;
	public Transform bulletSpawn;
	public static float cooldown = 1.0f;
	public float pubCooldown;
	public bool isShooting = false;
	public AudioSource shootSound;
	private float countdown;
	private bool canShoot = true;




	void Start(){
		countdown = cooldown;
		cooldown = pubCooldown;

	}
	
	// Update is called once per frame
	void Update () {


		countdown -= Time.deltaTime;

		if(countdown <= 0)
		{
			canShoot = true;
			countdown = 0;

		}

		if(canShoot == true)
	    {
			Instantiate(bullet, bulletSpawn.position, bullet.transform.rotation);
			canShoot = false;
			isShooting = true;
			countdown = cooldown; 
			shootSound.Play();
		}
			else{
			isShooting = false;
		}
				
	}
}



