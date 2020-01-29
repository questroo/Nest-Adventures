using UnityEngine;

public class AttackController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("AttackKeyPressed");
            animator.Play("Attack");
        }
    }
}
