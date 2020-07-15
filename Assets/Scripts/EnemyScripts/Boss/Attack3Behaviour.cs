using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack3Behaviour : StateMachineBehaviour
{
    private BossController boss;
    private Rigidbody rigidBody;

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
            rigidBody.transform.Translate(Vector3.back * Time.deltaTime);
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
    }
}
