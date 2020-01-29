using UnityEngine;

public class AttackController : MonoBehaviour
{
    public AnimationClip animClip;
    public Animator animator;
    public AnimationEvent evt;

    private void Start()
    {
        evt = new AnimationEvent();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("AttackKeyPressed");
        }
    }
}
