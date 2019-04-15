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
    /// 
    /// Main에서는 총을 장착하고 사격하는 기능이 없기 떄문에
    /// 임시로 무기 데이터를 싱글턴에 적용 했다가
    /// 플레이씬이 시작되면
    /// 싱글턴에서 데이터를 받아 초기화 시킨다
    /// 
    /// </summary>
    [Serializable]
    public enum BulletType
    {
        ArrowBlue, ArrowGreen,
    }

    public class GunCtrl : WeaponeData
    {
        #region 탄 발사
        [SerializeField, Header("탄의 외형을 설정한다")]
        BulletType bulletType;

        [SerializeField, Header("발사 위치")]
        Transform m_trFirePos;

        [SerializeField, Header("총구 화염")]
        GameObject m_objMuzzle;
        ParticleSystem[] m_parMuzzle;
        #endregion

        protected PlayerCtrl player;

        MemoryPooling m_Pool;

        bool isReload = false;

        WeaponeAmmoUI m_WeaponeUI;

        //파싱 된 데이터 리스트에서 데이터를 가져오는 역할
        WeaponeParsing weaponeParsing;
        [SerializeField, Header("적용 시킬 무기의 이름")]
        string m_WeaponeName;


        protected virtual void Start()
        {
         //   Debug.Log("GunCtrl");

            m_trFirePos = GameObject.Find("PLAYER/FirePos").GetComponent<Transform>();

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
            m_parMuzzle = m_objMuzzle.GetComponentsInChildren<ParticleSystem>();
            m_Pool = GameObject.Find("Manager/MemoryPool").GetComponent<MemoryPooling>();

            weaponeParsing = GameObject.Find("ParsingData").GetComponent<WeaponeParsing>();

            m_WeaponeUI = GetComponent<WeaponeAmmoUI>();
            m_WeaponeUI.WeaponeBarUI(this);

            //무기 데이터 적용
            WeaponeDataInit();
        }

        /// <summary>
        /// 임시 초기화 태스트
        /// 게임메니저에서 데이터를 받아와야 한다
        /// </summary>
        void WeaponeDataInit()
        {
            for(int i=0;i<weaponeParsing.weaponeList.Count;i++)
            {
                if(m_WeaponeName.Equals(weaponeParsing.weaponeList[i].WeaponeName))
                {
                    Id = weaponeParsing.weaponeList[i].Id;
                    WeaponeName = weaponeParsing.weaponeList[i].WeaponeName;
                    ItemIconPath = weaponeParsing.weaponeList[i].ItemIconPath;
                    Type = weaponeParsing.weaponeList[i].Type;
                    WeaponeType = weaponeParsing.weaponeList[i].WeaponeType;

                    FMinDmgF = weaponeParsing.weaponeList[i].FMinDmgF;
                    FMinDmgE = weaponeParsing.weaponeList[i].FMinDmgE;

                    FMinDmgF = weaponeParsing.weaponeList[i].FMinDmgF;
                    FMaxDmgE = weaponeParsing.weaponeList[i].FMaxDmgE;

                    weaponeParsing.weaponeList[i].FMinDmg = UnityEngine.Random.Range(FMinDmgF, FMinDmgE);
                    weaponeParsing.weaponeList[i].MaxDmg = UnityEngine.Random.Range(FMaxDmgF, FMaxDmgE);

                    FMinDmg = weaponeParsing.weaponeList[i].FMinDmg;
                    MaxDmg = weaponeParsing.weaponeList[i].MaxDmg;

                    NMag = weaponeParsing.weaponeList[i].NMag;
                    NCurBullet = weaponeParsing.weaponeList[i].NCurBullet;
                }
            }
        }


        protected virtual void Fire()
        {
            if(Input .GetKeyDown(KeyCode.O))
            {
                if(!isReload &&
                    NCurBullet > 0)
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
