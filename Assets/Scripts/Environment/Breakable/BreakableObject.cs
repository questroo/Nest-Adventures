using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField]
    private float hitsToBreak = 3.0f;
    private float hitsLeft;

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
}