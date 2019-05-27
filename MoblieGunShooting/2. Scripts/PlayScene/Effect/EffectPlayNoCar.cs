using Black.CameraUtil;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ExlposionAfter이 호환이 되지 않아 새로 만듬;;;
/// 이벤트 연출로 폭발 이펙트 실행
/// </summary>
namespace Black
{
    namespace Effect
    {
        public class EffectPlayNoCar : MonoBehaviour
        {
            [SerializeField, Header("Effect")]
            ParticleSystem explosionPar;

            AudioSource _audio;
            [SerializeField, Header("Explosion Sfx")]
            AudioClip[] _sfx;

            [SerializeField, Header("폭발 타이머")]
            float explosionTimer;
            float repeatTimer = 0; //반복 이펙트인 경우 임시 값을 저장 하여 지정해준다

            bool isExplosion = false;

            shakeCamera shake;

            [SerializeField,Header("이펙트를 무한 반복할지 결정")]
            bool isRepeat = false;

            void Start()
            {
                _audio = GetComponent<AudioSource>();
                shake = GameObject.FindGameObjectWithTag("ShakeCam").GetComponent<shakeCamera>();
                repeatTimer = explosionTimer;
            }

            
            void Update()
            {
                if(!isExplosion && !GameManager.INSTANCE.system.isPause)
                {
                    //타이머
                    if (explosionTimer > 0)
                    {
                        explosionTimer -= Time.deltaTime * 1.0f;

                    }

                    //이펙트 실행
                    if (explosionTimer <= 0)
                    {
                        StartCoroutine(shake.ShakeCamera(0.1f,0.3f,0.3f));

                        isExplosion = true;

                        explosionTimer = 0;

                        if (isRepeat)
                        {
                            isExplosion = false;
                            explosionTimer = repeatTimer;
                        }
                            

                        explosionPar.Play();
                        _audio.volume = GameManager.INSTANCE.volume.sfx;
                        _audio.PlayOneShot(_sfx[0]);
                    }
                }                

                
            }

        }

    }
}
