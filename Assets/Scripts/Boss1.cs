using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {

    Animator anim;
   
    
    public Transform[] bulletSpawns;
    public float fireRate = .1f;
    public GameObject bulletPrefab;
    public float timeTillStart = 3f;

  
    
    bool shooting = false;


	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        
        StartCoroutine(Shoot());
        Invoke("StartFight", timeTillStart);
        
	
	}
	
	// Update is called once per frame
	void Update () {

        
        
	}
    
    IEnumerator Shoot()
    {

        
        if(shooting)
        {
            for (int i = 0; i < bulletSpawns.Length; i++)
            {
                Instantiate(bulletPrefab, bulletSpawns[i].transform.position, bulletSpawns[i].rotation);
            }
        }

        yield return new WaitForSeconds(fireRate);
        StartCoroutine(Shoot());
    
    }

    void StartFight()
    {
        Debug.Log("Lets go");
        GetComponent<BossDeath>().OnFightStart();
        anim.SetBool("Spinning", true);
        Invoke("StartShooting", 1f);
        Invoke("StartTriangle", 5f);

    }

    void StartShooting()
    {
        GetComponent<BossDeath>().canBeDamaged = true;
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
        //anim.SetBool("Spinning", false);
        //anim.SetBool("FastSpinning", false);
        shooting = true;
        anim.SetTrigger("StartFastTriangleMove");
    }
    
    IEnumerator Charge()
    {
        shooting = false;
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("Charge");
    }

    void ChangeState()
    {
        int rand = Random.Range(1, 5);
        Debug.Log(rand);

        if (Random.value < .5f)
        {
            //Debug.Log("Spinning faster");
            anim.SetBool("Spinning", false);
            anim.SetBool("FastSpinning", true);
        }
        else
        {
            //Debug.Log("Spinning normal");
            anim.SetBool("Spinning", true);
            anim.SetBool("FastSpinning", false);
        }

        switch (rand)
        {
            case 1: StartTriangle();
                break;

            case 2: Idle();
                break;

            case 3: StartFastTriangle();
                break;

            case 4: StartCoroutine(Charge());
                break;


            default:
                break;
        }

        
    }

    
}
