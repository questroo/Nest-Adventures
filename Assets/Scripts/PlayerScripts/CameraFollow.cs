﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    #region LockOnCamera
    //public bool isTargetFollowOn = false;
    private List<EnemyStat> enemiesInLOS;
    private Transform enemyLockOnTransform;
    //public float cameraSwitchSpeed = 270.0f;
    //[SerializeField]
    private int enemyIndex = -1;
    bool isChanging = false;
    #endregion

    public float CameraMoveSpeed = 120.0f;
    public GameObject CameraFollowObj;
    Vector3 FollowPOS;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;
    public GameObject CameraObj;
    public GameObject PlayerObj;
    public float camDistanceXToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;



    // Use this for initialization
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ManageEnemiesInLOSList();
        // We setup the rotation of the sticks here
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX += finalInputZ * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;


    }

    void LateUpdate()
    {
        //if(isTargetFollowOn)
        //{
        //    Vector3 modifiedCameraPosition = transform.position;
        //    modifiedCameraPosition.y += lockOnAngle;
        //    cameraToBoss = enemyLockOnTransform.position - modifiedCameraPosition;
        //    newRot = Quaternion.LookRotation(cameraToBoss);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, Time.deltaTime * cameraSwitchSpeed);
        //    currentRotation = transform.eulerAngles;
        //}
        //if (enemiesInLOS.Count > 0)
        //{
        //    if (isTargetFollowOn)
        //    {
        //        enemyLockOnTransform = enemiesInLOS[enemyIndex].transform;
        //    }
        //}

        CameraUpdater();
        //isChanging = false;
    }

    void CameraUpdater()
    {
        // set the target object to follow
        Transform target = CameraFollowObj.transform;

        //move towards the game object that is the target
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
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
    //void LockOn()
    //{
    //    isTargetFollowOn = true;
    //
    //    enemyIndex++;
    //    if (enemyIndex >= enemiesInLOS.Count)
    //    {
    //        isTargetFollowOn = false;
    //        enemyIndex = -1;
    //    }
    //    isChanging = true;
    //}
}
