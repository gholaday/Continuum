using UnityEngine;
using System.Collections;

public class playerDeath : MonoBehaviour {

	public GameObject death;
	public GameObject coolDownText;
	public int hitpoints = 2;
	//public bool gameOver = false;
	public Transform shield;
	public bool shieldSound = false;
	public AudioSource shieldDown;
	//public AudioClip clip;

	private GameManager gm;
	private GameObject manager;

	float slowMoTime;

	float invulTimer = 1.0f;

	

	void Start () {

		gameObject.layer = 15;

		manager = GameObject.Find("GameManager");
		gm = manager.GetComponent<GameManager>();



	}

	// Update is called once per frame
	void Update () {

		slowMoTime = this.GetComponent<playerMovement>().slowmoTime;

		if(gameObject.layer == 15)     //layer 15 = invin layer 11 = player
		{
			invulTimer -= Time.deltaTime;
			if(invulTimer % .1 >= .05)
			{
		
				renderer.enabled = false;


			}
			else
			{
				renderer.enabled = true;
			}
		}


		if(invulTimer <= 0)
		{
			gameObject.layer = 11;
			renderer.enabled = true;
			invulTimer = 1.0f;
		}

		//
		
		if(hitpoints <= 0 || slowMoTime <= 0){
			if(death != null)
			{
				Instantiate(death,transform.position,death.transform.rotation);	
				
			}

			//gm.playerFireRate = shoot.cooldown;
			Destroy(gameObject);
			
			
		}
	
	}

	void OnTriggerEnter(Collider other) {

		if(other.tag == "Enemy" || other.tag == "Meteor")

		{
			gameObject.layer = 15;
			hitpoints -= 1;

			if(other.tag == "Meteor"){
				hitpoints -= 2;
			}

			if(hitpoints < 2 && shieldSound == false)
			{
				shield.gameObject.SetActive(false);
				shieldDown.Play();
				shieldSound = true;
				
			}
			else
			{
				shield.gameObject.SetActive(true);
			}

		}

		else if(other.tag == "FireRateUp" && shoot.cooldown > .05f)
		{
			//AudioSource.PlayClipAtPoint(clip,transform.position);
			Instantiate(coolDownText,transform.position + new Vector3(0,1.0f,0), Quaternion.identity);
			shoot.cooldown -= .025f;
		}

		else if(other.tag == "ExtraLife")
		{
			gm.playerLives++;
			//insert sound later
		}

	}








}







