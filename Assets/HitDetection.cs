using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public ComboDamage comboDamage;
    public GameObject iceParticle;
    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject.GetComponent<EnemyStat>();
        if (target != null)
        {
            var particle = Instantiate(iceParticle, transform.position, Quaternion.identity) as GameObject;
            Destroy(particle, 0.6f);
            if (SecondWaveSpawner.hasBossDied)
            {
                target.TakeDamage(comboDamage.comboDamage * 2);
            }
            else
            {
                target.TakeDamage(comboDamage.comboDamage);
            }
        }
        Destroy(gameObject);
    }
}
