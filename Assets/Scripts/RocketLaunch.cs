using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RocketLaunch : MonoBehaviour {

	public int rockets = 10;
	public float delay = .1f;

	public GameObject rocketPrefab;

	Vector3 startPosition;

	public static float rocketBar = 0;

	public float barFillUpSpeed = 5f;


	// Use this for initialization
	void Start () {

		startPosition = this.transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

		rocketBar += Time.deltaTime * barFillUpSpeed;

		if(rocketBar >= 100)
		{
			rocketBar = 100;
		}
			

		if(Input.GetKeyDown(KeyCode.Tab) && rocketBar >= 100)
		{
			rocketBar = 0;
			StartCoroutine("LaunchRockets");

		}
	
	}

	private IEnumerator LaunchRockets(){
		for(int i=0;i < rockets;i++){
			float rotation = 0f;

			if(i%2==0){
				rotation=Random.Range(-140f, -45f);
			}else{
				rotation=Random.Range(45f, 140f);
			}
			startPosition = this.transform.position;
			Instantiate(rocketPrefab, startPosition, Quaternion.Euler(0f, 0f, rotation));
			yield return new WaitForSeconds(delay);
		}
	}


}
