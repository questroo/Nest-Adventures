using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackBehaviour : StateMachineBehaviour
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

        if (distance >= enemy.attackRadius)
        {
            animator.SetTrigger("Idle");
            
            animator.ResetTrigger("Walk");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
    }
}
