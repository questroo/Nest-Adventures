using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IDamageable
{
    [SerializeField]
    public float objectHealth = 5.0f;
    public float health;

    public LootTable lootTable;

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
        if(lootTable)
        {
            lootTable.DropLoot();
        }
        Destroy(gameObject);
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
        DestroyThisBreakable();
    }
}