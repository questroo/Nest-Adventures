using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject fireBlast;
    private float fireSpeed = 10.0f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void FireProjectile(Vector3 startPos, Vector3 endPos)
    {
        Debug.Log("instantiated");
        Instantiate(fireBlast, startPos, Quaternion.identity);
        Vector3.MoveTowards(startPos, endPos, fireSpeed * Time.deltaTime);
    }
}
