using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackManager : MonoBehaviour
{
    private EnemyStat enemyStat;

    void Start()
    {
        enemyStat = GetComponent<EnemyStat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().TakeDamage(enemyStat.bossDamage);
        }
    }
}