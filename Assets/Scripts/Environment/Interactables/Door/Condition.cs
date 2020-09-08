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
        doorHandle = GetComponent<ConditionalDoor>();
        if (!doorHandle)
            Debug.LogError("No ConditionalDoor script found in parent!");
    }

    void Update()
    {
        if (conditionType == ConditionType.EnemiesKilled)
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
                doorHandle.Unlock();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(conditionType == ConditionType.PlayerEnter && (other.CompareTag("Player") || other.CompareTag("MeleeCharacter") || other.CompareTag("RangedCharacter")))
        {
            doorHandle.Unlock();
        }
        else if (conditionType == ConditionType.PlayerUse && (other.CompareTag("Player") || other.CompareTag("MeleeCharacter") || other.CompareTag("RangedCharacter")))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                doorHandle.Unlock();
            }
        }
    }
}