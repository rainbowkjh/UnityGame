using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 기즈모
/// </summary>
namespace GameUtil
{
    public class _Gizmo : MonoBehaviour
    {
        public float m_fRad = 0.5f;
        public Color m_Color = Color.red;

        private void OnDrawGizmos()
        {
            Gizmos.color = m_Color;
            Gizmos.DrawSphere(this.transform.position, m_fRad);
        }
    }

}
