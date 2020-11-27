using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Condition : MonoBehaviour
{
    public ConditionType conditionType = ConditionType.PlayerEnter;

    ConditionalDoor doorHandle;
    PlayerControls controls;
    public bool isDoorButtonPressed = false;

    [Tooltip("Only fill this array if the condition type is EnemiesKilled or BossKilled!")]
    public GameObject[] enemyList;

    private void Awake()
    {
        doorHandle = GetComponent<ConditionalDoor>();
        if (!doorHandle)
            Debug.LogError("No ConditionalDoor script found in parent!");
    }

    private void Start()
    {
        controls = ServiceLocator.Get<PlayerControls>();

        controls.ActionMap.Attack.performed += ctx => DoorButtonOn();
        controls.ActionMap.Attack.canceled += ctx => DoorButtonOff();
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

    void DoorButtonOn()
    {
        isDoorButtonPressed = true;
    }

    void DoorButtonOff()
    {
        isDoorButtonPressed = false;
    }

    void OnTriggerStay(Collider other)
    {
        if(conditionType == ConditionType.PlayerEnter && (other.CompareTag("Player") || other.CompareTag("MeleeCharacter") || other.CompareTag("RangedCharacter")))
        {
            doorHandle.Unlock();
        }
        else if (conditionType == ConditionType.PlayerUse && (other.CompareTag("Player") || other.CompareTag("MeleeCharacter") || other.CompareTag("RangedCharacter")))
        {
            if(isDoorButtonPressed)
            {
                doorHandle.Unlock();
            }
        }
    }
}