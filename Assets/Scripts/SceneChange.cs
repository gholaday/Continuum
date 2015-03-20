using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {

	public string sceneName;
	public bool clickToChange = true;

	// Use this for initialization
	void Start () {
	
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(clickToChange && Input.GetButton("Fire1"))
		{
			Application.LoadLevel(sceneName);
		}


	}
}
