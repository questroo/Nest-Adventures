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

    public void Init(Transform _target)
    {
        // Set up ball parameters within SorcererPlayerController
        target = _target;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(target != null)
        {
            transform.LookAt(target.position);
            Debug.Log("Looking at Target");
        }

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
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
            Destroy(gameObject);
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