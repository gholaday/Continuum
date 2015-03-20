using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//public AudioClip recordScratch;

	public Text lives;
	public Text scoreDisplay;
	public Text deadText;
	public Text highScoreDisplay;
	public Text ready;
	public Text multiplierDisplay;
	public Text newHighScoreDisplay;
	public Text extraLifeScore;

	public Slider slowTimeBar;

	public string[] deadFlavorText;
	
	public GameObject gameOverUI;
	public GameObject pauseUI;

	public GameObject player;
	private GameObject ship;


	//public static float score;
	public float highScore;
	public float scoreToExtraLife;
	public static float extraLifeScoreCounter;
	public int playerLives = 3;



	PlayerScore score;
	int displayScore = 0;

	float initialSpawn = 2.0f;
	public static float slowMo = 0;

	bool firstSpawn = true;

	private Vector2 spawnPos;

	bool isPaused;

	private Color originalColor;

	public static float currentMultiplier;
	public float slowMoMulitiplier = 1.0f;
	public static float totalMultiplier;

	int multiplierFontSize;
	bool isSpawning = false;
		
	// Use this for initialization
	void Start () {

		score = GetComponent<PlayerScore>();
		score.SetScore(0);
		extraLifeScoreCounter = 0;
		slowMo = 0;
		slowTimeBar.value = 1;
		totalMultiplier = 1.0f;
		currentMultiplier = 1.0f;
		multiplierFontSize = multiplierDisplay.fontSize;
	
		spawnPos = new Vector2(0,-4);
		highScore = PlayerPrefs.GetFloat("High Score");
		ready.enabled = true;

		originalColor = new Color(255,255,255,.5f);

	}
	
	void Update () {


		if(Time.timeSinceLevelLoad > 3.0f)
		{
			multiplierDisplay.enabled = true;
		}

		if(Time.timeScale <= .5f)
		{
			GetComponent<AudioSource>().pitch -= Time.deltaTime * 2;
		}
		else
		{
			GetComponent<AudioSource>().pitch += Time.deltaTime * 2;
		}

		if(GetComponent<AudioSource>().pitch <= .5f && playerLives > 0)
			GetComponent<AudioSource>().pitch = .5f;

		if(GetComponent<AudioSource>().pitch >= 1.0f)
			GetComponent<AudioSource>().pitch = 1.0f;

		totalMultiplier = currentMultiplier * slowMoMulitiplier;

		if(!firstSpawn)
		{
			if(slowMo > 60.0f)
			{
				multiplierDisplay.fontSize = multiplierFontSize;
				slowMoMulitiplier = 1.0f;
			}
			else if(slowMo > 25 && slowMo <= 60){
				multiplierDisplay.fontSize = multiplierFontSize + 10;
				slowMoMulitiplier = 2f;
			}
			else{
				multiplierDisplay.fontSize = multiplierFontSize + 20;
				slowMoMulitiplier = 3.0f;
			}


		}

		ChangeSlowMoColor();


		if(Input.GetButtonDown("Cancel"))
		{
			isPaused = !isPaused;
		}

		if(isPaused)
		{
			Time.timeScale = 0;
			GetComponent<AudioSource>().pitch = 0;
			pauseUI.SetActive(true);
		}else{
			pauseUI.SetActive(false);
			Time.timeScale = 1;
		}


		if(firstSpawn)
		{
			initialSpawn -= Time.deltaTime;
			slowTimeBar.value += 80.0f * Time.deltaTime;

			if(initialSpawn <= 0)
			{
				Instantiate(player, spawnPos, transform.rotation);
				firstSpawn = false;
				ready.enabled = false;
			}

		}
		else
		{
			slowTimeBar.value = slowMo;
		}

		if(slowMo >= 100)
		{
			slowMo = 100;
		}



		//Find our player ship, WARNING a bit slow may need to optimize later
		ship = GameObject.Find("playerShip(Clone)");

		//Set lives and score on GUI
		if(!firstSpawn)
		{
			lives.text = (playerLives - 1).ToString();
			CountTo(score.GetScore());
			scoreDisplay.text = "Score:" + (displayScore).ToString();
			multiplierDisplay.text = "x" + totalMultiplier.ToString();
		}


		if(score.GetScore() > highScore)
		{
			newHighScoreDisplay.enabled = true;
			highScore = score.GetScore();
		}

		highScoreDisplay.text = "High Score:" + highScore.ToString();

		//Displays game over text when lives are zero, prompt for reset
		if(playerLives <= 0)
		{
	
			GameOver();

			if(Input.GetKey(KeyCode.Return))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		//If we cant find the ship(i.e we are dead) and lives are greater than 0, spawn a new ship and deduct a live
		//Then we can also call our code to display the flavor text
		else if(ship == null && firstSpawn == false && !isSpawning)
		{
			Respawn();
		}

		if(extraLifeScoreCounter > scoreToExtraLife)
		{
			StartCoroutine("ExtraLifeOnScoreDisplay");
			playerLives++;
			extraLifeScoreCounter = 0;
		}

	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void ChangeSlowMoColor()
	{

		if(slowTimeBar.value <= 25)
		{
			slowTimeBar.image.color = Color.red;
		}
		else if(slowTimeBar.value > 25 && slowTimeBar.value <= 60)
		{
			slowTimeBar.image.color = Color.yellow;
		}
		else{
			slowTimeBar.image.color = originalColor;
		}
				
	}

	void Respawn()
	{
		playerLives -= 1;
		if(playerLives > 0)
		{
			currentMultiplier = 1.0f;
			StartCoroutine("SpawnPlayer");
			deadText.CrossFadeAlpha(255,1,false);
			deadText.text = deadFlavorText[Random.Range(0,deadFlavorText.Length)];
			deadText.CrossFadeAlpha(0,2,false);
		}
	}

	void GameOver()
	{
		lives.text = "0";
		gameOverUI.SetActive(true);

		PlayerPrefs.SetFloat("High Score", highScore);
		
		StartCoroutine("PitchSlow");

		GetComponent<EndGameStats>().enabled = true;
	
	}

	IEnumerator SpawnPlayer()
	{
		isSpawning = true;
		yield return new WaitForSeconds(1.0f);
		Instantiate(player,spawnPos,transform.rotation);
		isSpawning = false;
	}

	IEnumerator PitchSlow()
	{
		if(GetComponent<AudioSource>().pitch > 0.01f)
			GetComponent<AudioSource>().pitch -= .01f;

		yield return new WaitForSeconds(.75f);
		if(GetComponent<AudioSource>().pitch > .4f)
			StartCoroutine("PitchSlow");
	}

	IEnumerator ExtraLifeOnScoreDisplay()
	{

		extraLifeScore.enabled = true;
		yield return new WaitForSeconds(.5f);
		extraLifeScore.enabled = false;
		yield return new WaitForSeconds(.5f);
		extraLifeScore.enabled = true;
		yield return new WaitForSeconds(.5f);
		extraLifeScore.enabled = false;
		yield return new WaitForSeconds(.5f);
		extraLifeScore.enabled = true;
		yield return new WaitForSeconds(.5f);
		extraLifeScore.enabled = false;
	
	}

	void CountTo (int target) {

		if(displayScore != score.GetScore())
		{
			int start = displayScore;
			displayScore = (int)Mathf.Lerp (start, target + 9, .1f);
		}

	}

}
