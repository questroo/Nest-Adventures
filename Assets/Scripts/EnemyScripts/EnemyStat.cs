using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IDamageable
{
    public float enemyMaxHealth = 100f;
    public float bossDamage = 5f;
    public HealthBarManager healthBar;

    public float Health { get; set; }


    void Start()
    {
        Health = enemyMaxHealth;
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
        Health -= damage;
        Debug.Log("Boss takes " + damage + " damage");
        healthBar.SetHealth(Health);

        if (Health <= 0f)
        {
            Health = 0.0f;
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
