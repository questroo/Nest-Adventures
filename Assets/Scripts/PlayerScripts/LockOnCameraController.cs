using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LockOnCameraController : MonoBehaviour
{
    PlayerControls controls;
    CinemachineTargetGroup targetGroup;
    public Transform target;
    public float addWeight = 1.0f;
    public float addRadius = 10.0f;
    bool isIn = false;

    // Start is called before the first frame update
    void Start()
    {
        controls = ServiceLocator.Get<PlayerControls>();
        targetGroup = GetComponent<CinemachineTargetGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isIn)
            {
                isIn = false;
                targetGroup.RemoveMember(target);
            }
            else
            {
                isIn = true;
                targetGroup.AddMember(target, addWeight, addRadius);
            }
        }
    }
}