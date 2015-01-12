using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBounce : MonoBehaviour {

	Text text;
	public float length;


	// Use this for initialization
	void Start () {

		text = GetComponent<Text>();
		//originalFontSize = text.fontSize;
		StartCoroutine("Bounce");
	
	}
	
	// Update is called once per frame
	void Update () {

	

	}

	IEnumerator Bounce()
	{
		while(true)
		{
			text.fontSize = 50;
			yield return new WaitForSeconds(.1f);
			text.fontSize = 40;
			yield return new WaitForSeconds(length);

		}
	}
}



