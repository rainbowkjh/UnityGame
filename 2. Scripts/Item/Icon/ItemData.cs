using Manager;
using Manager.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Weapone;
using System;

namespace _Item
{   
    public class ItemData : MonoBehaviour
    {

        [SerializeField, Header("무기인지 소모 아이템인지 구분")]
        ItemType itemType;

        [SerializeField, Header("적용 시킬 아이템 이름")]
        string m_sItemName;

        /// <summary>
        /// 무기 아이템일 경우 Weapone형태로 저장
        /// </summary>
        WeaponeGameData weaponeData;

        /// <summary>
        /// 파싱 데이터에서 아이템 이름을 검색하는 역할
        /// </summary>
        WeaponeParsing m_Parsing;

        [SerializeField, Header("아이템 아이콘")]
        Image m_ItemIcon;

        [SerializeField, Header("장착 했는지 표시되는 아이콘")]
        GameObject m_UseIcon;

        //[SerializeField, Header("아이템 습득 상태(습득 되었으면 파싱 데이터에서 초기화하지 않는다")]
        public bool isEquip = false;
 
        #region Set,Get
        public ItemType ItemType
        {
            get
            {
                return itemType;
            }

            set
            {
                itemType = value;
            }
        }

        public WeaponeGameData WeaponeData
        {
            get
            {
                return weaponeData;
            }

            set
            {
                weaponeData = value;
            }
        }

        public string SItemName
        {
            get
            {
                return m_sItemName;
            }

            set
            {
                m_sItemName = value;
            }
        }

        public GameObject UseIcon
        {
            get
            {
                return m_UseIcon;
            }

            set
            {
                m_UseIcon = value;
            }
        }

        #endregion

        void Start()
        {
            m_Parsing = GameObject.Find("ParsingData").GetComponent<WeaponeParsing>();

            if (!isEquip)
            {
                //Debug.Log("아이템 파싱 데이터 적용");
                ItemSetting();
            }
                

            UseIcon.SetActive(false);
        }

        void ItemSetting()
        {
            //Debug.Log("아이템 데이터 검색 : "+m_Parsing.weaponeList.Count);
            if (ItemType == ItemType.Weapone)
            {
                for (int i = 0; i < m_Parsing.weaponeList.Count; i++)
                {
                    if (SItemName.Equals(m_Parsing.weaponeList[i].WeaponeName))
                    {
                        //Debug.Log("무기 데이터 세팅");
                        #region 파싱 데이터 적용 
                        WeaponeData = new WeaponeGameData();

                        WeaponeData.Id = m_Parsing.weaponeList[i].Id;
                        WeaponeData.WeaponeName = m_Parsing.weaponeList[i].WeaponeName;
                        WeaponeData.ItemIconPath = m_Parsing.weaponeList[i].ItemIconPath;
                        WeaponeData.Type = m_Parsing.weaponeList[i].Type;
                        WeaponeData.WeaponeType = m_Parsing.weaponeList[i].WeaponeType;

                        float randMin = UnityEngine.Random.Range(m_Parsing.weaponeList[i].FMinDmgF, m_Parsing.weaponeList[i].FMinDmgE);
                        float randMax = UnityEngine.Random.Range(m_Parsing.weaponeList[i].FMaxDmgF, m_Parsing.weaponeList[i].FMaxDmgE);

                        WeaponeData.FMinDmg = randMin;
                        WeaponeData.MaxDmg = randMax;

                        WeaponeData.NMag = m_Parsing.weaponeList[i].NMag;
                        WeaponeData.NCurBullet = m_Parsing.weaponeList[i].NCurBullet;

                        //아이콘 이미지
                        SpriteApply();
                        #endregion

                        break;
                    }
                }

            }
        }

        //아이콘 이미지 적용
        public void SpriteApply()
        {
            m_ItemIcon.sprite = Resources.Load<Sprite>(WeaponeData.ItemIconPath);
        }

    }

}
