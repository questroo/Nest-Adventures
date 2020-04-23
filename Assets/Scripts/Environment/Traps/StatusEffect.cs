using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum StatToModify
{
    Stun,
    Attack,
    Defence,
    Speed
}

[System.Serializable]
public class StatusEffect : MonoBehaviour
{
    public StatToModify statToMod;
    public float durationRemaining;
    public float amount;
}