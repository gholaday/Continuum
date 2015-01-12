using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

	public float fallSpeed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 pos = transform.position;

		pos.y -= fallSpeed * Time.deltaTime;

		transform.position = pos;


	}


	void OnTriggerEnter(Collider other)
	{

		Destroy(gameObject);

	}



}



