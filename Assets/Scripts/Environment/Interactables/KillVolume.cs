using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVolume : MonoBehaviour
{
    [Tooltip("Place a transform that this volume should teleport the player to if they fall in. If no position is chosen, player will teleport to the dungeon start.")]
    public Transform teleportPosition = null;

    ScreenFadeToBlack faderHandle;
    void Start()
    {
        faderHandle = FindObjectOfType<ScreenFadeToBlack>();
        if (!faderHandle)
            Debug.LogError("Kill volumes cannot find a screen fade object in the scene!!!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("RangedCharacter") || other.CompareTag("MeleeCharacter"))
        {
            if(faderHandle)
                faderHandle.TeleportPlayer(teleportPosition);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw Cube showing position of volume
        Gizmos.color = Color.magenta;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}