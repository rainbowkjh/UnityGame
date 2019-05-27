using Black.Characters;
using Black.Manager;
using Black.Weapone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 마우스 조작
/// </summary>
namespace Black
{
    namespace GameCtrlManager
    {
        public class MouseCtrl : MonoBehaviour
        {

            PlayerCtrl playerCtrl;

            [SerializeField,Header("캐릭터(FPS 오브젝트) 위치 조정")]
            Transform playerObj;

            WeaponeManager weaponeManager;
            
            ItemManager itemManager;

            float mouseX = 0.0f;
            float mouseY = 0.0f;

            /// <summary>
            /// 무기 타입(애니메이션 실행)
            /// </summary>
            int weaponeID = 0;

            void Start()
            {
                playerCtrl = GetComponent<PlayerCtrl>();
                weaponeManager = GetComponent<WeaponeManager>();
                itemManager = GetComponent<ItemManager>();

                if (playerCtrl.IsDrive)
                {
                    weaponeManager.WeaponeChange(3);
                    weaponeID = 3;
                }
                if (!playerCtrl.IsDrive)
                {
                    weaponeManager.WeaponeChange(0);
                    weaponeID = 0;
                }

            }

            void Update()
            {
                if (!GameManager.INSTANCE.system.isPause)
                {
                    //시작 시 현재 사용 중인 무기의 정보를 출력한다
                    playerCtrl.PlayerUI.CurWeaponeInfo(weaponeManager.GetWeaponeName(weaponeID),
                      weaponeManager.GetWeaponeMinDmg(weaponeID), weaponeManager.GetWeaponeMaxDmg(weaponeID));

   
                    if (playerCtrl.IsLive)
                    {
                        MouseView();
                        WeaponeSwap();

                        if(!playerCtrl.IsStop)
                        {
                            playerCtrl.PlayerNextMove();
                        }


                        GunFire();
                        GunReload();
                        GrenadeUse();
                        RecoveryUse();

                        Test2();

                        playerCtrl.EnemyInfoPrint();
                    }
                }

                    
            }

            /// <summary>
            /// 마우스를 회전하는 방향으로 바라본다
            /// 캐릭터 회전
            /// 상하는 카메라를 회전 시켜야 한다
            /// </summary>
            void MouseView()
            {
                mouseX -= Input.GetAxis("Mouse Y");
                mouseY += Input.GetAxis("Mouse X");

                ////캐릭터를 좌/우로 회전
                ////탑승상태에서는 시야 제한
                //if (playerCtrl.IsDrive)
                //    mouseY = Angle(mouseY, -45, 45);

                ////카메라를 좌/우, 상/하로 회전
                mouseX = Angle(mouseX, -30, 90);

                transform.rotation = Quaternion.Euler(0, mouseY, 0);
                //Camera.main.transform.rotation = Quaternion.Euler(mouseX, mouseY, 0);
                //Camera.main.transform.localRotation = Quaternion.Euler(mouseX, mouseY, 0);
                playerCtrl.CameraRigTr.localRotation = Quaternion.Euler(mouseX, mouseY, 0); //카메라의 상위 오브젝트를 회전, 카메라는 사격 시 흔들림 효과를 준다
            }

            /// <summary>
            /// 앵글의 최소와 최대 범위 지정
            /// </summary>
            /// <param name="angle"></param>
            /// <param name="min"></param>
            /// <param name="max"></param>
            /// <returns></returns>
            float Angle(float angle, float min, float max)
            {
                if (angle < min)
                    angle = min;
                if (angle > max)
                    angle = max;

                return angle;
            }

            /// <summary>
            /// 무기 변경 시 캐릭터의 팔의 위치를 변경
            /// </summary>
            void WeaponeSwap()
            {
                if(!playerCtrl.IsReload)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        weaponeID = 0;
                        playerObj.transform.localPosition = new Vector3(0f, -2.2f, 0.5f);
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        weaponeID = 1;
                        playerObj.transform.localPosition = new Vector3(0f, -2.2f, 0f);
                    }

                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {                        
                        weaponeID = 2;                        
                        playerObj.transform.localPosition = new Vector3(0f, -2.2f, 0f);                       
                    }

                    //무기 활성화
                    weaponeManager.WeaponeChange(weaponeID);
                    //애니메이션
                    playerCtrl.AniCtrl.WeaponeChange(weaponeID);

                    //무기 교체 시 UI 정보 변경
                    playerCtrl.PlayerUI.CurWeaponeInfo(weaponeManager.GetWeaponeName(weaponeID),
                        weaponeManager.GetWeaponeMinDmg(weaponeID), weaponeManager.GetWeaponeMaxDmg(weaponeID));

                    //탄 정보 변경 
                    playerCtrl.PlayerUI.AmmoInfo(weaponeManager.GetWeaponeAmmo(weaponeID), weaponeManager.GetWeaponeMaxAmmo(weaponeID));
                }
            }

            /// <summary>
            /// 마우스 버튼으로 공격
            /// </summary>
            void GunFire()
            {
                #region 버그 있음  
                
                //권총과 샷건일때
                if (weaponeID == 0 || weaponeID == 2)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        weaponeManager.WeaponePos[weaponeID].GetComponentInChildren<WeaponeCtrl>().Fire();
                        
                    }
                }

                //소총일때
                else if (weaponeID == 1)
                {
                    if (Input.GetMouseButton(0))
                    {
                        weaponeManager.WeaponePos[weaponeID].GetComponentInChildren<WeaponeCtrl>().Fire();
                        
                    }
                } 
                
                #endregion
                
            }

            /// <summary>
            /// 재장전
            /// </summary>
            void GunReload()
            {
                if (Input.GetMouseButtonDown(1))
                    weaponeManager.WeaponePos[weaponeID].GetComponentInChildren<WeaponeCtrl>().Reload();

            }

            /// <summary>
            /// 폭발!
            /// </summary>
            void GrenadeUse()
            {
                if(Input.GetKeyDown(KeyCode.S))
                {
                    itemManager.UseGrenade();
                }
            }

            void RecoveryUse()
            {
                if(Input.GetKeyDown(KeyCode.D))
                {
                    itemManager.UseRecovery();
                }
            }

            void Test2()
            {
                if(Input.GetKeyDown(KeyCode.Z))
                {
                    playerCtrl.MaxHp = 100000;
                    playerCtrl.Hp = 100000;
                }
            }

            void Test()
            {
                if (Input.GetKeyDown(KeyCode.Q))
                    weaponeManager.WeaponePos[weaponeID].GetComponentInChildren<WeaponeCtrl>().TestSfx(0);

                if(Input.GetKey(KeyCode.W))
                    weaponeManager.WeaponePos[weaponeID].GetComponentInChildren<WeaponeCtrl>().TestSfx(0);

                if (Input.GetKeyDown(KeyCode.E))
                    weaponeManager.WeaponePos[weaponeID].GetComponentInChildren<WeaponeCtrl>().TestSfx(1);

                if (Input.GetKeyDown(KeyCode.R))
                    weaponeManager.WeaponePos[weaponeID].GetComponentInChildren<WeaponeCtrl>().TestSfx(2);


            }
        }

    }
}
