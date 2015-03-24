using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFlash : MonoBehaviour {

	public float timeDelayMax;
	public float timeDelayMin;
	public bool autoDisable = false;
	public float disableTime = 0;
	public bool enable = false;

	Text text;
	Color color;
	[SerializeField] float timer = 0;

	// Use this for initialization
	void Start () {

		text = GetComponent<Text>();

		StartCoroutine("Flash");

		color = new Color(255,255,255,255);
	
	}
	
	// Update is called once per frame
	void Update () {

		//if(RocketLaunch.rocketBar >= 100)
			timer += Time.deltaTime;

		/*
		if(autoDisable && timer > disableTime)
		{
			Disable();
		}
			

		if(autoDisable && RocketLaunch.rocketBar < 100)
		{
			Disable();
		}
*/

		if(RocketLaunch.rocketBar == 100 && timer < disableTime)
		{
			text.enabled = true;
		}
		else
		{
			Disable();
		}


	}

	IEnumerator Flash()
	{

		color.a = 0;
		text.color = color;
		yield return new WaitForSeconds(timeDelayMin);
		color.a = 255;
		text.color = color;
		yield return new WaitForSeconds(timeDelayMax);
		StartCoroutine("Flash");
	}

	void Disable()
	{
		if(RocketLaunch.rocketBar < 100)
			timer = 0;

		text.enabled = false;
	}
	
}
