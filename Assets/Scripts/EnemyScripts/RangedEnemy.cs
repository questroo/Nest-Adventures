using Assets.Scripts.EnemyScripts.FSM;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyBehaviourType
{
    Patrol,
    None
}

[RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]
public class RangedEnemy : MonoBehaviour, IDamageable
{
    /// Enemy behaviour variables
    public EnemyBehaviourType behaviourType = EnemyBehaviourType.None;

    // Idle State
    public float idleWaitTimeMin = 1.0f;
    public float idleWaitTimeMax = 3.0f;

    // Patrol State
    public int patrolIndex = -1;
    public float forcedPositionChangeCooldown = 2.0f;
    public float waypointDistanceCheck = 0.2f;
    public float targetLossRange = 15.0f;
    public float noticeRange = 10.0f;

    // Chase State
    public float chaseStateSpeedModifier = 1.0f;

    // Attack State
    public GameObject projectileWeapon;
    public Transform projectileLaunchPosition;
    public float projectileDamage = 5.0f;
    public float projectileLaunchForce = 35.0f;
    public float attackCooldown = 1.0f;
    public float currentAttackCooldown = 1.0f;
    public float maxWeaponRange = 9.0f;
    public float minWeaponRange = 1.5f;

    Animator anim;

    NavMeshAgent navMeshAgent;
    FiniteStateMachine finiteStateMachine;

    [SerializeField]
    Transform[] patrolPoints;

    public float health;
    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        finiteStateMachine = GetComponent<FiniteStateMachine>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        ServiceLocator.Get<EnemyLockController>().RegisterEnemy(gameObject);
    }

    public Transform[] GetPatrolPoints()
    {
        return patrolPoints;
    }

    #region HEALTH IMPLEMENTATION
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            anim.SetTrigger("Dead");
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    #endregion

    private void OnDestroy()
    {
        ServiceLocator.Get<EnemyLockController>().DeregisterEnemy(gameObject);
    }
}