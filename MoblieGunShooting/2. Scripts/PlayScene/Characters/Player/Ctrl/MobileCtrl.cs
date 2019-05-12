using Black.Characters;
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

            void Start()
            {
                playerCtrl = GetComponent<PlayerCtrl>();
                weaponeManager = GetComponent<WeaponeManager>();

                //시작 시 현재 사용 중인 무기의 정보를 출력한다
                playerCtrl.PlayerUI.CurWeaponeInfo(weaponeManager.GetWeaponeName(weaponeID),
                      weaponeManager.GetWeaponeMinDmg(weaponeID), weaponeManager.GetWeaponeMaxDmg(weaponeID));
                             
            }

            
            void Update()
            {
                if(playerCtrl.IsLive)
                {
                    ViewJoystick();

                    if (isFireBtn)
                        FireBtn();

                    playerCtrl.EnemyInfoPrint();
                }
            }

            void ViewJoystick()
            {
                vir -= variableJoystick.Vertical;
                hor += variableJoystick.Horizontal;

                vir = Angle(vir, -45, 45);
                transform.rotation = Quaternion.Euler(0, hor, 0);

                hor = Angle(hor, -30, 30);
                Camera.main.transform.rotation = Quaternion.Euler(vir, hor, 0);
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
                weaponeManager.WeaponePos[weaponeID].GetComponentInChildren<WeaponeCtrl>().Fire();                
            }

            /// <summary>
            /// 재장전
            /// </summary>
            public void ReloadBtn()
            {
                weaponeManager.WeaponePos[weaponeID].GetComponentInChildren<WeaponeCtrl>().Reload();
            }

            /// <summary>
            /// 권총 사용
            /// </summary>
            public void UsePistolBtn()
            {
                if(!playerCtrl.IsReload)
                {
                    weaponeID = 0;

                    //new Vector3(0f, -2.2f, 0.5f);
                    playerObj.transform.localPosition = new Vector3(0f, -2.2f, 0.5f);

                    //무기 활성화
                    weaponeManager.WeaponeChange(weaponeID);
                    //애니메이션
                    playerCtrl.AniCtrl.WeaponeChange(weaponeID);

                    playerCtrl.PlayerUI.CurWeaponeInfo(weaponeManager.GetWeaponeName(weaponeID),
                      weaponeManager.GetWeaponeMinDmg(weaponeID), weaponeManager.GetWeaponeMaxDmg(weaponeID));

                    //탄 정보 변경
                    playerCtrl.PlayerUI.AmmoInfo(weaponeManager.GetWeaponeAmmo(weaponeID), weaponeManager.GetWeaponeMaxAmmo(weaponeID));
                }

            }

            /// <summary>
            /// 소총 사용
            /// </summary>
            public void UseRifleBtn()
            {
                if (!playerCtrl.IsReload)
                {
                    weaponeID = 1;                    
                    playerObj.transform.localPosition = new Vector3(0f, -2.2f, 0.0f);

                    weaponeManager.WeaponeChange(weaponeID);
                    playerCtrl.AniCtrl.WeaponeChange(weaponeID);

                    playerCtrl.PlayerUI.CurWeaponeInfo(weaponeManager.GetWeaponeName(weaponeID),
                      weaponeManager.GetWeaponeMinDmg(weaponeID), weaponeManager.GetWeaponeMaxDmg(weaponeID));

                    playerCtrl.PlayerUI.AmmoInfo(weaponeManager.GetWeaponeAmmo(weaponeID), weaponeManager.GetWeaponeMaxAmmo(weaponeID));
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
                    weaponeID = 2;
                    playerObj.transform.localPosition = new Vector3(0f, -2.2f, 0.0f);

                    weaponeManager.WeaponeChange(weaponeID);
                    playerCtrl.AniCtrl.WeaponeChange(weaponeID);

                    playerCtrl.PlayerUI.CurWeaponeInfo(weaponeManager.GetWeaponeName(weaponeID),
                      weaponeManager.GetWeaponeMinDmg(weaponeID), weaponeManager.GetWeaponeMaxDmg(weaponeID));

                    playerCtrl.PlayerUI.AmmoInfo(weaponeManager.GetWeaponeAmmo(weaponeID), weaponeManager.GetWeaponeMaxAmmo(weaponeID));
                }
            }

        }

    }
}

