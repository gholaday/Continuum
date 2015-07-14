using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	
	public Button[] buttons;
	public Scrollbar defaultScroll;
	
	
	public float timeToAcceptInput = 4f;
	
	public Animator splashAnimator;
	
	public Image loadPanel;
	
	public GameObject HtpPanel;
	
	public GameObject mainUI;
	public GameObject optionsUI;
	
	bool splashPlaying = true;
	
	bool displayOptions = false;
	
	bool displayHTP = true;
	
	bool ready = false;
	



	// Use this for initialization
	void Start () {
	
		Time.timeScale = 1f;
		
		Invoke("AcceptInput", timeToAcceptInput);
		InitializePriorScene.priorScene = "MainMenu";
		displayHTP = Options.showHTP;
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(splashPlaying)
		{
			if(Input.anyKeyDown)
			{
				CancelInvoke();
				splashAnimator.speed = 50f;
				AcceptInput();
			}
		}
	
		if(displayOptions)
		{
			optionsUI.SetActive(true);
			mainUI.SetActive(false);
			
			if(Input.GetButtonDown("Back"))
			{
				DisableOptions();
			}
		}
		
		if(HtpPanel.activeSelf)
		{
			
			if(Input.GetButtonDown("Submit") && ready)
			{
				displayHTP = false;
				StartGame();
			}
		}
		else 
		{
		
			ready = false;
			StopCoroutine("CountToStart");
		}
		
	
	}
	
	IEnumerator CountToStart()
	{
		yield return new WaitForSeconds(0.1f);
		ready = true;
	}
	
	void DisableOptions()
	{
		foreach(Button button in buttons)
		{
			
			button.interactable = true;
		}
		
		displayOptions = false;
		buttons[0].Select();
		
		optionsUI.SetActive(false);
		mainUI.SetActive(true);
		
	}
	
	
	
	
	void AcceptInput()	//function to be called at end of start anim
	{
		
		splashPlaying = false;
		
		buttons[0].Select();
		
		foreach(Button button in buttons)
		{
		
			button.interactable = true;
		}
		
	}
	
	public void StartGame()
	{
		if(displayHTP)
		{
			HtpPanel.SetActive(true);
			StartCoroutine("CountToStart");
		}
		else
		{
			loadPanel.enabled = true;
			loadPanel.CrossFadeAlpha(255,1.5f,true);
			Invoke ("ChangeLevel", 2);
		}
		
	}
	
	
	void ChangeLevel()
	{
		
		Application.LoadLevel("Endless");
		
	}
	
	
	public void StartLeaderboard()
	{
		Application.LoadLevel("LeaderBoardTest");
	}
	
	public void DisplayOptions()
	{
		foreach(Button button in buttons)
		{
			
			button.interactable = false;
		}
		
		displayOptions = true;
		
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
}
