using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockableEnemy : MonoBehaviour
{
    private void Start()
    {
        ServiceLocator.Get<EnemyLockController>().RegisterEnemy(gameObject);
    }

    private void OnDestroy()
    {
        ServiceLocator.Get<EnemyLockController>().DeregisterEnemy(gameObject);
    }
}