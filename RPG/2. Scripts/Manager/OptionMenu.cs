using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.PostProcessing;

/// <summary>
/// 옵션 메뉴를 관리 한다
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class OptionMenu : MonoBehaviour
        {
            [SerializeField, Header("BGM")]
            Slider bgmVolume;

            [SerializeField, Header("SFX")]
            Slider sfxVolume;

            [SerializeField,Header("PostProcessing 선택 ")]
            Text postState;

            [SerializeField,Header("포스트 옵션을 위해 카메라들을 가져온다")]
            PostProcessingBehaviour[] post;

            private void Start()
            {
                bgmVolume.value = GameManager.INSTANCE.Bgm.sfxVolume;
                sfxVolume.value = GameManager.INSTANCE.Bgm.bgmVolume;

                if (GameManager.INSTANCE.IsPostOn)
                {
                    PostOn();
                }
                else if(!GameManager.INSTANCE.IsPostOn)
                {
                    PostOff();
                }

            }

            private void Update()
            {
                GameManager.INSTANCE.Bgm.sfxVolume = sfxVolume.value;
                GameManager.INSTANCE.Bgm.bgmVolume = bgmVolume.value;
            }

            /// <summary>
            /// 포스트프로세싱 끄기
            /// </summary>
            public void PostBtnL()
            {
                PostOff();
            }

            /// <summary>
            /// 끄기
            /// </summary>
            private void PostOff()
            {
                for(int i=0;i<post.Length;i++)
                {
                    post[i].enabled = false;
                }

                GameManager.INSTANCE.IsPostOn = false;
                postState.text = "Off";
            }

            /// <summary>
            /// 포스트프로세싱 켜기
            /// </summary>
            public void PostBtnR()
            {
                PostOn();
            }

            /// <summary>
            /// 켜기
            /// </summary>
            private void PostOn()
            {
                for (int i = 0; i < post.Length; i++)
                {
                    post[i].enabled = true;
                }

                GameManager.INSTANCE.IsPostOn = true;
                postState.text = "On";
            }

        }
        //end
    }
}
