using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

namespace Black
{
    namespace CameraUtil
    {
        public class shakeCamera : MonoBehaviour
        {
            public Transform shakeCam;
            private Vector3 originPos;
            private Quaternion originRot;

            /// <summary>
            /// PostProssiong을 가져온다
            /// </summary>
            private PostProcessingBehaviour post;
            
            /// <summary>
            /// 피격 시 post 효과
            /// </summary>
            bool isPostSwitch = false;
            bool isPostprossiong = false; //Post효과 루프 제어

            [SerializeField,Header("피격 전 depthSetting 세팅 값")]
            DepthOfFieldModel.Settings depthSetting;

            [SerializeField, Header("피격 전 Vigntte")]
            VignetteModel.Settings vigntteStting;

            [SerializeField,Header("피격 시 depthSetting 설정 값")]
            DepthOfFieldModel.Settings hitDepthSetting;

            [SerializeField, Header("피격 시 Vigntte")]
            VignetteModel.Settings hitVigntteSetting;

            [SerializeField, Header("카메라 회전")]
            bool isRot = false;


            float depthDistance;
            float vigntteSmoothness;

            /// <summary>
            /// 효과가 끝났는지 확인
            /// </summary>
            bool isDep = false;
            bool isVig = false;

            #region Set,Get
            public bool IsPostSwitch
            {
                get
                {
                    return isPostSwitch;
                }

                set
                {
                    isPostSwitch = value;
                }
            }
            #endregion

            void Start()
            {
                originPos = shakeCam.localPosition;
                originRot = shakeCam.localRotation;

                post = Camera.main.GetComponent<PostProcessingBehaviour>();

                //천천히 값을 변경 하기 때문에 초기 값을 저장 시킨다.
                depthDistance = hitDepthSetting.focusDistance;
                vigntteSmoothness = hitVigntteSetting.smoothness;

                PostInit();
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

                    //Post 효과
                    //   post.profile.depthOfField.settings = hitDepthSetting;
                    //   post.profile.vignette.settings = hitVigntteStting;

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

               // post.profile.depthOfField.settings = depthSetting;
              //  post.profile.vignette.settings = vigntteStting;

            }

            /// <summary>
            /// 피격 당했을 때 Post 효과
            /// </summary>
            public void PainCam()
            {
                isDep = true;
                isVig = true;
                
                if(isDep && isVig)
                {
                    post.profile.depthOfField.settings = hitDepthSetting;
                    post.profile.vignette.settings = hitVigntteSetting;

                    if (hitDepthSetting.focusDistance < 5)
                    {

                        hitDepthSetting.focusDistance += Time.deltaTime * 0.5f;
                    }

                    if (hitVigntteSetting.smoothness > 0)
                    {

                        hitVigntteSetting.smoothness -= Time.deltaTime * 0.5f;
                    }


                    if (hitDepthSetting.focusDistance >= 5)
                    {
                        hitDepthSetting.focusDistance = 5;

                        isDep = false;
                    }

                    if (hitVigntteSetting.smoothness <= 0)
                    {
                        hitVigntteSetting.smoothness = 0;
                        isVig = false;
                    }
                }                

                if(!isDep && !isVig)
                {
                    //효과가 끝나면 false
                    isPostSwitch = false;

                    hitDepthSetting.focusDistance = depthDistance;
                    post.profile.depthOfField.settings = depthSetting;

                    hitVigntteSetting.smoothness = vigntteSmoothness;
                    post.profile.vignette.settings = vigntteStting;

                }

            }

            public void HitPostSetting()
            {
                hitDepthSetting.focusDistance = depthDistance;
                hitVigntteSetting.smoothness = vigntteSmoothness;
            }

            /// <summary>
            /// Post 초기 세팅
            /// </summary>
            void PostInit()
            {
                post.profile.depthOfField.settings = depthSetting;
                post.profile.vignette.settings = vigntteStting;
            }

        }

    }
}

