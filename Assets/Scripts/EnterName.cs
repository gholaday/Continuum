using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterName : MonoBehaviour {

    public InputField ifield;
    public Text errorMsg;
   
    public Text enterYourName;

    public string userName;
    
    
    bool readytochange = false;
    
    GameObject gameManager;



	// Use this for initialization
	void Start () {

        if(PlayerPrefs.GetString("UserName") != null)
        {
            ifield.text = PlayerPrefs.GetString("UserName");
        }
        
        ifield.Select();
        ifield.ActivateInputField();
        
        gameManager = GameObject.Find("GameManager");
	
	}
	
	// Update is called once per frame
	void Update () {

     	if(Input.GetKey(KeyCode.Return))
     	{
     		
			SaveName();
			
     		if(readytochange)
     		{
     			gameManager.GetComponent<GameManager>().SetLeaderboardScore();
     			Application.LoadLevel(Application.loadedLevel);
     		}
     		
			
     	}
	
	}

    public void SaveName()
    {
        
        if (ifield.text.Length > 0)
        {
            userName = ifield.text;
            PlayerPrefs.SetString("UserName", userName);
            
            readytochange = true;
            
        }
        else
        {
            readytochange = false;
            StopAllCoroutines();
            
            //StartCoroutine(ShowErrorMessage());
               
        }
        
    }

    IEnumerator ShowErrorMessage()
    {
        errorMsg.CrossFadeAlpha(255f, 2f, true);
        yield return new WaitForSeconds(4.0f);
        errorMsg.CrossFadeAlpha(1f, 2f, true);

    }

   

    
}
