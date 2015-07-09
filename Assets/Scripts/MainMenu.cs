using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	
	public Button[] buttons;
	public Scrollbar defaultScroll;
	
	
	public float timeToAcceptInput = 4f;
	
	public Animator splashAnimator;
	
	public Image loadPanel;
	
	public GameObject mainUI;
	public GameObject optionsUI;
	
	bool splashPlaying = true;
	
	bool displayOptions = false;
	



	// Use this for initialization
	void Start () {
		
		Invoke("AcceptInput", timeToAcceptInput);
		InitializePriorScene.priorScene = "MainMenu";
		
	
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
		loadPanel.enabled = true;
		loadPanel.CrossFadeAlpha(255,2f,true);
		Invoke ("ChangeLevel", 2);
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
