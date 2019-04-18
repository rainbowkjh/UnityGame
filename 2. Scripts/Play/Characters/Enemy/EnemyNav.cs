using System.Collections;
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
        [SerializeField, Header("위치 고정(t :추적 안함")]
        bool isLookup = false;

        [SerializeField, Header("true : x, y, z좌표를 이용해 바라본다")]
        bool isLookFree = false;

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

        bool m_isMovePosStart = false;
        
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
                if(isMovePos)
                {
                    //Debug.Log("이동 경로");

                    if (!m_isMovePosStart)
                    {
                        m_isMovePosStart = true;
                        PosMove();
                    }
                    
                    PosNextMove();
                    
                }

                if(!isMovePos)
                {
                    //Debug.Log("플레이어 추적");

                    //자리 고정 캐릭터라도 공격 범위는 있기 때문에 거리를 계산 한다
                    float dis = Vector3.Distance(transform.position, m_trTarget.position);

                    //고정 캐릭터가 아니면 이동
                    if (!isLookup)
                    {
                        if (dis <= m_fTrackingDis)
                        {
                            EnemyMove();
                        }
                    }

                   // else
                   // {
                        if (dis <= m_fAttackDis)
                        {
                            EnemyStop();
                            EnemyAttack();
                            EnemyReload();
                        }
                    //}
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
            if (isLookFree)
            {
                transform.LookAt(new Vector3(m_trTarget.position.x, transform.position.y, m_trTarget.position.z));
            }

            else if (!isLookFree)
                transform.LookAt(new Vector3(m_trTarget.position.x, transform.position.y, transform.position.z));
        }

        void PosMove()
        {

            if (m_Nav.isPathStale)
                return;

            m_Nav.destination = movePos.MovePosTr[movePos.NextPos].position;

            State = CharState.Run;
            RunAni(true);
            m_Nav.isStopped = false;
            
        }

        void PosNextMove()
        {
            if(m_Nav.velocity.sqrMagnitude >= 0.2f *0.2f
                && m_Nav.remainingDistance <=0.5f)
            {
                PosMoveStop();
                movePos.NextPos = ++movePos.NextPos % movePos.MovePosTr.Length;
                //Debug.Log("NextPos : " + movePos.NextPos);
                PosMove();                
            }
        }

        void PosMoveStop()
        {
            if(movePos.NextPos == 0)
            {
                //Debug.Log("NextPos : " + movePos.NextPos);
                
                isMovePos = false;
            }
        }

    }

}
