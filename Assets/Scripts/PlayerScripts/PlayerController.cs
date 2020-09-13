using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // InputSystem
    PlayerControls controls;
    //Components
    private Animator charAnimator;
    //Variables
    private bool isMoving = false;
    public bool isAttacking = false;
    private bool isDodging = false;
    private bool lockOnTarget;
    [SerializeField] private bool disableInput = false;
    public float fireSpeed = 10.0f;
    public float moveSpeed = 20.0f;
    public float jumpHeight = 225.0f;
    public float horizontalLaunchSpeed = 1777.0f;
    public float speedAccelSmoothTime = 0.3f;
    public float turnSmoothVelocity;
    float speedSmoothVelocity;
    float currentSpeed = 0.0f;
    public float turnSmoothTime = 0.1f;
    private Vector3 myDirection;
    private Vector2 moveDirection;
    private Transform cameraTransform;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.ActionMap.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        controls.ActionMap.Move.canceled += ctx => moveDirection = Vector2.zero;

        controls.ActionMap.DodgeRoll.performed += ctx => DodgeRoll();
    }
    public void Start()
    {
        myDirection = Vector3.forward;
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        if (!disableInput && !isAttacking)
        {
            float x = moveDirection.x;
            float z = moveDirection.y;
            Vector2 inputDir = new Vector2(x, z).normalized;
            float targetSpeed = moveSpeed * inputDir.magnitude;

            if (inputDir != Vector2.zero)
            {
                isMoving = true;

                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            }
            else
            {
                isMoving = false;
            }
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedAccelSmoothTime);
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        }
    }
    public void DisableInput()
    {
        disableInput = true;
    }
    public void EnableInput()
    {
        disableInput = false;
    }
    // Returns true if input is disabled
    public bool GetInputStatus()
    {
        return disableInput;
    }
    public void ResetCharacterComponents()
    {
        charAnimator = GetComponentInChildren<Animator>();
    }
    private void DodgeRoll()
    {
        if (!isDodging)
        {
            isDodging = true;
            charAnimator.SetTrigger("DodgeRoll");
            isMoving = false;
        }
    }
    public bool GetAttackBool()
    {
        return isAttacking;
    }
    public void TurnOffAttackBool()
    {
        isAttacking = false;
    }
    public void TurnOffRunningAnim()
    {
        charAnimator.SetBool("IsRunning", false);
    }
    private void OnEnable()
    {
        controls.ActionMap.Enable();
    }
    private void OnDisable()
    {
        controls.ActionMap.Disable();
    }
    public void TurnOffIsDodgingBool()
    {
        isDodging = false;
    }
}