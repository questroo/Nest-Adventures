using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IceBall : MonoBehaviour
{
    public float maxSpeed = 10.0f;
    public float damage = 20.0f;

    Transform target;

    float timing = 0.0f;
    float maxTiming = 10.0f;

    private void Start()
    {

    }

    private void Update()
    {
        transform.Translate(Vector3.forward.normalized * Time.deltaTime * maxSpeed);
        Debug.Log(transform.forward);

        timing += Time.deltaTime;
        if (timing >= maxTiming)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyStat>().TakeDamage(damage);
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            // Ignore but do not destroy
        }
        else
        {
            // If it hits a wall, or something like that
            Destroy(gameObject);
        }
    }
}