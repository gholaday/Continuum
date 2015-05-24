using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {

    public Transform[] bulletSpawns;
    public float fireRate = .1f;
    public GameObject bulletPrefab;
    public float timeTillStart = 3f;

    bool shooting = false;
    Animator anim;

    int numTurrets = 4;

    bool almostDead = false;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        
        StartCoroutine(Shoot());        //Begin the shooting function

        Invoke("StartFight", timeTillStart);        //Invokes the start fight function when boss is in place
        
	}
    
    IEnumerator Shoot()
    {
        //as long as shoot is true, fire bullets from each turret and call this coroutine again after a short delay
        if(shooting)
        {
            for (int i = 0; i < numTurrets; i++)
            {    
                Instantiate(bulletPrefab, bulletSpawns[i].transform.position, bulletSpawns[i].rotation);
            }
        }

        yield return new WaitForSeconds(fireRate);

        StartCoroutine(Shoot());
    
    }

    public void EnableExtraTurrets()
    {
        if(!almostDead)
        {
            numTurrets += 4;
            almostDead = true;
        }
    }

    void StartFight()
    {
        GetComponent<BossDeath>().OnFightStart();   //calls function in boss death script which helps sync things up
        anim.SetBool("Spinning", true);     //start spinning
        Invoke("StartShooting", .5f);       //begin shooting
        Invoke("StartTriangle", 5f);        //start triangle movement anim
    }

    void StartShooting()
    {
        GetComponent<BossDeath>().canBeDamaged = true;      //allow boss to be damaged in boss death script
        shooting = true;
    }

    void StartTriangle()
    {
        shooting = true;
        anim.SetTrigger("StartTriangleMove");
    }

    void Idle()
    {
        shooting = true;
        Invoke("ChangeState", 3f);
    }

    void StartFastTriangle()
    {
        shooting = true;
        anim.SetTrigger("StartFastTriangleMove");
    }
    
    IEnumerator Charge()
    {
        shooting = false;
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("Charge");
    }

    public void ChangeSpinDirection()
    {
      
        if(anim.GetBool("Spinning") == true)
        {
            if(Random.value < .5f)
            {
                anim.SetBool("Spinning", false);
                anim.SetBool("ReverseSpin", true);
            }
        }
        else if (anim.GetBool("ReverseSpin") == true)
        {
            if (Random.value < .5f)
            {
                anim.SetBool("ReverseSpin", false);
                anim.SetBool("Spinning", true);
            }
        }
    }

    void ChangeState()  //is called at the end of each animation, used to randomly pick a state for boss to enter
    {
        int rand = Random.Range(1, 5);
       
        if (Random.value < .5f)     //50% chance to change from normal to fast spin mode
        {
            anim.SetBool("Spinning", false);
            anim.SetBool("ReverseSpin", false);
            anim.SetBool("FastSpinning", true);
        }
        else
        {
            anim.SetBool("ReverseSpin", false);
            anim.SetBool("FastSpinning", false);
            anim.SetBool("Spinning", true);   
        }

        switch (rand)       //calls the corresponding function depending on random number 
        {
            case 1: Invoke("StartTriangle", 1f);
                break;

            case 2: Idle();
                break;

            case 3: if(!almostDead)
                {
                    Invoke("StartFastTriangle", 1f);
                }
                else
                {
                    ChangeState();
                }
                break;

            case 4: StartCoroutine(Charge());
                break;

            default:
                break;
        }

    }
  
}
