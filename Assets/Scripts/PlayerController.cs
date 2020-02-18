using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator charAnimator;
    private bool isMoving = false;
    private bool isAttacking = false;
    public float moveSpeed = 20.0f;
    public float jumpHeight = 225.0f;
    public float horizontalLaunchSpeed = 1777.0f;
    [SerializeField] private bool disableInput = false;
    private Vector3 myDirection;
    private Collider attackCollider;

    // Gary's Testing Code
    // Start
    private Transform camaeraTransform;
    // End

    private void Start()
    {
        myDirection = Vector3.forward;
        charAnimator = GetComponentInChildren<Animator>();
        attackCollider = GetComponent<Collider>();

        // Gary's Testing Code
        // Start
        camaeraTransform = Camera.main.transform;
        // End

        //+ camaeraTransform.eulerAngles.y

    }
    void Update()
    {
        if (isMoving)
        {
            charAnimator.SetBool("IsRunning", true);
        }
        else
        {
            charAnimator.SetBool("IsRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.E) && !isAttacking)
        {
            isAttacking = true;
            charAnimator.SetTrigger("Attack");
            StartCoroutine("Attacking");
            isMoving = false;
        }
        if (!disableInput && !isAttacking)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (x != 0 || z != 0)
            {
                isMoving = true;
                Vector3 direction = new Vector3(x, 0.0f, z) * moveSpeed * Time.deltaTime;

                float mouseDeltaX = Input.GetAxis("Mouse X");

                Vector3 movement = myDirection * z + Vector3.Cross(Vector3.up, myDirection) * x;

                transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World) ;
                transform.rotation = Quaternion.LookRotation(movement);
            }
            else
            {
                isMoving = false;
            }
        }
    }
    public Vector3 Direction()
    {
        return myDirection;
    }
    public void DisableInput()
    {
        disableInput = true;
    }
    public void EnableInput()
    {
        disableInput = false;
    }
    public void GetOtherAnimator()
    {
        charAnimator = GetComponentInChildren<Animator>();
    }
    IEnumerator Attacking()
    {
        //attackCollider.
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
}
