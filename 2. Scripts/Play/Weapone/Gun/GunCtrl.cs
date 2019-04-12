using Characters;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayUI;

namespace Weapone
{
    /// <summary>
    /// 탄의 외형을 결정하는데 사용
    /// 탄 추가 시
    /// enum에 타입 추가
    /// BulletSelect()의 switch문에
    /// 타입에 맞는 오브젝트 풀링을 적용 시키면 된다
    /// </summary>
    [Serializable]
    public enum BulletType
    {
        ArrowBlue, ArrowGreen,
    }

    public class GunCtrl : WeaponeData
    {
        [SerializeField, Header("탄의 외형을 설정한다")]
        BulletType bulletType;

        [SerializeField, Header("발사 위치")]
        Transform m_trFirePos;

        [SerializeField, Header("총구 화염")]
        GameObject m_objMuzzle;
        ParticleSystem[] m_parMuzzle;

       protected PlayerCtrl player;

        MemoryPooling m_Pool;

        bool isReload = false;

        WeaponeAmmoUI m_WeaponeUI;

        protected virtual void Start()
        {
         //   Debug.Log("GunCtrl");

            m_trFirePos = GameObject.Find("PLAYER/FirePos").GetComponent<Transform>();

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
            m_parMuzzle = m_objMuzzle.GetComponentsInChildren<ParticleSystem>();
            m_Pool = GameObject.Find("Manager/MemoryPool").GetComponent<MemoryPooling>();

            m_WeaponeUI = GetComponent<WeaponeAmmoUI>();
            m_WeaponeUI.WeaponeBarUI(this);
        }

        protected virtual void Fire()
        {
            if(Input .GetKeyDown(KeyCode.O))
            {
                if(!isReload &&
                    NCurBullet>0)
                {
                    BulletSelect();

                    NCurBullet--;

                    player.FireAni();
                    MuzzlePlay();
                    m_WeaponeUI.WeaponeBarUI(this);
                }                
            }
        }

        protected virtual void Reload()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                if(!isReload)
                {
                    StartCoroutine(MagReload());
                    NCurBullet = NMag;
                    m_WeaponeUI.WeaponeBarUI(this);
                }
                
            }
        }

        IEnumerator MagReload()
        {
            isReload = true;
            player.ReloadAni();
            yield return new WaitForSeconds(1.5f);
            isReload = false;
        }

        protected void MuzzlePlay()
        {
            for(int i=0;i<m_parMuzzle.Length;i++)
            {
                m_parMuzzle[i].Play();
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

                obj.GetComponent<BulletCtrl>().IsPlayerBullet = true;

                obj.SetActive(true);
            }
        }

    }

}
