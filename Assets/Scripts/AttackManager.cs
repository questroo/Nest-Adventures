using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public float damage = 50.0f;
    public bool hasBeenHit { get; set; }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            hasBeenHit = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            Debug.Log("dealt damage to boss");

            other.GetComponent<EnemyStat>().TakeDamage(damage);

            hasBeenHit = true;
        }
    }
}
