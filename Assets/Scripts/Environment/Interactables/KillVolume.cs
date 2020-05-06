using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVolume : MonoBehaviour
{
    ScreenFadeToBlack faderHandle;
    void Start()
    {
        faderHandle = FindObjectOfType<ScreenFadeToBlack>();
        if (!faderHandle)
            Debug.LogError("Kill volumes cannot find a screen fade object in the scene!!!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            faderHandle.TriggerFade();
        }
    }
}