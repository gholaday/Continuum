using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameStats : MonoBehaviour {

	public Animator GOAnim;
	public Animator cameraAnim;
	public Text displayText;
	public GameObject hud;
	
	public GameObject inputField;
	
	public Image fadePanel;

	PlayerScore ps;



	// Use this for initialization
	void Start () {

		StartCoroutine("DisplayStats");
		ps = GetComponent<PlayerScore>();
		cameraAnim.SetTrigger("Click");
		
	
	}
	
	// Update is called once per frame
	void Update () {

		displayText.text = "Final Score: " + ps.GetScore();
		fadePanel.CrossFadeAlpha(255f,3f,true);
	
	}

	IEnumerator DisplayStats()
	{
		//yield return new WaitForSeconds(2.0f);
//		GOAnim.SetTrigger("Click");
		hud.SetActive(false);
		yield return new WaitForSeconds(1.0f);
		displayText.enabled = true;
		
		if(GetComponent<GameManager>().newHighScore)
		{
			inputField.SetActive(true);
		}
		
	}
}
