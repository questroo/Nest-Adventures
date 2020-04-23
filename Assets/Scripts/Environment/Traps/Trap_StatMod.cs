using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_StatMod : MonoBehaviour
{
    public enum TrapState
    {
        ReadyToTrigger,     //  Ready to activate the trap
        Triggering,         //  currently activating the trap
        WaitingToReset,     //  waiting a delay before being reset
        Resetting,          //  currently resetting the trap
        Dead                //  is no longer able to be activated
    };

    [System.NonSerialized]
    public StatusEffect statusEffect;

    [Tooltip("Which stat do you want the value to apply")]
    public StatToModify statToModify = StatToModify.Attack;
    [Tooltip("Stat Amount is applied over the duration. A Stat amount of 10 with a duration of 5 will apply 2 per second.")]
    public float statAmount = 1.0f;
    [Tooltip("The time it takes for the status to wear off. A value of -1 is permanent.")]
    public float statDuration = 3.0f;

    public float timeToFullyLift = 0.05f;
    public float delayBeforeReset = 1.5f;
    public Vector3 downPosition;
    public Vector3 upPosition;

    private TrapState trapState = TrapState.ReadyToTrigger;
    private float t = 0.0f;

    private PlayerStats playerStatsHandle;

    void Start()
    {
        //statusEffect = new StatusEffect();
        statusEffect.statToMod = statToModify;
        statusEffect.durationRemaining = statDuration;
        statusEffect.amount = statAmount;
    }

    private void Update()
    {
        UpdateTrapMovement();
    }

    public void TriggerTrap()
    {
        trapState = TrapState.Triggering;
    }

    void MoveTrap()
    {
        transform.localPosition = Vector3.Lerp(downPosition, upPosition, (t / timeToFullyLift));
    }

    public bool IsTriggerable()
    {
        if (trapState == TrapState.ReadyToTrigger)
            return true;
        else
            return false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!playerStatsHandle)
            {
                playerStatsHandle = other.GetComponent<PlayerStats>();
            }
            //playerStatsHandle.ModifyStat(statToModify, statDuration, statAmount);
            playerStatsHandle.ModifyStat(statusEffect);
        }
    }

    private void UpdateTrapMovement()
    {
        if (trapState == TrapState.Triggering)
        {
            t += Time.deltaTime;
            if (t >= timeToFullyLift)
            {
                t = timeToFullyLift;
                trapState = TrapState.WaitingToReset;

                MoveTrap();

                t = delayBeforeReset;
            }
            else
            {
                MoveTrap();
            }
        }
        else if (trapState == TrapState.WaitingToReset)
        {
            t -= Time.deltaTime;
            if (t <= 0)
            {
                trapState = TrapState.Resetting;
                t = timeToFullyLift;
            }
        }
        else if (trapState == TrapState.Resetting)
        {
            t -= Time.deltaTime;
            if (t <= 0)
            {
                t = 0;
                trapState = TrapState.ReadyToTrigger;
            }

            MoveTrap();
        }
    }
}