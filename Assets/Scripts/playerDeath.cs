using UnityEngine;
using System.Collections;

public class playerDeath : MonoBehaviour {


	public GameObject death;
	public int hitpoints = 2;
	public Transform shield;
	public bool shieldSound = false;
	public AudioSource shieldDown;
	public float shakeAmount = 0.1f;

	private GameManager gm;
	private GameObject manager;
	private CameraShake camShake;

	float slowMoTime;

	float invulTimer = 1.0f;

	

	void Start () {

		gameObject.layer = 15;

		manager = GameObject.Find("GameManager");
		if(manager != null)
		{
			gm = manager.GetComponent<GameManager>();
		}

		camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

	}

	// Update is called once per frame
	void Update () {

		slowMoTime = this.GetComponent<playerMovement>().slowmoTime;

		if(gameObject.layer == 15)     //layer 15 = invin layer 11 = player
		{
			invulTimer -= Time.deltaTime;
			if(invulTimer % .1 >= .05)
			{
		
				GetComponent<Renderer>().enabled = false;


			}
			else
			{
				GetComponent<Renderer>().enabled = true;
			}
		}


		if(invulTimer <= 0)
		{
			gameObject.layer = 11;
			GetComponent<Renderer>().enabled = true;
			invulTimer = 1.0f;
		}

		//
		
		if(hitpoints <= 0 || slowMoTime <= 0){
			if(death != null)
			{
				Instantiate(death,transform.position,death.transform.rotation);	
				
			}

			//gm.playerFireRate = shoot.cooldown;
			OnHitShake(shakeAmount);
			Destroy(gameObject);
			
			
		}
	
	}

	void OnTriggerEnter(Collider other) {

		if(other.tag == "Enemy" || other.tag == "Meteor" || other.tag == "EnemyBullet")

		{
			OnHitShake(shakeAmount);
			gameObject.layer = 15;
			hitpoints -= 1;

			if(other.tag == "Meteor"){
				hitpoints -= 2;
			}

			if(hitpoints < 2 && shieldSound == false)
			{
				shield.gameObject.SetActive(false);
				//shieldDown.Play();
				shieldSound = true;
				
			}
			else
			{
				shield.gameObject.SetActive(true);
			}

		}

		else if(other.tag == "FireRateUp" && shoot.cooldown > .05f)
		{

			shoot.cooldown -= .025f;
		}

		else if(other.tag == "ExtraLife")
		{
			gm.playerLives++;
			//insert sound later
		}

	}



	void OnHitShake(float amount)
	{
		camShake.shake = amount;
	}




}







