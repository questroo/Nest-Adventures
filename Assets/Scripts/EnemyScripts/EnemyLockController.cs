using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLockController : MonoBehaviour
{
    public int maxEnemies = 100;
    public float lockWeight = 1.0f;
    public float lockRadius = 2.0f;
    public float maxLockableEnemyDistance = 15.0f;
    public float maxEnemyDistanceUntilLockBreak = 20.0f;

    Cinemachine.CinemachineTargetGroup targetGroupScript;
    Transform playerTransform;
    Transform currentLockedEnemy;
    GameObject[] enemyList;
    int index = 0;


    void Awake()
    {
        ServiceLocator.Register<EnemyLockController>(this);
        enemyList = new GameObject[maxEnemies];
    }

    private void Start()
    {
        playerTransform = ServiceLocator.Get<PlayerStats>().transform;

        targetGroupScript = GetComponent<Cinemachine.CinemachineTargetGroup>();
    }

    private void Update()
    {
        if (currentLockedEnemy != null)
        {
            float dist = Vector3.Distance(currentLockedEnemy.position, playerTransform.position);
            if (dist > maxEnemyDistanceUntilLockBreak)
            {
                RemoveEnemyFromLockGroup();
            }
        }
    }

    private Transform NearestEnemyTransform()
    {
        Transform enemyTransform = null;

        float minDistance = float.MaxValue;
        float dist = 0.0f;
        for (int enemyIndex = 0; enemyIndex < enemyList.Length; enemyIndex++)
        {
            if (enemyList[enemyIndex] == null)
                continue;

            dist = Vector3.Distance(enemyList[enemyIndex].transform.position, playerTransform.position);
            if (dist < minDistance && dist <= maxLockableEnemyDistance && currentLockedEnemy != enemyList[enemyIndex].transform)
            {
                if(currentLockedEnemy)
                {
                    RemoveEnemyFromLockGroup();
                }

                minDistance = dist;
                enemyTransform = enemyList[enemyIndex].transform;
                currentLockedEnemy = enemyTransform;
            }
        }

        return enemyTransform;
    }

    public void RegisterEnemy(GameObject enemy)
    {
        // Register Enemy currently grabs all enemies on the map and registers them at once. New enemies are not permitted in this system. (We can change this if we need to)
        enemyList[index] = enemy;
        index++;
    }

    public void DeregisterEnemy(GameObject enemy)
    {
        for(int i = 0; i < enemyList.Length; ++i)
        {
            if(enemyList[i] == enemy)
            {
                enemyList[i] = null;
            }
        }
    }

    public Transform AddEnemyToLockGroup()
    {
        Transform enemyToLockOnto;
        enemyToLockOnto = NearestEnemyTransform();
        if(enemyToLockOnto != null)
        {
            targetGroupScript.AddMember(enemyToLockOnto, lockWeight, lockRadius);
            // Camera found a target
            return enemyToLockOnto;
        }
        // Camera didn't find a target
        return null;
    }

    public void RemoveEnemyFromLockGroup()
    {
        if (currentLockedEnemy != null)
        {
            targetGroupScript.RemoveMember(currentLockedEnemy);
            currentLockedEnemy = null;
        }
    }

    public bool HasLockOnEnemy()
    {
        return (currentLockedEnemy != null) ? true : false;
    }
}