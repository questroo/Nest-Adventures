﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IDamageable
{
    public float enemyMaxHealth = 100f;
    public float bossDamage = 5f;
    // TODO: Implement the health bar
    //public HealthBarManager healthBar;
    public float Health { get; set; }
    Animator animator;

    void Start()
    {
        Health = enemyMaxHealth;
        animator = GetComponentInChildren<Animator>();
        //healthBar.SetMaxHealth(enemyMaxHealth);
    }


    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Enemy takes " + damage + " damage");
  
        if (Health <= 0.0f)
        {
            Health = 0.0f; 
            animator.SetInteger("Die", 1);
        }

        //healthBar.SetHealth(Health);
    }

    public void Die()
    {
        //Death Animation
        //Stop all movement
        //Remove collision
        //Model Disappear
        Debug.Log("DEAD");
        FindObjectOfType<CameraController>().RemoveSelfFromList(this);
        Destroy(gameObject);
    }
}
