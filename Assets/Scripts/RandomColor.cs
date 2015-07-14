using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour {
	
	
	SpriteRenderer sr;
	Color color1;
	Color color2;
	
	float lerpCounter = 0f;
	
	// Use this for initialization
	void Start () {
	
		sr = GetComponent<SpriteRenderer>();
		
		ChangeTwoColors();
	
	}
	
	void Update()
	{
		sr.color = Color.Lerp(color1, color2, lerpCounter);
		lerpCounter += .01f;
		
		if(lerpCounter > 2f)
		{
			ChangeTwoColors();
		}
		
	}
	
	void ChangeTwoColors()
	{
		lerpCounter = 0;
		color1 = new Color(Random.Range(0.0f, 1.01f), Random.Range(0.0f, 1.01f),Random.Range(0.0f, 1.01f));
		color2 = new Color(Random.Range(0.0f, 1.01f), Random.Range(0.0f, 1.01f),Random.Range(0.0f, 1.01f));
	}
	
}
