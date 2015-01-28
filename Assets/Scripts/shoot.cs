using UnityEngine;
using System.Collections;

public class shoot: MonoBehaviour {
	
	public GameObject bullet;
	public Transform bulletSpawn;
	[SerializeField]
	public static float cooldown = 0.25f;
	public float maxCooldown = 0.25f;
	public bool isShooting = false;
	public AudioSource shootSound;
	private float countdown;
	private bool canShoot = true;




	void Start(){

		countdown = cooldown;
		cooldown += .1f;
	
	}
	
	// Update is called once per frame
	void Update () {

		if(cooldown > maxCooldown)
		{
			cooldown = maxCooldown;
		}
	

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



