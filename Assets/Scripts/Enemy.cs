﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public float health = 0;
    public float moveSpeed = 0;
    public float attackSpeed = 0;

    enemyShoot es;

    void Start()
    {
        es = gameObject.GetComponent<enemyShoot>();
        health = health + EnemyModifier.bonusHealth;
        moveSpeed = moveSpeed + EnemyModifier.bonusSpeed;

       if(es != null)
       {
            attackSpeed += es.cooldown;
       }
        

    }

}
