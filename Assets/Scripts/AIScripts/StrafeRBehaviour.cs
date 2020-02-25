using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeRBehaviour : StateMachineBehaviour
{
    private BossController boss;
    private Rigidbody rb;
    private Transform playerPos;
    private float strafeTimer;
    private float rushTimer;
    private float attackTimer;
    private float minTimer = 0;
    private float maxTimer = 15;

    public float speed;
    public float strafeTimerNum;
    public float attackTimerNum;
    public float rushTimerNum;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();
        rb = animator.GetComponent<Rigidbody>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        strafeTimer = Random.Range(minTimer, maxTimer);
        attackTimer = Random.Range(minTimer, maxTimer);
        rushTimer = Random.Range(minTimer, maxTimer);

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        rb.transform.position += rb.transform.right * speed * Time.deltaTime;
        
        if(strafeTimer <= strafeTimerNum)
        {
            animator.SetTrigger("strafeToLeft");
        }
        if (Vector3.Distance(playerPos.position, rb.transform.position) > boss.gapCloserRadius && rushTimer <= rushTimerNum)
        {
            animator.SetTrigger("run");
        }
        if (Vector3.Distance(playerPos.position, rb.transform.position) <= boss.gapCloserRadius && attackTimer <= attackTimerNum)
        {
            animator.SetTrigger("jumpAttack");
        }
        else
        {
            strafeTimer -= Time.deltaTime;
            rushTimer -= Time.deltaTime;
            attackTimer -= Time.deltaTime;
        }

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
