using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trooper의 폭발 효과
/// 
/// 스크립트를 적용 시킨
/// 오브젝트를 정해진 시간에
/// 폭발 시킨다
/// 
/// 또는 바로 폭발(탑승 차량 또는 헬기의 HP가 0이 될경우?)- 아직은  HP가 없다
/// 
/// ExplosionTimerStart에서 콜라이더로 타이머 작동(기본)
/// 또는 상황에 맞게 다른 스크립트에서 호출하도록 한다
/// </summary>
namespace Black
{
    namespace Effect
    {
        public class ExplosionAfter : MonoBehaviour
        {
            [SerializeField, Header("폭발 이펙트")]
            ParticleSystem explosionPar;
                        
            AudioSource _audio;

            [SerializeField, Header("폭발 효과음 오디오 소스")]
            AudioSource _explosionAudio;

            [SerializeField, Header("폭발 효과음")]
            AudioClip[] _sfx;

            [SerializeField, Header("폭발 타이머")]
            float explosionTime;

            Rigidbody rigidbody;

            [SerializeField, Header("폭발 시 위로 미는 힘")]
            float force;

            bool isExplosion = false;

            /// <summary>
            /// 타이머 작동
            /// </summary>
            bool isTimerStart = false;

            #region Set,Get
            public bool IsExplosion
            {
                get
                {
                    return isExplosion;
                }

                set
                {
                    isExplosion = value;
                }
            }

            public bool IsTimerStart
            {
                get
                {
                    return isTimerStart;
                }

                set
                {
                    isTimerStart = value;
                }
            }
            #endregion

            private void Start()
            {
                _audio = GetComponent<AudioSource>();
                rigidbody = GetComponent<Rigidbody>();
            }

            private void SfxVolume()
            {
                _audio.volume = GameManager.INSTANCE.volume.sfx * 2;
            }

            private void AudioPlay()
            {
                //탑승 차량의 엔진음을 끄고 실행                
                if (_audio.isPlaying)
                {
                    //Debug.Log("Sfx Stop");
                    _audio.Stop();
                }

                _explosionAudio.PlayOneShot(_sfx[0]);
                //Debug.Log("Sfx Play : " + _audio.isPlaying);
            }

            /// <summary>
            /// 폭발 이펙트만
            /// </summary>
            public void ExplosionTimer()
            {
                if(!IsExplosion)
                {
                    //타이머 작동
                    explosionTime -= Time.deltaTime * 1.0f;

                    //0이 되면 폭발
                    if (explosionTime <= 0)
                    {
                        explosionTime = 0;

                        ExplosionShake();

                        IsExplosion = true;

                        explosionPar.Play();

                        SfxVolume();

                        AudioPlay();
                    }
                }
                
            }

            /// <summary>
            /// 오브젝트를 비활성화
            /// </summary>
            public void ExplosionTimerEx()
            {
                if (!IsExplosion)
                {
                    explosionTime -= Time.deltaTime * 1.0f;

                    if (explosionTime <= 0)
                    {
                        //한번 폭발 시키고 비활성화 또는 제거이므로
                        //시간을 설정하지 않는다

                        IsExplosion = true;

                        explosionPar.Play();

                        SfxVolume();

                        AudioPlay();

                        StartCoroutine(DisObj());
                    }
                }

            }

            /// <summary>
            /// 오브젝트 비활성화
            /// </summary>
            /// <returns></returns>
            IEnumerator DisObj()
            {
                yield return new WaitForSeconds(1.5f);
                gameObject.SetActive(false);
            }

            /// <summary>
            /// 타이머 없이 바로 폭발
            /// </summary>
            public void ExplosionPlay()
            {
                if(!IsExplosion)
                {
                    ExplosionShake();

                    IsExplosion = true;

                    explosionPar.Play();

                    SfxVolume();

                    AudioPlay();

                }
            }

            /// <summary>
            /// 폭발 시 차량을 위로 띄운다
            /// </summary>
            void ExplosionShake()
            {
                rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
            }

        }

    }
}
