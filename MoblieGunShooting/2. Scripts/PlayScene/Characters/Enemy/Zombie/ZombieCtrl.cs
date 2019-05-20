using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 일반적인 좀비로
/// 플레이어에게 다가와 공격한다
/// 무조건 다가와 공격이므로 공격 범위만 필요
/// (추적 시작 범위 없음)
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class ZombieCtrl : EnemyCtrl
        {
            /// <summary>
            /// 추적/공격 할 대상(플레이어)
            /// </summary>
            Transform TargetTr;            

            [SerializeField, Header("공격 시작 범위")]
            float attackDis;

            [Header("좀비 대기 애니메이션 실행?")]
            public bool isEatAni = false;

            override protected void Start()
            {
                base.Start();
                TargetTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

                nav.speed = Speed;
            }

            void Update()
            {
                if (IsLive && !isExtra)
                {
                    float dis = Vector3.Distance(TargetTr.position, this.transform.position);

                    //공격 범위보다 멀리 있으면
                    //플레이어 추적
                    if (dis > attackDis)
                    {
                        Tracking();
                    }

                    if (dis <= attackDis)
                    {
                        Attack();
                    }

                }

                else if(isExtra)
                {
                    voice.IdlePlay();
                    Ani.EatingAni(isEatAni);
                }

                if (!IsLive && Hp == 0 && !isDead)
                {
                    isDead = true;
                    Ani.RunAni(false);
                    nav.velocity = Vector3.zero;
                    nav.isStopped = true;

                    Ani.DeadAni();
                }

            }

            /// <summary>
            /// 공격 범위 밖
            /// </summary>
            void Tracking()
            {
                //음성
                voice.IdlePlay();

                nav.isStopped = false;
                nav.destination = TargetTr.position;

                Ani.RunAni(true);
                AttackCancle(); //공격 게이지(텍스트) 비활성화
            }

           /// <summary>
            /// 공격 범위에 들어오면 우선 정지
            /// 공격 상태에 따라 공격
            /// </summary>
           protected void Attack()
            {
                //음성
                voice.AttackPlay();

                nav.velocity = Vector3.zero;
                nav.isStopped = true;

                Ani.RunAni(false);

                AttackDelayTime();
            }



        }

    }
}
