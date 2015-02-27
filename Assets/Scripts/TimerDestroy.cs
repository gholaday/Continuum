using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerDestroy : MonoBehaviour {

	public bool textFade = false;
	public float deathTimer = 5.0f;

	Color color;


	// Use this for initialization
	void Start () {
	
		if(textFade)
			color = new Color(255,255,255);
	}
	
	// Update is called once per frame
	void Update () {
	
		color.a -= Time.deltaTime;
		deathTimer -= Time.deltaTime;

		if(deathTimer <= 0)
			Destroy(gameObject);

		if(textFade)
		{
			this.GetComponent<Text>().color = color;
		}

	}
}
