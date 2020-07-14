using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack2Behaviour : StateMachineBehaviour
{
    private BossController boss;
    private Rigidbody rigidBody;
    private int animationTrigger2;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
        rigidBody = animator.GetComponent<Rigidbody>();
        animationTrigger2 = Random.Range(0, 1);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(boss.GetTarget().position, rigidBody.position);

        if (distance <= boss.GetMeleeRadius() && animationTrigger2 == 0)
        {
            //rigidBody.transform.Translate(Vector3.forward * Time.deltaTime);
            animator.SetInteger("attack", animationTrigger2);
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("Attack");
        //animator.ResetTrigger("Idle");
    }
}
