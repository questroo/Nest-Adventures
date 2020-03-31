using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float m_maxHealth = 100.0f;
    private float m_Health;
    public float m_AtkDamage = 14.0f;
    private bool invincible = false;
    public Image healthSlider;

    private EnemyStat enemyStat;

    private void Start()
    {
        m_Health = m_maxHealth;
        enemyStat = FindObjectOfType<EnemyStat>();
        healthSlider.fillAmount = 1.0f;
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
}
