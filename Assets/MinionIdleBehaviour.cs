using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionIdleBehaviour : StateMachineBehaviour
{
    EnemyController enemy;
    Rigidbody rigidBody;
    private float dashTime;
    private float startDashTime = 5.0f;
    private float idleCooldown;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponentInParent<EnemyController>();
        rigidBody = animator.GetComponentInParent<Rigidbody>();
        idleCooldown = enemy.startIdleCooldown;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(enemy.GetTarget().position, animator.transform.position);

        enemy.Movement();

        if (idleCooldown <= 0f)
        {
            if (distance <= enemy.attackRadius)
            {
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            idleCooldown -= Time.deltaTime;
            Debug.Log(idleCooldown + "s before next attack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
