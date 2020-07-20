﻿using System.Collections;
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
        boss = animator.GetComponentInParent<BossController>();
        rigidBody = animator.GetComponentInParent<Rigidbody>();
        dashTime = startDashTime;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(boss.GetTarget().position, rigidBody.position);

        boss.LookAtPlayer();
        
        if (distance <= boss.GetMeleeRadius())
        {
            animator.SetTrigger("Attack1");
            dashTime = startDashTime;
        }
        else if (distance <= boss.lookRadius)
        {
            animator.SetTrigger("Run");
            dashTime = startDashTime;
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Attack1");
    }
}