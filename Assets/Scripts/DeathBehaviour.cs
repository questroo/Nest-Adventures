using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : StateMachineBehaviour
{
    EnemyController enemy;
    EnemyStat enemyStat;
    Rigidbody rigidBody;
    private float dashTime;
    private float startDashTime = 5.0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponentInParent<EnemyController>();
        enemyStat = animator.GetComponentInParent<EnemyStat>();
        rigidBody = animator.GetComponentInParent<Rigidbody>();

    }

    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyStat.Die();
    }
}
