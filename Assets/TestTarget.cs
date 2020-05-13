using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour, IDamageable
{
    private float health = 100.0f;

    public float Health {
        get { return health; }
        set { health = value; }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Die();
    }
    public void Die()
    {
        if(Health <= 0.0f)
        {
            Debug.Log("die");
            FindObjectOfType<CameraController>().RemoveSelfFromList(this);
            Destroy(gameObject);
        }
    }
}
