using UnityEngine;
using System.Collections;

public class enemyDeath : MonoBehaviour {

	public GameObject death = null;
	public int health = 1;
	//public int powerupChance = 15;
	public Transform[] powerups;
	public float pointsValue = 100;
	//public AudioClip clip;



	int powerUpSpawn;

	float multiplier;

	WaveSystem ws;
	SpriteRenderer sr;

	bool oneTime = true;

	void Start()
	{
		powerUpSpawn = Random.Range(0,100);
		ws = GameObject.Find("EnemySpawner").GetComponent<WaveSystem>();
		sr = GetComponentInChildren<SpriteRenderer>();


	}
	

	void OnTriggerEnter(Collider other) {

		if(other.tag == "playerBullet" || other.tag == "Player")
		{
			health--;
			if(other.tag == "playerbullet")
				Destroy (other.gameObject);

			if(gameObject.tag != "Meteor")
			StartCoroutine("Flash");

			if(health <= 0 && oneTime)
			{
				oneTime = false;
				//AudioSource.PlayClipAtPoint(clip,transform.position);
				GameManager.currentMultiplier++;
				GameManager.score += pointsValue * GameManager.totalMultiplier;
				ws.enemiesLeft--;

				Destroy(gameObject);
				if(death != null)
			
				{
					Instantiate(death,transform.position,death.transform.rotation);		
				}
				if(powerUpSpawn >= 75)
				{
					Instantiate(powerups[0], transform.position, Quaternion.Euler(0,0,0));
					
				}
				else if(powerUpSpawn >= 0 && powerUpSpawn <= 5 && powerups.Length > 1)
				{
					Instantiate(powerups[1], transform.position, Quaternion.Euler(0,0,0));
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




