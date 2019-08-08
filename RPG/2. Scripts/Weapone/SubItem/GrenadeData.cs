using Black.Characters;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 수류탄의 기본 데이터
/// </summary>

namespace Black
{
    namespace Weapone
    {
        public class GrenadeData : MonoBehaviour
        {
            float fRage = 5; //폭발 범위            
            int minDmg;
            int maxDmg;

            //폭발 이펙트 풀링
            protected MemoryPooling pool;

            ParsingData parsing;


            public int MinDmg { get => minDmg; set => minDmg = value; }
            public int MaxDmg { get => maxDmg; set => maxDmg = value; }

            protected bool isExplosion = false;

            protected virtual void Start()
            {
                pool = GameObject.Find("MemoryPool").GetComponent<MemoryPooling>();
                parsing = GameObject.Find("ParsingData").GetComponent<ParsingData>();
            }

            /// <summary>
            ///  파싱 데이터 적용
            /// </summary>
            /// <param name="name"></param>
            protected void DataInit(string name)
            {
                for(int i=0;i<parsing.SubItemList.Count;i++)
                {
                    //데이터를 검색 해서
                    if(name.Equals(parsing.SubItemList[i].name))
                    {
                        //최소 데미지와 최대 데미지를 결정 한다
                        MinDmg = parsing.SubItemList[i].minDmg;
                        MaxDmg = parsing.SubItemList[i].maxDmg;
                    }
                }
            }

            /// <summary>
            /// 폭발 각 360            
            /// </summary>
            /// <param name="angle"></param>
            /// <returns></returns>
            public Vector3 CirclePoint(float angle)
            {
                //로컬 좌표계 기준으로 설정하기 위해 적 캐릭터의 Y 회전값을 더함
                angle += transform.eulerAngles.y;
                return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad)
                                    , 0
                                    , Mathf.Cos(angle * Mathf.Deg2Rad));
            }

            /// <summary>
            /// 수류탄은 플레이어도 데미지를 받는다
            /// 발사체의 경우 상대 캐릭터만 피해를 준다
            /// </summary>
            public void GrenadeDmg()
            {
                Collider[] colls = Physics.OverlapSphere(transform.position, fRage); //폭발 범위
                
                if (colls.Length > 0)
                {
                    for (int i = 0; i < colls.Length; i++)
                    {
                        //if (colls[i].GetComponent<HitDmg>())
                        if (colls[i].GetComponent<EnemyCtrl>())
                        {
                            Transform target = colls[i].GetComponent<EnemyCtrl>()._HitInfo;
                            colls[i].GetComponent<HitDmg>().HitDmage(target, Random.Range(MinDmg, MaxDmg));
                            //광역 피해 이펙트
                            HitEffect(target);
                        }
                            
                    }
                }
            }


            /// <summary>
            /// 범위 공격 피격 이펙트
            /// 적 캐릭터 적용 전용
            /// </summary>
            public void HitEffect(Transform target)
            {
                ParticleSystem effect = pool.GetParticlePool(pool.hitCount, pool.hitList);

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

