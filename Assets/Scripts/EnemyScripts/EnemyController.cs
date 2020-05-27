﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    public Transform[] moveSpots;

    private NavMeshAgent agent;
    private int randomSpot;
    private float waitTime;
    //private float minX = -42.44f;
    //private float minZ = -10.0f;
    //private float maxX = -24.5f;
    //private float maxZ = 10.0f;

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        agent = GetComponent<NavMeshAgent>();
        //moveSpots.position = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        //transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        agent.SetDestination(moveSpots[randomSpot].position);
        float spotDistance = Vector3.Distance(transform.position, moveSpots[randomSpot].position);
        LookAtDirection();

        if (agent.stoppingDistance <= 3.0f)
        {
            if (waitTime <= 0)
            {
                //moveSpots.position = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void LookAtDirection()
    {
        Vector3 direction = (moveSpots[randomSpot].position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);
    }
}
