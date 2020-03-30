using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    public GameObject fireBlast;
    private Animator charAnimator;
    public GameObject projectileStartLocation;
    public GameObject projectileEndLocation;
    private ProjectileController projectileController;
    private Camera cam;

    public Vector3 camForward;
    public Vector3 camRight;
    //Variables
    public float fireSpeed = 10.0f;
    private bool isMoving = false;
    private bool isAttacking = false;
    public float moveSpeed = 20.0f;
    public float jumpHeight = 225.0f;
    public float horizontalLaunchSpeed = 1777.0f;
    [SerializeField] private bool disableInput = false;
    private Vector3 myDirection;
    private Transform cameraTransform;
    public float speedAccelSmoothTime = 0.3f;
    float speedSmoothVelocity;
    public float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    private bool lockOnTarget;
    float currentSpeed = 0.0f;


    public void Start()
    {
        cam = Camera.main;
        myDirection = Vector3.forward;
        charAnimator = GetComponentInChildren<Animator>();
        projectileController = GetComponent<ProjectileController>();
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        camForward = cam.transform.forward;
        camRight = cam.transform.right;
        camForward.y = 0.0f;
        camRight.y = 0.0f;
        camForward.Normalize();
        camRight.Normalize();
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
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector2 inputDir = new Vector2(x, z).normalized;
            float targetSpeed = moveSpeed * inputDir.magnitude;
            if (Input.GetKeyDown(KeyCode.E) && !isAttacking && charAnimator.CompareTag("Tanjiro"))
            {
                isAttacking = true;
                charAnimator.SetTrigger("Attack");
                StartCoroutine("Attacking");
                isMoving = false;
                StartCoroutine(FireProjectile(projectileStartLocation.transform.position, projectileEndLocation.transform.position));
            }
            else if (Input.GetKeyDown(KeyCode.E) && !isAttacking)
            {
                isAttacking = true;
                charAnimator.SetTrigger("Attack");
                StartCoroutine("Attacking");
                isMoving = false;
            }
            if (inputDir != Vector2.zero)
            {
                isMoving = true;

                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
                //currentSpeed = Mathf.SmoothDamp(targetSpeed, currentSpeed, ref speedSmoothVelocity, speedDecelSmoothTime);
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
        projectileController = GetComponent<ProjectileController>();
    }
    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
    IEnumerator FireProjectile(Vector3 startPos, Vector3 endPos)
    {
        GameObject fireBall = Instantiate(fireBlast, startPos, Quaternion.identity, null) as GameObject;
        Rigidbody fireRigid = fireBall.GetComponent<Rigidbody>();
        fireRigid.velocity = Vector3.zero;
        fireRigid.AddForce((endPos - startPos).normalized * fireSpeed * Time.deltaTime);
        yield return new WaitForSeconds(1.5f);
        Destroy(fireBall);
    }
    public void TurnOffRunningAnim()
    {
        charAnimator.SetBool("IsRunning", false);
    }
}
