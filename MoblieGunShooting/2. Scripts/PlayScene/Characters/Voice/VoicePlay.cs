using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터의 음성 파일을 관리 한다
/// 재생은 파일 중에서 랜덤으로 플레이
/// </summary>
namespace Black
{
    namespace Characters
    {
        [RequireComponent(typeof(AudioSource))]
        public class VoicePlay : MonoBehaviour
        {
            AudioSource _audio;

            [SerializeField, Header("대기 음성")]
            AudioClip[] _idleSfx;

            [SerializeField, Header("공격 음성")]
            AudioClip[] _attackSfx;

            [SerializeField, Header("피격 당할떄")]
            AudioClip[] _hitSfx;

            [SerializeField, Header("쓰러질때 음성")]
            AudioClip[] _downSfx;

            private void Start()
            {
                _audio = GetComponent<AudioSource>();
            }

            /// <summary>
            /// 효과음 볼륨
            /// </summary>
            private void SfxVolume()
            {
                _audio.volume = GameManager.INSTANCE.volume.sfx;
            }

            /// <summary>
            /// 대기 효과음
            /// </summary>
            public void IdlePlay()
            {
                SfxVolume();

                if (!_audio.isPlaying)
                    _audio.PlayOneShot(_idleSfx[Random.Range(0, _idleSfx.Length)]);
            }

            /// <summary>
            /// 여러 캐릭터가 동시에 효과음을 내는 것을 
            /// 막기 위해 랜덤으로 재생 시킨다
            /// </summary>
            /// <returns></returns>
            IEnumerator IdleSfxPlayDelay()
            {
                SfxVolume();

                float ran = Random.Range(1.0f, 3.0f);

                yield return new WaitForSeconds(ran);

                if (!_audio.isPlaying)
                    _audio.PlayOneShot(_idleSfx[Random.Range(0, _idleSfx.Length)]);
            }

            /// <summary>
            /// 공격 효과음
            /// </summary>
            public void AttackPlay()
            {
                SfxVolume();

                if (!_audio.isPlaying)
                    _audio.PlayOneShot(_attackSfx[Random.Range(0, _idleSfx.Length)]);
            }

            /// <summary>
            /// 공격 당할떄
            /// </summary>
            public void HitPlay()
            {
                SfxVolume();

                if (!_audio.isPlaying)
                    _audio.PlayOneShot(_hitSfx[Random.Range(0, _idleSfx.Length)]);
            }

            /// <summary>
            /// 쓰러질때 효과음
            /// </summary>
            public void DownPlay()
            {
                SfxVolume();

                if (!_audio.isPlaying)
                    _audio.PlayOneShot(_downSfx[Random.Range(0, _idleSfx.Length)]);
            }
        }

    }
}
