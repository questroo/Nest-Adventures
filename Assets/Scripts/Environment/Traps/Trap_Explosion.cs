using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Explosion : MonoBehaviour
{
    public float bossExplosionDamage = 10.0f;
    public float playerExplosionDamage = 30.0f;

    public bool isTriggered = false;

    Collider thisCollider;

    bool bossIsHit = false;
    bool playerIsHit = false;

    float t = 0.0f;
    float scaleTime = 0.1f;

    float minScale = 1.0f;
    float maxScale = 12.0f;

    float scaleValue = 0.0f;

    private void Start()
    {
        thisCollider = GetComponent<Collider>();
        thisCollider.enabled = false;
    }

    private void Update()
    {
        if(isTriggered)
        {
            t += Time.deltaTime;
            if (t >= scaleTime)
            {
                t = scaleTime;
                Destroy(this.gameObject);
            }
            scaleValue = Mathf.Lerp(minScale, maxScale, t / scaleTime);

            transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        }
    }

    public void TriggerTrap()
    {
        isTriggered = true;
        thisCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!playerIsHit && other.CompareTag("Player"))
        {
            playerIsHit = true;
            other.GetComponent<PlayerDungeonTester>().TakeDamage(playerExplosionDamage);
        }
        else if(!bossIsHit && other.CompareTag("Boss"))
        {
            bossIsHit = true;
            // TODO - Boss takes damage
        }
        else if(other.CompareTag("Breakable"))
        {
            other.GetComponent<BreakableObject>().DestroyThisBreakable();
        }
    }
}