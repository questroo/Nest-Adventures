﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 11.0f;
    public float jumpHeight = 225.0f;
    public float horizontalLaunchSpeed = 1777.0f;

    private bool isDodging = false;
    private Vector3 myDirection;
    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        myDirection = Vector3.forward;
    }
    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 1.05f))
        {
            //stop the character movement
            rigidbody.velocity = Vector3.zero;
            isDodging = false;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, 0.0f, z) * moveSpeed * Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isDodging)
        {
            DodgeRoll(direction);
        }
        if(!isDodging)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
    public Vector3 Direction()
    {
        return myDirection;
    }
    void DodgeRoll(Vector3 dir)
    {
        isDodging = true;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(new Vector3(dir.x * horizontalLaunchSpeed, jumpHeight, dir.z * horizontalLaunchSpeed));
    }
}
