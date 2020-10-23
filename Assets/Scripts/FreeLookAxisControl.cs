using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLookAxisControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    public float GetAxisCustom(string axisName)
    {
        if(axisName == "X Axis")
        {
            float r = 0.0f;
            r += Input.GetAxis("Mouse X");
            return Mathf.Clamp(r, -1.0f, 1.0f);
        }
        else if(axisName == "Y Axis")
        {
            float r = 0.0f;
            r += Input.GetAxis("Mouse Y");
            return Mathf.Clamp(r, -1.0f, 1.0f);
        }
        return 0.0f;
    }
}
