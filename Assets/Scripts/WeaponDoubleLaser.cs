using UnityEngine;
using System.Collections;

public class WeaponDoubleLaser : MonoBehaviour {

    public float maxCooldown = .25f;
    public float cooldownIncrease = .03f;
    public static float doubleLaserCooldown = .25f;
    public int level = 1;

    public GameObject[] bullets;
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
            case 4: Level4Fire();
                break;
        }
    }

    void Level1Fire()
    {
        Instantiate(bullets[0], bulletSpawns[1].position, bulletSpawns[1].rotation);
        Instantiate(bullets[0], bulletSpawns[2].position, bulletSpawns[2].rotation);
    }

    void Level2Fire()
    {
        Instantiate(bullets[0], bulletSpawns[4].position, bulletSpawns[1].rotation);
        Instantiate(bullets[0], bulletSpawns[5].position, bulletSpawns[2].rotation);
        Instantiate(bullets[0], bulletSpawns[6].position, bulletSpawns[2].rotation);
    }

    void Level3Fire()
    {
        Instantiate(bullets[0], bulletSpawns[0].position, bulletSpawns[0].rotation);
        Instantiate(bullets[0], bulletSpawns[7].position, bulletSpawns[7].rotation);
        Instantiate(bullets[0], bulletSpawns[8].position, bulletSpawns[8].rotation);
        Instantiate(bullets[0], bulletSpawns[3].position, bulletSpawns[3].rotation);
        Instantiate(bullets[0], bulletSpawns[6].position, bulletSpawns[6].rotation);
    }

    void Level4Fire()
    {
        Instantiate(bullets[1], bulletSpawns[0].position, bulletSpawns[0].rotation);
        Instantiate(bullets[1], bulletSpawns[7].position, bulletSpawns[7].rotation);
        Instantiate(bullets[1], bulletSpawns[8].position, bulletSpawns[8].rotation);
        Instantiate(bullets[1], bulletSpawns[3].position, bulletSpawns[3].rotation);
        Instantiate(bullets[1], bulletSpawns[6].position, bulletSpawns[6].rotation);
    }
}
