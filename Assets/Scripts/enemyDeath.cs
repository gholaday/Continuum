using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class enemyDeath : MonoBehaviour {

	public GameObject death = null;
    public int health = 1;
	public float pointsValue = 100;
	public GameObject pointsText = null;

	float multiplier;

	WaveSystem ws;
	SpriteRenderer sr;
	PlayerScore ps;

	bool oneTime = true;

	Color color;
    Enemy enemy;

    [SerializeField]
    Object[] powerups;
    GameObject powerupToSpawn;

    

    void Awake()
    {
        powerups = Resources.LoadAll("PowerUps") as Object[];
    }

	void Start()
	{
		ws = GameObject.Find("EnemySpawner").GetComponent<WaveSystem>();
		sr = GetComponentInChildren<SpriteRenderer>();
		ps = GameObject.Find("GameManager").GetComponent<PlayerScore>();
		color = new Color(255,255,255,255);
        enemy = GetComponent<Enemy>();
        enemy.health += health;

       
        
        powerupToSpawn = powerups[Random.Range(0, powerups.Length)] as GameObject;
        
	}

  
	

	void OnTriggerEnter(Collider other) {

		if(other.tag == "playerBullet" || other.tag == "Player" || other.tag == "Rocket")
		{
			if(other.tag == "Rocket")
			{
				enemy.health -= 2;
			}
			else
			{
				enemy.health--;
			}
				

			if(other.tag == "playerbullet")
				Destroy (other.gameObject);

			if(gameObject.tag != "Meteor")
				StartCoroutine("Flash");

			if(enemy.health <= 0 && oneTime)
			{
				RocketLaunch.rocketBar += 5;
				oneTime = false;
				//AudioSource.PlayClipAtPoint(clip,transform.position);
				GameManager.currentMultiplier++;
				//GameManager.score += pointsValue * GameManager.totalMultiplier;
				ps.IncreaseScore(pointsValue,GameManager.totalMultiplier);
				GameManager.extraLifeScoreCounter += pointsValue * GameManager.totalMultiplier;
				ws.enemiesLeft--;

				Destroy(gameObject);
				if(death != null)
				{
					Instantiate(death,transform.position,death.transform.rotation);		
				}

				if(pointsText != null)
				{
					GameObject text = Instantiate(pointsText, transform.position, Quaternion.identity) as GameObject;
					text.GetComponent<Text>().text = (pointsValue * GameManager.totalMultiplier).ToString();
					text.GetComponent<Text>().color = color;


				}

                if(Random.value <= powerupToSpawn.GetComponent<PowerUp>().spawnChance)
                {
                    Instantiate(powerupToSpawn, transform.position, Quaternion.identity);
                }

				
			}


		}
		else if(other.tag == "OOB" && oneTime)
		{
			oneTime = false;
			ws.enemiesLeft--;
			Destroy(gameObject);
		}
	}

	IEnumerator Flash()
	{
		sr.color = Color.red;
		yield return new WaitForSeconds(.1f);
		sr.color = Color.white;
	}
}




