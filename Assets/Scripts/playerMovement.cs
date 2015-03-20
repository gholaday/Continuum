using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour {

	public float moveSpeed = 5.0f;
	public float slowmoTime = 0;
	public float slowmoMoveSpeed = 12.0f;


	float shipBoundaryRadius = 0.5f;
	bool spawn = false;

	//public 

	void Start () {

		spawn = true;
		Cursor.visible = false;


	}
	
	// Update is called once per frame
	void Update () {

		GameManager.slowMo = slowmoTime;

		//Our basic up/down/left/right movement
		Vector3 pos = transform.position;

		pos.y += Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		//pos.y += Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;

		pos.x += Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		//pos.x += Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;


		//Slow mo

		if(slowmoTime >= 100)
		{
			slowmoTime = 100f;
		}


		if(Input.GetButton("Jump") && spawn == true && slowmoTime > 0)
		{

			Time.timeScale = 0.25f;
			slowmoTime -= 80.0f * Time.deltaTime;
			moveSpeed = slowmoMoveSpeed;
	
		}
		else 
		{
			slowmoTime += 15.0f * Time.deltaTime;
		}

		if(Input.GetButtonUp("Jump") && spawn == true || slowmoTime <= 0)
		{
			Time.timeScale = 1.0f;
			moveSpeed = 8.0f;
		}





		//RESTRICT player movement to the camera bounds

		//The y axis is simple enough...
		if(pos.y + shipBoundaryRadius > Camera.main.orthographicSize)
		{
			pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
		}

		if(pos.y - shipBoundaryRadius < -Camera.main.orthographicSize)
		{
			pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
		}

		//For the x axis, we have to find the ratio and multiply it by the screen size
		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;

		//Restrict similar to above
		if(pos.x + shipBoundaryRadius > widthOrtho)
		{
			pos.x = widthOrtho - shipBoundaryRadius;
		}

		if(pos.x - shipBoundaryRadius < -widthOrtho)
		{
			pos.x = -widthOrtho + shipBoundaryRadius;
		}

		//Apply all changes to objects position
		transform.position = pos;

	}




}