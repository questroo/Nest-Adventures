using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public delegate void PlayerDeath();
    public static event PlayerDeath OnPlayerDeath;

    public float m_maxHealth = 100.0f;
    private float m_Health;
    public float m_AtkDamage = 14.0f;
    private bool invincible = false;
    private bool isDead = false;
    public HealthBarManager healthBar;

    private EnemyStat enemyStat;

    /// Health Potion
    [HideInInspector] public bool hasHealthPotion = false;
    public int maxHealthPotionUses = 3;
    [HideInInspector] public int currentHealthPotionUses = 0;
    public float healthPotionValue = 25.0f;

    /// Status Effects
    // Poison
    bool isPoisoned = false;
    float poisonDurationRemaining = 0.0f;
    float currentPoisonStrength = 0.0f;
    // AttackDown
    bool isAttackDown = false;
    float attackDownDurationRemaining = 0.0f;
    float currentAttackDownStrength = 0.0f;
    // Healing Potion
    bool isHealing = false;
    public float healingPotionStrength = 5.0f;
    public float healingPotionDuration = 8.0f;
    float healingPotionDurationRemaining = 0.0f;
    float lastHealth = 0.0f;

    private void Awake()
    {
        ServiceLocator.Register<PlayerStats>(this);
    }

    private void Start()
    {
        m_Health = m_maxHealth;
        enemyStat = FindObjectOfType<EnemyStat>();
        healthBar.SetMaxHealth(m_maxHealth);

        currentHealthPotionUses = 0;
        lastHealth = m_Health;
    }
    public void TakeDamage(float damage)
    {
        if (!invincible && !isDead)
        {
            SoundManager.PlaySound(SoundManager.Sound.player2_get_hit, gameObject.transform.position);
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
        if (isHealing)
            UpdateHealingPotion();

        if (Input.GetKeyDown(KeyCode.P))
        {
            UseHealthPotion();
        }
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
    public void UpdateHealingPotion()
    {
        healingPotionDuration -= Time.deltaTime;
        if(healingPotionDurationRemaining <= 0.0f)
        {
            isHealing = false;
        }
        else
        {
            TakeDamage(-healingPotionStrength * Time.deltaTime);
        }

        // Check if player took damage so we can stop healing
        if(m_Health < lastHealth)
        {
            isHealing = false;
            healingPotionDurationRemaining = 0.0f;
        }
        else
        {
            lastHealth = m_Health;
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

            case StatusType.HealingPotion:
                if(isHealing)
                {
                    // Only trigger if not already running.
                    // Could implement duration extension?
                }
                else
                {
                    isHealing = true;
                    lastHealth = m_Health;
                    healingPotionDurationRemaining = healingPotionDuration;
                }
                break;
        }
    }
    /// Status Effects End

    /// Health Potion Start
    public bool PickupHealthPotion()
    {
        if (!hasHealthPotion)
        {
            hasHealthPotion = true;
        }

        ++currentHealthPotionUses;

        if (currentHealthPotionUses > maxHealthPotionUses)
        {
            currentHealthPotionUses = maxHealthPotionUses;
            return false;
        }
        else
        {
            healthBar.UIAddHealthPotion(currentHealthPotionUses);
            return true;
        }
    }

    public void UseHealthPotion()
    {
        if (hasHealthPotion)
        {
            if (currentHealthPotionUses > 0)
            {
                healthBar.UIUseHealthPotion(--currentHealthPotionUses);
                //TakeDamage(-healthPotionValue);  CHANGED TO A HEAL OVER TIME
                ModifyStatus(StatusType.HealingPotion, healingPotionStrength, healingPotionDuration);
            }
        }
    }
    /// Health Potion End

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

    public bool CheckIsDead()
    {
        return isDead;
    }

    private void Die()
    {
        isDead = true;
        //trigger death anim
        GetComponentInChildren<Animator>().SetTrigger("Death");
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath();
        }
    }
}
