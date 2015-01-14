using UnityEngine;
using System.Collections;

public class AdjustToScreenRatio : MonoBehaviour {

	private float screenRatio;
	private float widthOrtho;

	private Vector3 pos;

	public bool right = true;
	public bool left = false;
	

	// Update is called once per frame
	void Update () {

		screenRatio = (float)Screen.width / (float)Screen.height;
		widthOrtho = Camera.main.orthographicSize * screenRatio;


		if(right)
			AdjustRight();

		if(left)
			AdjustLeft();

		transform.position = pos;
	
	}

	void AdjustRight()
	{
		pos = new Vector3(widthOrtho,0);
	}

	void AdjustLeft()
	{
		pos = new Vector3(-widthOrtho,0);
	}
}
