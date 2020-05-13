using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float projectileDamage = 10.0f;
    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject.GetComponent<IDamageable>();
        if(target != null)
        {
            Debug.Log("Hitting target");
            target.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}
