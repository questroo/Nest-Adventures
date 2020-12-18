using Assets.Scripts.EnemyScripts.FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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

public abstract class AbstractFSMState : MonoBehaviour
{
    protected bool isInitialized = false;
    protected NavMeshAgent navMeshAgent;
    protected RangedEnemy rangedEnemy;
    protected FiniteStateMachine finiteStateMachine;

    protected GameObject player;

    Animator animator;

    //public ExecutionState ExecutionState { get; protected set; }
    public FSMStateType StateType { get; protected set; }
    public bool enteredState { get; protected set; }

    public virtual void OnEnable()
    {}

    public virtual void Awake ()
    {
        //ServiceLocator.Get<CoroutineCaller>().instance.StartCoroutine(Initialize());
        while (player == null)
        {
            //PlayerStats temp = ServiceLocator.Get<PlayerStats>();
            //if (temp)
            //    player = temp.gameObject;
            player = FindObjectOfType<PlayerStats>()?.gameObject;
        }
        Debug.Log("PlayerStats found.");
        isInitialized = true;

        animator = GetComponent<Animator>();

    }

    public virtual IEnumerator Initialize()
    {
        yield return null;
    }

    public virtual bool EnterState()
    {
        bool successNavMesh = true;
        bool successRangedEnemy = true;


        successNavMesh = (navMeshAgent != null);

        successRangedEnemy = (navMeshAgent != null);

        return successNavMesh & successRangedEnemy;
    }

    public abstract void UpdateState();

    public virtual bool ExitState()
    {   
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

    public virtual bool IsInitialized()
    {
        return isInitialized;
    }

    public void AnimStateCheck()
    {
        switch (StateType)
        {
            case FSMStateType.IDLE:
                //animator.SetTrigger("Idle");
                animator.SetBool("isIdle", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isShoting", false);
                break;
            case FSMStateType.PATROL:
                //animator.SetTrigger("Walk");
                animator.SetBool("isWalking", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isShoting", false);
                break;
            //case FSMStateType.CHASE:
            //    break;
            case FSMStateType.MOVEAWAY:
                //animator.SetTrigger("Walk");
                animator.SetBool("isWalking", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isShoting", false);
                break;
            case FSMStateType.ATTACK:
                //animator.SetTrigger("Shot");
                animator.SetBool("isShoting", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalking", false);
                break;
        }
    }
}