using UnityEngine;
using System.Collections;

public class PauseEffect : MonoBehaviour {

	bool isPaused = false;
	Vector3 initialPos;
	public GameObject hud;
	public Vector3 endPos = new Vector3(0,0,700);
	public float lerpTime = 2.0f;

	// Use this for initialization
	void Start () {

		initialPos = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown("Cancel"))
		{
			isPaused = !isPaused;
		}
		
		if(isPaused)
		{
			StartCoroutine("Pause");

		}else{
			//StartCoroutine("UnPause");
		}
	
	}

	IEnumerator Pause()
	{
		hud.SetActive(false);
		//Time.timeScale = 0.0f;
		GameObject.Find("GameManager").GetComponent<AudioSource>().pitch = 0;
		yield return new WaitForSeconds(.5f);
		transform.position = Vector3.Lerp(initialPos, endPos, lerpTime);
	}

	IEnumerator UnPause()
	{
		hud.SetActive(false);
		transform.position = Vector3.Lerp(initialPos, endPos, lerpTime);
		yield return new WaitForSeconds(.5f);
		Time.timeScale = 0.0f;


	}
}
