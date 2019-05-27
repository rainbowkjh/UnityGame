using Black.Characters;
using Black.Manager;
using Black.Weapone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모바일 조이스틱으로 조작
/// </summary>
namespace Black
{
    namespace GameCtrlManager
    {
        public class MobileCtrl : MonoBehaviour
        {
            public VariableJoystick variableJoystick;

            float vir = 0;
            float hor = 0;

            PlayerCtrl playerCtrl;
            WeaponeManager weaponeManager;

            [SerializeField, Header("캐릭터(FPS 오브젝트) 위치 조정")]
            Transform playerObj;

            int weaponeID = 0;

            /// <summary>
            /// 공격 버튼 눌렸을때
            /// 계속 누르고 있으면 연사
            /// </summary>
            bool isFireBtn = false;

            [SerializeField, Header("탑승 상태에서 무기 교체 버튼을 비활성화")]
            GameObject attackBtn;

            public int WeaponeID
            {
                get
                {
                    return weaponeID;
                }

                set
                {
                    weaponeID = value;
                }
            }

            void Start()
            {
              //  Debug.Log("MobileCtrl");
                playerCtrl = GetComponent<PlayerCtrl>();
                weaponeManager = GetComponent<WeaponeManager>();

                DriveCheck(); //탑승 상태 확인

            }

            
            void Update()
            {

                if(!GameManager.INSTANCE.system.isPause)
                {
                    //시작 시 현재 사용 중인 무기의 정보를 출력한다
                    playerCtrl.PlayerUI.CurWeaponeInfo(weaponeManager.GetWeaponeName(WeaponeID),
                          weaponeManager.GetWeaponeMinDmg(WeaponeID), weaponeManager.GetWeaponeMaxDmg(WeaponeID));

                    //캐릭터가 우워 있는 상태에서 게임 시작 시
                    //일어나는 애니메이션을 실행
                    playerCtrl.RiseEvent();

                    //일어나는 연출을 실행하지 않을떄
                    if (playerCtrl.IsLive && !playerCtrl.IsRiseEvent)
                    {
                    //    DriveCheck(); //탑승 상태 확인

                        ViewJoystick();

                        if (!playerCtrl.IsStop)
                        {
                            playerCtrl.PlayerNextMove();
                        }

                        if (isFireBtn)
                            FireBtn();

                        playerCtrl.EnemyInfoPrint();
                    }

                    if(!playerCtrl.IsLive && playerCtrl.Hp==0 && !playerCtrl.IsDead)
                    {
                        playerCtrl.IsDead = true;
                        //카메라 연출
                        playerCtrl.DeadCamera.DeadCamAni();

                        //playerCtrl.AniCtrl.LiveAni(playerCtrl.IsLive);
                        playerCtrl.AniCtrl.DeadAni();
                                               
                    }

                    //캐릭터가 쓰러지고 이벤트 캐릭터일때
                    if(!playerCtrl.IsLive && playerCtrl.IsEventPlayer)
                    {
                        //FadeOut
                        playerCtrl.FadeOutCg.FadeOutPlay();

                    }

                    else if(!playerCtrl.IsLive)
                    {
                        playerCtrl.FadeOutCg.GameOverFade();
                    }

                    //피격 시 Post 효과
                    if(playerCtrl.PostCam.IsPostSwitch)
                    {
                        playerCtrl.PostCam.PainCam();
                    }

                }
                
            }

            /// <summary>
            /// 탑승상태 확인
            /// </summary>
            void DriveCheck()
            {
                if (playerCtrl.IsDrive)
                {
                    weaponeManager.WeaponeChange(3);
                    WeaponeID = 3;
                    attackBtn.SetActive(false);
                }
                if (!playerCtrl.IsDrive)
                {
                    WeaponeID = 0;
                    weaponeManager.WeaponeChange(0);
                    attackBtn.SetActive(true);
                }
            }

            void ViewJoystick()
            {
                vir -= variableJoystick.Vertical * 2;
                hor += variableJoystick.Horizontal * 2;

                ////탑승 상태 시야 제한
                //if (playerCtrl.IsDrive)
                //    hor = Angle(hor, -30, 30);

                vir = Angle(vir, -45, 90);
                
                transform.rotation = Quaternion.Euler(0, hor, 0);
                // Camera.main.transform.rotation = Quaternion.Euler(vir, hor, 0);
                //Camera.main.transform.localRotation = Quaternion.Euler(vir, hor, 0); //로컬이 회전속도가 빠르다
                //playerCtrl.CameraRigTr.localRotation = Quaternion.Euler(vir, hor, 0); //카메라의 상위 오브젝트를 회전, 카메라는 사격 시 흔들림 효과를 준다
                playerCtrl.CameraRigTr.rotation = Quaternion.Euler(vir, hor, 0); //로컬로 돌리면..캐릭터 회전과 캐릭터 최상위 회전 값이 어긋나버린다..(마커 회전에 영향)

            }

