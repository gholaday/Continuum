using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Parse;

public class GameManager : MonoBehaviour {

	//public AudioClip recordScratch;

	public Text lives;
	public Text scoreDisplay;
	public Text deadText;
	public Text highScoreDisplay;
	public Text ready;
	public Text multiplierDisplay;
    public Animator multiplierAnim;
	public Text newHighScoreDisplay;
	public Text extraLifeScore;

	public Slider slowTimeBar;
	public Slider rocketSlider;

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
    bool newHighScore = false;

	private Vector2 spawnPos;

	public static bool isPaused;

	private Color originalColor;

	public static float currentMultiplier;
	public float slowMoMulitiplier = 1.0f;
	public static float totalMultiplier;
    float multDisplay;

	int multiplierFontSize;
	bool isSpawning = false;

    bool gameOver = false;

	
		
	// Use this for initialization
	void Start () {

        InitializePriorScene.priorScene = "Endless";

		RocketLaunch.rocketBar = 0;
		score = GetComponent<PlayerScore>();
		score.SetScore(0);
		extraLifeScoreCounter = 0;
		slowMo = 100;
		slowTimeBar.value = 1;
		totalMultiplier = 1.0f;
		currentMultiplier = 1.0f;
        multDisplay = 1.0f;
		multiplierFontSize = multiplierDisplay.fontSize;
	
		spawnPos = new Vector2(0,-4);
		highScore = PlayerPrefs.GetFloat("High Score");
		ready.enabled = true;

		originalColor = new Color(255,255,255,.5f);
        isPaused = false;
        Time.timeScale = 1.0f;
        shoot.weaponName = "LaserWeapon";
        shoot.cooldown = .25f;

	}
	
	void Update () {

        AudioSource[] audioSources;

		if(Time.timeSinceLevelLoad > 3.0f)
		{
			multiplierDisplay.enabled = true;
		}

		if(Time.timeScale <= .5f)
		{
            audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach(AudioSource source in audioSources)
            {
                source.pitch -= Time.deltaTime * 2;
            }
			//GetComponent<AudioSource>().pitch -= Time.deltaTime * 2;
		}
		else
		{
            if (GetComponent<AudioSource>().pitch < 1 && playerLives > 0)
            {
                audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
                foreach (AudioSource source in audioSources)
                {
                    source.pitch = 1;
                }
               // GetComponent<AudioSource>().pitch = 1;
            }
			
		}

		if(GetComponent<AudioSource>().pitch <= .5f && playerLives > 0)
        {
            audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource source in audioSources)
            {
                source.pitch = .5f;
            }
           // GetComponent<AudioSource>().pitch = .5f;
        }
			

		//if(GetComponent<AudioSource>().pitch >= 1.0f)
			//GetComponent<AudioSource>().pitch = 1.0f;

		

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
            if (isPaused) Time.timeScale = 1;

