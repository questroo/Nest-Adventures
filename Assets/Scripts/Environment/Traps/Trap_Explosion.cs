using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Explosion : MonoBehaviour
{
    public float bossExplosionDamage = 10.0f;
    public float enemyExplosionDamage = 20.0f;
    public float playerExplosionDamage = 30.0f;

    float t = 0.0f;
    float scaleTime = 0.1f;
    float minScale = 1.0f;
    float maxScale = 12.0f;
    float scaleValue = 0.0f;

    bool bossIsHit = false;
    bool playerIsHit = false;

    void Update()
    {
        t += Time.deltaTime;
        if (t >= scaleTime)
        {
            t = scaleTime;
            Destroy(gameObject);
        }
        scaleValue = Mathf.Lerp(minScale, maxScale, t / scaleTime);

        transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!playerIsHit && other.CompareTag("Player") || other.CompareTag("RangedCharacter") || other.CompareTag("MeleeCharacter"))
        {
            playerIsHit = true;
            other.GetComponentInParent<PlayerStats>().TakeDamage(playerExplosionDamage);
        }
        else if (!bossIsHit && other.CompareTag("Boss"))
        {
            bossIsHit = true;
            // TODO - Boss Damage
        }
        else if (other.CompareTag("Breakable"))
        {
            other.GetComponent<BreakableObject>().DestroyThisBreakable();
        }
        else if (other.CompareTag("RangedEnemy"))
        {
            other.GetComponent<RangedEnemy>().TakeDamage(enemyExplosionDamage);
        }
    }
}