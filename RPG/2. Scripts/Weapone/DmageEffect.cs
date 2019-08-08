using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AttackBox, 탄 충돌 스크립트에서
/// 공통 부분이 있어
/// 클래스 하나로 묶었음
/// 피격 시 이펙트 효과(광역 피해)
/// </summary>

namespace Black
{
    namespace Weapone
    {
        public class DmageEffect : MonoBehaviour
        {
            MemoryPooling pool;

            public MemoryPooling Pool { get => pool; set => pool = value; }

            private void Start()
            {
                Pool = GameObject.Find("MemoryPool").GetComponent<MemoryPooling>();
            }

            /// <summary>
            /// 이펙트 실행
            /// </summary>
          protected  void HitEffect()
            {
                ParticleSystem effect = Pool.GetParticlePool(Pool.hitCount, Pool.hitList);

                if (effect != null)
                {
                    effect.transform.position = transform.position; //활성화 위치를 타격 위치로 한다
                    effect.transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z)); //카메라를 바라본다
                    effect.gameObject.SetActive(true); //활성화
                    effect.Play(); //이펙트 재생

                    //비활성화 탄이 사라지면 실행디 되지 않아 파티클 옵션에서 비활성화 시킴
                    StartCoroutine(Pool.ParticleFalse(effect, 1.0f));
                }
            }

            /// <summary>
            /// 범위 공격 피격 이펙트
            /// 적 캐릭터 적용 전용
            /// </summary>
            protected void HitEffect(Transform target)
            {
                ParticleSystem effect = Pool.GetParticlePool(Pool.hitCount, Pool.hitList);

                if (effect != null)
                {
                    effect.transform.position = target.position; //활성화 위치를 타격 위치로 한다
                    effect.transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z)); //카메라를 바라본다
                    effect.gameObject.SetActive(true); //활성화
                    effect.Play(); //이펙트 재생

                    StartCoroutine(Pool.ParticleFalse(effect, 1.0f)); //비활성화
                }
            }

        }

    }
}
