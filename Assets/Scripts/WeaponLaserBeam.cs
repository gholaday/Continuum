using UnityEngine;
using System.Collections;

public class WeaponLaserBeam : MonoBehaviour {

    LineRenderer line;
    public int level = 1;
    int levelTracker;
    public float damagePerTick;
    public float damageBase = .02f;

    public LayerMask mask;
    
    public Vector3[] spawns;

    public LineRenderer[] lines;

    float widthSize = .03f;
    
    public GameObject hitParticle;

	// Use this for initialization
	void Start () {

        levelTracker = level;

        Fire();
        
     
	}

    

	// Update is called once per frame
	void Update () {

        damagePerTick = damageBase / shoot.cooldown;


        if(level != levelTracker)
        {
            StopAllCoroutines();
            levelTracker = level;
            Fire();
            
        }
            
	
	}
	
	void OnEnable()
	{
		Fire ();
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
            case 4: StartCoroutine(Level4Beam());
                break;

        }
    }

    IEnumerator Level1Beam()
    {
     
     widthSize *= -1;

     line = lines[0];
     line.enabled = true;

     line.SetWidth(widthSize, widthSize);
    
     Ray ray = new Ray(transform.position, transform.up);
     RaycastHit hit; 
 
     line.SetPosition(0, ray.origin);

     if (Physics.Raycast(ray, out hit, 100, mask))
     {

         if (hit.collider.tag != "EnemyBullet" || hit.collider.tag != "Rocket")
         {
             line.SetPosition(1, hit.point);

				if (hit.collider.tag == "Enemy" || hit.collider.tag == "Meteor")
				{
					
					Instantiate(hitParticle, hit.point, Quaternion.identity);
					
					if(hit.collider.tag == "Enemy")
					{
						hit.transform.GetComponent<Enemy>().health -= damagePerTick * Time.deltaTime;
						
						if (hit.transform.GetComponent<enemyDeath>() != null)
						{
							hit.transform.GetComponent<enemyDeath>().StartCoroutine("Flash");
						}
						else if (hit.transform.GetComponent<BossDeath>() != null)
						{
							hit.transform.GetComponent<BossDeath>().StartCoroutine("Flash");
						}
						
					}
					
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

        widthSize *= -1;

        for (int i = 0; i < 3; i++)
        {

            line = lines[i];
            line.SetWidth(widthSize, widthSize);
            line.enabled = true;
            Ray ray = new Ray(transform.position, spawns[i]);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100, mask))
            {

                if (hit.collider.tag != "EnemyBullet" || hit.collider.tag != "Rocket")
                {
                    line.SetPosition(1, hit.point);

					if (hit.collider.tag == "Enemy" || hit.collider.tag == "Meteor")
					{
						
						Instantiate(hitParticle, hit.point, Quaternion.identity);
						
						if(hit.collider.tag == "Enemy")
						{
							hit.transform.GetComponent<Enemy>().health -= damagePerTick * Time.deltaTime;
							
							if (hit.transform.GetComponent<enemyDeath>() != null)
							{
								hit.transform.GetComponent<enemyDeath>().StartCoroutine("Flash");
							}
							else if (hit.transform.GetComponent<BossDeath>() != null)
							{
								hit.transform.GetComponent<BossDeath>().StartCoroutine("Flash");
							}
							
						}
						
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
        widthSize *= -1;

        for (int i = 0; i < 5; i++)
        {
            line = lines[i];
            line.SetWidth(widthSize, widthSize);
            line.enabled = true;
            Ray ray = new Ray(transform.position, spawns[i]);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100, mask))
            {

                if (hit.collider.tag != "EnemyBullet" || hit.collider.tag != "Rocket")
                {
                    line.SetPosition(1, hit.point);

					if (hit.collider.tag == "Enemy" || hit.collider.tag == "Meteor")
                    {
						
						Instantiate(hitParticle, hit.point, Quaternion.identity);
						
						if(hit.collider.tag == "Enemy")
						{
							hit.transform.GetComponent<Enemy>().health -= damagePerTick * Time.deltaTime;
							
							if (hit.transform.GetComponent<enemyDeath>() != null)
							{
								hit.transform.GetComponent<enemyDeath>().StartCoroutine("Flash");
							}
							else if (hit.transform.GetComponent<BossDeath>() != null)
							{
								hit.transform.GetComponent<BossDeath>().StartCoroutine("Flash");
							}
							
						}
                        
                    }
                }

            }

            else
                line.SetPosition(1, ray.GetPoint(100));
        }

        yield return new WaitForSeconds(.001f);
        StartCoroutine(Level3Beam());

    }

    IEnumerator Level4Beam()
    {
        widthSize *= -1;

        for (int i = 0; i < 7; i++)
        {
            line = lines[i];
            line.SetWidth(widthSize, widthSize);
            line.enabled = true;
            Ray ray = new Ray(transform.position, spawns[i]);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100, mask))
            {

                if (hit.collider.tag != "EnemyBullet" || hit.collider.tag != "Rocket")
                {
                    line.SetPosition(1, hit.point);

					if (hit.collider.tag == "Enemy" || hit.collider.tag == "Meteor")
					{
						
						Instantiate(hitParticle, hit.point, Quaternion.identity);
						
						if(hit.collider.tag == "Enemy")
						{
							hit.transform.GetComponent<Enemy>().health -= damagePerTick * Time.deltaTime;
							
							if (hit.transform.GetComponent<enemyDeath>() != null)
							{
								hit.transform.GetComponent<enemyDeath>().StartCoroutine("Flash");
							}
							else if (hit.transform.GetComponent<BossDeath>() != null)
							{
								hit.transform.GetComponent<BossDeath>().StartCoroutine("Flash");
							}
							
						}
						
					}
                }

            }

            else
                line.SetPosition(1, ray.GetPoint(100));
        }

        yield return new WaitForSeconds(.001f);
        StartCoroutine(Level4Beam());

    }


}

