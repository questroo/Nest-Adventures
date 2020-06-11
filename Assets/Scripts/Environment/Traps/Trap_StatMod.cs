using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public enum StatusType
{
    Poison,
    AttackDown
}

public class Trap_StatMod : MonoBehaviour
{
    public StatusType trapStatusEffect = StatusType.Poison;
    public float strength;
    public float duration;
    public VisualEffect visualEffect;
    float effectDuration = 2.0f;

    PlayerStats statsHandle;

    private void Start()
    {
        visualEffect.Stop();
        statsHandle = FindObjectOfType<PlayerStats>();
        if(!statsHandle)
        {
            Debug.LogError("PlayerStats script not found in scene!! StatMod trap " + transform.parent.name + " will not work!");
        }
    }

    public void TriggerTrap()
    {
        statsHandle.ModifyStatus(trapStatusEffect, strength, duration);
        StartCoroutine(PlayEffect());
    }

    IEnumerator PlayEffect()
    {
        visualEffect.Play();
        yield return new WaitForSeconds(effectDuration);
        visualEffect.Stop();
    }
}