using UnityEngine;
using System.Collections;

public class WeaponLaserBeam : MonoBehaviour {


    

 
    LineRenderer line;
    public int level = 1;
    int levelTracker;

    public LayerMask mask;
    
    public Vector3[] spawns;

    public LineRenderer[] lines;
    

	// Use this for initialization
	void Start () {

        levelTracker = level;

        Fire();
        
     
	}

    

	// Update is called once per frame
	void Update () {

        Mathf.Clamp(level, 1, 3);

        if(level != levelTracker)
        {
            StopAllCoroutines();
            levelTracker = level;
            Fire();
            
        }
            
	
	}

    void OnDisable()
    {
        StopAllCoroutines();
        foreach (LineRenderer line in lines)
            line.enabled = false;
    }

    void Fire()
    {
        foreach (LineRenderer line in lines)
            line.enabled = false;

        switch (level)
        {
            case 1: StartCoroutine(Level1Beam());
                break;
            case 2: StartCoroutine(Level2Beam());
                break;
            case 3: StartCoroutine(Level3Beam());
                break;
        }
    }

    IEnumerator Level1Beam()
    {

     line = lines[0];
     line.enabled = true;
     
     Ray ray = new Ray(transform.position, transform.up);
     RaycastHit hit; 
 
     line.SetPosition(0, ray.origin);

     if (Physics.Raycast(ray, out hit, 100, mask))
     {

         if (hit.collider.tag != "EnemyBullet" || hit.collider.tag != "Rocket")
         {
             line.SetPosition(1, hit.point);

             if(hit.collider.tag == "Enemy")
             {
                 
                 hit.transform.GetComponent<Enemy>().health -= .05f;
                 hit.transform.GetComponent<enemyDeath>().StartCoroutine("Flash");
             }
         }
         
     }
         
     else
         line.SetPosition(1, ray.GetPoint(100));
 
     yield return new WaitForSeconds(.01f);
     StartCoroutine(Level1Beam());
     
     }

    IEnumerator Level2Beam()
    {

        for (int i = 0; i < 3; i++)
        {
            line = lines[i];
            line.enabled = true;
            Ray ray = new Ray(transform.position, spawns[i]);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100, mask))
            {

                if (hit.collider.tag != "EnemyBullet" || hit.collider.tag != "Rocket")
                {
                    line.SetPosition(1, hit.point);

                    if (hit.collider.tag == "Enemy")
                    {

                        hit.transform.GetComponent<Enemy>().health -= .05f;
                        hit.transform.GetComponent<enemyDeath>().StartCoroutine("Flash");
                    }
                }

            }

            else
                line.SetPosition(1, ray.GetPoint(100));
        }

        yield return new WaitForSeconds(.001f);
        StartCoroutine(Level2Beam());

    }

    IEnumerator Level3Beam()
    {
        line.enabled = true;

        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit hit;

        line.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out hit, 100, mask))
        {

            if (hit.collider.tag != "EnemyBullet" || hit.collider.tag != "Rocket")
            {
                line.SetPosition(1, hit.point);

                if (hit.collider.tag == "Enemy")
                {

                    hit.transform.GetComponent<Enemy>().health -= .05f;
                    hit.transform.GetComponent<enemyDeath>().StartCoroutine("Flash");
                }
            }

        }

        else
            line.SetPosition(1, ray.GetPoint(100));

        yield return new WaitForSeconds(.01f);
        StartCoroutine(Level3Beam());

    }

   
}

