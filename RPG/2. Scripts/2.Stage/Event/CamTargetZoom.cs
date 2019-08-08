using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace EventCam
    {
        public class CamTargetZoom : MonoBehaviour
        {
            [SerializeField]
            EventCamCtrl eventCamCtrl;

            [SerializeField, Header("카메라 연출 후 메인 카메라로")]
            bool isMainCam = true;
            [SerializeField, Header("다음 이벤트 카메라(위에 변수 false 시 사용)")]
            GameObject nextCamObj;
            [SerializeField, Header("다음 이벤트 연출 시 오브젝트 활성화")]
            GameObject nextEventObj;

            [SerializeField, Header("카메라 확대 및 축소 타겟, 타겟을 변경 할수도 있다")]
            Transform[] target = null;

            [SerializeField, Header("카메라 타겟 고정")]
            bool isLookTarger = true;

            [SerializeField, Header("타겟의 땅을 보기 때문에 살짝 위로 잡아준다")]
            float hei = 1.5f;

            [SerializeField]
            Camera cam;

            #region 카메라 연출 세팅
            [SerializeField, Header("시야각 변경(zoomIn) 전 딜레이")]
            float delay1;

            [SerializeField, Header("시야각 변경 ")]
            float fov;

            [SerializeField, Header("시야각2 zoomIn 후 딜레이")]
            float delay2;

            [SerializeField, Header("시야각 변경 ")]
            float fov2;

            [SerializeField, Header("시야각2 zoomOut 후 딜레이 / 타겟 변경 전")]
            float delay3;

            [SerializeField, Header("이벤트 카메라 종료 전")]
            float delay4;

            [SerializeField, Header("타겟 변경 후 롹대 시야각")]
            float fov3 = 5;
            #endregion

            bool isEventStart = false; //카메라 이벤트 시작 전
            bool isEventEnd = false; //카메라 전환 스위치;

            bool isZoomIn = false;
            bool isZoomOut = false;
             
            int targetIndex = 0;

            public bool IsEventStart { get => isEventStart; set => isEventStart = value; }

            private void Start()
            {
                if (nextCamObj != null)
                {
                    nextCamObj.SetActive(false);
                    nextEventObj.SetActive(false);
                }
                    
            }

            private void LateUpdate()
            {
                
                if(isLookTarger)
                {
                    //타겟의 땅을 보기 때문에 살짝 위로 잡아준다
                    cam.transform.LookAt(new Vector3(target[targetIndex].position.x,
                    target[targetIndex].position.y + hei, target[targetIndex].position.z));
                }
                

                if (IsEventStart)
                    StartCoroutine(TargetCam());

                if (isZoomIn)
                    ZoomInCam(fov);

                if (isZoomOut)
                    ZoomOutCam(fov2);

                if (isEventEnd)
                {
                    isEventEnd = false;

                    cam.gameObject.SetActive(false);
                    
                    //메인 카메라 활성화
                    if(isMainCam)
                    {
                        //Debug.Log("Event Off 1 ");
                        //메인 카메라 활성화
                        eventCamCtrl.MainCamAct();
                    }
                    //다음 카메라 활성화
                    else if(!isMainCam)
                    {
                        if (nextCamObj != null)
                        {
                            nextCamObj.SetActive(true);
                            nextEventObj.SetActive(true);
                            nextCamObj.GetComponent<CamTargetZoom>().IsEventStart =true;
                        }
                            
                    }
                    
                }

            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="targetID"></param>
            /// <returns></returns>
            IEnumerator TargetCam()
            {
                IsEventStart = false;

                //Debug.Log("TargetCam 확대 전" + Time.time);
                //카메라 확대 하기 전 대기
                yield return new WaitForSeconds(delay1);

                //Debug.Log("TargetCam 확대 시작" + Time.time);
                //대기 후
                // ZoomInCam(fov);
                isZoomIn = true;

                 //확대 후 잠시 대기
                 yield return new WaitForSeconds(delay2);

                //Debug.Log("TargetCam 확대 후" + Time.time);

                //축소 시작
                // ZoomOutCam(fov2);
                isZoomIn = false;
                isZoomOut = true;
                
                //카메라 축소 후
                yield return new WaitForSeconds(delay3);

               // Debug.Log("TargetCam 축소 후" + Time.time);

                if (target.Length > 1)
                {
                    targetIndex++;                   
                }

                fov = fov3;
                isZoomIn = true;
                isZoomOut = false;
                yield return new WaitForSeconds(delay4);

                isEventEnd = true;
                isZoomIn = false;


            }


            /// <summary>
            /// 카메라 확대
            /// </summary>
            void ZoomInCam(float fov)
            {
                //카메라 확대
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, Time.deltaTime * 10);
                

            }

            /// <summary>
            /// 카메라 시야각 원래데러
            /// </summary>
            /// <param name="fov"></param>
            void ZoomOutCam(float fov)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, Time.deltaTime * 10);


            }
        }
        //class End
    }
}
