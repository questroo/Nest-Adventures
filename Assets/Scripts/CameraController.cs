using UnityEngine;

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

    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    Quaternion newRot;
    Vector3 cameraToBoss;
    public Transform bossTransform;
    public float cameraSwitchSpeed = 270.0f;

    bool isChanging = false;
    float yaw;
    float pitch;

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isBossFollowOn = !isBossFollowOn;
            isChanging = true;
        }


        if (turnOffCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (!isBossFollowOn)
        {
            if (Input.GetMouseButton(0))
            {
                yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
                pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
                pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
            }


            if (isChanging)
            {
                Debug.Log("Resetting rotation");
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
}