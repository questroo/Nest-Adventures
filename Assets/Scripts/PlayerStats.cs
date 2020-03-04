using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float m_Health = 100.0f;
    public float m_AtkDamage = 14.0f;
    private bool invincible = false;

    private EnemyStat enemyStat;

    private void Start()
    {
        enemyStat = FindObjectOfType<EnemyStat>();
    }
    public void TakeDamage(float damage)
    {
        if (!invincible)
        {
            m_Health -= damage;
            Debug.Log("Take damage. Curr health: " + m_Health);
            if (m_Health < 0.0f)
            {
                m_Health = 0.0f;
                Die();
            }
        }
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
