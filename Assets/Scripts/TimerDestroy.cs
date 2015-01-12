using UnityEngine;
using System.Collections;

public class TimerDestroy : MonoBehaviour {


	public float deathTimer = 5.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		deathTimer -= Time.deltaTime;

		if(deathTimer <= 0)
			Destroy(gameObject);

	}
}
