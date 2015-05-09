using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour, AudioProcessor.AudioCallbacks {

	private Transform target = null;
	public GameObject[] enemies;
	public float lifetime = 5f;
	
	public float MissileSpeed;
	private float turn = 2.5f;
	private float lastTurn = 0f;
	
	private Rigidbody rocketRigidbody;
	private float timer = 0;

	TrailRenderer tr;

	public Color color1;
	public Color color2;
	public Material mat;
	float spectrumColor;


	void Awake(){

		rocketRigidbody = GetComponent<Rigidbody>();
		tr = GetComponent<TrailRenderer>();

	}
	
	void Start(){
		Invoke ("Explode", lifetime);
		tr.material.shader = Shader.Find("Unlit/Color");
		AudioProcessor processor = FindObjectOfType<AudioProcessor>();
		processor.addAudioCallback(this);
	}
	

	void FixedUpdate(){


		tr.material.SetColor("_Color", Color.Lerp(color1,color2,spectrumColor));
		                    

		if(timer < 1)
			timer += Time.deltaTime;

		if(timer > 1)
			timer = 0;

		enemies = GameObject.FindGameObjectsWithTag("Enemy");

		if(enemies.Length > 0 && target == null && timer > .3f)
			target = enemies[Random.Range(0,enemies.Length-1)].transform;

		rocketRigidbody.velocity = transform.up * MissileSpeed * Time.deltaTime * Time.timeScale;

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
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
		Destroy(gameObject, 3f);
	}
	
	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Enemy")){
			Explode ();
		}
	}

	public void onOnbeatDetected()
	{
		//Debug.Log("Beat!!!");
	}

	public void onSpectrum(float[] spectrum)
	{
		//The spectrum is logarithmically averaged
		//to 12 bands
		
		//Vector3 start = new Vector3(1, 0, 0);
		//Vector3 end = new Vector3(1, spectrum[5] * 20, 0);
		//Debug.DrawLine(start, end);
		spectrumColor = (spectrum[5] * 100)/10;
		//Debug.Log(spectrumColor);
	}

	
}
