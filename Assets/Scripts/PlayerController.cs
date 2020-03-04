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
    public float fireSpeed = 10.0f;
    private bool isMoving = false;
    private bool isAttacking = false;
    public float moveSpeed = 20.0f;
    public float jumpHeight = 225.0f;
    public float horizontalLaunchSpeed = 1777.0f;
    [SerializeField] private bool disableInput = false;
    private Vector3 myDirection;
    private Vector2 moveDirection;
    private Transform cameraTransform;
    public float speedAccelSmoothTime = 0.3f;
    float speedSmoothVelocity;
    public float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    private bool lockOnTarget;
    float currentSpeed = 0.0f;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.ActionMap.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        controls.ActionMap.Move.canceled += ctx => moveDirection = Vector2.zero;

        controls.ActionMap.Attack.performed += ctx => Attack();
    }
    public void Start()
    {
        myDirection = Vector3.forward;
        charAnimator = GetComponentInChildren<Animator>();
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        if (disableInput)
        {
            isMoving = false;
        }
        if (isMoving)
        {
            charAnimator.SetBool("IsRunning", true);
        }
        else
        {
            charAnimator.SetBool("IsRunning", false);
        }
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
    public void ResetCharacterComponents()
    {
        charAnimator = GetComponentInChildren<Animator>();
    }
    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
    void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            charAnimator.SetTrigger("Attack");
            StartCoroutine("Attacking");
            isMoving = false;
        }
    }
    //IEnumerator FireProjectile(Vector3 startPos, Vector3 endPos)
    //{
    //    Debug.Log("projectile fired");
    //    GameObject fireBall = Instantiate(fireBlast, startPos, Quaternion.identity, null) as GameObject;
    //    Rigidbody fireRigid = fireBall.GetComponent<Rigidbody>();
    //    fireRigid.velocity = Vector3.zero;
    //    fireRigid.AddForce((endPos - startPos).normalized * fireSpeed * Time.deltaTime);
    //    yield return new WaitForSeconds(1.5f);
    //    Destroy(fireBall);
    //}
    public bool GetAttackBool()
    {
        return isAttacking;
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
}