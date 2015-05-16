using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFlashGeneric : MonoBehaviour {

    public float timeDelayOff = 1f;
    public float timeDelayOn = 1f;

    Text text;

	// Use this for initialization
	void Start () {

        text = GetComponent<Text>();
		StartCoroutine("Flash");

	}


	IEnumerator Flash()
	{
        text.enabled = true;
        yield return new WaitForSeconds(timeDelayOn);
        text.enabled = false;
        yield return new WaitForSeconds(timeDelayOff);
        StartCoroutine(Flash());

		
	}

	
	
}
