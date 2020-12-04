using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineCameraController : MonoBehaviour
{
    PlayerControls controls;
    Transform followTransform;

    private void Start()
    {
        controls = ServiceLocator.Get<PlayerControls>();
        if(controls == null)
        {
            Debug.LogError("No controls found!! Camera will not work.");
        }
    }

    private void Update()
    {

    }
}