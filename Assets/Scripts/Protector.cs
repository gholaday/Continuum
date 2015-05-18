using UnityEngine;
using System.Collections;

public class Protector : MonoBehaviour {

    
     public Transform target;

     int offset;

     float timer;

     // Use this for initialization
     void Start () {
         
         target = GameObject.FindGameObjectWithTag("Player").transform;
         if (target.gameObject.GetComponent<PlayerProtectorHandler>().numAlive % 2 == 0)
             offset = 1;
         else
             offset = -1;
        
        
     }
     
    

    void Update()
    {
        if(target != null)
        {
            timer += Time.smoothDeltaTime;
            var v = Quaternion.AngleAxis(timer * -90 * offset, Vector3.forward) * new Vector3(.75f, 0, 0);
            transform.position = target.position + v;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void OnDestroy()
    {
        if(target != null)
        {
            target.gameObject.GetComponent<PlayerProtectorHandler>().numAlive--;
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
 
}
