using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Teleport : MonoBehaviour
{
    public Transform[] teleportLocations;

    private Player player;

    public void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void TriggerTrap()
    {
        int limit = teleportLocations.Length-1;

        int randomPlace = Random.Range(0, limit);

        player.transform.position = teleportLocations[randomPlace].position;
        player.transform.rotation = teleportLocations[randomPlace].rotation;
    }
}