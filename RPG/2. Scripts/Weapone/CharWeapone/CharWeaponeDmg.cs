using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 무기의 발사체
/// (근접 무기 전용, 원거리는 탄의 발사체 오브젝트를 이용)
/// </summary>
namespace Black
{
    namespace Weapone
    {
        public class CharWeaponeDmg : MonoBehaviour
        {            
            ProjectilesMove proMove;
            
            private void Start()
            {
                proMove = GetComponent<ProjectilesMove>();
            }

            private void OnParticleCollision(GameObject other)
            {
                               //적이 사용
                if (!proMove.IsPlayerBullet)
                {
                    if (other.transform.tag.Equals("Player"))
                    {
                        proMove.HitEffect();

                        if (proMove.IsStun)
                            other.GetComponent<HitDmg>().StunDelayAni(proMove.FStunPer); //기절 효과(데미지는 밑에서 처리)

                        Transform target = other.GetComponent<PlayerCtrl>()._DmgUI;
                        other.GetComponent<HitDmg>().HitDmage(target, Random.Range(proMove.MinDmg, proMove.MaxDmg));
                        other.GetComponent<UIBar>().HpBar();

                    }
                }

                // 플레이어의 사용
                else if (proMove.IsPlayerBullet)
                {
                    if(other.transform.tag.Equals("Enemy"))
                    {
                        proMove.HitEffect();

                        if(proMove.IsExplosion)
                        {
                            ExplosionDmg();
                        }

                        if (proMove.IsStun)
                            other.GetComponent<HitDmg>().StunDelayAni(proMove.FStunPer); //기절 효과(데미지는 밑에서 처리)

                        Transform target = other.GetComponent<EnemyCtrl>()._HitInfo;
                        other.GetComponent<HitDmg>().HitDmage(target, Random.Range(proMove.MinDmg, proMove.MaxDmg)); //기본 데미지
                    }

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

                            colls[i].GetComponent<HitDmg>().HitDmage(target, Random.Range(proMove.MinDmg, proMove.MaxDmg));

                            //광역 피해 이펙트
                            proMove.HitEffect(target);

                            if (proMove.IsStun)
                                colls[i].GetComponent<HitDmg>().StunDelayAni(proMove.FStunPer);
                        }

                    }
                }
            }
        }


    }
}
