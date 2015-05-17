using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class playerMovement : MonoBehaviour {

	public bool canStop = false;
	public float moveSpeed = 5.0f;
	public float slowmoTime = 0;
	public float slowmoMoveSpeed = 12.0f;

	public static int timeStop = 1;

    public GameObject particles;
    public bool slowMoEffects = true;

    VignetteAndChromaticAberration vig;


	float shipBoundaryRadius = 0.5f;
	bool spawn = false;

	bool mouseControl = false;

	//public 

	void Start () {

		timeStop = 1;
		spawn = true;
		Cursor.visible = false;
        vig = GameObject.Find("Main Camera").GetComponent<VignetteAndChromaticAberration>();


	}

    void OnDestroy()
    {
        vig.chromaticAberration = 0;
    }

	// Update is called once per frame
	void Update () {

		//Our basic up/down/left/right movement
		Vector3 pos = transform.position;

		/*if(Input.GetKeyDown(KeyCode.Keypad1))
			mouseControl = true;
        */

		if(mouseControl)
		{
			pos.y += Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;
			pos.x += Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;

		}
		else{

            pos.y += Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            pos.x += Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		}

		//Slow mo

		if(slowmoTime >= 100)
		{
			slowmoTime = 100f;
		}

        

		if((Input.GetButton("Jump") || Input.GetAxis("Jump") > 0) && spawn == true && slowmoTime > 0 && !GameManager.isPaused)
		{
            if(Input.GetButtonDown("Jump") && particles != null)
            {
                GameObject go = Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
                go.transform.SetParent(gameObject.transform);
            }

            if(slowMoEffects && vig.chromaticAberration < 48.0f)
            {
                vig.chromaticAberration += Time.deltaTime * 10;
            }

			Time.timeScale = 0.25f;
			slowmoTime -= 80.0f * Time.deltaTime;
			moveSpeed = slowmoMoveSpeed;
	
		}
		else 
		{
			slowmoTime += 10.0f * Time.deltaTime;
		}

        if ((Input.GetButtonUp("Jump") || Input.GetAxis("Jump") == 0) && spawn == true || slowmoTime <= 0)
		{
            vig.chromaticAberration = 0;
			
			moveSpeed = 8.0f;

            if (slowMoEffects)
            {
                vig.chromaticAberration = 0;
            }

            if(!GameManager.isPaused)
            {
                Time.timeScale = 1.0f;
            }
		}

        GameManager.slowMo = slowmoTime;

        
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