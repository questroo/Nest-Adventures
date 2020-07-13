using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionIdleBehaviour : StateMachineBehaviour
{
    EnemyController enemy;
    Rigidbody rigidBody;
    private float dashTime;
    private float startDashTime = 5.0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponentInParent<EnemyController>();
        rigidBody = animator.GetComponentInParent<Rigidbody>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(enemy.GetTarget().position, animator.transform.position);

        enemy.Movement();

        if(distance <= enemy.attackRadius)
        {
            animator.SetTrigger("Attack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
