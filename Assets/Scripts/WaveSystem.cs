using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour {

	[SerializeField] int waveNumber = 1;
    int waveDisplayNumber;

	public int enemiesLeft = 0;

	public Text waveDisplay;
    public Text waveModifierDisplay;

	public float spawnTimer = 1.0f;
	public Transform spawnSpot;
	public int posy = 20;

	public int enemiesToSpawn;
	
	public EnemiesArray[] enemyWaves;

	float screenRatio;
	float widthOrtho;

	public float otherSpawnTimer;

    public int healthBonus = 0;
    public int moveSpeedBonus = 0;
    public float attackSpeedBonus = 0;
    public float spawnRateBonus = 0;

    GameObject gm;


	[SerializeField] int enemiesYetToSpawn;


	[System.Serializable]
	public class EnemiesArray
	{
		public GameObject[] enemies;
	}



	// Use this for initialization
	void Start () {
	
		enemiesToSpawn = 10;
		enemiesYetToSpawn = enemiesToSpawn;
		enemiesLeft = enemiesToSpawn;
        waveDisplayNumber = waveNumber - 1;
		waveDisplay.text = "Wave " + waveDisplayNumber;
		StartCoroutine("WaveStart");
		otherSpawnTimer = spawnTimer;
        gm = GameObject.Find("GameManager");
    
	}
	
	// Update is called once per frame
	void Update () {

		if(spawnTimer <= .3f)
			spawnTimer = .3f;


		

		screenRatio = (float)Screen.width / (float)Screen.height;
		widthOrtho = Camera.main.orthographicSize * screenRatio;


		if(enemiesLeft <= 0)
		{
            waveNumber++;

            if (waveNumber > 10)
                waveNumber = 1;

            if(waveNumber % 10 != 0)
            {
                enemiesToSpawn += Random.Range(8, 15);
            }

			enemiesLeft = enemiesToSpawn;
			enemiesYetToSpawn = enemiesToSpawn;
			StartCoroutine("WaveStart");
		}


	}

	IEnumerator WaveStart()
	{
        waveDisplayNumber++;
		waveDisplay.text = "Wave " + waveDisplayNumber;
		waveDisplay.enabled = true;
		WaveModifier(waveNumber);

        if((waveNumber) % 5 == 0)
        {
            //Debug.Log("Wave 5 or 10 -- add modifier");

            int randNum = Random.Range(0, 2);
            //Debug.Log("rand num = " + randNum);

            if(randNum == 0)
            {
                EnemyModifier.bonusHealth += 1;
                StartCoroutine(DisplayWaveModifier("Enemy Health Increased!"));
                Debug.Log("Enemy health increased");
            }
            else if(randNum == 1)
            {
                Debug.Log("Enemy Attack Speed increased by .10");
                EnemyModifier.bonusAttackSpeed += .10f;
                StartCoroutine(DisplayWaveModifier("Enemy Attack Speed Increased!"));
            }

        }

        if (waveNumber % 10 == 0)
        {
            enemiesLeft = 1;
            enemiesYetToSpawn = 1;
           // Debug.Log("10th wave");
        }

        yield return new WaitForSeconds(3.5f);

        waveDisplay.enabled = false;
        StartCoroutine(WaveSpawn(waveNumber));
	}

	IEnumerator WaveSpawn(int wave)
	{
       // Debug.Log("enemies yet to spawn = " + enemiesYetToSpawn);

		while(enemiesYetToSpawn > 0)
		{
		
			yield return new WaitForSeconds(spawnTimer);
			spawnSpot.position = new Vector3(Random.Range(-widthOrtho + 0.5f , widthOrtho - 0.5f),posy,0);
			GameObject go = enemyWaves[wave-1].enemies[Random.Range(0,9)];

			Instantiate(go,spawnSpot.transform.position,Quaternion.identity);
			enemiesYetToSpawn--;
		}


	}

	//need to eventually add text to tell player which modifier is happening
	void WaveModifier(int wave)
	{
		switch(wave)
		{
		case 1:
            StartCoroutine(TimeDelay(1.0f, "PitchSpeedDown"));
			break;
		case 2:

			break;
		case 3:

			break;
		case 4: 

			break;
		case 5:
			
			break;
		case 6:
			break;
		case 7:

			break;
		case 8:

			break;

		case 9:

			break;

		case 10:
			//Spawn Boss

            StartCoroutine(TimeDelay(1.0f, "PitchSpeedUp"));
            waveDisplay.text = "BOSS TIME";
            spawnTimer -= .005f;
           
			break;

		}
	}


    IEnumerator DisplayWaveModifier(string s)
    {
        waveModifierDisplay.CrossFadeAlpha(255, 1f, false);
        waveModifierDisplay.text = s;

        yield return new WaitForSeconds(2f);
        waveModifierDisplay.CrossFadeAlpha(0, 1f, false);

    }

    IEnumerator PitchSpeedUp()
    {
        if(gm.GetComponent<AudioSource>().pitch < 1.05f)
        {
            gm.GetComponent<AudioSource>().pitch += .0005f;
            yield return new WaitForSeconds(.005f);
            StartCoroutine("PitchSpeedUp");
        }

    }

    IEnumerator PitchSpeedDown()
    {
        if (gm.GetComponent<AudioSource>().pitch > 1f)
        {
            gm.GetComponent<AudioSource>().pitch -= .0005f;
            yield return new WaitForSeconds(.005f);
            StartCoroutine("PitchSpeedDown");
        }

    }

    IEnumerator TimeDelay(float delay, string coroutine)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(coroutine);

    }

}
