using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public Slider slider;
    public Image healthPotion;
    public Text healthPotionText;

    // Driven by PlayerStats
    private PlayerStats statsHandle;

    private int healthPotionUsesMax = 0;
    private int currentHealthPotionUses = 0;

    public void Start()
    {
        statsHandle = GetComponentInParent<PlayerStats>();
        if(statsHandle)
        {
            healthPotionUsesMax = statsHandle.maxHealthPotionUses;
            currentHealthPotionUses = statsHandle.currentHealthPotionUses;
        }

        healthPotion.enabled = false;
        healthPotionText.enabled = false;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void UIAddHealthPotion(int currentUses)
    {
        healthPotion.enabled = true;
        healthPotionText.enabled = true;
        healthPotionText.text = currentUses.ToString();
    }

    public void UIUseHealthPotion(int currentUses)
    {
        healthPotionText.text = currentUses.ToString();
    }
}