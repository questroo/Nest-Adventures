using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmoCube : MonoBehaviour
{
    public Color col = Color.green;
    public float size = 1.0f;

    public BoxCollider boxcollider;

    private void OnDrawGizmos()
    {
        Gizmos.color = col;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(boxcollider.center, boxcollider.size);
    }
}
