using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [System.Serializable]
    public struct NamedEffect
    {
        public string name;
        public ParticleSystem effect;
    }
    public NamedEffect[] effects;

    Dictionary<string, ParticleSystem> effectDictionary = new Dictionary<string, ParticleSystem>();

    // On Start, the values input in the inspector will be converted from a list into a dictionary for easier lookup.
    // NOTE: If any number of particles have the same name, only the first will be added.
    private void Start()
    {
        foreach (NamedEffect effect in effects)
        {
            if (effectDictionary.ContainsKey(effect.name))
            {
                Debug.LogError("[Effect Manager Script] for [" + name + "] is trying to add two effects with the same key! Effect has not been added.");
            }
            else
            {
                effectDictionary.Add(effect.name, effect.effect);
            }
        }
    }

    public void PlayEffect(string effectName)
    {
        ParticleSystem thisEffect;
        if(effectDictionary.TryGetValue(effectName, out thisEffect))
        {
            thisEffect.Play();
        }
    }
}