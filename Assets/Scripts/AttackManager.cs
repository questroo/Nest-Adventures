using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private PlayerController playerController;
    private Collider attackColliders;
    public float damage = 50.0f;

    public bool hasBeenHit { get; set; }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        attackColliders = GetComponentInChildren<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenHit)
        {
            if (other.CompareTag("Boss"))
            {
                Debug.Log("dealt damage to boss");
                hasBeenHit = true;
                other.GetComponent<EnemyStat>().TakeDamage(damage);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            hasBeenHit = false;
        }
    }
}
