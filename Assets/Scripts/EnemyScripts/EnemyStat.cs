using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float enemyMaxHealth = 100f;
    public float bossDamage = 5f;
    public HealthBarManager healthBar;

    private float enemyCurrentHealth;


    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        healthBar.SetMaxHealth(enemyMaxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(100);
        }
    }

    public void TakeDamage(float damage)
    {
        enemyCurrentHealth -= damage;
        Debug.Log("Boss takes " + damage + " damage");
        healthBar.SetHealth(enemyCurrentHealth);

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
