using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour, IDamageable
{
    public enum EnemyState
    {
        Patrolling,
        Attacking,
        LookingForPlayer,
        Chasing,
        MovingIntoRange,
        Dead
    }

    public float maxHealth = 100.0f;
    float health;

    [SerializeField]
    EnemyState currentState = EnemyState.Patrolling;

    private void Awake()
    {
        health = maxHealth;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                break;

            case EnemyState.Attacking:
                break;

            case EnemyState.LookingForPlayer:
                break;

            case EnemyState.Chasing:
                break;

            case EnemyState.MovingIntoRange:
                break;

            case EnemyState.Dead:
                break;
        }
    }



    ///  Health Interface
    public float Health
    {
        get { return health; }
        set { health = value; }
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
        Die();
    }
    public void Die()
    {
        if (Health <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}