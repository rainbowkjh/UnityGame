using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapone;

namespace Characters
{
    public class EnemyCtrl : AniCtrl
    {
        //점프 제어
        Rigidbody mRigid;
        //앉을때..
        CapsuleCollider mColl; //앉을때 콜라이더 크기를 조절한다
        [SerializeField, Header("앉고 서있을때 높이 조절(탄 발사위치)")]
        Transform m_trFirePos;

        #region Weapone
        /// <summary>
        /// 적의 무기 발사 제어
        /// </summary>
        int m_MagBullet = 30;
        int m_MaxBulelt = 30;

        float m_fMinDmg = 25;
        float m_fMaxDmg = 50;

        float m_fBulletSpeed = 5;

        bool mReload = false;

        [SerializeField, Header("발사 간격")]
        float m_fFireDelay = 0.5f;
        float m_fTime = 0.0f;

        [SerializeField, Header("총구 화염")]
        GameObject m_objMuzzle;
        ParticleSystem[] m_parMuzzle;

        [SerializeField, Header("탄의 외형을 설정한다")]
        BulletType bulletType;

        MemoryPooling m_Pool;
        #endregion

        protected override void Start()
        {
            base.Start();
            mRigid = GetComponent<Rigidbody>();
            mColl = GetComponent<CapsuleCollider>();

            m_parMuzzle = m_objMuzzle.GetComponentsInChildren<ParticleSystem>();
            m_Pool = GameObject.Find("Manager/MemoryPool").GetComponent<MemoryPooling>();

        }

        #region
        /// <summary>
        /// 캐릭터마다 AI 세팅을 할수 있게
        /// 행동별로 나누고 가상함수로 만들어
        /// 재정의 할수 있도록 함
        /// </summary>
        #endregion

        protected virtual void EnemyMove()
        {
            if(State != CharState.Crouch)
            {
                State = CharState.Run;
                RunAni(true);
            }
        }

        protected virtual void EnemyStop()
        {
            State = CharState.Idle;
            RunAni(false);
        }

        protected virtual void EnemyJump()
        {

        }

        protected virtual void EnemyAttack()
        {
            if (m_MagBullet > 0 && !mReload)
            {
                if(Time.time >= m_fTime)
                {
                    m_fTime = Time.time + m_fFireDelay + Random.Range(0.0f, 0.3f);
                    m_MagBullet--;
                    BulletSelect();
                    FireAni();
                }
  
            }
            
        }

        /// <summary>
        /// 무기 마다 사용하는 탄의 외형을 변경해준다
        /// </summary>
        protected void BulletSelect()
        {
            GameObject obj = null;

            //오브젝트 추가 시
            //enum 추가 후 컨트롤 + . 누르면 case가 추가 된다 
            switch (bulletType)
            {
                case BulletType.ArrowBlue:
                    obj = m_Pool.GetObjPool(m_Pool.nBulletMaxCount, m_Pool.BulletList);
                    break;

                case BulletType.ArrowGreen:
                    obj = m_Pool.GetObjPool(m_Pool.ArrowGreenCount, m_Pool.ArrowGreenList);
                    break;
            }

            if (obj != null)
            {
                obj.transform.position = m_trFirePos.position;
                obj.transform.rotation = m_trFirePos.rotation;

                float dmg = Random.Range(m_fMinDmg, m_fMaxDmg);
                obj.GetComponent<BulletCtrl>().FSpeed = m_fBulletSpeed;
                obj.GetComponent<BulletCtrl>().FDmg = dmg;
                obj.GetComponent<BulletCtrl>().IsPlayerBullet = false;

                obj.SetActive(true);
            }
        }


        protected virtual void EnemyReload()
        {
            if (m_MagBullet <= 0)
            {
                StartCoroutine(ReloadDelay());
            }
        }

        IEnumerator ReloadDelay()
        {
            mReload = true;

            m_MagBullet = m_MaxBulelt;
            ReloadAni();

            yield return new WaitForSeconds(1.5f);
            mReload = false;
        }

    }

}
