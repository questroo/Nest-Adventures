using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Camera cam;

    void Update()
    {
        transform.LookAt(transform.position +
            cam.transform.rotation * Vector3.back,
            cam.transform.rotation * Vector3.down);
    }
}
