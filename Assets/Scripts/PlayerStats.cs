﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float m_maxHealth = 100.0f;
    private float m_Health;
    public float m_AtkDamage = 14.0f;
    private bool invincible = false;
    public HealthBarManager healthBar;

    private EnemyStat enemyStat;


    /// Status Effects
    // Poison
    bool isPoisoned = false;
    float poisonDurationRemaining = 0.0f;
    float currentPoisonStrength = 0.0f;
    // AttackDown
    bool isAttackDown = false;
    float attackDownDurationRemaining = 0.0f;
    float currentAttackDownStrength = 0.0f;


    private void Start()
    {
        m_Health = m_maxHealth;
        enemyStat = FindObjectOfType<EnemyStat>();
        healthBar.SetMaxHealth(m_maxHealth);
    }
    public void TakeDamage(float damage)
    {
        if (!invincible)
        {
            m_Health -= damage;
            if (m_Health > m_maxHealth)
                m_Health = m_maxHealth;

            if (m_Health <= 0.0f)
            {
                m_Health = 0.0f;
                UpdateHealthBar();
                Die();
            }
            UpdateHealthBar();
        }
    }

    /// Status Effects Start
    private void Update()
    {
        if (isPoisoned)
            UpdatePoison();
        if (isAttackDown)
            UpdateAttackDown();
    }

    public void UpdatePoison()
    {
        poisonDurationRemaining -= Time.deltaTime;
        if (poisonDurationRemaining <= 0.0f)
        {
            isPoisoned = false;
            return;
        }
        else
        {
            TakeDamage(currentPoisonStrength * Time.deltaTime);
        }
    }
    public void UpdateAttackDown()
    {
        attackDownDurationRemaining -= Time.deltaTime;
        if (attackDownDurationRemaining <= 0.0f)
        {
            isAttackDown = false;
            m_AtkDamage += currentAttackDownStrength;
        }
    }
    public void ModifyStatus(StatusType stat, float strength, float duration)
    {
        switch (stat)
        {
            case StatusType.Poison:
                if (isPoisoned)
                {
                    // If poison is stronger, it will take the new one in exchange for the old one
                    if (currentPoisonStrength < strength)
                    {
                        currentPoisonStrength = strength;
                        poisonDurationRemaining = duration;
                    }
                    // If poison is the same strength, it will refresh the duration if it is longer.
                    else if (currentPoisonStrength == strength && duration > poisonDurationRemaining)
                    {
                        poisonDurationRemaining = duration;
                    }
                    // Otherwise do nothing
                }
                else
                {
                    isPoisoned = true;
                    poisonDurationRemaining = duration;
                    currentPoisonStrength = strength;
                }
                break;


            case StatusType.AttackDown:
                if (isAttackDown)
                {
                    // If poison is stronger, it will take the new one in exchange for the old one
                    if (currentAttackDownStrength < strength)
                    {
                        // In case attack is already down, but it is going down further, only subtract the difference between the two.
                        m_AtkDamage -= (strength - currentAttackDownStrength);

                        currentAttackDownStrength = strength;
                        attackDownDurationRemaining = duration;
                    }
                    // If poison is the same strength, it will refresh the duration if it is longer.
                    else if (currentAttackDownStrength == strength && duration > attackDownDurationRemaining)
                    {
                        attackDownDurationRemaining = duration;
                    }
                    // Otherwise do nothing
                }
                else
                {
                    isAttackDown = true;
                    attackDownDurationRemaining = duration;
                    currentAttackDownStrength = strength;

                    m_AtkDamage -= currentAttackDownStrength;
                }
                break;
        }
    }
    /// Status Effects End



    private void UpdateHealthBar()
    {
        healthBar.SetHealth(m_Health);
    }
    public void StartIFrame()
    {
        invincible = true;
    }

    public void EndIFrame()
    {
        invincible = false;
    }

    private void Die()
    {
        if (m_Health == 0)
        {
            //trigger death anim
            Destroy(gameObject);
            //and die
        }
    }
}
