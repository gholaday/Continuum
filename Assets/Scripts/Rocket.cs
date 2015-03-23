using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

	private Transform target = null;
	public GameObject[] enemies;
	
	public float MissileSpeed;
	private float turn = 2.5f;
	private float lastTurn=0f;
	
	private Rigidbody rocketRigidbody;
	private float timer = 0;

	TrailRenderer tr;

	public Color color1;
	public Color color2;


	void Awake(){
		rocketRigidbody = GetComponent<Rigidbody>();
		tr = GetComponent<TrailRenderer>();

	}
	
	void Start(){
		Invoke ("Explode", 5f);
	}
	
	void FixedUpdate(){

		tr.material.color = Color.Lerp(color1,color2,timer);



		if(timer < 1)
			timer += .01f;

		if(timer > 1)
			timer = 0;

		enemies = GameObject.FindGameObjectsWithTag("Enemy");

		if(enemies.Length > 0 && target == null && timer > .1)
			target = enemies[Random.Range(0,enemies.Length-1)].transform;

		rocketRigidbody.velocity = transform.up * MissileSpeed;

		if(target != null)
		{
			Quaternion newRotation = Quaternion.LookRotation(transform.position - target.position, Vector3.forward);
			newRotation.x = 0.0f;
			newRotation.y = 0.0f;
			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turn);
			if(turn<40f){
				lastTurn+=Time.deltaTime*Time.deltaTime*5f;
				turn+=lastTurn;
		}

		

		}


	}
	
	private void Explode(){
		CancelInvoke("Explode");
		Destroy(gameObject);
	}
	
	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Enemy")){
			Explode ();
		}
	}

	
}
