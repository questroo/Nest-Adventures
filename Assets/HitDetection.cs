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
            Debug.Log("Hitting target");
            if (SecondWaveSpawner.hasBossDied)
            {
                target.TakeDamage(comboDamage.comboDamage * 2);
            }
            else
            {
                target.TakeDamage(comboDamage.comboDamage);
            }
            Destroy(gameObject);
        }
    }
}
