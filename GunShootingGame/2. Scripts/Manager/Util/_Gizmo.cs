using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Gizmo : MonoBehaviour
{
    public bool isGizmoOn = false;

    public float fGizmoSize;
    public Color color;

    private void OnDrawGizmos()
    {
        if(isGizmoOn)
        {
            color.a = 1.0f;
            Gizmos.color = color;

            Gizmos.DrawSphere(transform.position, fGizmoSize);
        }
        
    }
}
