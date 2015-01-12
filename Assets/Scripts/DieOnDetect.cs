using UnityEngine;
using System.Collections;

public class DieOnDetect : MonoBehaviour {


	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other){


		if(other.tag == "OOB")
		Destroy(gameObject);

	}
}