			isPaused = !isPaused;
            
		}

		if(isPaused)
		{
			Time.timeScale = 0;
            
            GetComponent<AudioSource>().pitch = 0;
            pauseUI.SetActive(true);
		}else{
            //GetComponent<AudioSource>().pitch = 1;
			pauseUI.SetActive(false);
			
            
		}


		if(firstSpawn)
		{
			
			if(initialSpawn <= 0)
			{
				Instantiate(player, spawnPos, transform.rotation);
				firstSpawn = false;
				ready.enabled = false;
			}

            initialSpawn -= Time.deltaTime;
            slowTimeBar.value += 85.0f * Time.deltaTime;

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

            if(multDisplay != totalMultiplier && totalMultiplier > 0)
            {
                multDisplay = totalMultiplier;
                multiplierDisplay.text = "x" + multDisplay.ToString();
                multiplierAnim.Play("Bounce");
            }
			
		}

		rocketSlider.value = RocketLaunch.rocketBar;

			
		if(score.GetScore() > highScore)
		{
			if(!newHighScore)
            {
                StartCoroutine(NewHighScoreFlash());
                newHighScore = true;
                gameOver = true;
            }

			highScore = score.GetScore();
		}

		highScoreDisplay.text = "High Score:" + highScore.ToString();

		//Displays game over text when lives are zero, prompt for reset
		if(playerLives <= 0)
		{
            if(gameOver)
            {
                SetLeaderboardScore();
            }

            GameOver();

			if(Input.GetKey(KeyCode.Return))
			{
				Application.LoadLevel(Application.loadedLevel);
			}

            if (Input.GetButton("Cancel"))
            {
                Application.LoadLevel("LeaderBoardTest");
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

    public void LateUpdate()
    {
        if (!firstSpawn) totalMultiplier = currentMultiplier * slowMoMulitiplier;

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
        Time.timeScale = 1;
		playerLives -= 1;
        
		if(playerLives > 0)
		{
			
			StartCoroutine("SpawnPlayer");
			deadText.CrossFadeAlpha(255,1,false);
			deadText.text = deadFlavorText[Random.Range(0,deadFlavorText.Length)];
			deadText.CrossFadeAlpha(0,2,false);
		}
	}

	void GameOver()
	{
        gameOver = false;
        GetComponent<AudioProcessor>().enabled = false;
		lives.text = "0";
		gameOverUI.SetActive(true);

        StartCoroutine(CountDownTo(currentMultiplier));

		PlayerPrefs.SetFloat("High Score", highScore);
		
		StartCoroutine("PitchSlow");

		GetComponent<EndGameStats>().enabled = true;

        

	
	}

	IEnumerator SpawnPlayer()
	{
		isSpawning = true;
        StartCoroutine(CountDownTo(currentMultiplier));
		yield return new WaitForSeconds(1.0f);
        //currentMultiplier = 1.0f;
		Instantiate(player,spawnPos,transform.rotation);
		isSpawning = false;
	}

	IEnumerator PitchSlow()
	{
		if(GetComponent<AudioSource>().pitch > .5f)
		GetComponent<AudioSource>().pitch -= .01f;

		yield return new WaitForSeconds(1);
		if(GetComponent<AudioSource>().pitch > .5f)
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

    IEnumerator CountDownTo(float mult)
    {
        float seconds = 1 / mult;
        

        while(currentMultiplier != 1)
        {
            currentMultiplier--;
            yield return new WaitForSeconds(seconds);
        }
        
       
    }

    IEnumerator NewHighScoreFlash()
    {
        AudioSource aSource = newHighScoreDisplay.GetComponent<AudioSource>();

        for (int i = 0; i < 2; i++)
        {
            newHighScoreDisplay.enabled = !newHighScoreDisplay.enabled;
            newHighScoreDisplay.text = "NEW";

            if(i == 0)
            {
                aSource.Play();
            }

            yield return new WaitForSeconds(.25f);
        }

        for (int i = 0; i < 2; i++)
        {
            newHighScoreDisplay.enabled = !newHighScoreDisplay.enabled;
            newHighScoreDisplay.text = "HIGH";

            if (i == 0)
            {
                aSource.Play();
            }

            yield return new WaitForSeconds(.25f);
        }

        for (int i = 0; i < 2; i++)
        {
            newHighScoreDisplay.enabled = !newHighScoreDisplay.enabled;
            newHighScoreDisplay.text = "SCORE";

            if (i == 0)
            {
                aSource.Play();
            }

            yield return new WaitForSeconds(.25f);
        }


    }

    void SetLeaderboardScore()
    {
        ParseObject gameScore = new ParseObject("GameScore");
        gameScore["score"] = highScore;
        gameScore["playerName"] = PlayerPrefs.GetString("UserName");
        
        gameScore.SaveAsync();
    }

    public void ClearHighScore()
    {
        PlayerPrefs.SetInt("High Score", 0);
    }

}
