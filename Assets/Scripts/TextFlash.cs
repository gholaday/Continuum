using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFlash : MonoBehaviour {

	public float timeDelayMax;
	public float timeDelayMin;

	Text text;
	Color color;

	// Use this for initialization
	void Start () {

		text = GetComponent<Text>();
		StartCoroutine("Flash");
		color = new Color(255,255,255,255);
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
