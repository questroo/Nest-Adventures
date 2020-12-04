using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ShowRoomTeleporter : MonoBehaviour
{
    public Transform[] roomTeleports;
    GameObject player;
    int roomNumber = 0;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        TeleportRoom(0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            roomNumber++;
            roomNumber = roomNumber % roomTeleports.Length;
            TeleportRoom(roomNumber);
        }
        else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            roomNumber--;
            if(roomNumber < 0)
            {
                roomNumber = roomTeleports.Length - 1;
            }
            roomNumber = roomNumber % roomTeleports.Length;
            TeleportRoom(roomNumber);
        }
    }

    void TeleportRoom(int roomNumber)
    {
        player.transform.position = roomTeleports[roomNumber].position;
    }
}