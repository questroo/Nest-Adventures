using UnityEngine;
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
    public bool isBossFollowOn = false;


    // Controls
    PlayerControls cameraControls;

    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    Vector2 cameraMove;

    Quaternion newRot;
    Vector3 cameraToBoss;
    public Transform bossTransform;
    public float cameraSwitchSpeed = 270.0f;

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
    void LateUpdate()
    {
        if (turnOffCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (!isBossFollowOn)
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
            cameraToBoss = bossTransform.position - transform.position;
            newRot = Quaternion.LookRotation(cameraToBoss);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, Time.deltaTime * cameraSwitchSpeed);
            currentRotation = transform.eulerAngles;
        }

        transform.position = target.position - transform.forward * distFromTarget;
        isChanging = false;
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
        isBossFollowOn = !isBossFollowOn;
        isChanging = true;
    }
}