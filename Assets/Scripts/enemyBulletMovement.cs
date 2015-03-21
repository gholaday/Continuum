using UnityEngine;
using System.Collections;

public class enemyBulletMovement : MonoBehaviour {
	
	public float speed = 1.0f;
	
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position += transform.up * speed * Time.deltaTime * playerMovement.timeStop;
		
		
		
	}
	
	
	void OnBecameInvisible () {
		Destroy(gameObject);
	}
}