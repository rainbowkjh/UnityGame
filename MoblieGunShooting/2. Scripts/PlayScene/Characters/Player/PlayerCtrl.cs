using Black.CameraUtil;
using Black.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 캐릭터의 기본 데이터 및 필요 클래스를 연결한다
/// 모바일 또는 마우스 조작에서 제어
/// 
/// </summary>
namespace Black
{
    namespace Characters
    {
        [RequireComponent(typeof(AniCtrl))]
        public class PlayerCtrl : CharactersData
        {
            
            [SerializeField,Header("에임 (애니메이션 실행)")]
            AimUI aimUI;

            PlayerUI playerUI;

            [SerializeField, Header("발사 시작 지점")]
            Transform firePosTr;

            AniCtrl aniCtrl;

            [SerializeField, Header("헬기 또는 자동차 탑승 상태")]
            bool isDrive = false;

            /// <summary>
            /// 쉐이크 카메라를 사용하면서
            /// 카메라릭을 회전 시킨다
            /// </summary>
            [SerializeField, Header("카메라")]
            Transform cameraRigTr;

            /// <summary>
            /// 캐릭터 쓰러질떄 카메라 연출
            /// </summary>
            [SerializeField, Header("캐릭터 쓰러질때 활성화 카메라")]
            DeadCam deadCamera;

            FadeOut fadeOutCg;

            [SerializeField, Header("플레이어를 이벤트 연출로 사용할지 결정, 페이드 아웃 시 메인 버튼 관계")]
            bool isEventPlayer = false;

            bool isDead = false;

            [SerializeField]
            MarkerCam markerCamTr;

            #region Set,Get


            public AimUI AimUI
            {
                get
                {
                    return aimUI;
                }

                set
                {
                    aimUI = value;
                }
            }

            public PlayerUI PlayerUI
            {
                get
                {
                    return playerUI;
                }

                set
                {
                    playerUI = value;
                }
            }

            public Transform FirePosTr
            {
                get
                {
                    return firePosTr;
                }

                set
                {
                    firePosTr = value;
                }
            }

            public AniCtrl AniCtrl
            {
                get
                {
                    return aniCtrl;
                }

                set
                {
                    aniCtrl = value;
                }
            }

            public bool IsDrive
            {
                get
                {
                    return isDrive;
                }

                set
                {
                    isDrive = value;
                }
            }

            public Transform CameraRigTr
            {
                get
                {
                    return cameraRigTr;
                }

                set
                {
                    cameraRigTr = value;
                }
            }

            public bool IsDead
            {
                get
                {
                    return isDead;
                }

                set
                {
                    isDead = value;
                }
            }

            public DeadCam DeadCamera
            {
                get
                {
                    return deadCamera;
                }

                set
                {
                    deadCamera = value;
                }
            }

            public FadeOut FadeOutCg
            {
                get
                {
                    return fadeOutCg;
                }

                set
                {
                    fadeOutCg = value;
                }
            }

            public bool IsEventPlayer
            {
                get
                {
                    return isEventPlayer;
                }

                set
                {
                    isEventPlayer = value;
                }
            }

            public MarkerCam MarkerCamTr
            {
                get
                {
                    return markerCamTr;
                }

                set
                {
                    markerCamTr = value;
                }
            }

            #endregion

            private void Awake()
            {
                //   Debug.Log("PlayerCtrl");
                AniCtrl = GetComponent<AniCtrl>();
                PlayerUI = GetComponent<PlayerUI>();

                PlayerUI.CurHP(Hp, MaxHp);

                FadeOutCg = GetComponent<FadeOut>();
            }

            /// <summary>
            /// 에임이 적을 바라보면 정보를 보여준다 
            /// </summary>
            public void EnemyInfoPrint()
            {
                RaycastHit hit;
                int layerMask = 1 << 8;
                layerMask = ~layerMask;

                if (Physics.Raycast(FirePosTr.position, FirePosTr.forward,
                  out hit, 500, layerMask))
                {
                    Debug.DrawLine(FirePosTr.position, hit.point, Color.blue);
                    if (hit.transform.GetComponent<CharactersData>())
                    {
                        PlayerUI.EnemyHpInfoAct();
                        playerUI.EnemyHpInfo(hit.transform.GetComponent<CharactersData>().Hp,
                            hit.transform.GetComponent<CharactersData>().MaxHp);                        
                    }

                    else
                    {
                        PlayerUI.EnmryHpInfoDisable();
                    }
                }
            }

            /// <summary>
            /// 다음 위치로 이동 시킨다
            /// </summary>
            public void PlayerNextMove()
            {
                if(NextMove != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                                                NextMove.position, Speed * Time.deltaTime);
                }
            }


        }

    }
}
