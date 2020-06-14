using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IDamageable
{
    [SerializeField]
    public float objectHealth = 5.0f;
    private float health;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    private void Start()
    {
        health = objectHealth;
    }

    private void OnCollisionEnter(Collision other)
    {
    }

    public void DestroyThisBreakable()
    {
        // TODO - implement polish for destruction (effects, shaders etc.)
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Break();
    }

    public void Break()
    {
        // SpawnParticle or break apart
    }
}