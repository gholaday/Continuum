using UnityEngine;
using System.Collections;

public class shoot: MonoBehaviour {
	
	public GameObject bullet;
	public Transform bulletSpawn;
	[SerializeField] public static float cooldown = 0.25f;
	public float maxCooldown = 0.25f;
	public bool isShooting = false;
	public AudioSource shootSound;
	private float countdown;
	private bool canShoot = true;

	float currentCd;




	void Start(){

		countdown = cooldown;
		cooldown += (cooldown * .25f);
		currentCd = cooldown;
	
	}
	
	// Update is called once per frame
	void Update () {

		if(cooldown > maxCooldown)
		{
			cooldown = maxCooldown;
		}
	

		if(playerMovement.timeStop == 0)
		{
			currentCd = .05f;
		}
		else{
			currentCd = cooldown;
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
			countdown = currentCd; 
			shootSound.Play();
		}
			else{
			isShooting = false;
		}
				
	}
}



