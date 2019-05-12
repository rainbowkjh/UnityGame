using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// PC와 모바일 모드 전환 시 키 입력 스크립트를 관리한다
/// (활성화, 비활성화)
/// </summary>
namespace Black
{
    namespace GameCtrlManager
    {
        [RequireComponent(typeof(MobileCtrl)), RequireComponent(typeof(MouseCtrl))]
        public class CtrlManager : MonoBehaviour
        {
            MobileCtrl mobileCtrl;
            MouseCtrl mouseCtrl;

            [SerializeField]
            bool isMobile = true;

            private void Awake()
            {
                mobileCtrl = GetComponent<MobileCtrl>();
                mouseCtrl = GetComponent<MouseCtrl>();

                CtrlChange();
            }

            private void Update()
            {
                CtrlChange();
            }

            void CtrlChange()
            {

                if(Input.GetKeyDown(KeyCode.A))
                {
                    isMobile = !isMobile;
                }

                if(isMobile)
                {
                    MobileMode();
                }
                else
                {
                    MouseMode();
                }
            }

            public void MobileMode()
            {
                mobileCtrl.enabled = true;
                mouseCtrl.enabled = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            public void MouseMode()
            {
                mobileCtrl.enabled = false;
                mouseCtrl.enabled = true;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }



        }

    }
}

