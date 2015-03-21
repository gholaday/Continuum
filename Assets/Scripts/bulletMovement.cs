using UnityEngine;
using System.Collections;

public class bulletMovement : MonoBehaviour {
	
	public float speed = 1.0f;
	public GameObject sparks;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position += transform.up * speed * Time.deltaTime * playerMovement.timeStop;
	
		
	}


	void OnBecomeInvisible () {
		Destroy(transform.parent.gameObject);
	}

	void OnTriggerEnter (Collider other) {

		//Vector3 pos;
		//pos = transform.position + new Vector3(0,0,5);
		Instantiate(sparks,transform.position,Quaternion.Euler(-90,0,0) );
		Destroy(gameObject);

	}



}