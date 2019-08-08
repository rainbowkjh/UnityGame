using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 발사체
/// 아이템에서 속성 부여 시
/// 해당 속성 활성화!!
/// </summary>
namespace Black
{
    namespace Weapone
    {
        public class ProjectilesMove : MonoBehaviour
        {
            float moveSpeed = 10f;
            int minDmg;
            int maxDmg;
            float bulletDis; //탄 비활성화 타임

            //총에서 속석을 받아와 적용시킨다
            bool isExplosion = false;
            float fExplosionArea = 0; //폭발 범위
            bool isStun = false;
            float fStunPer = 0;

            bool isPlayerBullet = false; //플레이어가 발사한 탄인지 구별

            public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
            public int MinDmg { get => minDmg; set => minDmg = value; }
            public int MaxDmg { get => maxDmg; set => maxDmg = value; }
            public bool IsPlayerBullet { get => isPlayerBullet; set => isPlayerBullet = value; }
            public bool IsExplosion { get => isExplosion; set => isExplosion = value; }
            public bool IsStun { get => isStun; set => isStun = value; }
            public float FStunPer { get => fStunPer; set => fStunPer = value; }
            public float FExplosionArea { get => fExplosionArea; set => fExplosionArea = value; }
            public float FBulletDis { get => bulletDis; set => bulletDis = value; }
            public MemoryPooling Pool { get => pool; set => pool = value; }

            MemoryPooling pool;

            private void Start()
            {
                Pool = GameObject.Find("MemoryPool").GetComponent<MemoryPooling>();
            }

            private void Update()
            {
                //탄을 앞으로 날림
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

                StartCoroutine(BulletDis(bulletDis));
            }

            /// <summary>
            /// 탄 오브젝트 비활성화
            /// </summary>
            /// <returns></returns>
            IEnumerator BulletDis(float delay)
            {
                yield return new WaitForSeconds(delay); //탄은 5초가 적당
                gameObject.SetActive(false);
            }


            /// <summary>
            /// 이펙트 실행
            /// </summary>
            public void HitEffect()
            {
                ParticleSystem effect = Pool.GetParticlePool(Pool.hitCount, Pool.hitList);

                if (effect != null)
                {
                    effect.transform.position = transform.position; //활성화 위치를 타격 위치로 한다
                    if (Camera.main.gameObject != null)
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
            public void HitEffect(Transform target)
            {
                ParticleSystem effect = Pool.GetParticlePool(Pool.hitCount, Pool.hitList);

                if (effect != null)
                {
                    effect.transform.position = target.position; //활성화 위치를 타격 위치로 한다
                    effect.transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z)); //카메라를 바라본다
                    effect.gameObject.SetActive(true); //활성화
                    effect.Play(); //이펙트 재생

                    //StartCoroutine(Pool.ParticleFalse(effect, 1.0f)); //비활성화
                }
            }

            
        }

    }
}
