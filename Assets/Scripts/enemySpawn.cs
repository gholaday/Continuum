using UnityEngine;
using System.Collections;

public class enemySpawn : MonoBehaviour {
	
	public float spawnTimer = 1.0f;
	public Transform spawnSpot;
	public int posy;

	private float timer;
	private bool canSpawn = false;

	public GameObject[] enemies;


	void Start()
	{
		timer = spawnTimer;
	}
	

	// Update is called once per frame
	void Update () {
	 	
		//calculate the "bounds" of the screen so the enemies fit
		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;

		timer -= Time.deltaTime;

		//When timer hits zero...spawn a unit
		if(timer <= 0)
		{
			timer = 0;
			canSpawn = true;
		}

		if(canSpawn == true)
		{
			spawnSpot.position = new Vector3(Random.Range(-widthOrtho + 0.5f , widthOrtho - 0.5f),posy,0);

			Instantiate(enemies[Random.Range(0,9)], spawnSpot.position, spawnSpot.transform.rotation);
			canSpawn = false;
			timer = spawnTimer;
		}


	}
}
