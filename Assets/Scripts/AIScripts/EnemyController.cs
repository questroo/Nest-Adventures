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

    private float playerDistance;
    private float distance;
    private bool isMoving = false;
    private GameObject target;
    private NavMeshAgent agent;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("pseudoPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, waypoint[points].transform.position);
        playerDistance = Vector3.Distance(target.transform.position, transform.position);

        isMoving = true;

        if (isMoving)
        {
            if (distance > minDistance + 0.001f)
            {
                Move();

            }
            else
            {
                StartCoroutine("Pause");
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

    IEnumerator Pause()
    {

        //transform.position.Set(waypoint[points].transform.position.x, waypoint[points].transform.position.y, waypoint[points].transform.position.z);
        yield return new WaitForSeconds(0.5f);
        ++points;
        //points = Random.Range(0, waypoint.Length);

    }


}
