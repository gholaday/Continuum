using UnityEngine;
using System.Collections;

public class enemyMovement : MonoBehaviour {

	public float speed = 5.0f;
	int ranHeight;
	float posy;
	int ranStop;



	// Use this for initialization
	void Start () {
		ranHeight = Random.Range(-3,5);
		ranStop = Random.Range(-25,25);


	}
	
	// Update is called once per frame
	void Update () {

		posy = transform.position.y;

	if(ranStop > 0)
	{
		if(posy <= ranHeight)
		{
			//STOP

		}
		else
		{
			transform.position += new Vector3(0,1,0)* speed * Time.deltaTime;
		}
	}
		else
			{
				transform.position += new Vector3(0,1,0)* speed * Time.deltaTime;
			}




	}
}
