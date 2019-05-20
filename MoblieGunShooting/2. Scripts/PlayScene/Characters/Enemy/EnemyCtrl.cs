using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 좀비와 총을 사용하는 적 캐릭터의 상위 클래스
/// </summary>
namespace Black
{
    namespace Characters
    {
        [RequireComponent(typeof(NavMeshAgent))]
        public class EnemyCtrl : CharactersData
        {
            EnemyAniCtrl ani;

            protected NavMeshAgent nav;

            /// <summary>
            /// 쓰러지는 애니메이션을 
            /// 연속 재생 되지 않도록 제어
            /// </summary>
            protected bool isDead = false;

            [SerializeField, Header("배경?? 캐릭터")]
            protected bool isExtra = false;

            //공격 대기 상태
            protected bool AttackWait = false;

            [SerializeField, Header("Attack Bar UI")]
            protected AttackBar attackBar;

            protected float delayTime = 0;

            [SerializeField, Header("Attck Min Dmg"), Range(10f, 100f)]
            float minDmg;

            [SerializeField, Header("Attck Max Dmg"), Range(50f, 200f)]
            float maxDmg;

            [SerializeField, Header("Attack Min Speed"), Range(0.01f, 0.02f)]
            float minAttackSpeed;

            [SerializeField, Header("Attack Max Speed"), Range(0.5f, 1f)]
            float maxAttackSpeed;

            protected VoicePlay voice;

            #region Set,Get
            public EnemyAniCtrl Ani
            {
                get
                {
                    return ani;
                }

                set
                {
                    ani = value;
                }
            }

            public float MinAttackSpeed
            {
                get
                {
                    return minAttackSpeed;
                }

                set
                {
                    minAttackSpeed = value;
                }
            }

            public float MaxAttackSpeed
            {
                get
                {
                    return maxAttackSpeed;
                }

                set
                {
                    maxAttackSpeed = value;
                }
            }
            #endregion

            virtual protected void Start()
            {
                Ani = GetComponent<EnemyAniCtrl>();
                nav = GetComponent<NavMeshAgent>();

                voice = GetComponent<VoicePlay>();
            }

            /// <summary>
            /// 풀링에서 활성화 시킬때 초기값 설정
            /// </summary>
            /// <param name="live"></param>
            /// <param name="maxHp"></param>
            public void EnemyInit(bool live, float maxHp, float speed, bool dead)
            {
                IsLive = live;
                Hp = maxHp;
                MaxHp = maxHp;
                isDead = dead;
                Speed = speed;


            }

            /// <summary>
            /// 공격 딜레이(UI)
            /// </summary>
            virtual  protected void AttackDelayTime()
            {
                //Debug.Log("Zombie");

                attackBar.LookAtCam();

                //공격 게이지 작동
                AttackWait = true;
               // IsFire = false;

                float Ran = Random.Range(MinAttackSpeed, MaxAttackSpeed);

                delayTime += Time.deltaTime * Ran;

                attackBar.AttackGauge(delayTime);
                

                if (delayTime >= 1.0f)
                {
                    //delayTime = 0;
                    //IsFire = true;
                    //AttackWait = false;
                    //Ani.AttackAni();                  
                    StartCoroutine(AttackDelay());
                }

                //Debug.Log("Attack Text : " + attackBar.name);

                //데인져 텍스트 깜빡 거림
                //attackBar.AttackBarAni(IsFire);
                attackBar.AttackBarAni(AttackWait);
            }

            /// <summary>
            /// 공격 데미지 적용 타이밍
            /// </summary>
            /// <returns></returns>
            IEnumerator AttackDelay()
            {
                delayTime = 0;
                IsFire = true;
                AttackWait = false;
                Ani.AttackAni();

                yield return new WaitForSeconds(1.0f);
                IsFire = false;
            }

            protected void AttackCancle()
            {
                AttackWait = false;
                attackBar.AttackBarAni(AttackWait);
            }

            /// <summary>
            /// 데미지 값을 반환
            /// </summary>
            /// <returns></returns>
            public float Dmg()
            {
                return Random.Range(minDmg, maxDmg);
                
            }
        }

    }
}

