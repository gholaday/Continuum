using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	
	public Button[] buttons;
	
	
	public float timeToAcceptInput = 4f;
	
	public Animator splashAnimator;
	
	public Image loadPanel;
	
	bool splashPlaying = true;
	



	// Use this for initialization
	void Start () {
		
		Invoke("AcceptInput", timeToAcceptInput);
		
	
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
		
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
}
