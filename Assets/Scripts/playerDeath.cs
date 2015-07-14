using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class playerDeath : MonoBehaviour {


	public GameObject death;
	public int hitpoints = 2;

	
	
	public float shakeAmount = 0.1f;

	private GameManager gm;
	private GameObject manager;
	private CameraShake camShake;

	float slowMoTime;

	public float invulTimer = 1.0f;

    public GameObject powerupdisplaycanvas;

    public GameObject particles;
    float timer;

	Text displayText;
	
	SpriteRenderer sr;
	
	SpinEffect spin;

	void Start () {

        timer = invulTimer;

		gameObject.layer = 15;

		manager = GameObject.Find("GameManager");

		if(manager != null)
		{
			gm = manager.GetComponent<GameManager>();
		}

		camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
		
		displayText = GetComponentInChildren<Text>();
		
		sr = GetComponent<SpriteRenderer>();
		spin = Camera.main.GetComponent<SpinEffect>();

	}

	// Update is called once per frame
	void Update () {

		slowMoTime = this.GetComponent<playerMovement>().slowmoTime;

		if(gameObject.layer == 15)     //layer 15 = invin layer 11 = player
		{
			timer -= Time.deltaTime;
			if(timer % .1 >= .05)
			{
		
				sr.enabled = false;


			}
			else
			{
                sr.enabled = true;
			}
		}


		if(timer <= 0)
		{
			gameObject.layer = 11;
            sr.enabled = true;
            timer = invulTimer;
		}

		
		
		if(hitpoints <= 0)
		{ 
			Death ();
		}
		
		if(slowMoTime <= 0)
		{
			spin.StartEffect();
			Death();
		}
	
	}
	
	void Death()
	{
		if(death != null)
		{
			Instantiate(death,transform.position,death.transform.rotation);	
			
		}
		
		
		OnHitShake(shakeAmount);
		Destroy(gameObject);
		
	}
	
	void DisplayText(string s)
	{
		displayText.CrossFadeAlpha(255,.1f,false);
		displayText.text = s;
		Invoke ("DisableText", 1f);
	}
	
	void DisableText()
	{
		displayText.CrossFadeAlpha(0, 1.5f,false);
		
	}
	
	IEnumerator BlinkRed()
	{
		sr.color = Color.red;
		yield return new WaitForSeconds(.5f);
		sr.color = Color.white;
		yield return new WaitForSeconds(1.0f);
		StartCoroutine("BlinkRed");
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

            if (hitpoints < 2)
            {
                StartCoroutine("BlinkRed");

            }
            

        }

        else if (other.tag == "FireRateUp")
        {

            shoot.cooldown -= .025f;

            if (particles != null)
            {
                GameObject go = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
                go.transform.SetParent(gameObject.transform);
            }
            
			DisplayText("Fire Rate Increased!");
        }

        else if (other.tag == "ExtraLife")
        {
            gm.playerLives++;

            if (particles != null)
            {
                GameObject go = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
                go.transform.SetParent(gameObject.transform);
            }
            
			DisplayText("Extra Life!");
        }
        else if(other.tag == "ProtectorPowerUp")
        {
            if (particles != null)
            {
                GameObject go = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
                go.transform.SetParent(gameObject.transform);
            }

            GetComponent<PlayerProtectorHandler>().spawn = true;
            DisplayText("Protector Spawned!");
            

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
                if (GetComponentInChildren<WeaponDoubleLaser>().level < 4)
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
			string s = "Laser Level " + GetComponentInChildren<WeaponDoubleLaser>().level.ToString();
			DisplayText(s);

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
                if (GetComponentInChildren<WeaponLaserBeam>().level < 4)
                {
                    GetComponentInChildren<WeaponLaserBeam>().level++;
                }

                
            }
            else
            {
                shoot.weaponName = "Beam";
                GetComponentInChildren<WeaponLaserBeam>().enabled = true;
                GetComponentInChildren<WeaponDoubleLaser>().enabled = false;
                GetComponentInChildren<WeaponLaserBeam>().level = 1;

            }
			
			string s = "Beam Level " + GetComponentInChildren<WeaponLaserBeam>().level.ToString();
			
			DisplayText(s);

        }
    }



	void OnHitShake(float amount)
	{
		camShake.shake += amount;
	}


	IEnumerator TimeFreeze()
	{
        
		Time.timeScale = 0;
		yield return new WaitForSeconds(1f);
		Time.timeScale = 1;
        
	}

}







