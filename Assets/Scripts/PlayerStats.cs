using UnityEngine;

public class PlayerStats : MonoBehaviour
{


    private float m_Health = 100.0f;
    private bool invincible = false;

    public void TakeDamage(float damage)
    {
        if (!invincible)
        {
            m_Health -= damage;
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
            //and die
        }
    }
}
