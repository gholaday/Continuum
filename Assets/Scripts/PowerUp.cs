using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

	public float fallSpeed = 5.0f;
    public float spawnChance;
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
        {
            Instantiate(powerUpText, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            GetComponent<AudioSource>().Play();
            StartCoroutine(DeleteObject());
        }
        else
        {
            Destroy(gameObject);
        }

        
		

	}

    IEnumerator DeleteObject()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }



}



