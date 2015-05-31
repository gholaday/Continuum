using UnityEngine;
using System.Collections;

public class Boss2AttackHandler : MonoBehaviour {

    public float timeTillStart = 4f;
    public float LaserSpeed = 10f;

    public float mainLaserWidth = 5.0f;
    public Transform mainLaserTransform;
    public Transform mainEndline;
    public LineRenderer mainLaserLine;

    public float sideLaserWidth = 2.0f;
    public Transform sideLaserTransformR;
    public Transform sideLaserTransformL;
    public Transform rightEndline;
    public Transform leftEndline;
    public LineRenderer sideLaserLineR;
    public LineRenderer sideLaserLineL;

    public GameObject mainCol;
    public GameObject leftCol;
    public GameObject rightCol;



    Animator anim;

    bool fighting = false;

    
    bool allEnabled = false;


	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        
        StartCoroutine("StartFight");

        UpdateColliders();
        

	}
	
	// Update is called once per frame
	void Update () {

    
        if(fighting)
        {
            mainLaserLine.SetPosition(0, mainLaserTransform.position);
            sideLaserLineR.SetPosition(0, sideLaserTransformR.position);
            sideLaserLineL.SetPosition(0, sideLaserTransformL.position);

            /*
            mainCurrent = Vector3.MoveTowards(mainCurrent, mainEndline.position, Time.deltaTime * LaserSpeed);
            rightCurrent = Vector3.MoveTowards(rightCurrent, rightEndline.position, Time.deltaTime * LaserSpeed);
            leftCurrent = Vector3.MoveTowards(leftCurrent, leftEndline.position, Time.deltaTime * LaserSpeed);
            */
            mainLaserLine.SetPosition(1, mainEndline.position);
            sideLaserLineR.SetPosition(1, rightEndline.position);
            sideLaserLineL.SetPosition(1, leftEndline.position);
            UpdateColliders();
        }

        if(allEnabled)
        {
            mainLaserLine.enabled = true;
            sideLaserLineL.enabled = true;
            sideLaserLineR.enabled = true;
        }

        
	
	}

    void UpdateColliders()
    {
        mainCol.SetActive(mainLaserLine.enabled);
        leftCol.SetActive(sideLaserLineL.enabled);
        rightCol.SetActive(sideLaserLineR.enabled);
        /*
        mainCol.transform.localPosition = new Vector3(0, mainCurrent.y + 5f, 0);
        rightCol.transform.localPosition = new Vector3(0, rightCurrent.y + 5f, 0);
        leftCol.transform.localPosition = new Vector3(0, leftCurrent.y + 5f, 0);
         * */
    }

    void ResetEndlines()
    {
        mainEndline.position = mainLaserTransform.position;
        rightEndline.position = sideLaserTransformR.position;
        leftEndline.position = sideLaserTransformL.position;
    }

    IEnumerator StartFight()
    {
        yield return new WaitForSeconds(timeTillStart);
        GetComponent<Boss2Death>().canBeDamaged = true;
        GetComponent<Boss2Death>().OnFightStart();
        fighting = true;
        EnableBothSideLasers();

        yield return new WaitForSeconds(2.0f);

        if (Random.value < .5)
        {
            anim.SetTrigger("RightLeft");
        }
        else
        {
            anim.SetTrigger("LeftRight");
        }
        
        

    }
    
    void Idle()
    {
        Invoke("ChangeState", 1f);
    }

    void ChangeState()
    {
        Invoke("PickState", .5f);

    }

    void PickState()
    {
        int rand = Random.Range(1, 7);


        switch (rand)       //calls the corresponding function depending on random number 
        {
            case 1: anim.SetTrigger("RightLeft");
                break;

            case 2: anim.SetTrigger("LeftRight");
                break;

            case 3: anim.SetTrigger("MainLaserFire");
                break;

            case 4: anim.SetTrigger("RotRight");
                break;

            case 5: anim.SetTrigger("RotLeft");
                break;

            case 6: Idle();
                break;

            default:
                break;
        }
    }

    void EnableMainLaser()
    {
     
        mainLaserLine.enabled = !mainLaserLine.enabled;
        
        if(mainLaserLine.enabled == false)
        {
            EnableBothSideLasers();
        }
        
    }

    public void Spin()
    {
        anim.SetTrigger("MoveToMiddle");
    }

    void EnableLeftSideLaser()
    {

        sideLaserLineL.enabled = true;
    }

    void EnableRightSideLaser()
    {

        sideLaserLineR.enabled = true;
    }

    void EnableBothSideLasers()
    {
        

        sideLaserLineR.enabled = true;
        sideLaserLineL.enabled = true;
    }

    public void DisableMainLaser()
    {
        mainLaserLine.enabled = false;
    }

    void DisableLeftSideLaser()
    {
        sideLaserLineL.enabled = false;
    }

    void DisableRightSideLaser()
    {
        sideLaserLineR.enabled = false;
    }

    public void DisableBothSideLasers()
    {
        sideLaserLineR.enabled = false; 
        sideLaserLineL.enabled = false;
    
    }

    void DisableAllLasers()
    {
        DisableBothSideLasers();
        mainLaserLine.enabled = false;
    }

    void ForceMainLaser()
    {
        DisableBothSideLasers();
        mainLaserLine.enabled = true;
    }

}
