using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Spike : MonoBehaviour
{
    public enum TrapState
    {
        ReadyToTrigger,     //  Ready to activate the trap
        Triggering,         //  currently activating the trap
        WaitingToReset,     //  waiting a delay before being reset
        Resetting,          //  currently resetting the trap
        Dead                //  is no longer able to be activated
    };

    public float trapDamage = 10.0f;
    public float timeToFullyLift = 0.1f;
    public float delayBeforeReset = 1.5f;
    public Vector3 downPosition;
    public Vector3 upPosition;

    private TrapState trapState = TrapState.ReadyToTrigger;
    private float t = 0.0f;

    private void Update()
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
        if(other.CompareTag("Player") || other.CompareTag("Bertha") || other.CompareTag("Tanjiro"))
        {
            other.GetComponentInParent<PlayerStats>().TakeDamage(10.0f);
        }
    }
}