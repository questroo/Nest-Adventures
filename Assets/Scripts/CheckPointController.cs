using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    private PlayerStats player;

    private void Start()
    {
        player = ServiceLocator.Get<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.reachCheakPoint = true;
        }
    }
}
