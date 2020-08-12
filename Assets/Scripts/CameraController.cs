using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
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

    [Tooltip("Controls The Visability For The Mouse Cursor")]
    public bool isTargetFollowOn = false;

    [Tooltip("Controls The Angle At Which You Lock On To A Target")]
    public float lockOnAngle;
    [SerializeField]

    private List<EnemyStat> enemiesInLOS;

    // Controls
    PlayerControls cameraControls;

    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    Vector2 cameraMove;

    public CharacterManager characterManager;
    Quaternion newRot;
    Vector3 cameraToBoss;
    private Transform enemyLockOnTransform;
    public float cameraSwitchSpeed = 270.0f;
    [SerializeField]
    private int enemyIndex = -1;

    bool isChanging = false;
    float yaw;
    float pitch;
    private void Awake()
    {
        cameraControls = new PlayerControls();

        cameraControls.ActionMap.MoveCamera.performed += ctx => cameraMove = ctx.ReadValue<Vector2>();
        cameraControls.ActionMap.MoveCamera.canceled += ctx => cameraMove = Vector2.zero;

        cameraControls.ActionMap.LockOn.performed += ctx => LockOn();
    }

    private void Start()
    {
        enemyIndex = -1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        ManageEnemiesInLOSList();
    }

    void LateUpdate()
    {
        if (turnOffCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (enemiesInLOS.Count > 0)
        {
            if (isTargetFollowOn)
            {
                enemyLockOnTransform = enemiesInLOS[enemyIndex].transform;
            }
        }

        if (!characterManager.CheckSwapping())
        {
            if (!isTargetFollowOn)
            {
                yaw += cameraMove.x * mouseSensitivity;
                pitch -= cameraMove.y * mouseSensitivity;
                pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);


                if (isChanging)
                {
                    currentRotation = transform.eulerAngles;
                    pitch = transform.eulerAngles.x;
                    yaw = transform.eulerAngles.y;
                }
                currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw, 0.0f), ref rotationSmoothVelocity, rotationSmoothTime);
                transform.eulerAngles = currentRotation;
            }
            else
            {
                Vector3 modifiedCameraPosition = transform.position;
                modifiedCameraPosition.y += lockOnAngle;
                cameraToBoss = enemyLockOnTransform.position - modifiedCameraPosition;
                newRot = Quaternion.LookRotation(cameraToBoss);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, Time.deltaTime * cameraSwitchSpeed);
                currentRotation = transform.eulerAngles;
            }
            transform.position = target.position - transform.forward * distFromTarget;
            isChanging = false;
        }
    }
    private void OnEnable()
    {
        cameraControls.ActionMap.Enable();
    }
    private void OnDisable()
    {
        cameraControls.ActionMap.Disable();
    }
    void LockOn()
    {
        isTargetFollowOn = true;

        enemyIndex++;
        if (enemyIndex >= enemiesInLOS.Count)
        {
            isTargetFollowOn = false;
            enemyIndex = -1;
        }

        isChanging = true;
    }
    void ManageEnemiesInLOSList()
    {
        var allEnemies = FindObjectsOfType<EnemyStat>();
        foreach (EnemyStat enemy in allEnemies)
        {
            if (enemy == null)
            {
                enemiesInLOS.Remove(enemy);
                if (enemyIndex >= enemiesInLOS.Count)
                {
                    enemyIndex--;
                }
            }
            var tempVect = Camera.main.WorldToViewportPoint(enemy.transform.position);
            if (tempVect.x >= 0 && tempVect.x <= 1 &&
                tempVect.y >= 0 && tempVect.y <= 1 &&
                tempVect.z > 0)
            {
                if (!enemiesInLOS.Any(x => x.transform == enemy.transform))
                {
                    enemiesInLOS.Add(enemy);
                }
            }
            else
            {
                enemiesInLOS.Remove(enemy);
                if (enemyIndex >= enemiesInLOS.Count)
                {
                    enemyIndex--;
                }
            }
        }
    }
    public bool GetLockOn() { return isTargetFollowOn; }

    public Transform GetCurrentlyLockedOnTransform() { return enemyLockOnTransform; }

    public void RemoveSelfFromList(EnemyStat targetEnemy)
    {
        if (enemiesInLOS.Contains(targetEnemy))
        {
            enemiesInLOS.Remove(targetEnemy);
            if (enemyIndex >= enemiesInLOS.Count)
            {
                enemyIndex--;
            }
        }
    }
}