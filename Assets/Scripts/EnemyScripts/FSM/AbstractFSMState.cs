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

public abstract class AbstractFSMState : ScriptableObject
{
    protected NavMeshAgent navMeshAgent;

    public ExecutionState ExecutionState { get; protected set; }

    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
    }

    public virtual bool EnterState()
    {
        bool success = true;
        ExecutionState = ExecutionState.ACTIVE;

        success = (navMeshAgent != null);

        return success;
    }

    public abstract void UpdateState();

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        return true;
    }

    public virtual void SetNavMeshAgent(NavMeshAgent _navMeshAgent)
    {
        if(navMeshAgent != null)
        {
            navMeshAgent = _navMeshAgent;
        }
    }
}