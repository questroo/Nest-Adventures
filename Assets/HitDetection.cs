using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public ComboDamage comboDamage;
    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject.GetComponent<IDamageable>();
        if (target != null)
        {
            if (SecondWaveSpawner.hasBossDied)
            {
                target.TakeDamage(comboDamage.comboDamage * 2);
            }
            else
            {
                target.TakeDamage(comboDamage.comboDamage);
            }
            Debug.Log("Projectile should die");
        }
        Destroy(gameObject);
    }
}
