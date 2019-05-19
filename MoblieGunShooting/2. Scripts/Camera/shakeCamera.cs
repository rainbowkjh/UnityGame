using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace CameraUtil
    {
        public class shakeCamera : MonoBehaviour
        {
            public Transform shakeCam;
            private Vector3 originPos;
            private Quaternion originRot;

            [SerializeField, Header("카메라 회전")]
            bool isRot = false;

         
            void Start()
            {
                originPos = shakeCam.localPosition;
                originRot = shakeCam.localRotation;
            }

            /// <summary>
            /// 카메라 위치 초기 값
            /// 카메라 흔들 때 위치가 부자연스러울때
            /// 초기 값을 먼저 잡아주고 실행
            /// </summary>
            public void CamInit()
            {
                originPos = shakeCam.localPosition;
                originRot = shakeCam.localRotation;

            }


            //흔들리는 시간, 좌표, 회전 값 0.05, 0.03, 0.1
            public IEnumerator ShakeCamera(float duration = 0.05f, float magnitudePos = 0.1f, float magnitudeRot = 0.1f)
            {
                //Debug.Log("ShakeCam");

                float passTime = 0.0f;

                while (passTime < duration)
                {
             
                    Vector3 shakePos = Random.insideUnitSphere;
                    shakeCam.localPosition = shakePos * magnitudePos;
                    
                    if (isRot)
                    {
                        Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise(Time.time * magnitudeRot, 0.0f));                        
                        shakeCam.localRotation = Quaternion.Euler(shakeRot);
                        
                    }

                    passTime += Time.deltaTime;

                    yield return null;
                }

                shakeCam.localPosition = originPos;
                shakeCam.localRotation = originRot;

            }

        }

    }
}

