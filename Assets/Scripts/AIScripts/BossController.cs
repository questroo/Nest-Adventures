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

    private float meleeAttackRadius;
    private float damageCooldown;
    private float damageCooldownStart = 2.0f;

    public Transform hitPoint;
    public LayerMask terrainLayer;
    public float hitRange;
    public float lookRadius = 5.0f;
    public float dashRadius = 5.0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        enemyStat = GetComponent<EnemyStat>();
        agent = GetComponent<NavMeshAgent>();
        meleeAttackRadius = agent.stoppingDistance;
        damageCooldown = damageCooldownStart;
    }

    // void Update()
    // {
    //
    // }

    public Transform GetTarget()
    {
        return target;
    }

    public float GetMeleeRadius()
    {
        return meleeAttackRadius;
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
        float distance = Vector3.Distance(target.position, transform.position);

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
        rb.AddForce(transform.forward * 10);

        Collider[] hitTerrain = Physics.OverlapSphere(hitPoint.position, hitRange, terrainLayer);

        foreach (Collider terrain in hitTerrain)
        {
            if (damageCooldown == 2.0f)
            {
                enemyStat.TakeDamage(3);
                damageCooldown -= Time.deltaTime;
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
