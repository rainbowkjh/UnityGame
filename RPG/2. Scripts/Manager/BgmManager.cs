using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 배경음 관리
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class BgmManager : MonoBehaviour
        {

            AudioSource _audio;
            [SerializeField, Header("배경음")]
            AudioClip[] _bgm;

            /// <summary>
            /// 게임중, 이벤트 등 상황에 맞게 재생
            /// </summary>
            [SerializeField,Header("재생 인덱스(시작 시 배경음 설정)")]
            int bmgIndex = 0;

            [SerializeField, Header("시작 시 배겨음 재생")]
            bool isBgmStart = true;

            void Start()
            {
                _audio = GetComponent<AudioSource>();

                _audio.volume = GameManager.INSTANCE.Bgm.bgmVolume;
                if (isBgmStart)
                    GameManager.INSTANCE.BgmPlay(_audio, _bgm[bmgIndex]);
            }

            private void Update()
            {
                _audio.volume = GameManager.INSTANCE.Bgm.bgmVolume;
            }

            /// <summary>
            /// 상황에 맞게 변경 할때 호출 시킨다
            /// 
            /// 이벤트 시작 및 종료 시 변경(EventCamCtrl)
            /// Boss 처치 시 변경(QuestClear)
            /// </summary>
            /// <param name="index"></param>
            public void BgmIndexPlay(int index)
            {
                GameManager.INSTANCE.BgmPlay(_audio, _bgm[index]);
            }

        }
    }
}
