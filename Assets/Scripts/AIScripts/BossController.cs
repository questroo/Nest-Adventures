using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    private float meleeAttackRadius;
    public float lookRadius = 5.0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        meleeAttackRadius = agent.stoppingDistance;
    }

    //void Update()
    //{
    //    if (target != null)
    //    {
    //        Movement();
    //    }
    //}

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, meleeAttackRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
