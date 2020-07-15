using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack1Behaviour : StateMachineBehaviour
{
    private BossController boss;
    private Rigidbody rigidBody;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
        rigidBody = animator.GetComponentInParent<Rigidbody>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // float distance = Vector3.Distance(boss.GetTarget().position, rigidBody.position);
        
        //rigidBody.transform.Translate(Vector3.forward * Time.deltaTime);

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Idle");
    }
}
