using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(spawnObject, transform);
    }
}