﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController playerController;
    public Vector3 offsetFromPlayer;

    private void Start()
    {
        offsetFromPlayer = transform.position - playerController.transform.position;
    }
    void LateUpdate()
    {
        Vector3 playerPos = playerController.transform.position;
        Vector3 playerDir = playerController.Direction();

        transform.position = playerPos + (playerDir * offsetFromPlayer.z) + (Vector3.up * offsetFromPlayer.y);
        transform.forward = playerPos - transform.position;
    }
}