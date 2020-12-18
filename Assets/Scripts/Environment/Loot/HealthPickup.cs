using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthValue;
    public float spawnForce = 10.0f;

    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(!rb)
        {
            Debug.LogError("No rigidbody on this health pickup!");
        }
        else
        {
            rb.velocity = (Random.onUnitSphere * spawnForce) + (Vector3.up * 2.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SoundManager.PlaySound(SoundManager.Sound.player2_get_hit);
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(-1.0f * healthValue);
            Destroy(gameObject);
        }
    }
}