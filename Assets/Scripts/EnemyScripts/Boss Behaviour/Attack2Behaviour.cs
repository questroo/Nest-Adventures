using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack2Behaviour : StateMachineBehaviour
{
    BossController boss;
    Rigidbody rigidBody;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponentInParent<BossController>();
        rigidBody = animator.GetComponentInParent<Rigidbody>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        float distance = Vector3.Distance(boss.GetTarget().position, rigidBody.position);

        if (distance <= boss.GetMeleeRadius())
        {
            rigidBody.transform.Translate(Vector3.forward * Time.deltaTime);
            animator.SetTrigger("Attack");
        }
        else
        {
            animator.SetTrigger("Idle");
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Idle");
    }

}
