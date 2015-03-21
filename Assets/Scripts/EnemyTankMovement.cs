using UnityEngine;
using System.Collections;

public class EnemyTankMovement : MonoBehaviour {

	public float speed = 5.0f;

	private float ranHeight;
	private float posy;

	private float direction = 0;

	private bool start = true;

	private float chance;

	// Use this for initialization
	void Start () {

		ranHeight = Random.Range(-0.5f,3.0f);
		chance = Random.value;

		
	}
	
	// Update is called once per frame
	void Update () {

		posy = transform.position.y;
		
		//only move down untill the objects y pos is == to the random y pos generated at start
		if(posy >= ranHeight)
		{
			transform.position += new Vector3(0,-1,0)* speed * Time.deltaTime * playerMovement.timeStop;
		}
		else
		{
			speed = 1.5f; 

			transform.position += new Vector3(direction,0,0)* speed * Time.deltaTime * playerMovement.timeStop;

			if(start)
			{
				if(chance > 0.5f)
				{
					direction = -1;
				}
				else
				{
					direction = 1;
				}

			}


		}


	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "SideOOB")
		{
			start = false;

			direction *= -1;

		}
	}
}





