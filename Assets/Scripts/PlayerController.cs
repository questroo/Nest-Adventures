using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator charAnimator;
    private bool isMoving = false;
    public float moveSpeed = 20.0f;
    public float jumpHeight = 225.0f;
    public float horizontalLaunchSpeed = 1777.0f;
    [SerializeField] private bool isDodging = false;
    [SerializeField] private bool disableInput = false;
    private Vector3 myDirection;

    private void Start()
    {
        myDirection = Vector3.forward;
        charAnimator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if(isMoving)
        {
            charAnimator.SetBool("IsRunning", true);
        }
        else
        {
            charAnimator.SetBool("IsRunning", false);
        }
        if (!disableInput)
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, 1.05f))
            {
                isDodging = false;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if(x != 0 || z != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            Vector3 direction = new Vector3(x, 0.0f, z) * moveSpeed * Time.deltaTime;

            float mouseDeltaX = Input.GetAxis("Mouse X");

            Quaternion rotation = Quaternion.Euler(0.0f, mouseDeltaX * 3.0f, 0.0f);
            myDirection = rotation * myDirection;

            Vector3 movement = myDirection * z + Vector3.Cross(Vector3.up, myDirection) * x;

            if (!isDodging)
            {
                transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
                transform.rotation = Quaternion.LookRotation(movement);
            }
        }
    }
    public Vector3 Direction()
    {
        return myDirection;
    }
    void DodgeRoll(Vector3 dir)
    {
        isDodging = true;
    }
    public void DisableInput()
    {
        disableInput = true;
    }
    public void EnableInput()
    {
        disableInput = false;
    }
}
