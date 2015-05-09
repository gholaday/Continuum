using UnityEngine;
using System.Collections;

public class enemyShoot: MonoBehaviour {
	
	public GameObject bullet;
	public Transform bulletSpawn;
	public float cooldown = 1.0f;
	public bool tripleShot = false;

	private float countdown;
	private bool canShoot = false;
	
	void Start(){

        cooldown -= EnemyModifier.bonusAttackSpeed;
		countdown = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
		
		countdown -= Time.deltaTime;

		
		if(countdown <= 0)
		{
			canShoot = true;
			countdown = cooldown; 
		}
		
		if(canShoot == true && playerMovement.timeStop == 1)
		{
			if(tripleShot)
			{
				TripleShot();
			}
			else{
					NormalShot();
				}
			
			canShoot = false;
		}
		
	}

	void NormalShot()
	{
		Instantiate(bullet, bulletSpawn.position, bullet.transform.rotation);
	}

	void TripleShot()
	{
		Instantiate(bullet, bulletSpawn.position, Quaternion.Euler(0,0,-200));
		Instantiate(bullet, bulletSpawn.position, Quaternion.Euler(0,0,-160));
		Instantiate(bullet, bulletSpawn.position, Quaternion.Euler (0,0,-180));
	}

	


}
