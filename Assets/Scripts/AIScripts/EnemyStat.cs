using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float bossMaxHealth = 100f;
    public float bossCurrentHealth = 100f;
    public float bossDamage = 10f;

    private PlayerStats player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }

    public void TakeDamage(float damage)
    {
        bossCurrentHealth -= damage;
        if (bossCurrentHealth <= 0f)
        {
            bossCurrentHealth = 0.0f;
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Tanjiro") || collision.gameObject.CompareTag("Bertha"))
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
