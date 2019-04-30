using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField]
    Transform targetTr;

    [SerializeField, Header("높이")]
    float fHeight;

    [SerializeField, Header("거리")]
    float fDistance;

    [SerializeField, Header("좌/우 거리")]
    float fRight;

    [SerializeField, Header("추적 속도")]
    float fSpeed;

    [SerializeField, Header("회전 속도")]
    float fRot;

    [SerializeField, Header("Offset")]
    float fOffHig;

    [SerializeField, Header("Offset")]
    float fOffDis;

    [SerializeField, Header("Offset")]
    float fOffRig;


    private void LateUpdate()
    {
        Vector3 camPos = targetTr.position +
            targetTr.up * fHeight - targetTr.forward * fDistance - targetTr.right * fRight;

        transform.position = Vector3.Slerp(transform.position, camPos, fSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetTr.rotation, fRot * Time.deltaTime);

        transform.LookAt(targetTr.position + targetTr.up * fHeight - targetTr.right * fRight - targetTr.forward * fDistance);
        // transform.LookAt(targetTr.position + targetTr.up * fOffHig - targetTr.right * fOffRig - targetTr.forward * fOffDis);
    }

}
