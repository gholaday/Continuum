﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class enemyDeath : MonoBehaviour {

	public GameObject death = null;
	public int health = 1;
	public Transform[] powerups;
	public float pointsValue = 100;
	public GameObject pointsText = null;
	
	int powerUpSpawn;

	float multiplier;

	WaveSystem ws;
	SpriteRenderer sr;
	PlayerScore ps;

	bool oneTime = true;

	Color color;
    Enemy enemy;

	void Start()
	{
		powerUpSpawn = Random.Range(0,100);
		ws = GameObject.Find("EnemySpawner").GetComponent<WaveSystem>();
		sr = GetComponentInChildren<SpriteRenderer>();
		ps = GameObject.Find("GameManager").GetComponent<PlayerScore>();
		color = new Color(255,255,255,255);
        enemy = GetComponent<Enemy>();
        enemy.health += health;

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

				if(powerUpSpawn >= 89)
				{
					if(powerups.Length > 0)
					Instantiate(powerups[0], transform.position, Quaternion.Euler(0,0,0));
					
				}
				else if(powerUpSpawn >= 0 && powerUpSpawn <= 2 && powerups.Length > 1)
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




