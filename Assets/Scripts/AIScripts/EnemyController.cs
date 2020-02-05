using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float attackRadius = 3f;
    public float minDistance = 1.0f;
    public int points = 0;
    public float speed = 2.0f; 
    public GameObject[] waypoint;

    private bool isMoving = false;
    private Transform target;
    private NavMeshAgent agent;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, waypoint[points].transform.position);
        isMoving = true;

        if (isMoving)
        {
            if(distance > minDistance)
            {
                Move();
            }
            else
            {
                if(points + 1 == waypoint.Length)
                {
                    points = 0;
                }
                else
                {
                    points++;
                }
            }
        }
        
    }

    private void Move()
    {
        transform.LookAt(waypoint[points].transform.position);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
    }


}
