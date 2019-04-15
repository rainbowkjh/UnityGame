using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Weapone
{
    public enum ItemType
    {
        Weapone, Item,
    }

    public enum WeaponeType
    {
       AR, SG, SMG, SR,
    }

    [Serializable]
    public class WeaponeData : MonoBehaviour
    {
        int id = 0;
        string weaponeName = "";
        string itemIconPath = ""; //아이템 아이콘 경로

        //소모 아이템인지 무기인지 구분
        ItemType type = ItemType.Weapone;
        //무기 종류
        WeaponeType weaponeType = WeaponeType.AR;
        
        //데미지 최소 수치1 ~최소 수치2 중 랜덤 결정
        //최대치도 같음 (같은 무기라도 최소에서 최대 데미지가 다르다)
        //결정된 수치에서 공격할때 또 랜덤 데미지를 적용
        float m_fMinDmgF = 50;
        float m_fMinDmgE = 100;

        float m_fMaxDmgF = 150;
        float m_fMaxDmgE = 200;

        //무기마다 실제 적용되는 데미지
        float m_fMinDmg = 0;
        float m_MaxDmg = 0;

        //탄창안의 최대 탄 수
        int m_nMag = 30;
        int m_nCurBullet = 30; // 현재 탄 수

        #region Set,Get
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string WeaponeName
        {
            get
            {
                return weaponeName;
            }

            set
            {
                weaponeName = value;
            }
        }

        public ItemType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public WeaponeType WeaponeType
        {
            get
            {
                return weaponeType;
            }

            set
            {
                weaponeType = value;
            }
        }

        public float FMinDmgF
        {
            get
            {
                return m_fMinDmgF;
            }

            set
            {
                m_fMinDmgF = value;
            }
        }

        public float FMinDmgE
        {
            get
            {
                return m_fMinDmgE;
            }

            set
            {
                m_fMinDmgE = value;
            }
        }

        public float FMaxDmgF
        {
            get
            {
                return m_fMaxDmgF;
            }

            set
            {
                m_fMaxDmgF = value;
            }
        }

        public float FMaxDmgE
        {
            get
            {
                return m_fMaxDmgE;
            }

            set
            {
                m_fMaxDmgE = value;
            }
        }

        public int NMag
        {
            get
            {
                return m_nMag;
            }

            set
            {
                m_nMag = value;
            }
        }

        public string ItemIconPath
        {
            get
            {
                return itemIconPath;
            }

            set
            {
                itemIconPath = value;
            }
        }

        public int NCurBullet
        {
            get
            {
                return m_nCurBullet;
            }

            set
            {
                m_nCurBullet = value;
            }
        }

        public float FMinDmg
        {
            get
            {
                return m_fMinDmg;
            }

            set
            {
                m_fMinDmg = value;
            }
        }

        public float MaxDmg
        {
            get
            {
                return m_MaxDmg;
            }

            set
            {
                m_MaxDmg = value;
            }
        }
        #endregion

    }

}
