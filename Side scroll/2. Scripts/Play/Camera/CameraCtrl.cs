using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraRig
{
    public class CameraCtrl : MonoBehaviour
    {
        [SerializeField, Header("Target")]
        Transform m_trTarget;

        [SerializeField, Header("타겟과의 거리")]
        float m_fDis;

        [SerializeField, Header("타겟과의 높이")]
        float m_fHei;

        [SerializeField, Header("카메라 속도")]
        float m_fCamSpeed;

        [SerializeField, Header("LookAt Height")]
        float m_fLookAtHei;

        private void LateUpdate()
        {
            Vector3 camTr = m_trTarget.position + (-Vector3.forward * m_fDis) + Vector3.up * m_fHei;

            transform.position = Vector3.Slerp(transform.position, camTr, m_fCamSpeed * Time.deltaTime);

            transform.LookAt(m_trTarget.position + Vector3.up * m_fLookAtHei);
        }
    }

}

