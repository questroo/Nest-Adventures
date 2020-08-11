using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField]
    GameObject explosionEffect;
    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
    }
}
