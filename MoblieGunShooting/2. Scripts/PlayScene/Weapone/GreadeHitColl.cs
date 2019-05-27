using Black.Characters;
using Black.DmgManager;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 앞에 수류탄 타격 지역에 적용
/// 수류탄 키를 입력하면
/// 범위안의 모든 적 데미지
/// </summary>
namespace Black
{
    namespace Weapone
    {
        public class GreadeHitColl : MonoBehaviour
        {
            bool isAttack = false;

            float greadeDMG = 100;

            public bool IsAttack
            {
                get
                {
                    return isAttack;
                }

                set
                {
                    isAttack = value;
                }
            }

            public float GreadeDMG
            {
                get
                {
                    return greadeDMG;
                }

                set
                {
                    greadeDMG = value;
                }
            }

            [SerializeField, Header("폭발 이펙트")]
            ParticleSystem explosionPar;

            [SerializeField, Header("폭발 효과음")]
            AudioClip[] _sfx;

            AudioSource _audio;

            private void Start()
            {
                _audio = GetComponent<AudioSource>();
            }

            /// <summary>
            /// 파티클 실행
            /// </summary>
            public void ExplosionPaly()
            {
                explosionPar.Play();
                SfxPlay();
            }
            
            //효과음
            public void SfxPlay()
            {
                if(_audio == null)
                {
                    _audio = GetComponent<AudioSource>();
                }

                
                _audio.volume = GameManager.INSTANCE.volume.sfx;

                _audio.PlayOneShot(_sfx[0]);
            }

        }

    }
}
