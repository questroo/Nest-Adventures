using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Transform[] attackPoint;
    private EnemyStat enemyStat;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;


    private void Start()
    {
        enemyStat = GetComponentInParent<EnemyStat>();
    }

    public void Attack1()
    {
        
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint[0].position, attackRange, playerLayer);

        foreach (Collider player in hitPlayer)
        {
            Debug.Log("we hit " + player.name);
            player.GetComponentInParent<PlayerStats>().TakeDamage(enemyStat.bossDamage);
            
        }
    }

    public void Attack2()
    {

        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint[1].position, attackRange, playerLayer);

        foreach (Collider player in hitPlayer)
        {
            Debug.Log("we hit " + player.name);
            player.GetComponentInParent<PlayerStats>().TakeDamage(enemyStat.bossDamage);

        }
    }

    public void Attack3()
    {

        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint[2].position, attackRange, playerLayer);

        foreach (Collider player in hitPlayer)
        {
            Debug.Log("we hit " + player.name);
            player.GetComponentInParent<PlayerStats>().TakeDamage(enemyStat.bossDamage);

        }

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint[0].position, attackRange);
    }
}
