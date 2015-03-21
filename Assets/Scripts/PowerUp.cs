using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

	public float fallSpeed = 5.0f;
	public GameObject powerUpText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 pos = transform.position;

		pos.y -= fallSpeed * Time.deltaTime * playerMovement.timeStop;

		transform.position = pos;


	}


	void OnTriggerEnter(Collider other)
	{
		if(powerUpText != null && other.tag == "Player")
			Instantiate(powerUpText, transform.position + new Vector3(0,1,0), Quaternion.identity);

		Destroy(gameObject);

	}



}



