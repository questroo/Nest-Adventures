﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float projectileDamage = 10.0f;
    private void OnCollisionEnter(Collision collision)
    {
        var target = collision.transform.GetComponent<EnemyStat>();
        if (target)
        {
            target.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}