using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 날아가는 탄의 충격 체크
/// 데미지를 적용 시킨다
/// </summary>
namespace Black
{
    namespace Weapone
    {
        public class ProjectilesColl : MonoBehaviour
        {

            ProjectilesMove proMove;
            //MemoryPooling pool;

            ParticleSystem bulletEffect; //탄 충돌 시 해당 속성의 이펙트 실행
            
            void Start()
            {
                proMove = GetComponent<ProjectilesMove>();
                //pool = GameObject.Find("MemoryPool").GetComponent<MemoryPooling>();
            }


            /// <summary>
            /// 피격 시 데미지
            /// </summary>
            /// <param name="other"></param>
            private void OnTriggerEnter(Collider other)
            {
                //Debug.Log("Coll");

                //적 캐릭터의 탄일경우 플레이어만 확인한다
                if (!proMove.IsPlayerBullet)
                {
                    if (other.transform.tag.Equals("Player"))
                    {
                        proMove.HitEffect();
                        BulletHitEffect();

                        //if (proMove.IsExplosion) //폭발 효과 데미지(범위 공격)
                        //    ExplosionDmg();

                        if (proMove.IsStun)
                            other.GetComponent<HitDmg>().StunDelayAni(proMove.FStunPer); //기절 효과(데미지는 밑에서 처리)

                        Transform target = other.GetComponent<PlayerCtrl>()._DmgUI;
                        other.GetComponent<HitDmg>().HitDmage(target,Random.Range(proMove.MinDmg, proMove.MaxDmg));
                        other.GetComponent<UIBar>().HpBar();

                        /* 나중에 HP말고 Mana등 다른 수치에 데미지를 줄 경우 UI 동기화
                         other.GetComponent<UIBar>().ManaBar();
                        other.GetComponent<UIBar>().SatietyBar();
                        other.GetComponent<UIBar>().ThirstBar();
                         */
                        gameObject.SetActive(false);
                    }

                }
                //플레이어의 탄일 경우 적 캐릭터만 확인한다
                else if (proMove.IsPlayerBullet)
                {
                    if (other.transform.tag.Equals("Enemy"))
                    {
                        proMove.HitEffect();
                        BulletHitEffect();


                        if (proMove.IsExplosion) //폭발 효과 데미지(범위 공격)
                            ExplosionDmg();

                        if (proMove.IsStun && other.GetComponent<HitDmg>())
                            other.GetComponent<HitDmg>().StunDelayAni(proMove.FStunPer); //기절 효과(데미지는 밑에서 처리)


                        //Debug.Log("Enemy Attack");
                        if(other.GetComponent<EnemyCtrl>())
                        {
                            Transform target = other.GetComponent<EnemyCtrl>()._HitInfo;
                            other.GetComponent<HitDmg>().HitDmage(target, Random.Range(proMove.MinDmg, proMove.MaxDmg)); //기본 데미지
                        }

                        gameObject.SetActive(false);
                    }

                }

                if(other.transform.tag.Equals("OBS"))
                {
                    //장애물 충돌 시 플레이어 탄이면서 폭발 속성이 있으면
                    if(proMove.IsPlayerBullet && proMove.IsExplosion)
                    {
                        ExplosionDmg();
                    }

                    BulletHitEffect();
                    gameObject.SetActive(false);
                }
                
            }

        
            //탄 충돌 이펙트
            void BulletHitEffect()
            {
                if (!proMove.IsExplosion)
                {
                    bulletEffect = proMove.Pool.GetParticlePool(proMove.Pool.bulletHitCount, proMove.Pool.bulletHitList);
                }
                else if (proMove.IsExplosion)
                {
                    bulletEffect = proMove.Pool.GetParticlePool(proMove.Pool.GrenadeExplosionCount, proMove.Pool.GrenadeExplosionList);
                }

                if (bulletEffect != null)
                {
                    bulletEffect.transform.position = transform.position; //활성화 위치를 타격 위치로 한다
                    if (Camera.main.gameObject!= null)
                        bulletEffect.transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z)); //카메라를 바라본다
                    bulletEffect.gameObject.SetActive(true); //활성화
                    bulletEffect.Play(); //이펙트 재생

                    StartCoroutine(proMove.Pool.ParticleFalse(bulletEffect, 1.0f)); //비활성화
                }
            }

            /// <summary>
            /// 폭발탄의 경우 주변에도 데미지를 준다
            /// </summary>
            void ExplosionDmg()
            {

                Collider[] colls = Physics.OverlapSphere(this.transform.position, proMove.FExplosionArea, LayerMask.GetMask("Enemy"));

                if (colls.Length > 0)
                {
                    for (int i = 0; i < colls.Length; i++)
                    {
                        if (colls[i].GetComponent<HitDmg>())
                        {
                            Transform target = colls[i].GetComponent<EnemyCtrl>()._HitInfo;
                            colls[i].GetComponent<HitDmg>().HitDmage(target, Random.Range(proMove.MinDmg, proMove.MaxDmg)); //데미지 처리

                            //광역 피해 이펙트(hit)
                            proMove.HitEffect(target);
                            

                            if (proMove.IsStun)
                                colls[i].GetComponent<HitDmg>().StunDelayAni(proMove.FStunPer);
                        }
                            
                    }
                }
            }

            #region ProjectilesMove 묶음(AttackBox / CharWeaponeDmg와 공통 부분)
            /*
              /// <summary>
              /// 이펙트 실행
              /// </summary>
              void HitEffect()
              {
                  ParticleSystem effect = pool.GetParticlePool(pool.hitCount, pool.hitList);

                  if (effect != null)
                  {
                      effect.transform.position = transform.position; //활성화 위치를 타격 위치로 한다
                      effect.transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z)); //카메라를 바라본다
                      effect.gameObject.SetActive(true); //활성화
                      effect.Play(); //이펙트 재생

                      //비활성화 탄이 사라지면 실행디 되지 않아 파티클 옵션에서 비활성화 시킴
                      StartCoroutine(pool.ParticleFalse(effect, 1.0f)); 
                  }
              }


               /// <summary>
              /// 범위 공격 피격 이펙트
              /// 적 캐릭터 적용 전용
              /// </summary>
              void HitEffect(Transform target)
              {
                  ParticleSystem effect = pool.GetParticlePool(pool.hitCount, pool.hitList);

                  if (effect != null)
                  {
                      effect.transform.position = target.position; //활성화 위치를 타격 위치로 한다
                      effect.transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z)); //카메라를 바라본다
                      effect.gameObject.SetActive(true); //활성화
                      effect.Play(); //이펙트 재생

                      StartCoroutine(pool.ParticleFalse(effect, 1.0f)); //비활성화
                  }
              }
              */
            #endregion

            //private void OnDrawGizmos()
            //{
            //    Gizmos.DrawSphere(this.transform.position, proMove.FExplosionArea);
            //}
        }

    }
}
