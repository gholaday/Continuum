using UnityEngine;
using System.Collections;

public class EnemySeekerAI : MonoBehaviour {

	public float speed = 5.0f;
	
	private float height = 5.5f;
	private float posy;
	
	private float direction = 0;
	
	private bool start = true;
	private bool foundTarget = false;
	
	private float chance;
	

	Transform go;

	
	// Use this for initialization
	void Start () {

		chance = Random.value;
	}
	
	// Update is called once per frame
	void Update () {
		
		posy = transform.position.y;
		
		//only move down untill the objects y pos is == to the random y pos generated at start
		if(posy >= height)
		{
			transform.position += new Vector3(0,-1,0)* speed * Time.deltaTime;
		}
		else if(!foundTarget)
		{
			speed = 3.0f; 


			transform.position += new Vector3(direction,0,0)* speed * Time.deltaTime;
			
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


			RaycastHit hit;

			if(Physics.Raycast(transform.position, Vector3.down, out hit, 50.0f))
			{
				if(hit.collider.tag == "Player")
				{
					if(hit.transform != null)
					{
						foundTarget = true;
						go = hit.transform;
					}

				}

			}
		}
		
		if(foundTarget)
		{
			speed = 15.0f;

			if(go.gameObject != null)
			{
				transform.position += Vector3.down * speed * Time.deltaTime;
				transform.position -= go.position / 20.0f * Time.deltaTime;
			}
			else
			{
				transform.position += Vector3.down * speed * Time.deltaTime;
			}


			/*Vector3 dir = transform.position - go.position;
			float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
			transform.localRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
			*/
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
