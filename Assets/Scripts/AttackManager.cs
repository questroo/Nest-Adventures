using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackManager : MonoBehaviour
{
    PlayerControls attackControls;
    private PlayerController playerController;
    private Animator weaponAnimator;
    public float damage = 50.0f;
    public bool canDealDamage = true;

    private void Awake()
    {
        attackControls = new PlayerControls();

        attackControls.ActionMap.Attack.performed += ctx => Attack();
    }
    private void Start()
    {
        weaponAnimator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            playerController.weaponCollider.enabled = false;
            Debug.Log("dealt damage to boss");
            other.GetComponent<EnemyStat>().TakeDamage(damage);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
        }
    }

    void Attack()
    {
        weaponAnimator.SetTrigger("Attack");
    }

    private void OnEnable()
    {
        attackControls.ActionMap.Enable();
    }

    private void OnDisable()
    {
        attackControls.ActionMap.Disable();
    }
}
