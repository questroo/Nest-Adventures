using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeRBehaviour : StateMachineBehaviour
{
    private BossController boss;
    private Rigidbody rb;
    private Transform playerPos;
    private float timer;

    public float speed;
    public float minTimer;
    public float maxTimer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
        rb = animator.GetComponent<Rigidbody>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = Random.Range(minTimer, maxTimer);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        rb.transform.position += rb.transform.right * speed * Time.deltaTime;
        
        if(timer <= 0)
        {
            animator.SetTrigger("strafeToLeft");
        }
        else
        {
           timer -= Time.deltaTime;
        }

        if (Vector3.Distance(playerPos.position, rb.transform.position) >= boss.gapCloserRadius)
        {

        }
        else if (Vector3.Distance(playerPos.position, rb.transform.position) <= boss.meleeAttackRadius)
        {
            animator.SetTrigger("attack");
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
