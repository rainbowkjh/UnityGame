using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace CameraUtil
    {
        public class FPSCam : MonoBehaviour
        {
            [SerializeField, Header("Player")]
            Transform targetTr;

            public float Speed;
            public float Dis; //거리
            public float Hei; //높이
            public float offSet; //lookAt (눈높이?)

            private void LateUpdate()
            {
                Vector3 pos = targetTr.position - Vector3.forward * Dis + Vector3.up * Hei;

                transform.position = Vector3.Slerp(transform.position, pos, Speed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetTr.rotation, 50 * Time.deltaTime);

                transform.LookAt(targetTr.position + Vector3.up * offSet);
            }
        }

    }
}
