using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 배경음악을 관리
/// 업그래이드 메뉴가 있는 스테이지의 경우
/// 0은 기본 처음 재생 후
/// 업그래이드 종료 후 배경음 변경(업그래이드 종류 후 바로 게임 시작)
/// 
/// 업그래이드 메뉴가 없는 스테이지의 경우
/// 상황에 맞게 음을 교체한다(인덱스 값을 변경)
/// </summary>
namespace Black
{
    namespace Manager
    {
        [RequireComponent(typeof(AudioSource))]
        public class BgmManager : MonoBehaviour
        {
            AudioSource _audio;

            [SerializeField, Header("배경음")]
            AudioClip[] _bgm;

            [SerializeField,Header("플레이 인덱스")]
            int playIndex = 0;
         
            void Start()
            {
                _audio = GetComponent<AudioSource>();

                _BgmPlay(playIndex);
            }

            private void Update()
            {
                _audio.volume = GameManager.INSTANCE.volume.bgm;

                if (!_audio.isPlaying)
                {
                    _BgmPlay(playIndex);
                }

                //Debug.Log("Volume : " + _audio.volume);
                //Debug.Log("GameManager : " + GameManager.INSTANCE.volume.bgm);
            }

            public void _BgmPlay(int index)
            {                
               
                _audio.PlayOneShot(_bgm[index]);
                playIndex = index;

            }

            public void _BgmIndexChange(int index)
            {
                _audio.Stop();

                _BgmPlay(index);
            }
        }

    }
}
