using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthValue;

    Rigidbody rb;
    public float spawnForce = 10.0f;
    public float spawnRadius = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(!rb)
        {
            Debug.LogError("No rigidbody on this health pickup!");
        }
        else
        {
            rb.AddExplosionForce(spawnForce, Random.onUnitSphere, spawnRadius, 1.0f, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        if(collision.gameObject.CompareTag("Player"))
        {
            int i = 0;
            int j = i + 2;
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(-1.0f * healthValue);
            Destroy(gameObject);
        }
    }
}