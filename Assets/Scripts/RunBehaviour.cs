using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunBehaviour : StateMachineBehaviour
{
    BossController boss;
    Rigidbody rigidBody;
    private float dashTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponentInParent<BossController>();
        rigidBody = animator.GetComponentInParent<Rigidbody>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(boss.GetTarget().position, rigidBody.position);

        boss.LookAtPlayer();

        if (distance <= boss.GetMeleeRadius())
        {
            animator.SetTrigger("Attack1");
            boss.StopMovement();
        }
        else if (distance <= boss.lookRadius)
        {
            boss.Movement();
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.ResetTrigger("Attack1");
    }
}
