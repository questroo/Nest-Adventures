using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleBehaviour : StateMachineBehaviour
{
    BossController boss;
    Rigidbody rigidBody;
    private float dashTime;
    private float startDashTime = 5.0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
        rigidBody = animator.GetComponent<Rigidbody>();
        dashTime = startDashTime;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(boss.GetTarget().position, rigidBody.position);

        boss.LookAtPlayer();
        
        if (distance <= boss.GetMeleeRadius())
        {
            animator.SetTrigger("Attack");
            dashTime = startDashTime;
        }
        else if (distance <= boss.lookRadius)
        {
            animator.SetTrigger("Walk");
            dashTime = startDashTime;
        }
        else if (distance <= boss.dashRadius && distance >= boss.lookRadius && dashTime <= 0.0f)
        {
            animator.SetTrigger("Dash");
            dashTime = startDashTime;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Walk");
    }
}
