using UnityEngine;
using System.Collections;

public class bulletMovement : MonoBehaviour {
	
	public float speed = 1.0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position += transform.up * speed * Time.deltaTime;
	
		
	}


	void OnBecomeInvisible () {
		Destroy(transform.parent.gameObject);
	}

	void OnTriggerEnter (Collider other) {

		Destroy(gameObject);

	}



}