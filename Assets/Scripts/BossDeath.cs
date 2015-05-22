using UnityEngine;
using System.Collections;

public class BossDeath : MonoBehaviour {

    public float health = 500f;

    bool onetime = true;

    WaveSystem ws;

    SpriteRenderer sr;

    public bool canBeDamaged = false;

	// Use this for initialization
	void Start () {

        health += EnemyModifier.bonusHealth * 25;
        ws = GameObject.Find("EnemySpawner").GetComponent<WaveSystem>();
        sr = GetComponentInChildren<SpriteRenderer>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (health <= 0 && onetime)
        {
            onetime = false;
            Death();
        }
	
	}

    void Death()
    {
        Destroy(gameObject.transform.parent.gameObject);
        ws.enemiesLeft--;
        //spawn death particles

    }

    public IEnumerator Flash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(.1f);
        sr.color = Color.white;
    }

    void OnTriggerEnter(Collider other)
    {

        if ((other.tag == "playerBullet" || other.tag == "Player" || other.tag == "Rocket") && canBeDamaged)
        {

            if (other.tag == "Rocket")
            {
                health -= 4;
            }
            else if (other.tag == "playerBullet")
            {

                health -= other.GetComponent<PlayerBulletProperties>().damage;
                Destroy(other.gameObject);
            }
            else
            {
                health--;
               
            }

            StartCoroutine(Flash());
        }
    }

}
