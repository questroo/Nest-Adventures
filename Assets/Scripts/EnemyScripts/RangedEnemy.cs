using Assets.Scripts.EnemyScripts.FSM;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]
public class RangedEnemy : MonoBehaviour, IDamageable
{
    protected NavMeshAgent navMeshAgent;
    protected FiniteStateMachine finiteStateMachine;

    public float health;
    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        finiteStateMachine = GetComponent<FiniteStateMachine>();
    }

    #region HEALTH IMPLEMENTATION
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public void Die()
    {
        
    }

    public void TakeDamage(float damage)
    {
        
    }
    #endregion
}