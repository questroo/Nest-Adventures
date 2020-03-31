using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private PlayerController playerController;
    public float damage = 50.0f;
    public bool canDealDamage = true;

    private void Start()
    {
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
}
