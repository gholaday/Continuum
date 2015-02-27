using UnityEngine;
using System.Collections;

public class FadeUpAway : MonoBehaviour {
	
	Color imageAlpha;
	public Vector3 direction;
	public float lifetime = 1.1f;


	// Use this for initialization
	void Start () {
	
		imageAlpha = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
	

		imageAlpha.a -= Time.deltaTime;

		renderer.material.color = imageAlpha;

		transform.position += direction * Time.deltaTime;

		Destroy(gameObject,lifetime);

	}
}
