using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBehaviour : StateMachineBehaviour
{
    public int startHitcount;
    private int hitCount = 3;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (hitCount <= 0)
        {
            animator.SetTrigger("Counter");
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
        hitCount--;

        Debug.Log("Hitcount " + hitCount);
    }


}
