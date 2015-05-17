using UnityEngine;
using System.Collections;

public class WeaponDoubleLaser : MonoBehaviour {

    public float maxCooldown = .25f;
    public float cooldownIncrease = .03f;
    public static float doubleLaserCooldown = .25f;
    public int level = 1;

    public GameObject bullet;
    public Transform[] bulletSpawns;

    float countdown;


	// Use this for initialization
	void Start () {

        countdown = shoot.cooldown;
	
	}
	
	// Update is called once per frame
	void Update () {

        Mathf.Clamp(level, 1, 3);

        countdown -= Time.deltaTime;

        if(countdown <= 0)
        {
            Fire();
            countdown = shoot.cooldown;
        }
        
	
	}

    void Fire()
    {
        switch(level)
        {
            case 1: Level1Fire();
                break;
            case 2: Level2Fire();
                break;
            case 3: Level3Fire();
                break;
        }
    }

    void Level1Fire()
    {
        Instantiate(bullet, bulletSpawns[1].position, bulletSpawns[1].rotation);
        Instantiate(bullet, bulletSpawns[2].position, bulletSpawns[2].rotation);
    }

    void Level2Fire()
    {
        Instantiate(bullet, bulletSpawns[4].position, bulletSpawns[1].rotation);
        Instantiate(bullet, bulletSpawns[5].position, bulletSpawns[2].rotation);
        Instantiate(bullet, bulletSpawns[6].position, bulletSpawns[2].rotation);
    }

    void Level3Fire()
    {
        Instantiate(bullet, bulletSpawns[0].position, bulletSpawns[0].rotation);
        Instantiate(bullet, bulletSpawns[7].position, bulletSpawns[7].rotation);
        Instantiate(bullet, bulletSpawns[8].position, bulletSpawns[8].rotation);
        Instantiate(bullet, bulletSpawns[3].position, bulletSpawns[3].rotation);
        Instantiate(bullet, bulletSpawns[6].position, bulletSpawns[6].rotation);
    }
}
