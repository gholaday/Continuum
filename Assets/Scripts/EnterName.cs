using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterName : MonoBehaviour {

    public InputField ifield;
    public Text errorMsg;
    public Text pressToStart;
    public Text enterYourName;

    public string userName;

    public GameObject sceneChange;


	// Use this for initialization
	void Start () {

        if(PlayerPrefs.GetString("UserName") != null)
        {
            ifield.text = PlayerPrefs.GetString("UserName");
        }

        if (ifield.text.Length > 0)
        {
            StartCoroutine(ReadyToPlay());
            enterYourName.gameObject.SetActive(false);
            sceneChange.SetActive(true);
        }
	
	}
	
	// Update is called once per frame
	void Update () {

     
	
	}

    public void SaveName()
    {
        
        if (ifield.text.Length > 0)
        {
            userName = ifield.text;
            PlayerPrefs.SetString("UserName", userName);
            enterYourName.gameObject.SetActive(false);
            StartCoroutine(ReadyToPlay());
            sceneChange.SetActive(true);
        }
        else
        {
            sceneChange.SetActive(false);
            StopAllCoroutines();
            pressToStart.enabled = false;
            StartCoroutine(ShowErrorMessage());
            enterYourName.gameObject.SetActive(true);      
        }
        
    }

    IEnumerator ShowErrorMessage()
    {
        errorMsg.CrossFadeAlpha(255f, 2f, true);
        yield return new WaitForSeconds(4.0f);
        errorMsg.CrossFadeAlpha(1f, 2f, true);

    }

    IEnumerator ReadyToPlay()
    {
        pressToStart.enabled = true;
        yield return new WaitForSeconds(1f);
        pressToStart.enabled = false;
        yield return new WaitForSeconds(1f);

        StartCoroutine(ReadyToPlay());
    }

    public void DisableSceneChange()
    {
        sceneChange.SetActive(false);
    }


    
}
