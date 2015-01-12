using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float moveForwardSpeed = 0.15f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += transform.up * moveForwardSpeed * Time.deltaTime;
	
	}
}
