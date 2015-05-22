using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossDeath : MonoBehaviour {

    public float health = 500f;
    public float pointsValue = 10000;
    public GameObject deathParticles;
    public GameObject powerUp;
    public GameObject pointsText = null;
    public Slider hpBar;

    bool onetime = true;

    WaveSystem ws;

    SpriteRenderer sr;

    public bool canBeDamaged = false;

    PlayerScore ps;

    float fadeColor = 1;

	// Use this for initialization
	void Start () {

        health += EnemyModifier.bonusHealth * 25;   //set health according to any modifiers
        ws = GameObject.Find("EnemySpawner").GetComponent<WaveSystem>();
        sr = GetComponentInChildren<SpriteRenderer>();
        ps = GameObject.Find("GameManager").GetComponent<PlayerScore>();
        hpBar.maxValue = health;
        hpBar.value = health;

	}
	
	// Update is called once per frame
	void Update () {

        if (health <= 0 && onetime)
        {
            onetime = false;
            StartCoroutine(Death());
        }

        hpBar.value = health;
	
	}

    IEnumerator Death()     //Destroys the boss after showing effects
    {
        DisableComponents();
        Destroy(gameObject.transform.parent.gameObject, 3f);
        InvokeRepeating("Fade", 0, .1f);

        if(deathParticles != null)
        {
            
            for (int i = 0; i < 25; i++)
            {
                float posx = transform.position.x;
                float posy = transform.position.y;

                Vector3 rand = new Vector3(Random.Range(posx - 2f, posx + 2.1f), Random.Range(posy - 2f, posy + 2.1f), -1);
                Instantiate(deathParticles, rand, Quaternion.identity);
                yield return new WaitForSeconds(.1f);
            }
        }

        GameManager.currentMultiplier += 10;
        ps.IncreaseScore(pointsValue, GameManager.totalMultiplier);

        if (pointsText != null)
        {
            GameObject text = Instantiate(pointsText, transform.position, Quaternion.identity) as GameObject;
            text.GetComponent<CanvasScaler>().dynamicPixelsPerUnit = 1000f;
            text.GetComponent<Text>().text = (pointsValue * GameManager.totalMultiplier).ToString();
            text.GetComponent<Text>().color = Color.white;

        }


    }

    void Fade()
    {
        fadeColor -= .033f;

        sr.color = Color.red;
        sr.color = new Color(sr.color.a, sr.color.b, sr.color.g, fadeColor);
    
    }

    public void OnFightStart()
    {
        hpBar.gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        StopAllCoroutines();
        ws.enemiesLeft--;

        if(powerUp != null)
        {
            Instantiate(powerUp, transform.position, Quaternion.identity);
        }
    }

    public IEnumerator Flash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(.1f);
        sr.color = Color.white;
    }

    void OnTriggerEnter(Collider other)
    {

        if ((other.tag == "playerBullet" || other.tag == "Player" || other.tag == "Rocket") && canBeDamaged)
        {

            if (other.tag == "Rocket")
            {
                health -= 4;
            }
            else if (other.tag == "playerBullet")
            {

                health -= other.GetComponent<PlayerBulletProperties>().damage;
                Destroy(other.gameObject);
            }
            else
            {
                health--;
               
            }

            StartCoroutine(Flash());
        }
    }

    void DisableComponents()
    {
        GetComponent<Boss1>().StopAllCoroutines();
        GetComponent<Boss1>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }

}
