using UnityEngine;
using System.Collections;

public class MeteorMovement : MonoBehaviour {

	public float fallSpeed = 3.0f;
	public float rotSpeed = 2.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//Vector3 pos = transform.position;

		transform.position -= new Vector3(0,1,0)* fallSpeed * Time.deltaTime;

		//transform.position = pos;

		transform.Rotate(new Vector3(0,0,1), rotSpeed);

	}
}
