using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraController : MonoBehaviour
{
    /*
    // CAMERA TEST #1 FAILED
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private float xRotation = 0f;
    */

    public float mouseSensitivity = 10f;
    public Transform target;
    public float distanceFromTarget = 2f;
    public Vector2 pitchMinimumAndMaximum = new Vector2 (5, 85);

    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    private float yaw;
    private float pitch;

    void Start()
    {

    }

    void LateUpdate()
    {
        /*
        // CAMERA TEST #1 FAILED
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp (xRotation, 0f, 25f);
        transform.localRotation = Quaternion.Euler(xRotation,  0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        */

        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinimumAndMaximum.x, pitchMinimumAndMaximum.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw),ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTarget;
    }


}
