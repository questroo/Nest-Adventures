using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_ExplosionSpawner : MonoBehaviour
{
    public GameObject explosionObject;

    public float bossExplosionDamage = 10.0f;
    public float playerExplosionDamage = 30.0f;

    MeshRenderer thisMeshRenderer;
    bool isReady = true;

    private void Start()
    {
        thisMeshRenderer = GetComponent<MeshRenderer>();
        thisMeshRenderer.material.color = Color.blue;
    }

    public void TriggerTrap()
    {
        thisMeshRenderer.material.color = Color.red;

        Instantiate(explosionObject, transform);
        //isReady = false;
    }

    public void ResetTrap()
    {
        thisMeshRenderer.material.color = Color.blue;
        //isReady = true;
    }    
}