using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float enemyMaxHealth = 100f;
    public float bossDamage = 5f;

    private float enemyCurrentHealth;


    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    public void TakeDamage(float damage)
    {

        enemyCurrentHealth -= damage;

        if (enemyCurrentHealth <= 0f)
        {
            enemyCurrentHealth = 0.0f;
            Die();
        }

    }

    public void Die()
    {
        //Death Animation
        //Stop all movement
        //Remove collision
        //Model Disappear

        Destroy(gameObject);
    }
}
