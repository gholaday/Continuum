using UnityEngine;
using System.Collections;

public class bulletMovement : MonoBehaviour {
	
	public float speed = 1.0f;
	public GameObject sparks;

    Vector3 dir;
	
	// Use this for initialization
	void Start () {

        dir = transform.up;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position += dir * speed * Time.deltaTime * playerMovement.timeStop;
	
		
	}


	void OnBecomeInvisible () {
		Destroy(transform.parent.gameObject);
	}

	void OnTriggerEnter (Collider other) {

		
         Instantiate(sparks, transform.position, Quaternion.Euler(-90, 0, 0));
         Destroy(gameObject);
        
       
		

	}



}