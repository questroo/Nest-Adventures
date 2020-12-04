using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject boss;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
     
        boss.SetActive(true);
        Destroy(gameObject);
    }
}
