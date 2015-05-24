using UnityEngine;
using System.Collections;

public class EnemyStalkerAI : MonoBehaviour {

    public GameObject bullet;
    public Transform bulletSpawn;
    public float rotateSpeed = 2f;
    public float moveSpeed = 5f;
    public float waitTime = 2f;

    Vector3 flySpot;
    Transform lookTarget;

    bool flyDown = true;
    float randHeight;
    public float shootCooldown;

    Quaternion bulletRot;
    

	// Use this for initialization
	void Start () {

        lookTarget = GameObject.FindGameObjectWithTag("Player").transform;
        randHeight = Random.Range(0, 3.1f);
        StartCoroutine("Shoot");

        shootCooldown -= EnemyModifier.bonusAttackSpeed;

        flySpot = new Vector3(Random.Range(-5, 5.1f), Random.Range(-5, 5.1f), 0);
	}
	
	// Update is called once per frame
	void Update () {

        if (lookTarget == null && GameObject.FindWithTag("Player") != null) 
        {
            lookTarget = GameObject.FindWithTag("Player").transform;
        }

        if(transform.position.y < randHeight) flyDown = false;

        if (flyDown)
        {
            transform.position -= new Vector3(0, moveSpeed, 0) * Time.deltaTime;
            
        }
        else
        {
            if(transform.position.y > 4.5f && transform.position.y < -3.5f)
            {
                flySpot = transform.position;
            }

            transform.position = Vector3.Lerp(transform.position, flySpot, Time.deltaTime);

            if(lookTarget != null) LookToPlayer();
         
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "OOB")
        {
            flySpot = transform.position;
        }
    }
    
    
    IEnumerator Shoot()
    {
        if(!flyDown)
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(shootCooldown);
                Instantiate(bullet, bulletSpawn.position, transform.rotation);
                yield return new WaitForSeconds(shootCooldown);
                Instantiate(bullet, bulletSpawn.position, transform.rotation);
                yield return new WaitForSeconds(shootCooldown);
                Instantiate(bullet, bulletSpawn.position, transform.rotation);
            }
               
            yield return new WaitForSeconds(waitTime);
            
            flySpot = new Vector3(Random.Range(-5, 5.1f), Random.Range(-5, 5.1f), 0);
            StartCoroutine("Shoot");
        }
        else
        {
            yield return new WaitForSeconds(waitTime / 2);
            StartCoroutine("Shoot");
        }

        

    }

    void LookToPlayer()
    {
        Vector3 vectorToTarget = lookTarget.position - transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);
    }

    
     
}
