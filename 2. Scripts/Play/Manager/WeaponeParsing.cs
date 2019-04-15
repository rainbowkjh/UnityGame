using Characters;
using Manager.GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using Weapone;

namespace Manager
{
    /// <summary>
    /// 파싱할때 받을 데이터 및 저장 로드할때
    /// 사용할 데이터 클레스
    /// 이곳에 저장 및 로드 파싱 데이터를 받고
    /// 실제 사용 되는 오브젝트에 값을 전달해준다
    /// WeaponeData가 파싱 할때 null로 만들어 지기 떄문에
    /// 실제 사용과 데이터 받을때 따로 클레스를 만들어 관리
    /// 
    /// 무기와 캐릭터 파싱을 따로 하려고 하다가;;
    /// 하나로 통합 시키기로 함
    /// (그래서 스크립트 이름은 Wepaone이지만.. 캐릭터도 같이 한다)
    /// </summary>
    namespace GameData
    {
        [Serializable]
        public class WeaponeGameData
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

        [Serializable]
        public class CharactersGameData
        {
            int m_nId = 0;
            string m_sName ="";
            CharState m_State = CharState.Idle;
            int m_nLevel = 1;
            float m_fHP = 500.0f;
            float m_fMaxHP = 500.0f;
            float m_fMana = 200.0f;
            float m_fMaxMana = 200.0f;
            float m_fExp = 0;
            float m_fNextExp = 0;
            float m_fSpeed = 2.0f;
            float m_fSta = 200.0f;
            float m_fMaxSta = 200.0f;
            bool isJump = false;
            bool isRoll = false;

            #region Set,Get
            public float FHP
            {
                get
                {
                    return m_fHP;
                }

                set
                {
                    m_fHP = value;

                    if (m_fHP <= 0)
                        m_fHP = 0;

                    if (m_fHP >= m_fMaxHP)
                        m_fHP = m_fMaxHP;
                }
            }

            public float FMaxHP
            {
                get
                {
                    return m_fMaxHP;
                }

                set
                {
                    m_fMaxHP = value;
                }
            }

            public float FSpeed
            {
                get
                {
                    return m_fSpeed;
                }

                set
                {
                    m_fSpeed = value;
                }
            }

            public float FSta
            {
                get
                {
                    return m_fSta;
                }

                set
                {
                    m_fSta = value;

                    if (m_fSta <= 0)
                        m_fSta = 0;

                    if (m_fSta >= m_fMaxSta)
                        m_fSta = m_fMaxSta;
                }
            }

            public float FMaxSta
            {
                get
                {
                    return m_fMaxSta;
                }

                set
                {
                    m_fMaxSta = value;
                }
            }

            public CharState State
            {
                get
                {
                    return m_State;
                }

                set
                {
                    m_State = value;
                }
            }

            public bool IsJump
            {
                get
                {
                    return isJump;
                }

                set
                {
                    isJump = value;
                }
            }

            public float FMana
            {
                get
                {
                    return m_fMana;
                }

                set
                {
                    m_fMana = value;

                    if (m_fMana <= 0)
                        m_fMana = 0;

                    if (m_fMana >= m_fMaxMana)
                        m_fMana = m_fMaxMana;

                }
            }

            public float FMaxMana
            {
                get
                {
                    return m_fMaxMana;
                }

                set
                {
                    m_fMaxMana = value;
                }
            }

            public float FExp
            {
                get
                {
                    return m_fExp;
                }

                set
                {
                    m_fExp = value;


                }
            }

            public float FNextExp
            {
                get
                {
                    return m_fNextExp;
                }

                set
                {
                    m_fNextExp = value;
                }
            }

            public int NLevel
            {
                get
                {
                    return m_nLevel;
                }

                set
                {
                    m_nLevel = value;
                }
            }

            public bool IsRoll
            {
                get
                {
                    return isRoll;
                }

                set
                {
                    isRoll = value;
                }
            }

            public int NId
            {
                get
                {
                    return m_nId;
                }

                set
                {
                    m_nId = value;
                }
            }

            public string SName
            {
                get
                {
                    return m_sName;
                }

                set
                {
                    m_sName = value;
                }
            }


            #endregion
        }
    }
    


    public class WeaponeParsing : MonoBehaviour
    {
        string weaponeFile = "WeaponeData";

