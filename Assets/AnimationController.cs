using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    PlayerControls controls;


    private Vector2 moveDirection;

    private Animator animator;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.ActionMap.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        controls.ActionMap.Move.canceled += ctx => moveDirection = Vector2.zero;

        //controls.ActionMap.Attack.performed += ctx => TriggerAttackAnimation();
    }
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<CharacterManager>().CheckSwapping())
        {
            RegetAnimator();

            animator.SetFloat("Blend", Mathf.Max(Mathf.Abs(moveDirection.x), Mathf.Abs(moveDirection.y)));
        }
        else
        {
            animator.SetFloat("Blend", 0.0f);
        }
    }
    public void TriggerAttackAnimation()
    {
        Debug.Log("Attack animation");
        animator.SetTrigger("Attack");
    }
    private void OnEnable()
    {
        controls.ActionMap.Enable();
    }
    private void OnDisable()
    {
        controls.ActionMap.Disable();
    }
    public void RegetAnimator()
    {
        animator = GetComponentInChildren<Animator>();
    }
}