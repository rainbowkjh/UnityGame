using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Black
{
    namespace ItemData
    {
        [Serializable]
        public class WeaponeData
        {
            int id = 0;
            string weaponeName = "";
            string itemIconPath = ""; //아이템 아이콘 경로

            int weaponeState = 0; //무기 형태 0 권총 1 AR 2 SG
            
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

            bool m_isUse = false; //로드 할때 사용 중이였던 무기 였는지 확인 후 표시

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

            public bool IsUse
            {
                get
                {
                    return m_isUse;
                }

                set
                {
                    m_isUse = value;
                }
            }

            public int WeaponeState
            {
                get
                {
                    return weaponeState;
                }

                set
                {
                    weaponeState = value;
                }
            }
            #endregion
        }

        public class ParsingData : MonoBehaviour
        {
            string weaponeFile = "WeaponeData";
            WeaponeData data;
            [SerializeField]
            List<WeaponeData> weaponeList = new List<WeaponeData>();

            #region 외부에서 파싱 데이터를 검색할수 있게 함
            public List<WeaponeData> WeaponeList
            {
                get
                {
                    return weaponeList;
                }

                set
                {
                    weaponeList = value;
                }
            }
            #endregion

            private void Awake()
            {
                LoadWeaponeXml();
            }

            /// <summary>
            /// 종료하면 리스트를 삭제한다
            /// </summary>
            private void OnDestroy()
            {
                WeaponeList.Clear();
            }

            void LoadWeaponeXml()
            {
                TextAsset textAsset = (TextAsset)Resources.Load(weaponeFile);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(textAsset.text);

                //   Debug.Log("textAsset.text : " + textAsset.text);

                XmlNodeList all_node = xmlDoc.SelectNodes("dataroot/Weapone");
                foreach (XmlNode node in all_node)
                {
                   data = new WeaponeData();
                                       
                    data.Id = Int32.Parse(node.SelectSingleNode("ID").InnerText);
                    data.WeaponeState = Int32.Parse(node.SelectSingleNode("State").InnerText);
                    
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
                    data.NCurBullet = Int32.Parse(node.SelectSingleNode("CurrMagazine").InnerText);

                    //data.nMaxBullet = Int32.Parse(node.SelectSingleNode("MaxBullet").InnerText);
                    //data.nCount = Int32.Parse(node.SelectSingleNode("Count").InnerText);
                    //data.nPrice = Int32.Parse(node.SelectSingleNode("Price").InnerText);
                    //data.nUpgrade = Int32.Parse(node.SelectSingleNode("UpgradePoint").InnerText);


                    WeaponeList.Add(data);

                    data = null; ;
                }
            }
        }

    }
}

