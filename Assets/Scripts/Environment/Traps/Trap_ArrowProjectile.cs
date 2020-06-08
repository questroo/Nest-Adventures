using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_ArrowProjectile : MonoBehaviour
{
    public float arrowDamage = 10.0f;
    [Tooltip("Time from instantiation to wait until the game deletes this object.")]
    public float lifeTime = 10.0f;

    PlayerDungeonTester playerScript;
    float lifetimeLeft = 0.0f;
    bool stuck = false;

    private void Start()
    {
        lifetimeLeft = lifeTime;
    }

    private void Update()
    {
        lifetimeLeft -= Time.deltaTime;
        if (lifetimeLeft <= 0)
        {
            Destroy(this.gameObject);
        }

        transform.LookAt(transform.position + GetComponent<Rigidbody>().velocity.normalized);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Trap") && !stuck)
        {
            stuck = true;
            transform.SetParent(other.transform); // Sticks to target hit
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;

            if (other.CompareTag("Player"))
            {
                playerScript = other.GetComponent<PlayerDungeonTester>();

                if (playerScript)
                {
                    playerScript.TakeDamage(arrowDamage);
                }
                else
                {
                    Debug.LogError("No player script found for " + name + " to interact with!");
                }
            }
        }
    }
}