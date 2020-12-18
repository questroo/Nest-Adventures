using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleMover : MonoBehaviour
{
    float val = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        ServiceLocator.Get<EnemyLockController>().RegisterEnemy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        val += Time.deltaTime;
        transform.Translate(0, 0, 5.0f*Mathf.Clamp(Mathf.Sin(val),-1f, 1f) * Time.deltaTime);
    }
}