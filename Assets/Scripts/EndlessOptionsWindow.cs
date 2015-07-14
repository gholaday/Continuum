using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndlessOptionsWindow : MonoBehaviour {
	
	public GameObject optionsPanel;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetButtonDown("Back") || Input.GetButtonDown("Cancel"))
		{
			optionsPanel.SetActive(false);
		}
	
	}
	
	public void EnableOptions()
	{
		optionsPanel.SetActive(true);
	}
}
