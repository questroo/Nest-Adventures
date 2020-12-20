using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public ParticleSystem vfx_punchDust;
    public ParticleSystem vfx_onePunch;
    public ParticleSystem vfx_triplePunch;
    public void PunchEffectOne()
    {
        vfx_onePunch.Emit(1);
    }

    public void PunchEffectTwo()
    {
        vfx_triplePunch.Play();
    }
}