using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunningBehaviour : StateMachineBehaviour
{
    public float speed;

    private Transform playerPos;
    private BossController boss;
    private Rigidbody rb;
    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = animator.GetComponent<Rigidbody>();
        boss = animator.GetComponent<BossController>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        //Vector3 target = new Vector3(playerPos.position.x, animator.transform.position.y);
        rb.transform.position += rb.transform.forward * speed * Time.deltaTime;
            
        if(Vector3.Distance(playerPos.position, animator.transform.position) <= boss.attackRadius)
        {
            animator.SetTrigger("attack");
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
