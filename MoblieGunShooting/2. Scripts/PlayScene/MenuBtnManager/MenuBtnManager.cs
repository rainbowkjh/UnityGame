using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Black
{
    namespace GamePlayMenu{

        public class MenuBtnManager : MonoBehaviour
        {
            [SerializeField, Header("종료 버튼을 누르면 확인 창이 나온다")]
            GameObject QuitCheckWindow;

            [SerializeField, Header("음향 크기 조절 오브젝트")]
            GameObject VolumeWindow;
            Slider[] volumeOption;

            private void Start()
            {
                QuitCheckWindow.SetActive(false);

                volumeOption = VolumeWindow.GetComponentsInChildren<Slider>();
                VolumeWindow.SetActive(false);
                VolumeInit();
            }

            private void Update()
            {
                GameManager.INSTANCE.volume.bgm = volumeOption[0].value;
                GameManager.INSTANCE.volume.sfx = volumeOption[1].value;                
            }

            /// <summary>
            /// 종료 버튼 누름
            /// </summary>
            public void QuitBtn()
            {
                //GameManager.INSTANCE.system.isPause = true;
                //Time.timeScale = 0;
                GameManager.INSTANCE.GamePause();
                QuitCheckWindow.SetActive(true);
            }

            /// <summary>
            /// 종료 확인
            /// </summary>
            public void QuitOK()
            {
                //GameManager.INSTANCE.system.isPause = false;
                //Time.timeScale = 1;
                GameManager.INSTANCE.GamePlay();
                Application.Quit();
            }

            /// <summary>
            /// 종료 취소
            /// </summary>
            public void QuitCancle()
            {
                //GameManager.INSTANCE.system.isPause = false;
                //Time.timeScale = 1;
                GameManager.INSTANCE.GamePlay();
                QuitCheckWindow.SetActive(false);
            }

            /// <summary>
            /// 소리 조절 옵션 켜기
            /// </summary>
            public void VolumeCheck()
            {
                GameManager.INSTANCE.GamePause();
                VolumeWindow.SetActive(true);
            }

            /// <summary>
            /// 소리 옵션 끄기
            /// </summary>
            public void VolumeCancle()
            {
                GameManager.INSTANCE.GamePlay();
                VolumeWindow.SetActive(false);
            }

            void VolumeInit()
            {
                volumeOption[0].value = GameManager.INSTANCE.volume.bgm;
                volumeOption[1].value = GameManager.INSTANCE.volume.sfx;
            }

        }

    }
}

