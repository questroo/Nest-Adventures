using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
<<<<<<< HEAD
    public PlayerController playerController;
    public Vector3 offsetFromPlayer;
=======
    [Tooltip("Controls The Mouse Sensitivity")]
    public float mouseSensitivity = 5.0f;

    [Tooltip("Place A GameObject Here For The Camera To Follow")]
    public Transform target;

    [Tooltip("Controls The Distance Of The Camera From The Target")]
    public float distFromTarget = 3.5f;

    [Tooltip("Controls The Pitch For The Target")]
    public Vector2 pitchMinMax = new Vector2(10, 80);

    [Tooltip("Controls The Delay For The Camera")]
    public float rotationSmoothTime = .15f;

    [Tooltip("Controls The Visability For The Mouse Cursor")]
    public bool turnOffCursor = false;

    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    
    float yaw;
    float pitch;
>>>>>>> 32e2dfff56ab7509fb40ecbf7aa67a81502f73a8

    void LateUpdate()
    {
        if (turnOffCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetMouseButton(0))
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;
            transform.position = target.position - transform.forward * distFromTarget;
    }


}