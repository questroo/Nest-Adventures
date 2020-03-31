using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float bossMaxHealth = 100f;
    public float bossDamage = 10f;
    public HealthBarManager healthBar;
    public AttackManager attackManager;

    private float bossCurrentHealth;
    private PlayerStats player;
    private Animator anim;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        bossCurrentHealth = bossMaxHealth;
        healthBar.SetMaxHealth(bossMaxHealth);
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        
            bossCurrentHealth -= damage;
            healthBar.SetHealth(bossCurrentHealth);

            if (bossCurrentHealth <= 0f)
            {
                bossCurrentHealth = 0.0f;
                anim.SetTrigger("dead");
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
    public void Die()
    {
        //Death Animation
        //Stop all movement
        //Remove collision
        //Model Disappear

        Destroy(gameObject);
    }
}
