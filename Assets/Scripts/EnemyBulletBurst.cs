using UnityEngine;
using System.Collections;

public class EnemyBulletBurst : MonoBehaviour {

	public GameObject bullet;
	public float speed = 5.0f;
	public float rotSpeed = 2.0f;

	bool destination = false;




	// Use this for initialization
	void Start () {

		StartCoroutine("Explode");

	}
	
	// Update is called once per frame
	void Update () {


		if(destination == false)
		{
			transform.position += new Vector3(0,-speed * Time.deltaTime,0);
			transform.Rotate(new Vector3(0,0,1), rotSpeed);
		}
		else
		{
			Instantiate(bullet, transform.position, Quaternion.Euler(0,0,0));
			Instantiate(bullet, transform.position, Quaternion.Euler(0,0,90));
			Instantiate(bullet, transform.position, Quaternion.Euler(0,0,180));
			Instantiate(bullet, transform.position, Quaternion.Euler(0,0,270));
			Destroy (gameObject);
		
		}

	
	}

	IEnumerator Explode()
	{
		yield return new WaitForSeconds(Random.Range(0.6f,1.0f));
		SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
		sr.color = new Color(255,0,0,255);
		yield return new WaitForSeconds(0.3f);
		destination = true;
	}
}