        public List<WeaponeGameData> weaponeList = new List<WeaponeGameData>();
        WeaponeGameData data;

        string playerFile = "PlayerInfo";

        public List<CharactersGameData> CharList = new List<CharactersGameData>();
        CharactersGameData charData;

        private void Awake()
        {
            LoadWeaponeXml();
            LoadPlayerXml();
        }

        //void Start()
        //{
        //    LoadWeaponeXml();
        //    LoadPlayerXml();
        //}

        void LoadWeaponeXml()
        {
            TextAsset textAsset = (TextAsset)Resources.Load(weaponeFile);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);

         //   Debug.Log("textAsset.text : " + textAsset.text);

            XmlNodeList all_node = xmlDoc.SelectNodes("dataroot/Weapone");
            foreach (XmlNode node in all_node)
            {
                data = new WeaponeGameData();

                data.Type = ItemType.Weapone;

                data.Id = Int32.Parse(node.SelectSingleNode("ID").InnerText);
                int state = Int32.Parse(node.SelectSingleNode("State").InnerText);
                WeaponeType(state); //무기 타입 결정
                data.ItemIconPath = node.SelectSingleNode("IconLocal").InnerText;
                data.WeaponeName = node.SelectSingleNode("WeaponeName").InnerText;
                

                data.FMinDmgF = Int32.Parse(node.SelectSingleNode("MinRangeF").InnerText);
                data.FMinDmgE = Int32.Parse(node.SelectSingleNode("MinRangeE").InnerText);

                data.FMaxDmgF = Int32.Parse(node.SelectSingleNode("MaxRangeF").InnerText);
                data.FMaxDmgE = Int32.Parse(node.SelectSingleNode("MaxRangeE").InnerText);
                //피격 데미지는 발사 할떄 랜덤으로 결정된다
                //여기서는 범위만 만들어 준다
                
                //info.nDamage = UnityEngine.Random.Range(info.nMinDmg, info.nMaxDmg);
                data.NMag = Int32.Parse(node.SelectSingleNode("Magazine").InnerText);

                //data.nMaxBullet = Int32.Parse(node.SelectSingleNode("MaxBullet").InnerText);
                //data.nCount = Int32.Parse(node.SelectSingleNode("Count").InnerText);
                //data.nPrice = Int32.Parse(node.SelectSingleNode("Price").InnerText);
                //data.nUpgrade = Int32.Parse(node.SelectSingleNode("UpgradePoint").InnerText);

              
                weaponeList.Add(data);

                data = null; ;
            }
        }

        void WeaponeType(int state)
        {
            switch(state)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    data.WeaponeType = Weapone.WeaponeType.AR;
                    break;
                case 3:
                    data.WeaponeType = Weapone.WeaponeType.SMG;
                    break;
                case 4:
                    data.WeaponeType = Weapone.WeaponeType.SG;
                    break;
                case 5:
                    data.WeaponeType = Weapone.WeaponeType.SR;
                    break;


            }
        }

        void LoadPlayerXml()
        {
            TextAsset textAsset = (TextAsset)Resources.Load(playerFile);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);

            //   Debug.Log("textAsset.text : " + textAsset.text);

            XmlNodeList all_node = xmlDoc.SelectNodes("dataroot/Characters");
            foreach (XmlNode node in all_node)
            {
                charData = new CharactersGameData();

                charData.NId = Int32.Parse(node.SelectSingleNode("ID").InnerText);
                charData.SName = node.SelectSingleNode("CharName").InnerText;
                charData.NLevel = Int32.Parse(node.SelectSingleNode("Level").InnerText);
                charData.FMaxHP = Int32.Parse(node.SelectSingleNode("MaxHP").InnerText);
                charData.FHP = Int32.Parse(node.SelectSingleNode("HP").InnerText);                
                charData.FMaxMana = Int32.Parse(node.SelectSingleNode("MaxMana").InnerText);
                charData.FMana = Int32.Parse(node.SelectSingleNode("Mana").InnerText);                
                charData.FExp = Int32.Parse(node.SelectSingleNode("Exp").InnerText);
                charData.FNextExp = Int32.Parse(node.SelectSingleNode("NextExp").InnerText);
                charData.FSpeed = Int32.Parse(node.SelectSingleNode("Speed").InnerText);
                
                CharList.Add(charData);

                charData = null;
            }
        }

    }

}
