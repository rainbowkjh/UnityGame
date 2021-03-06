﻿using Black.CameraUtil;
using Black.DmgManager;
using Black.Manager;
using Black.UI;
using Black.Weapone;
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
        [RequireComponent(typeof(AniCtrl)),
            RequireComponent(typeof(ItemManager))]
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

            [SerializeField, Header("캐릭터가 일어나는 애니메이션(플레이어 누워서 시작)")]
            bool isRiseEvent = false;

            [SerializeField, Header("플레이어를 이벤트 연출로 사용할지 결정, 페이드 아웃 시 메인 버튼 관계")]
            bool isEventPlayer = false;

            bool isDead = false;

            [SerializeField]
            MarkerCam markerCamTr;

            float tempSpeed = 0;

            [SerializeField, Header("피격 시 Post효과를 위해")]
            shakeCamera postCam;

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

            public bool IsRiseEvent
            {
                get
                {
                    return isRiseEvent;
                }

                set
                {
                    isRiseEvent = value;
                }
            }

            public shakeCamera PostCam
            {
                get
                {
                    return postCam;
                }

                set
                {
                    postCam = value;
                }
            }

            #endregion

            private void Awake()
            {
                //   Debug.Log("PlayerCtrl");
                AniCtrl = GetComponent<AniCtrl>();
                PlayerUI = GetComponent<PlayerUI>();

                FadeOutCg = GetComponent<FadeOut>();

            }

            private void Start()
            {
                PlayerDataLoad();
                PlayerUI.CurHP(Hp, MaxHp);
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

            /// <summary>
            /// 게임 시작 시 캐릭터가 일어나는 애니메이션
            /// </summary>
            public void RiseEvent()
            {
                if(IsRiseEvent)
                {
                    IsRiseEvent = false;

                    //밝아지면 일어나는 애니메이션
                    aniCtrl.ReadyAni(true); //플레이어 애니
                    deadCamera.RiseCamAni(); //카메라 애니

                    //애니메이션 실행 후 이벤트 종료, 이벤트 연출 카메라 비활성화 
                    StartCoroutine(RiseEventCancle());
                }
            }

            IEnumerator RiseEventCancle()
            {
                yield return new WaitForSeconds(2.5f);

                //연출이 끝나면 게임을 플레이 할수 있게 한다
                aniCtrl.ReadyAni(false);
                
                deadCamera.Ani.enabled = false;

              //  deadCamera.transform.rotation = Quaternion.identity;
            }

            /// <summary>
            /// 적의 폭탄을 감지한다
            /// </summary>
            /// <param name="other"></param>
            private void OnTriggerStay(Collider other)
            {
                if (other.tag.Equals("EnemyGrenadeArea"))
                {
                    if (other.GetComponent<GreadeHitColl>().IsAttack)
                    {
                        //Debug.Log("Grenade Hit");
                        GetComponent<HitDmg>().HitDamage(other.GetComponent<GreadeHitColl>().GreadeDMG);

                        other.GetComponent<GreadeHitColl>().IsAttack = false; //연속 데미지를 막기 위해 바로 false

                        playerUI.CurHP(Hp, MaxHp);
                    }
                }
            }

            /// <summary>
            /// 플레이어 데이터 초기화
            /// </summary>
            void PlayerDataLoad()
            {
                //데이터를 불러와 싱글턴에 저장된 정보를 가지고 초기화 시킨다 
                Hp = GameManager.INSTANCE.playerData.hp;
                MaxHp = GameManager.INSTANCE.playerData.maxHp;
                Speed= GameManager.INSTANCE.playerData.speed;
                IsLive= GameManager.INSTANCE.playerData.isLive;
                IsFire= GameManager.INSTANCE.playerData.isFire;
                IsReload= GameManager.INSTANCE.playerData.isReload;
                IsStop= GameManager.INSTANCE.playerData.isStop;
            }

        }

    }
}
