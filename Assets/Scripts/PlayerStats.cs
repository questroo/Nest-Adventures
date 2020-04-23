﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public float m_maxHealth = 100.0f;
    private float m_Health;
    public float m_AtkDamage = 14.0f;
    private bool invincible = false;
    public Image healthSlider;

    private EnemyStat enemyStat;

    // StatMod Trap Idea

    private List<StatusEffect> activeStatusEffects;
    // StatMod Trap Idea

    private void Start()
    {
        m_Health = m_maxHealth;
        enemyStat = FindObjectOfType<EnemyStat>();
        //healthSlider.fillAmount = 1.0f;

        activeStatusEffects = new List<StatusEffect>();
    }

    private void Update()
    {
        UpdateStatusEffects();
    }

    public void TakeDamage(float damage)
    {
        if (!invincible)
        {
            m_Health -= damage;
            Debug.Log("Take damage. Curr health: " + m_Health);
            if (m_Health <= 0.0f)
            {
                m_Health = 0.0f;
                UpdateHealthBar();
                Die();
            }
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        float healthPercent = m_Health / m_maxHealth;
        healthSlider.fillAmount = healthPercent;
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

    // StatMod Trap Idea
    public void ModifyStat(StatToModify statType, float statModAmount, float statModDuration)
    {
        //activeStatusEffects.Add(newStatEffect);
    }
    public void ModifyStat(StatusEffect statEffect)
    {
        if(!activeStatusEffects.Contains(statEffect))
        {
            activeStatusEffects.Add(statEffect);
        }
    }

    private void UpdateStatusEffects()
    {
        foreach(StatusEffect statEffect in activeStatusEffects)
        {
            statEffect.durationRemaining -= Time.deltaTime;
            if(statEffect.durationRemaining <= 0.0f)
            {
                activeStatusEffects.Remove(statEffect);
            }
            else
            {
                if(!invincible)
                {
                    switch (statEffect.statToMod)
                    {
                        case StatToModify.Stun:
                            break;

                        case StatToModify.Attack:
                            break;

                        case StatToModify.Defence:
                            break;

                        case StatToModify.Speed:
                            break;
                    }
                }
            }
        }
    }

    public void ClearAllStatusEffects()
    {
        activeStatusEffects.Clear();
    }
    // StatMod Trap Idea
}
