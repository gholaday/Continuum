using UnityEngine;
using System.Collections;
using Parse;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class LeaderBoardDisplay : MonoBehaviour {

    public Text user;
    public Text score;
    public Text loadText;

    ParseQuery<ParseObject> query;
    
    public IEnumerable<ParseObject> results;

    bool toDisplay = true;
    bool loading = true;
    

	// Use this for initialization
	void Start () {

        
        Time.timeScale = 1;
        Invoke("GenerateData", 1f);
 
	}
	
	// Update is called once per frame
	void Update () {

        
        if (toDisplay)
        {
            StartCoroutine(Display());
        }
      

        if(!loading)
        {
            loadText.enabled = false;
        }

        
	
	}

    
    IEnumerator Display()
    {
        int count = 1;

        toDisplay = false;
        
        yield return new WaitForSeconds(3f);

        loading = false;

        foreach (ParseObject obj in results)
        {
            if(count < 10)
            {
                user.text += " " + count + ". " + obj.Get<string>("playerName") + "\n";
            }
            else
            {
                user.text += count + ". " + obj.Get<string>("playerName") + "\n"; 
            }

            score.text += obj.Get<int>("score") + "\n";
            count++;
        }

    }

    void GenerateData()
    {
        Debug.Log("generating data...");

        query = ParseObject.GetQuery("GameScore").OrderByDescending("score").Limit(25);

        query.FindAsync().ContinueWith(t =>
        {
            Debug.Log("run");
            results = t.Result;
        });

        

    }
}
