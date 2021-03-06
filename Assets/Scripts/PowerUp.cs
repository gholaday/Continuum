﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

	public float fallSpeed = 5.0f;
    public float spawnChance;

    AudioSource aSource;

	// Use this for initialization
	void Start () {
        aSource = GetComponent<AudioSource>();
        aSource.pitch += Random.Range(-.4f, .41f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 pos = transform.position;

		pos.y -= fallSpeed * Time.deltaTime * playerMovement.timeStop;

		transform.position = pos;


	}


	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
        {
            aSource.Play();
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
        
       	transform.GetChild(0).gameObject.SetActive(false);
        
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }



}



