using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_StatMod : MonoBehaviour
{
    public StatusType trapStatusEffect = StatusType.Poison;
    public float strength;
    public float duration;

    PlayerStats statsHandle;

    private void Start()
    {
        statsHandle = FindObjectOfType<PlayerStats>();
        if(!statsHandle)
        {
            Debug.LogError("PlayerStats script not found in scene!! StatMod trap " + transform.parent.name + " will not work!");
        }
    }

    public void TriggerTrap()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            statsHandle.ModifyStatus(trapStatusEffect, strength, duration);
        }
    }
}