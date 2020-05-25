using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    public Transform moveSpots;

    private int randomSpot;
    private float waitTime;
    private float minX = -42.44f;
    private float minZ = -10.0f;
    private float maxX = -24.5f;
    private float maxZ = 10.0f;

    void Start()
    {
        waitTime = startWaitTime;
        moveSpots.position = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots.position, speed * Time.deltaTime);
        float spotDistance = Vector3.Distance(transform.position, moveSpots.position);

        //LookAtDirection();
        transform.LookAt(moveSpots.position);

        if (spotDistance < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpots.position = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
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
        Vector3 direction = (moveSpots.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);
    }
}
