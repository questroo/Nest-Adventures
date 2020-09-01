using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBehaviour : StateMachineBehaviour
{
    BossController boss;
    public int startHitcount;
    private int hitCount = 3;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponentInParent<BossController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(boss.GetTarget().position, animator.transform.position);
        
        if (hitCount <= 0)
        {
            animator.SetTrigger("Counter");
            hitCount = startHitcount;
        }
        else if(hitCount <= 0 && distance > boss.meleeAttackRadius)
        {
            animator.SetTrigger("Attack1");
            hitCount = startHitcount;
        }
        else
        {
            animator.SetTrigger("Idle");
           
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Counter");
        animator.ResetTrigger("Hit");
        animator.ResetTrigger("Attack1");

        hitCount--;

        Debug.Log("Hitcount " + hitCount);
    }


}
