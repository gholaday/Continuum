using UnityEngine;
using System.Collections;

public class enemyBulletMovement : MonoBehaviour {
	
	public float speed = 1.0f;
    public float destroyTime = 8f;
	
	// Use this for initialization
	void Start () {

        Destroy(gameObject, destroyTime);
	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position += transform.up * speed * Time.deltaTime * playerMovement.timeStop;
		
		
		
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" || col.tag == "Shield")
        {
            Destroy(gameObject);
        }
    }
	
	
	void OnBecameInvisible () {
		Destroy(gameObject);
	}
}