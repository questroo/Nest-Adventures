using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IDamageable
{
    public float enemyMaxHealth = 100f;
    public float bossDamage = 5f;
    private float startStaggerCooldown = 5f;
    private float staggerCooldown;
    // TODO: Implement the health bar
    [SerializeField]
    private EnemyHealthBar healthBar;

    public float Health { get; set; }
    Animator animator;

    void Start()
    {
        ServiceLocator.Get<EnemyLockController>().RegisterEnemy(gameObject);
        Health = enemyMaxHealth;
        animator = GetComponentInChildren<Animator>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(enemyMaxHealth);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        animator.SetTrigger("Hit");

        if (Health <= 0.0f)
        {
            Health = 0.0f;
            animator.SetInteger("Die", 1);
            Die();
        }

        //healthBar.SetHealth(Health);
    }

    public void Die()
    {
        //Death Animation
        //Stop all movement
        //Remove collision
        //Model Disappear        
        ServiceLocator.Get<EnemyLockController>().DeregisterEnemy(gameObject);
        var boss = GetComponent<BossDeath>();
        if (boss)
        {
            boss.BossIsDead();
        }
        Destroy(gameObject);
    }
}
