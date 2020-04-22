using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public int enemyMaxHealth = 100;
    public int bossDamage = 5;
    public HealthBarController healthbar;

    private int enemyCurrentHealth;
    public Material material;


    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        healthbar.SetMaxHealth(enemyMaxHealth);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);
            Debug.Log("Boss took " + 5 + " damage ");
        }

        if (enemyCurrentHealth <= 70)
        {
            BossPhase2();
        }

    }

    public void TakeDamage(int damage)
    {

        enemyCurrentHealth -= damage;

        healthbar.SetHealth(enemyCurrentHealth);

        if (enemyCurrentHealth <= 0)
        {
            enemyCurrentHealth = 0;
            Die();
        }

    }

    public void BossPhase2()
    {
        //TODO: Change color of boss, increase damage, increase speed

        Debug.Log("Boss Change to Phase 2");
        Debug.Log("Increase damage in multiples of 2");
        bossDamage *= 2;
       
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
