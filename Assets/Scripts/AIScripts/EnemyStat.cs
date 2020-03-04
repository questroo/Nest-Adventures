using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float bossMaxHealth = 100f;
    public float bossDamage = 10f;
    public HealthBarManager healthBar;
    private float bossCurrentHealth;
    private PlayerStats player;
    public AttackManager attackManager;


    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        bossCurrentHealth = bossMaxHealth;
        healthBar.SetMaxHealth(bossMaxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (!attackManager.hasBeenHit)
        {
            bossCurrentHealth -= damage;
            healthBar.SetHealth(bossCurrentHealth);

            if (bossCurrentHealth <= 0f)
            {
                bossCurrentHealth = 0.0f;
                Die();
            }
        }
    }

    public float GetHealthNormalized()
    {
        return (float)bossCurrentHealth / bossMaxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tanjiro") || collision.gameObject.CompareTag("Bertha"))
        {
            player.GetComponentInParent<PlayerStats>().TakeDamage(bossDamage);
        }

    }
    private void Die()
    {
        //Death Animation
        //Stop all movement
        //Remove collision
        //Model Disappear

        Destroy(gameObject);
    }
}
