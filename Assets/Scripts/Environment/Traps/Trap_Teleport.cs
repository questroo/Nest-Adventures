using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Teleport : MonoBehaviour
{
    public GameObject teleportPositionPrefab;
    public List<Transform> teleportLocations;

    private GameObject player;

    public void Awake()
    {
        player = FindObjectOfType<PlayerStats>().gameObject;
    }

    public void TriggerTrap()
    {
        int limit = teleportLocations.Count;

        int randomPlace = Random.Range(0, limit); // 0-limit EXCLUSIVE... Do not subtract one from length to get limit

        player.transform.position = teleportLocations[randomPlace].position;
        player.transform.rotation = teleportLocations[randomPlace].rotation;
    }

    public void MakeNewTeleportPosition()
    {
        teleportLocations.Add(Instantiate(teleportPositionPrefab, transform).transform);
    }
}