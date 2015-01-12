using UnityEngine;
using System.Collections;

public class FadeUpAway : MonoBehaviour {
	
	Color imageAlpha;


	// Use this for initialization
	void Start () {
	
		imageAlpha = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
	

		imageAlpha.a -= Time.deltaTime;

		renderer.material.color = imageAlpha;

		transform.position += new Vector3(0,Time.deltaTime,0);

		Destroy(gameObject,1.1f);

	}
}
