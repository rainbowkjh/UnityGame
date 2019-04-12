using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using Weapone;

namespace Manager
{
    public class WeaponeParsing : MonoBehaviour
    {
        string weaponeFile = "WeaponeData";

        public List<WeaponeData> weaponeList = new List<WeaponeData>();
        WeaponeData data;

        void Start()
        {
            LoadWeaponeXml();
        }

        void LoadWeaponeXml()
        {
            TextAsset textAsset = (TextAsset)Resources.Load(weaponeFile);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);

            XmlNodeList all_node = xmlDoc.SelectNodes("dataroot/Weapone");
            foreach (XmlNode node in all_node)
            {
                data.Id = Int32.Parse(node.SelectSingleNode("ID").InnerText);                
                data.WeaponeName = node.SelectSingleNode("WeaponeName").InnerText;
                data.ItemIconPath = node.SelectSingleNode("IconLocal").InnerText;
                data.Type = ItemType.Weapone;

                int state = Int32.Parse(node.SelectSingleNode("State").InnerText);
                WeaponeType(state); //무기 타입 결정

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
            }
        }

    }

}
