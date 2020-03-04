﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeRBehaviour : StateMachineBehaviour
{
    private BossController boss;
    private Rigidbody rb;
    private Transform playerPos;
    private float timer;
   
    public float speed;
    public float maxTimer = 5;
    public float minTimer = 0;
 
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
        float step = 2.0f * Time.deltaTime;
        float distance = Vector3.Distance(playerPos.position, rb.transform.position);

        rb.transform.position += rb.transform.right * speed * Time.deltaTime;
        
        if(timer <= 1)
        {
            animator.SetTrigger("strafeToLeft");
        }
        if (distance > boss.gapCloserRadius && distance > boss.meleeAttackRadius && timer <= 4)
        {
            animator.SetTrigger("run");
        }
        if (distance <= boss.gapCloserRadius && distance > boss.meleeAttackRadius && timer <= 3)
        {
            animator.SetTrigger("jumpAttack");
           
        }
        if(distance <= boss.meleeAttackRadius && timer <= 0)
        {
            animator.SetTrigger("attack");
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
