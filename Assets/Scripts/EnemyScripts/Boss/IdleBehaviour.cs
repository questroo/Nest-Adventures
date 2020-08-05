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
    [SerializeField]
    private float startIdleCoolddown;
    private float idleCooldown;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponentInParent<BossController>();
        rigidBody = animator.GetComponentInParent<Rigidbody>();
        dashTime = startDashTime;
        idleCooldown = startIdleCoolddown;
        animator.ResetTrigger("Hit");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(boss.GetTarget().position, animator.transform.position);

        boss.LookAtPlayer();

        if (idleCooldown <= 0.0f)
        {
            if (distance <= boss.lookRadius && distance >= boss.meleeAttackRadius)
            {
                animator.SetTrigger("Run");
            }
            else if (distance <= boss.meleeAttackRadius)
            {
                animator.SetTrigger("Attack1");
            }
        }
        else
        {
            idleCooldown -= Time.deltaTime;
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Attack1");

    }
}
