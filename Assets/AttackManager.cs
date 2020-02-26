using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private PlayerController playerController;
    private Collider attackColliders;
    public float damage = 50.0f;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        attackColliders = GetComponentInChildren<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boss") && playerController.GetAttackBool() == false)
        {
            Debug.Log("boss should take damage");
            other.GetComponent<EnemyStat>().TakeDamage(damage);
        }
    }
}
