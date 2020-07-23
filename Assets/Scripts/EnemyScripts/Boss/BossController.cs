using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    Rigidbody rb;
    EnemyStat enemyStat;
    
    private float damageCooldown;
    private float damageCooldownStart = 2.0f;

    public Transform hitPoint;
    public LayerMask terrainLayer;
    public float hitRange;
    public float meleeAttackRadius = 5.0f;
    public float lookRadius = 5.0f;
    public float dashRadius = 5.0f;

    public float damageFromObstacle = 10.0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        enemyStat = GetComponent<EnemyStat>();
        agent = GetComponent<NavMeshAgent>();
        damageCooldown = damageCooldownStart;
    }

    public Transform GetTarget()
    {
        return target;
    }

    public float GetDMGCooldown(float reset)
    {
        return damageCooldown = reset;
    }

    public void LookAtPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);
    }

    public void Movement()
    {
        LookAtPlayer();

        agent.SetDestination(target.position);
        agent.isStopped = false;

    }

    public void StopMovement()
    {
        agent.isStopped = true;
    }

    public void Dash()
    {
        rb.AddForce(transform.forward * 20);

        Collider[] hitTerrain = Physics.OverlapSphere(hitPoint.position, hitRange, terrainLayer);

        foreach (Collider terrain in hitTerrain)
        {
            if (damageCooldown == 2.0f)
            {
                enemyStat.TakeDamage(damageFromObstacle);
                damageCooldown -= 1;
            }
            else
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, dashRadius);

        Gizmos.DrawWireSphere(hitPoint.position, hitRange);
    }


}
