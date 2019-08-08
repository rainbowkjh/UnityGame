using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 근점 무기의 충돌 체크
/// 데미지를 준다
/// </summary>
namespace Black
{
    namespace Weapone
    {
        public class AttackBox : MonoBehaviour
        {
            [SerializeField, Header("공격 박스의 사용 객체")]
            bool isPlayerBox = false;

            //캐릭터 데이터의 정보를 얻어와서 데미지 적용
            [SerializeField,Header("데미지 테스트")]
            int minDmg;
            [SerializeField, Header("데미지 테스트")]
            int maxDmg;

            private bool isExplosion; //폭발 속성
            private float fExplosionArea; //폭발 범위
            bool isStun; //기절 속성
            float fStunPer; //기절 확률

            int nExplosionSlot;
            int nStunSlot;

            public int MinDmg { get => minDmg; set => minDmg = value; }
            public int MaxDmg { get => maxDmg; set => maxDmg = value; }
            public bool IsExplosion { get => isExplosion; set => isExplosion = value; }
            public bool IsStun { get => isStun; set => isStun = value; }
            public float FStunPer { get => fStunPer; set => fStunPer = value; }
            public int NExplosionSlot { get => nExplosionSlot; set => nExplosionSlot = value; }
            public int NStunSlot { get => nStunSlot; set => nStunSlot = value; }
            public float FExplosionArea { get => fExplosionArea; set => fExplosionArea = value; }

            [SerializeField,Header("사용 객체_공격 상테인지 확인하여 데미지 적용")]
            CharactersData charData;

            MemoryPooling pool;

            //공격 속성 관련 코드!
            //파츠 장착 시 해당 속성을 적용 시킴
            //지속 데미지 등


            private void Start()
            {
                pool = GameObject.Find("MemoryPool").GetComponent<MemoryPooling>();

            }

            private void OnTriggerStay(Collider other)
            {
                if (charData.IsAttack)
                {
                    //적 캐릭터의 탄일경우 플레이어만 확인한다
                    if (!isPlayerBox)
                    {
                        if (other.transform.tag.Equals("Player"))
                        {
                            Transform target = other.GetComponent<PlayerCtrl>()._DmgUI;
                            other.GetComponent<HitDmg>().HitDmage(target, Random.Range(MinDmg, MaxDmg));
                            other.GetComponent<UIBar>().HpBar();

                            /* 나중에 HP말고 Mana등 다른 수치에 데미지를 줄 경우 UI 동기화
                             other.GetComponent<UIBar>().ManaBar();
                            other.GetComponent<UIBar>().SatietyBar();
                            other.GetComponent<UIBar>().ThirstBar();
                             */
                            HitEffect();
                        }
                    }
                    //플레이어의 탄일 경우 적 캐릭터만 확인한다
                    else if (isPlayerBox)
                    {
                        if (other.transform.tag.Equals("Enemy"))
                        {
                            //Debug.Log("Enemy Attack");

                            //함수로 처리 했더니 타격 이팩트가 내려감;;;
                            if (IsExplosion)
                            {
                                ExplosionDmg();
                            }

                            if (IsStun)
                            {
                                other.GetComponent<HitDmg>().StunDelayAni(FStunPer);
                            }

                            Transform target = other.GetComponent<EnemyCtrl>()._HitInfo;
                            other.GetComponent<HitDmg>().HitDmage(target, Random.Range(MinDmg, MaxDmg));
                            HitEffect();
                        }
                    }
                }
            }

            #region 캐릭터 타격 부위에 충돌 박스 적용 시
            /*
              private void OnTriggerEnter(Collider other)
            {
                if(charData.IsAttack)
                {
                    //적 캐릭터의 탄일경우 플레이어만 확인한다
                    if (!isPlayerBox)
                    {
                        if (other.transform.tag.Equals("Player"))
                        {
                            other.GetComponent<HitDmg>().HitDmage(Random.Range(MinDmg, MaxDmg));
                            other.GetComponent<UIBar>().HpBar();

                            //나중에 HP말고 Mana등 다른 수치에 데미지를 줄 경우 UI 동기화
                            //other.GetComponent<UIBar>().ManaBar();
                            //other.GetComponent<UIBar>().SatietyBar();
                            //other.GetComponent<UIBar>().ThirstBar();
                             
                            HitEffect();
                        }
                    }
                    //플레이어의 탄일 경우 적 캐릭터만 확인한다
                    else if (isPlayerBox)
                    {
                        if (other.transform.tag.Equals("Enemy"))
                        {
                            //Debug.Log("Enemy Attack");
                            
                            //함수로 처리 했더니 타격 이팩트가 내려감;;;
                            if (IsExplosion)
                            {
                                ExplosionDmg();
                            }

                            if (IsStun)
                            {
                                other.GetComponent<HitDmg>().StunDelayAni(FStunPer);
                            }


                            other.GetComponent<HitDmg>().HitDmage(Random.Range(MinDmg, MaxDmg));
                            HitEffect();
                        }
                    }
                }

            }
             */

            #endregion
            /// <summary>
            /// 폭발효과(범위 공격)
            /// </summary>
            void ExplosionDmg()
            {
                Collider[] colls = Physics.OverlapSphere(this.transform.position, FExplosionArea);

                if (colls.Length > 0)
                {
                    for (int i = 0; i < colls.Length; i++)
                    {
                        if (colls[i].GetComponent<HitDmg>())
                        {
                            Transform target = colls[i].GetComponent<EnemyCtrl>()._HitInfo;

                            colls[i].GetComponent<HitDmg>().HitDmage(target,Random.Range(MinDmg, MaxDmg));

                            //광역 피해 이펙트
                            HitEffect(target);


                            if (IsStun)
                                colls[i].GetComponent<HitDmg>().StunDelayAni(FStunPer);
                        }

                    }
                }
            }

            #region ProjectilesColl에 있는 코드와 같다;;; 
            //클래스로 묶으려고 했는데;;
            //pool이 중간에 증발해버림;;

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

                    StartCoroutine(pool.ParticleFalse(effect, 1.0f)); //비활성화
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
            #endregion


        }

    }
}
