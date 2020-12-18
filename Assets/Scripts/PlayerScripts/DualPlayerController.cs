using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DualPlayerController : MonoBehaviour
{
    // InputSystem
    private PlayerControls mainControls;

    // Variables
    [Header("Movement Tweaking Variables")]
    public float moveSpeed = 7.5f;
    public float dodgeSpeed = 6.0f;
    public float speedAccelSmoothTime = 0.15f;
    public float runAnimationDampener = 0.2f;

    public float turnSmoothDampener = 100.0f;
    public float turnSmoothVelocity = 0.0f;
    public float turnSmoothTime = 0.08f;

    [SerializeField] private bool disableInput = false;
    private bool isDodging = false;
    private float speedSmoothVelocity;
    private float currentSpeed = 0.0f;

    // Dodging control
    private bool hasRollIntoCoroutineBeenCalled = false;
    private bool startRollHasEnded = false;
    private bool endRollHasEnded = false;

    // Attacking control
    private bool isAttacking = false;

    // Debug values
    [Header("Debug Variables")]
    public Vector2 moveDirection;
    public Vector3 dodgeDirection;

    // Camera variables
    private EnemyLockController lockController;
    private Transform cameraTransform;
    private bool isLockPerformed = false;
    private Transform lockedEnemy;

    // Character Variables
    public enum CharacterClass
    {
        Pugilist,
        Sorcerer
    };
    public CharacterClass currentCharacter = CharacterClass.Pugilist;

    private PlayerStats playerStats;
    PugilistPlayerController pugilistController;
    SorcererPlayerController sorcererController;
    Animator pugilistAnimator;
    Animator sorcererAnimator;
    Animator currentCharacterAnimator;

    [HideInInspector]
    public bool isReload = false;

    private void Awake()
    {
        mainControls = new PlayerControls();
        ServiceLocator.Register<PlayerControls>(mainControls);
        ServiceLocator.Register<DualPlayerController>(this);

        mainControls.ActionMap.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        mainControls.ActionMap.Move.canceled += ctx => moveDirection = Vector2.zero;
        mainControls.ActionMap.DodgeRoll.performed += ctx => DodgeRoll();

        mainControls.ActionMap.LockOn.canceled += ctx => AttemptCameraLock();
        mainControls.ActionMap.LockOn.performed += ctx => UnlockCameraFromEnemy();

        mainControls.ActionMap.Attack.performed += ctx => pugilistController.SendAttack();

        mainControls.ActionMap.Enable();
    }

    void Start()
    {
        cameraTransform = Camera.main.transform;

        pugilistController = ServiceLocator.Get<PugilistPlayerController>();
        sorcererController = ServiceLocator.Get<SorcererPlayerController>();

        pugilistAnimator = pugilistController.GetComponentInChildren<Animator>();
        sorcererAnimator = sorcererController.GetComponentInChildren<Animator>();

        lockController = ServiceLocator.Get<EnemyLockController>();

        playerStats = GetComponent<PlayerStats>();

        SwitchToCharacter(currentCharacter);
    }

    void Update()
    {
        UpdateAttacking();
        UpdateMovement();
    }

    void UpdateAttacking()
    {
        if (currentCharacter == CharacterClass.Pugilist)
            isAttacking = pugilistController.IsPugilistAttacking();
        else if (currentCharacter == CharacterClass.Sorcerer)
            isAttacking = sorcererController.IsSorcererAttacking();
    }

    void UpdateMovement()
    {
        float x = moveDirection.x;
        float z = moveDirection.y;
        Vector2 inputDir = new Vector2(x, z).normalized;
        float targetSpeed = moveSpeed * inputDir.magnitude;
        Vector3 moveDir;

        if (inputDir != Vector2.zero)
        {
            currentCharacterAnimator.SetBool("IsRunning", true);
            CancelAttack();
        }
        else
        {
            currentCharacterAnimator.SetBool("IsRunning", false);
        }

        if (!disableInput && !isAttacking && !playerStats.CheckIsDead())
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedAccelSmoothTime);

            if (inputDir != Vector2.zero)
            {
                currentCharacterAnimator.SetFloat("RunningSpeed", targetSpeed * runAnimationDampener);
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveDir *= currentSpeed * Time.deltaTime;

                transform.Translate(moveDir, Space.World);
            }
            else
            {
                currentCharacterAnimator.SetFloat("RunningSpeed", 0.0f);
            }
        }

        if (isDodging)
        {
            // While dodging, we want to call the coroutines related to dodging in, or dodging out only once, but still move the character in the correct direction.
            transform.Translate(new Vector3(dodgeDirection.x, 0.0f, dodgeDirection.z) * dodgeSpeed * Time.deltaTime, Space.World);
            if (!hasRollIntoCoroutineBeenCalled)
            {
                StartCoroutine(RollIntoSwitch());
                hasRollIntoCoroutineBeenCalled = true;
            }
        }

        if (lockedEnemy)
        {
            transform.LookAt(new Vector3(lockedEnemy.position.x, transform.position.y, lockedEnemy.position.z));
        }
    }

    void DodgeRoll()
    {
        if (!isDodging && !disableInput)
        {
            SoundManager.PlaySound(SoundManager.Sound.switchPlayer);
            isDodging = true;
            disableInput = true;
            currentCharacterAnimator.SetTrigger("StartRoll");
            if (moveDirection == Vector2.zero)
                dodgeDirection = transform.forward;
            else
            {
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                dodgeDirection = moveDir;
            }
            transform.LookAt((transform.position + dodgeDirection));

            CancelAttack();
        }
    }

    void CancelAttack()
    {
        if (currentCharacter == CharacterClass.Pugilist)
            pugilistController.CancelAttack();
        else if (currentCharacter == CharacterClass.Sorcerer)
            sorcererController.CancelAttack();
    }

    public void SwitchToCharacter(CharacterClass characterClass)
    {
        if (characterClass == CharacterClass.Pugilist)
        {
            sorcererController.gameObject.SetActive(false);
            pugilistController.gameObject.SetActive(true);
            currentCharacterAnimator = pugilistAnimator;
        }
        else
        {
            pugilistController.gameObject.SetActive(false);
            sorcererController.gameObject.SetActive(true);
            currentCharacterAnimator = sorcererAnimator;
        }
    }

    void AttemptCameraLock()
    {
        // isLockPerformed ensures that the lock will only trigger once per button press at most
        if (!isLockPerformed)
        {
            lockedEnemy = lockController.AddEnemyToLockGroup();
        }
        isLockPerformed = false;
    }

    void UnlockCameraFromEnemy()
    {
        lockController.RemoveEnemyFromLockGroup();
        isLockPerformed = true;
        lockedEnemy = null;
    }

    public void AlertEndOfFirstRoll()
    {
        startRollHasEnded = true;
    }

    public void AlertEndOfSecondRoll()
    {
        endRollHasEnded = true;
    }

    IEnumerator RollIntoSwitch()
    {
        yield return new WaitUntil(() => startRollHasEnded == true);
        startRollHasEnded = false;

        // SWITCH CHARACTER
        if (currentCharacter == CharacterClass.Pugilist)
            currentCharacter = CharacterClass.Sorcerer;
        else
            currentCharacter = CharacterClass.Pugilist;

        SwitchToCharacter(currentCharacter);
        StartCoroutine(RollOutOfSwitch());
    }

    IEnumerator RollOutOfSwitch()
    {
        currentCharacterAnimator.Play("SecondHalfRoll");

        yield return new WaitUntil(() => endRollHasEnded == true);
        endRollHasEnded = false;

        // Reset Dodge variables
        hasRollIntoCoroutineBeenCalled = false;
        isDodging = false;
        disableInput = false;
    }
}