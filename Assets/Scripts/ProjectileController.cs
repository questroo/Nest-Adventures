using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public GameObject explosion;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject exp = Instantiate(explosion) as GameObject;
        //Destroy(explosion);
    }
    public void Explode()
    {
        Instantiate(explosion);
    }
}
