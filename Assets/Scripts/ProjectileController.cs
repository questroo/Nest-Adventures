using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject fireBlast;
    void Start()
    {
        
    }

    void Update()
    {
    }
    public void FireProjectile(Vector3 startPos, Vector3 endPos)
    {
        
    }
    IEnumerator MoveProjectile(Vector3 startPos, Vector3 endPos)
    {
        GameObject fireBall = Instantiate(fireBlast, startPos, Quaternion.identity) as GameObject;
        yield return null;

    }
}