            float Angle(float angle, float min, float max)
            {
                if (angle < min)
                    angle = min;
                if (angle > max)
                    angle = max;

                return angle;
            }

            /// <summary>
            /// 공격 버튼 눌렸을때
            /// </summary>
            public void FireBtnDown()
            {                
                isFireBtn = true;
            }

            /// <summary>
            /// 공격 버튼 해제
            /// </summary>
            public void FireBtnUp()
            {
                isFireBtn = false;
            }

            /// <summary>
            /// 공격 버튼 눌렸을때 발사 되도록 함
            /// </summary>
            public void FireBtn()
            {
                //Debug.Log("Mobile Fire");
                weaponeManager.WeaponePos[WeaponeID].GetComponentInChildren<WeaponeCtrl>().Fire();                
            }

            /// <summary>
            /// 재장전
            /// </summary>
            public void ReloadBtn()
            {
                weaponeManager.WeaponePos[WeaponeID].GetComponentInChildren<WeaponeCtrl>().Reload();
            }

            /// <summary>
            /// 권총 사용
            /// </summary>
            public void UsePistolBtn()
            {
                if(!playerCtrl.IsReload)
                {
                    WeaponeID = 0;

                    //new Vector3(0f, -2.2f, 0.5f);
                    playerObj.transform.localPosition = new Vector3(0f, -2.2f, 0.5f);

                    //무기 활성화
                    weaponeManager.WeaponeChange(WeaponeID);
                    //애니메이션
                    playerCtrl.AniCtrl.WeaponeChange(WeaponeID);

                    playerCtrl.PlayerUI.CurWeaponeInfo(weaponeManager.GetWeaponeName(WeaponeID),
                      weaponeManager.GetWeaponeMinDmg(WeaponeID), weaponeManager.GetWeaponeMaxDmg(WeaponeID));

                    //탄 정보 변경
                    playerCtrl.PlayerUI.AmmoInfo(weaponeManager.GetWeaponeAmmo(WeaponeID), weaponeManager.GetWeaponeMaxAmmo(WeaponeID));
                }

            }

            /// <summary>
            /// 소총 사용
            /// </summary>
            public void UseRifleBtn()
            {
                if (!playerCtrl.IsReload)
                {
                    WeaponeID = 1;                    
                    playerObj.transform.localPosition = new Vector3(0f, -2.2f, 0.0f);

                    weaponeManager.WeaponeChange(WeaponeID);
                    playerCtrl.AniCtrl.WeaponeChange(WeaponeID);

                    playerCtrl.PlayerUI.CurWeaponeInfo(weaponeManager.GetWeaponeName(WeaponeID),
                      weaponeManager.GetWeaponeMinDmg(WeaponeID), weaponeManager.GetWeaponeMaxDmg(WeaponeID));

                    playerCtrl.PlayerUI.AmmoInfo(weaponeManager.GetWeaponeAmmo(WeaponeID), weaponeManager.GetWeaponeMaxAmmo(WeaponeID));
                }
                //Debug.Log("Use Weapone  :" + weaponeManager.UseWeapone);
            }

            /// <summary>
            /// 샷건 사용
            /// </summary>
            public void ShotGunBtn()
            {
                if (!playerCtrl.IsReload)
                {
                    WeaponeID = 2;
                    playerObj.transform.localPosition = new Vector3(0f, -2.2f, 0.0f);

                    weaponeManager.WeaponeChange(WeaponeID);
                    playerCtrl.AniCtrl.WeaponeChange(WeaponeID);

                    playerCtrl.PlayerUI.CurWeaponeInfo(weaponeManager.GetWeaponeName(WeaponeID),
                      weaponeManager.GetWeaponeMinDmg(WeaponeID), weaponeManager.GetWeaponeMaxDmg(WeaponeID));

                    playerCtrl.PlayerUI.AmmoInfo(weaponeManager.GetWeaponeAmmo(WeaponeID), weaponeManager.GetWeaponeMaxAmmo(WeaponeID));
                }
            }

        }

    }
}

