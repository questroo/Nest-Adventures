using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float bossMaxHealth = 100f;
    public float bossCurrentHealth = 100f;
    public float bossDamage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TakeDamage(float damage)
    {
        bossCurrentHealth -= damage;
        if (bossCurrentHealth <= 0f)
        {
            bossCurrentHealth = 0f;
            Die();
        }
    }

    private void Die()
    {
        //Death Animation
        //Stop all movement
        //Remove collision
        //Model Disappear
    }
}
