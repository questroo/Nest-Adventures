using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour
{
    public ConditionType conditionType = ConditionType.PlayerEnter;

    ConditionalDoor doorHandle;

    [Tooltip("Only fill this array if the condition type is EnemiesKilled or BossKilled!")]
    public GameObject[] enemyList;

    private void Awake()
    {
        doorHandle = transform.parent.GetComponentInChildren<ConditionalDoor>();
        if (!doorHandle)
            Debug.LogError("No ConditionalDoor script found in parent!");
    }

    private void Update()
    {
        if (conditionType != ConditionType.PlayerEnter)
        {
            bool areEnemiesStillLiving = false;
            foreach (GameObject enemy in enemyList)
            {
                if(enemy)
                {
                    areEnemiesStillLiving = true;
                }
            }
            if(!areEnemiesStillLiving)
            {
                doorHandle.UnlockDoor();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(conditionType == ConditionType.PlayerEnter && other.CompareTag("Player"))
        {
            doorHandle.UnlockDoor();
        }
    }
}