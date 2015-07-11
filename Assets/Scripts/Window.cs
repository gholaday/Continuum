using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour {

	public string closeButton;
	
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetButtonDown(closeButton))
		{
			gameObject.SetActive(false);
		}
	
	}
}
