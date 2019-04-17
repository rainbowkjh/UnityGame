using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapone;


/// <summary>
/// ???? 적 캐릭터는 간단하게 몰아서 코딩하다보니;;;
/// 공격 부분을 무의미하게 반복 사용하는 코드가 있음;;
/// 플레이어의 경우 무기에서(GunCtrl) 제어
/// 적의 경우 자체에서 해결하다보니
/// 똑같은 코드를 사용하게됨
/// 완성 후에 관리를 위해 코드 수정! -GunCtrl에서 관리하도록
/// </summary>
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

        [SerializeField, Header("HP 세팅 값")]
        float m_fHPSetting;

        [SerializeField, Header("적이 쓰러질때 획득할 경험치")]
        float m_fMinExp = 10.0f;
        [SerializeField, Header("적이 쓰러질때 획득할 경험치")]
        float m_fMaxExp = 30.0f;

        #region Weapone

        /// <summary>
        /// 적의 무기 발사 제어
        /// </summary>
        int m_MagBullet = 30;
        int m_MaxBulelt = 30;

        [SerializeField,Header("최소 공격 데미지")]
        float m_fMinDmg = 25;
        [SerializeField, Header("최대 공격 데미지")]
        float m_fMaxDmg = 50;

        float m_fBulletSpeed = 5;

        bool mReload = false;

        [SerializeField, Header("발사 간격")]
        float m_fFireDelay = 1.0f;
        float m_fTime = 0.0f;
        float m_fFireRand = 0.0f; //발사 간격을 지정한 값 기준으로 랜덤 값으로 결정 m_fFireDelay ~ 2.0f

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

            FHP = m_fHPSetting;

            m_fFireRand = Random.Range(m_fFireDelay, 2.0f);
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

        //protected virtual void EnemyJump()
        //{

        //}

        protected virtual void EnemyAttack()
        {
            if (m_MagBullet > 0 && !mReload)
            {
                if(Time.time >= m_fTime)
                {
                    m_fTime = Time.time + m_fFireRand + Random.Range(0.0f, 0.3f);
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
