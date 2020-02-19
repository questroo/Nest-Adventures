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

    //Variables
    public float fireSpeed = 10.0f;
    private bool isMoving = false;
    private bool isAttacking = false;
    public float moveSpeed = 20.0f;
    public float jumpHeight = 225.0f;
    public float horizontalLaunchSpeed = 1777.0f;
    [SerializeField] private bool disableInput = false;
    private Vector3 myDirection;
    public Transform cameraTransform;
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    public float turnSmoothVelocity;
    public float turnSmoothTime = 0.15f;
    private bool lockOnTarget;


    public void Start()
    {
        myDirection = Vector3.forward;
        charAnimator = GetComponentInChildren<Animator>();
        projectileController = GetComponent<ProjectileController>();
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
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector2 inputDir = new Vector2(x, z).normalized;

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
                Vector3 movement = myDirection * z + Vector3.Cross(Vector3.up, myDirection) * x;

                transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
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
