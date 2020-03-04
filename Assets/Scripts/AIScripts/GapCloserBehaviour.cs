using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapCloserBehaviour : StateMachineBehaviour
{
    BossController boss;
    Rigidbody rb;
    public float speed;
    private float time;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
        rb = animator.GetComponent<Rigidbody>();
        time = 1.0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (time > 0.0f)
        {
            animator.transform.position += rb.transform.forward * speed * Time.deltaTime;
        }

        time -= Time.deltaTime;




    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
