using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGizmo : MonoBehaviour
{
    public Color col = Color.white;
    public float size = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = col;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(Vector3.zero, size);
    }
}