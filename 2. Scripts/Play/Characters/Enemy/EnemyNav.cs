﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Characters
{
    public class EnemyNav : EnemyCtrl
    {
        NavMeshAgent m_Nav;

        Transform m_trTarget;

        /// <summary>
        /// 대부분 적은 특정 위치에서 일정 간격으로 공격을 한다
        /// </summary>
        [SerializeField, Header("위치 고정(t :추적 안함)")]
        bool isLookup = false;

        /// <summary>
        /// 범위에 들어오면 추적 시작
        /// </summary>
        [SerializeField, Header("추적 시작 거리")]
        float m_fTrackingDis;

        /// <summary>
        /// 공격을 한다
        /// </summary>
        [SerializeField, Header("공격 사정 거리")]
        float m_fAttackDis;



        protected override void Start()
        {
            base.Start();
            m_Nav = GetComponent<NavMeshAgent>();
            m_trTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            m_Nav.speed = FSpeed;

        }

      
        void Update()
        {
            if(State != CharState.Dead)
            {
                //자리 고정 캐릭터라도 공격 범위는 있기 때문에 거리를 계산 한다
                float dis = Vector3.Distance(transform.position, m_trTarget.position);

                if (!isLookup)
                {
                    if (dis <= m_fTrackingDis)
                    {
                        EnemyMove();
                    }
                }

                else
                {
                    if (dis <= m_fAttackDis)
                    {
                        EnemyStop();
                        EnemyAttack();
                        EnemyReload();
                    }
                }

                LiveAni(true);

            }

        }

        protected override void EnemyMove()
        {
            base.EnemyMove();
            m_Nav.isStopped = false;
            m_Nav.destination = m_trTarget.position;
        }

        protected override void EnemyStop()
        {
            base.EnemyStop();
            m_Nav.velocity = Vector3.zero;
            m_Nav.isStopped = true;
        }

        protected override void EnemyAttack()
        {
            base.EnemyAttack();
            transform.LookAt(new Vector3(m_trTarget.position.x,transform.position.y, transform.position.z));
        }

    }

}
