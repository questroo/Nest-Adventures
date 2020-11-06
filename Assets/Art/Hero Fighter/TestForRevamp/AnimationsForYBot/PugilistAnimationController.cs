using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PugilistAnimationController : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 moveDirection;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Awake()
    {
        controls = new PlayerControls();

        controls.ActionMap.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        controls.ActionMap.Move.canceled += ctx => moveDirection = Vector2.zero;

        controls.ActionMap.DodgeRoll.performed += ctx => DodgeRoll();
    }


    void Update()
    {
        animator.SetFloat("Running", Mathf.Max(Mathf.Abs(moveDirection.x), Mathf.Abs(moveDirection.y)));
    }

    void DodgeRoll()
    {
        animator.Play("StartRoll");
    }
}