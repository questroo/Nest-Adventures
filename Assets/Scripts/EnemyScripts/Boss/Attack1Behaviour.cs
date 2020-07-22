using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack1Behaviour : StateMachineBehaviour
{
    private BossController boss;
    private Rigidbody rigidBody;
    private float moveTime = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
        rigidBody = animator.GetComponentInParent<Rigidbody>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (moveTime >= 0.06f && moveTime <= 1.04f)
        {
            rigidBody.transform.Translate((Vector3.forward + Vector3.forward) * Time.deltaTime);
            Debug.Log("is moving ");
        }

        moveTime += Time.deltaTime;

        //Debug.Log("moveTime: " + moveTime);

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Idle");
        moveTime = 0.0f;
    }
}
