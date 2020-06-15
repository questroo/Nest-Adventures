using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathBehaviour : StateMachineBehaviour
{
    public float startTime = 10.0f;
    float currentTime;
    EnemyStat enemy;
    Rigidbody rb;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponentInParent<EnemyStat>();
        rb = animator.GetComponentInParent<Rigidbody>();
        currentTime = startTime;
        animator.SetInteger("Die", 0);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            enemy.Die();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
