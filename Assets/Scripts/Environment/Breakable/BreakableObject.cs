using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float objectHealth = 3.0f;
    private float health;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    private void Start()
    {
        hitsLeft = hitsToBreak;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Weapon"))
        {
            hitsLeft--;
            if (hitsLeft <= 0)
            {
                DestroyThisBreakable();
            }
        }
    }

    public void DestroyThisBreakable()
    {
        // TODO - implement polish for destruction (effects, shaders etc.)
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}