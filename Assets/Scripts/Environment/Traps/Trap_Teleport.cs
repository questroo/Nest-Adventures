using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Teleport : MonoBehaviour
{
    public Transform[] teleportLocations;

    public PlayerDungeonTester player;

    public void TriggerTrap()
    {
        int limit = teleportLocations.Length;
        int randomPlace = Random.Range(0, limit); // WARNING - Not length-1 because Range is between min(inclusive) and max(EXCLUSIVE)

        player.transform.position = teleportLocations[randomPlace].position;
        player.transform.rotation = teleportLocations[randomPlace].rotation;
    }
}