using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public enum TrapType
    {
        ExplosionTrap,
        PlacementTrap,
        PlacementStatModTrap,
        SpikeTrap,
        StatModTrap,
        TeleportTrap,
        ArrowTrap
    }

    Trap_ExplosionSpawner explosionTrap;
    Trap_Placement placementTrap;
    Trap_Spike spikeTrap;
    Trap_StatMod statModTrap;
    Trap_Teleport teleportTrap;
    Trap_Arrow arrowTrap;

    public TrapType trapType = TrapType.SpikeTrap;

    [Tooltip("Can Reset specifies if a trap is triggerable more than once")]
    public bool canReset = true;
    [Tooltip("Time it takes for the trap to go from tripped position to reset position")]
    public float resetDelay = 10.0f;
    [Tooltip("Time it waits before triggering the trap")]
    public float triggerDelay = 0.3f;

    // Trap Currents
    bool isReset = true;


    private void Start()
    {
        switch (trapType)
        {
            case TrapType.ExplosionTrap:
                explosionTrap = GetComponentInChildren<Trap_ExplosionSpawner>();
                if (!explosionTrap)
                    Debug.LogError("Trap_Explosion script can't be found even though this is marked as a Explosion Trap!!");
                break;

            case TrapType.PlacementTrap:
                placementTrap = GetComponentInChildren<Trap_Placement>();
                if (!placementTrap)
                    Debug.LogError("Trap_Placement script can't be found even though this is marked as a Placement Trap!!");
                break;

            case TrapType.PlacementStatModTrap:
                placementTrap = GetComponentInChildren<Trap_Placement>();
                statModTrap = GetComponentInChildren<Trap_StatMod>();
                if (!placementTrap)
                    Debug.LogError("Trap_Placement script can't be found even though this is marked as a Placement Trap!!");
                if (!statModTrap)
                    Debug.LogError("Trap_StatMod script can't be found even though this is marked as a statMod Trap!!");
                break;

            case TrapType.SpikeTrap:
                spikeTrap = GetComponentInChildren<Trap_Spike>();
                if (!spikeTrap)
                    Debug.LogError("Trap_Spike script can't be found even though this is marked as a Spike Trap!!");
                break;

            case TrapType.StatModTrap:
                statModTrap = GetComponentInChildren<Trap_StatMod>();
                if (!statModTrap)
                    Debug.LogError("Trap_StatMod script can't be found even though this is marked as a StatMod Trap!!");
                break;

            case TrapType.TeleportTrap:
                teleportTrap = GetComponentInChildren<Trap_Teleport>();
                if (!teleportTrap)
                    Debug.LogError("Trap_Teleport script can't be found even though this is marked as a Teleport Trap!!");
                break;

            case TrapType.ArrowTrap:
                arrowTrap = GetComponent<Trap_Arrow>();
                if (!arrowTrap)
                    Debug.LogError("Trap_Arrow script can't be found even though this is marked as a Teleport Trap!!");
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isReset && (other.CompareTag("Player") || other.CompareTag("MeleeCharacter") || other.CompareTag("RangedCharacter")))
        {
            isReset = false;
            StartCoroutine(CountDownUntilTrigger());
        }
    }

    IEnumerator CountDownUntilTrigger()
    {
        yield return new WaitForSeconds(triggerDelay);
        TriggerTrap();
        StartCoroutine(CountDownUntilReset());
    }

    IEnumerator CountDownUntilReset()
    {
        yield return new WaitForSeconds(resetDelay);
        isReset = true;

        if (trapType == TrapType.ExplosionTrap)
            explosionTrap.ResetTrap();
    }



    private void TriggerTrap()
    {
        switch (trapType)
        {
            case TrapType.ExplosionTrap:
                explosionTrap.TriggerTrap();
                break;

            case TrapType.PlacementTrap:
                placementTrap.TriggerTrap();
                break;

            case TrapType.PlacementStatModTrap:
                placementTrap.TriggerTrap();
                statModTrap.TriggerTrap();
                break;

            case TrapType.SpikeTrap:
                spikeTrap.TriggerTrap();
                break;

            case TrapType.StatModTrap:
                statModTrap.TriggerTrap();
                break;

            case TrapType.TeleportTrap:
                teleportTrap.TriggerTrap();
                break;

            case TrapType.ArrowTrap:
                arrowTrap.TriggerTrap();
                break;
        }
    }
}