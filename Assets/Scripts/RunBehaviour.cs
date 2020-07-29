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
        float distance = Vector3.Distance(boss.GetTarget().position, animator.transform.position);

        Debug.Log("Distance " + distance);

        if (distance < boss.meleeAttackRadius)
        {
            Debug.Log("ATTACKS PLAYER");
            animator.SetInteger("Attack", 1);
            boss.StopMovement();
        }

        boss.Movement();
        Debug.Log("BOSS MOVING. . .");
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("Attack1");
        //animator.ResetTrigger("Run");
        animator.SetInteger("Attack", 0);
    }
}
