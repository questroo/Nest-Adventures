using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    Rigidbody rb;

    private float meleeAttackRadius;
    public float lookRadius = 5.0f;
    public float dashRadius = 5.0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        meleeAttackRadius = agent.stoppingDistance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            Dash();
    }

    public Transform GetTarget()
    {
        return target;
    }

    public float GetMeleeRadius()
    {
        return meleeAttackRadius;
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
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }


    }

    public void Dash()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        LookAtPlayer();
        
        rb.AddForce(transform.forward * 500);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, dashRadius);
    }
}
