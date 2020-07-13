using Assets.Scripts.EnemyScripts.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ExecutionState
{
    NONE,
    ACTIVE,
    COMPLETED,
    TERMINATED,
};

public enum FSMStateType
{
    IDLE,
    PATROL,
    CHASE,
    MOVEAWAY,
    ATTACK,
};

public abstract class AbstractFSMState : ScriptableObject
{
    protected NavMeshAgent navMeshAgent;
    protected RangedEnemy rangedEnemy;
    protected FiniteStateMachine finiteStateMachine;

    protected GameObject player;

    public ExecutionState ExecutionState { get; protected set; }
    public FSMStateType StateType { get; protected set; }
    public bool enteredState { get; protected set; }

    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
        player = FindObjectOfType<PlayerStats>().gameObject;
    }

    public virtual bool EnterState()
    {
        bool successNavMesh = true;
        bool successRangedEnemy = true;

        ExecutionState = ExecutionState.ACTIVE;

        successNavMesh = (navMeshAgent != null);

        successRangedEnemy = (navMeshAgent != null);

        return successNavMesh & successRangedEnemy;
    }

    public abstract void UpdateState();

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    public virtual void SetExecutingFSM(FiniteStateMachine fsm)
    {
        if (fsm != null)
        {
            finiteStateMachine = fsm;
        }
    }

    public virtual void SetExecutingNavMeshAgent(NavMeshAgent _navMeshAgent)
    {
        if (_navMeshAgent != null)
        {
            navMeshAgent = _navMeshAgent;
        }
    }
    public virtual void SetExecutingRangedEnemy(RangedEnemy _rangedEnemy)
    {
        if (_rangedEnemy != null)
        {
            rangedEnemy = _rangedEnemy;
        }
    }
}