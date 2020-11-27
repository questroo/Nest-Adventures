using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_ArrowProjectile : MonoBehaviour
{
    public float arrowDamage = 10.0f;
    [Tooltip("Time from instantiation to wait until the game deletes this object.")]
    public float lifeTime = 10.0f;

    PlayerStats playerScript;
    float lifetimeLeft = 0.0f;
    bool stuck = false;

    [SerializeField]
    private ParticleSystem hitEffect;

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
            StickToObject(other);
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;

            if (other.CompareTag("Player") || other.CompareTag("RangedCharacter") || other.CompareTag("MeleeCharacter"))
            {
                SoundManager.PlaySound(SoundManager.Sound.player2_arrow);
                hitEffect.Play();
                playerScript = other.GetComponentInParent<PlayerStats>();

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

    private void StickToObject(Collider other)
    {
        Vector3 scale = other.transform.localScale;

        Vector3 reverseScale = new Vector3(1f / scale.x, 1f / scale.y, 1f / scale.z);

        Vector3 myScale = transform.localScale;
    }
}