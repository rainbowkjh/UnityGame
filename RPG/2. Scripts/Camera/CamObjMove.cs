using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카매라의 장애물 감자 기능
/// 카메라의 콜라이더가 있는 하워 오브젝트에 적용
/// 카메라의 초기 높이 및 거리 값으로 고정 이동 중
/// 장애물 감지 시 카메라만 위로 올림
/// 장애물이 없어지면 카메라를 다시 내린다
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class CamObjMove : MonoBehaviour
        {
            [SerializeField]
            CameraCtrl cam;

            public float changeHei = 40;
            public float changeDis = 5;
            public float changeSpeed = 100;

            bool isEnter = false;

            [SerializeField]
            SphereCollider coll;

            //private void Start()
            //{
            //    cam = GetComponent<CameraCtrl>();
            //    coll = GetComponent<SphereCollider>();
            //}

            private void Update()
            {
                if(isEnter)
                {
                    cam.Hei = Mathf.Lerp(cam.Hei, changeHei, cam.CamMove * Time.deltaTime);
                    cam.Dis = Mathf.Lerp(cam.Dis, changeDis, cam.CamMove * Time.deltaTime);
                }
                else
                {
                    cam.Hei = Mathf.Lerp(cam.Hei, cam.OriginHei, cam.CamMove * Time.deltaTime);
                    cam.Dis = Mathf.Lerp(cam.Dis, cam.OriginDis, cam.CamMove * Time.deltaTime);
                }
            }

            private void LateUpdate()
            {
                cam.OriginPosMove(this.transform); //콜라이더 이동
            }

            private void OnTriggerStay(Collider other)
            {
                if (other.transform.CompareTag("OBS"))
                {
                    isEnter = true;
                }
            }


            private void OnTriggerExit(Collider other)
            {
                if (other.transform.CompareTag("OBS"))
                {
                    isEnter = false;                    
                }
            }

        }

    }
}
