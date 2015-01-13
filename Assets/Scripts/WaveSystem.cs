using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour {

	[SerializeField] int waveNumber = 1;

	public int enemiesLeft = 0;

	public Text waveDisplay;

	public float spawnTimer = 1.0f;
	public Transform spawnSpot;
	public int posy = 20;
	
	public EnemiesArray[] enemyWaves;

	float screenRatio;
	float widthOrtho;

	float otherSpawnTimer;

	[SerializeField] int enemiesYetToSpawn;


	[System.Serializable]
	public class EnemiesArray
	{
		public GameObject[] enemies;
	}



	// Use this for initialization
	void Start () {
	
		enemiesLeft = 10;
		enemiesYetToSpawn = enemiesLeft;
		waveDisplay.text = "Wave " + 1;
		StartCoroutine("WaveStart");
		otherSpawnTimer = spawnTimer;
	}
	
	// Update is called once per frame
	void Update () {

		if(spawnTimer <= .3f)
			spawnTimer = .3f;


		if(waveNumber > 9)
			waveNumber = 1;

		screenRatio = (float)Screen.width / (float)Screen.height;
		widthOrtho = Camera.main.orthographicSize * screenRatio;


		if(enemiesLeft <= 0)
		{
			waveNumber++;
			enemiesLeft = waveNumber * 10;

			if(waveNumber > 4){
				enemiesLeft = waveNumber * 10 + Random.Range(10,30);
			}

			enemiesYetToSpawn = enemiesLeft;
			StartCoroutine("WaveStart");
		}

	}

	IEnumerator WaveStart()
	{
		waveDisplay.text = "Wave " + waveNumber;
		waveDisplay.enabled = true;
		yield return new WaitForSeconds(3.5f);
		waveDisplay.enabled = false;
		StartCoroutine("WaveSpawn",waveNumber);
		WaveModifier(waveNumber);

	}

	IEnumerator WaveSpawn(int wave)
	{
		if(wave == 5)
		{
			//Debug.Log ("rock");
			StartRockWave();
			StopCoroutine("WaveSpawn");
		}
			

		while(enemiesYetToSpawn > 0)
		{
			yield return new WaitForSeconds(spawnTimer);
			spawnSpot.position = new Vector3(Random.Range(-widthOrtho + 0.5f , widthOrtho - 0.5f),posy,0);
			GameObject go = enemyWaves[wave-1].enemies[Random.Range(0,9)];
			Instantiate(go,spawnSpot.transform.position,Quaternion.identity);
			enemiesYetToSpawn--;
			//Debug.Log ("not rock");


		}

	}

	//need to eventually add text to tell player which modifier is happening
	void WaveModifier(int wave)
	{
		switch(wave)
		{
		case 1:

			break;
		case 2:

			break;
		case 3:

			break;
		case 4: 

			break;
		case 5:
			spawnTimer = 0f;
			break;
		case 6:

			break;
		case 7:
			spawnTimer = otherSpawnTimer;
			spawnTimer -= 0.1f;
			otherSpawnTimer = spawnTimer;
			break;
		case 8:

			break;

		case 9:

			break;

		case 10:
			//Spawn Boss
			break;

		}
	}

	void StartRockWave()
	{
		//Need to put in logic here. Will spawn a gameobject prefab that
		//contains the rock pattern
	}

}
