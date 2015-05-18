using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

	public float invulTimer = 1.0f;

    public GameObject powerupdisplaycanvas;

    public GameObject particles;
    float timer;

	

	void Start () {

        timer = invulTimer;

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
			timer -= Time.deltaTime;
			if(timer % .1 >= .05)
			{
		
				GetComponent<Renderer>().enabled = false;


			}
			else
			{
				GetComponent<Renderer>().enabled = true;
			}
		}


		if(timer <= 0)
		{
			gameObject.layer = 11;
			GetComponent<Renderer>().enabled = true;
            timer = invulTimer;
		}

		
		
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

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy" || other.tag == "Meteor" || other.tag == "EnemyBullet")
        {
            OnHitShake(shakeAmount);
            StartCoroutine("TimeFreeze");
            gameObject.layer = 15;
            hitpoints -= 1;

            if (other.tag == "Meteor")
            {
                hitpoints -= 2;
            }

            if (hitpoints < 2 && shieldSound == false)
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

        else if (other.tag == "FireRateUp" && shoot.cooldown > .1f)
        {

            shoot.cooldown -= .04f;

            if (particles != null)
            {
                GameObject go = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
                go.transform.SetParent(gameObject.transform);
            }
        }

        else if (other.tag == "ExtraLife")
        {
            gm.playerLives++;

            if (particles != null)
            {
                GameObject go = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
                go.transform.SetParent(gameObject.transform);
            }
        }
        else if (other.tag == "LaserPowerUp")
        {
            if (particles != null)
            {
                GameObject go = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
                go.transform.SetParent(gameObject.transform);
            }

            if (shoot.weaponName == "Laser")
            {
                if (GetComponentInChildren<WeaponDoubleLaser>().level < 3)
                {
                    GetComponentInChildren<WeaponDoubleLaser>().level++;
                }

                
            }
            else
            {
                shoot.weaponName = "Laser";
                GetComponentInChildren<WeaponDoubleLaser>().enabled = true;
                GetComponentInChildren<WeaponLaserBeam>().enabled = false;
                GetComponentInChildren<WeaponDoubleLaser>().level = 1;
                

            }

            GameObject text = Instantiate(powerupdisplaycanvas, transform.position, Quaternion.identity) as GameObject;
            text.GetComponent<Text>().text = "Laser Level " + GetComponentInChildren<WeaponDoubleLaser>().level;

        }
        else if (other.tag == "BeamPowerUp")
        {
            if (particles != null)
            {
                GameObject go = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
                go.transform.SetParent(gameObject.transform);
            }

            if (shoot.weaponName == "Beam")
            {
                if (GetComponentInChildren<WeaponLaserBeam>().level < 3)
                {
                    GetComponentInChildren<WeaponLaserBeam>().level++;
                }

                
            }
            else
            {
                shoot.weaponName = "Beam";
                GetComponentInChildren<WeaponDoubleLaser>().enabled = false;
                GetComponentInChildren<WeaponLaserBeam>().enabled = true;
                GetComponentInChildren<WeaponLaserBeam>().level = 1;
            }

            GameObject text = Instantiate(powerupdisplaycanvas, transform.position, Quaternion.identity) as GameObject;
            text.GetComponent<Text>().text = "Beam Level " + GetComponentInChildren<WeaponLaserBeam>().level;

        }
    }



	void OnHitShake(float amount)
	{
		camShake.shake = amount;
	}


	IEnumerator TimeFreeze()
	{
		Time.timeScale = Mathf.Epsilon;
		yield return new WaitForSeconds(.1f);
		Time.timeScale = 1;
        
	}

}







