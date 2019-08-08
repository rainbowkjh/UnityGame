using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;

/// <summary>
/// 파싱 데이터 관리
/// 
/// 아이템 추가 시 
/// Check부분에 타입 결정 추가
/// 아이템 클래스 변수 추가(struct와 클래스... 하나로 통일 시켜서 불필요 부분 정리 할것!)
/// 아이템 오브젝트에서 아이콘 데이터로 데이터 적용 시키는 부분에서
/// 추가 시킨 변수의 데이터를 적용 시켜준다
/// 
/// </summary>
namespace Black
{
    namespace Manager
    {
        /// <summary>
        /// 파싱 데이터 받아와서
        /// 시르트에 추가 시키는 역할
        /// 데이터 저장 / 불러오기 시
        /// 인벤 정보 저장 / 불러오기 기능
        /// </summary>
        [Serializable]
        public class PlayerData
        {
            public int id;
            public string charName;
            public bool isLive;
            public int nHp;
            public int nMaxHp;
            public float fMana;
            public float fMaxMana;
            public float fThirst;
            public float fMaxThirst;
            public float fSatiety;
            public float fMaxSatiety;
            public float fSpeed;

            //최대 수치 (업그래이드를 하고 아이템을 사용해야 최대 수치 증가) 
            public int nMaxUpgradeHp;
            public float fMaxUpgradeMana;
            public float fMaxUpgradeThirst;
            public float fMaxUpgradeSatiety;

            public float fWeight; //소모 아이템 인벤 련재 무게
            public float fMaxWeight; //소모 아이템 인벤 최대 무게(업그래이드 가능 항목)
            public int nAmmo; //사용 중인 탄의 수
            public int nMaxAmmo; //최대 습득 탄의 수, 업그래이드 가능 항목

            public int nMoney; //소지금
            public int nMaterial; //업그래이드 재료
            public int nPartsMaterial; //파츠 업그래이드 재료

            public float UseSkillMana1;
            public float UseSkillMana2;
            public float UseSkillMana3;

            //무기 파츠 슬롯 1
            public List<PartsItemData> weaponeParts1_1 = new List<PartsItemData>();
            public List<PartsItemData> weaponeParts1_2 = new List<PartsItemData>();
            public List<PartsItemData> weaponeParts1_3 = new List<PartsItemData>();


            //2
            public List<PartsItemData> weaponeParts2_1 = new List<PartsItemData>();
            public List<PartsItemData> weaponeParts2_2 = new List<PartsItemData>();
            public List<PartsItemData> weaponeParts2_3 = new List<PartsItemData>();

            //퀵 슬롯(Bag와 Sub의 타입이 들어오기 때문에
            //GameObj로 받고 데이터를 확인하여 적용??
            public List<QuickSlotData> quickSlot_1 = new List<QuickSlotData>();
            public List<QuickSlotData> quickSlot_2 = new List<QuickSlotData>();
            public List<QuickSlotData> quickSlot_3 = new List<QuickSlotData>();



            //업그래이드 슬롯(아이템을 해제 하이 않을 경우 남아 있기 떄문에;;)
            public List<PartsItemData> upgradeSlot = new List<PartsItemData>();

            ////플레이어 인벤토리
            public List<BagItemData> playerBagInven = new List<BagItemData>();
            public List<PartsItemData> playerPartsInven = new List<PartsItemData>();
            public List<SubItemData> playerSubInven = new List<SubItemData>();

            ////창고 인벤
            public List<BagItemData> chestBagInven = new List<BagItemData>();
            public List<PartsItemData> chestPartsInven = new List<PartsItemData>();
            public List<SubItemData> chestSubInven = new List<SubItemData>();

            public List<bool> stageClearList = new List<bool>(); //스테이지 클리어 현황 

        }

        /// <summary>
        /// 퀵 슬롯 저장 시
        /// 두 타입 중 하나의 데이터를 가지도록 한다
        /// </summary>
        [Serializable]
        public class QuickSlotData
        {
            public BagItemData bagItem;
            public SubItemData subItem;
        }

        [Serializable]
        public struct BagItemData
        {
            public int id;
            public eItemType type;
            public eBagItemType useType;
            public string name;
            public int value;
            public float weight;
            public int count;
            public int price;
            public string tip;
            public string path;
        }

        [Serializable]
        public struct PartsItemData
        {
            public int id;
            public eItemType type;            
            public string name;
            public int nLevel; //현재 레벨
            public int nMaxLevel; //최대 레벨(아이템 생성 시 렌덤으로 받아온다 5~30)
            public bool isExplosion; //폭발탄 속성
            public float fMinRage; //폭발 최소 범위 파싱
            public float fMaxRage; //폭발 최대 범위 파싱
            public float fExplosionArea; //폭발탄 범위(아이템 생성 시 최소~최대 값 중 랜덤 적용 시킴)           
            public bool isStun; //기절 속성
            public float fStunMinPer; //기절 확률 최소
            public float fStunMaxPer; //기절 확률 최대 (최소에서 최대 값중 랜덤으로 받는다)
            public float fStunPer; //기절 확률 (아이템 생성 시 최소~최대 값 중 랜덤 적용 시킴)           
            public int dmgMinUp; //추가 데미지 (위와 같은 원리)
            public int dmgMaxUp; //추가 데미지
            public int dmgUp; //추가 데미지
            public float accUp; //정확도
            public int count;
            public int price;
            public string tip;
            public string path;
        }

        [Serializable]
        public struct SubItemData
        {
            public int id;
            public eItemType type;
            public eSubItemType itemType;
            public string name;
            public int minDmg;
            public int maxDmg;
            public int count;
            public int price;
            public string tip;
            public string path;
        }

        public class ParsingData : MonoBehaviour
        {
            #region Player
            string playerDataXmlFile = "PlayerInfo";
            PlayerData playerInfo = new PlayerData();
            [SerializeField] List<PlayerData> playerList = new List<PlayerData>();
            #endregion

            #region BagItem
            string bagItemXmlFile = "ItemData";
            BagItemData bagItem;
            [SerializeField] List<BagItemData> bagItemList = new List<BagItemData>();
            #endregion

            #region PartsItem
            string PartsItemXmlFile = "PartsData";
            PartsItemData partsItem;
            [SerializeField] List<PartsItemData> partsItemList = new List<PartsItemData>();
            #endregion

            #region BagItem
            string subItemXmlFile = "SubItemData";
            SubItemData subgItem;
            [SerializeField] List<SubItemData> subItemList = new List<SubItemData>();
            #endregion

            eItemType eType;
            bool isTrue;
            eBagItemType tempType; //Bag아이템 회복 속성 확인
            eSubItemType tempSubType; //SubItem 속성

            public List<BagItemData> BagItemList { get => bagItemList; set => bagItemList = value; }
            public List<PartsItemData> PartsItemList { get => partsItemList; set => partsItemList = value; }
            public List<SubItemData> SubItemList { get => subItemList; set => subItemList = value; }
            public List<PlayerData> PlayerList { get => playerList; set => playerList = value; }

            private void Awake()
            {
                LoadPlayerData(); //플레이어 정보
                LoadBagITem(); //가방 아이템
                LoadPartsITem(); //파트 아이템
                LoadSubITem(); //서브 아이템
            }

            void LoadPlayerData()
            {
                TextAsset textAsset = (TextAsset)Resources.Load(playerDataXmlFile);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(textAsset.text);

                XmlNodeList all_node = xmlDoc.SelectNodes("dataroot/Characters");
                foreach (XmlNode node in all_node)
                {
                    playerInfo.id = Int32.Parse(node.SelectSingleNode("ID").InnerText);
                    playerInfo.charName = node.SelectSingleNode("CharName").InnerText;

                    playerInfo.nHp = Int32.Parse(node.SelectSingleNode("HP").InnerText);
                    playerInfo.nMaxHp = Int32.Parse(node.SelectSingleNode("MaxHP").InnerText);

                    playerInfo.fMana = float.Parse(node.SelectSingleNode("Mana").InnerText);
                    playerInfo.fMaxMana = float.Parse(node.SelectSingleNode("MaxMana").InnerText);

                    playerInfo.fThirst = float.Parse(node.SelectSingleNode("Thirst").InnerText);
                    playerInfo.fMaxThirst = float.Parse(node.SelectSingleNode("MaxThirst").InnerText);

                    playerInfo.fSatiety = float.Parse(node.SelectSingleNode("Satiety").InnerText);
                    playerInfo.fMaxSatiety = float.Parse(node.SelectSingleNode("MaxSatiety").InnerText);

                    playerInfo.fSpeed = float.Parse(node.SelectSingleNode("Speed").InnerText);

                    playerInfo.nMaxUpgradeHp = Int32.Parse(node.SelectSingleNode("UpgradeHP").InnerText);
                    playerInfo.fMaxUpgradeMana = float.Parse(node.SelectSingleNode("UpgradeMana").InnerText);
                    playerInfo.fMaxUpgradeThirst = float.Parse(node.SelectSingleNode("UpgradeThirst").InnerText);
                    playerInfo.fMaxUpgradeSatiety = float.Parse(node.SelectSingleNode("UpgradeSatiety").InnerText);

                    playerInfo.fWeight = float.Parse(node.SelectSingleNode("CurBagWeight").InnerText);
                    playerInfo.fMaxWeight = float.Parse(node.SelectSingleNode("MaxBagWeight").InnerText);

                    playerInfo.nAmmo = Int32.Parse(node.SelectSingleNode("CurAmmo").InnerText);
                    playerInfo.nMaxAmmo = Int32.Parse(node.SelectSingleNode("MaxAmmo").InnerText);

                    playerInfo.nMoney = Int32.Parse(node.SelectSingleNode("Money").InnerText);
                    playerInfo.nMaterial = Int32.Parse(node.SelectSingleNode("Material").InnerText);
                    playerInfo.nPartsMaterial = Int32.Parse(node.SelectSingleNode("PartsMaterial").InnerText);

                    playerInfo.UseSkillMana1 = float.Parse(node.SelectSingleNode("SkillUseMana1").InnerText);
                    playerInfo.UseSkillMana2 = float.Parse(node.SelectSingleNode("SkillUseMana2").InnerText);
                    playerInfo.UseSkillMana3 = float.Parse(node.SelectSingleNode("SkillUseMana3").InnerText);
                            
                    //스테이지 클리어 관리
                    bool isStage = false;
                    playerInfo.stageClearList.Add(isStage); //첫번째 스테이지 클리어 미 완료

                    PlayerList.Add(playerInfo);
                }

            }


            /// <summary>
            /// 회복, 무기 모듈, 보조 무기 타입 결정
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            eItemType TypeSettig(string type)
            {
                switch (type)
                {
                    case "Bag":
                        eType = eItemType.Bag;
                        break;

                    case "Pouch1":
                        eType = eItemType.Pouch1;
                        break;

                    case "Pouch2":
                        eType = eItemType.Pouch2;
                        break;
                }

                return eType;
            }

            /// <summary>
            /// 아이템 속성 파싱 중
            /// Y 또는 N값을 받는 항목에 대해서
            /// 값을 세팅해준다
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            bool PropertyCheack(string prop)
            {
                switch(prop)
                {
                    case "Y":
                        isTrue = true;
                        break;

                    case "N":
                        isTrue = false;
                        break;
                }
                return isTrue;
            }

            /// <summary>
            /// 가방 인벤에 들어가는 아이템 세팅
            /// </summary>
            void LoadBagITem()
            {
                TextAsset textAsset = (TextAsset)Resources.Load(bagItemXmlFile);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(textAsset.text);

                XmlNodeList all_node = xmlDoc.SelectNodes("dataroot/ITEM");
                foreach (XmlNode node in all_node)
                {
                    bagItem.id = Int32.Parse(node.SelectSingleNode("ID").InnerText);
                    bagItem.type = TypeSettig(node.SelectSingleNode("Type").InnerText);
                    bagItem.useType = BagItemTypeCheck(node.SelectSingleNode("ItemType").InnerText);
                    bagItem.name = node.SelectSingleNode("Name").InnerText;
                    bagItem.value = Int32.Parse(node.SelectSingleNode("Value").InnerText);
                    bagItem.weight = float.Parse(node.SelectSingleNode("Weight").InnerText);
                    bagItem.count = Int32.Parse(node.SelectSingleNode("Count").InnerText);
                    bagItem.price = Int32.Parse(node.SelectSingleNode("Price").InnerText);
                    bagItem.tip = node.SelectSingleNode("Tip").InnerText;
                    bagItem.path = node.SelectSingleNode("Path").InnerText;

                    BagItemList.Add(bagItem);

                }

            }

            /// <summary>
            /// Bag(소모 아이템 중) 회복 속성 타입 결정
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            private eBagItemType BagItemTypeCheck(string type)
            {                
                switch(type)
                {
                    case "HP_Recovery":
                        tempType = eBagItemType.HP_Recovery;
                        break;

                    case "Satiety_Recovery":
                        tempType = eBagItemType.Satiety_Recovery;
                        break;

                    case "Mana_Recovery":
                        tempType = eBagItemType.Mana_Recovery;
                        break;

                    case "Thirst_Recovery":
                        tempType = eBagItemType.Thirst_Recovery;
                        break;

                    case "HP_Up":
                        tempType = eBagItemType.HP_Up;
                        break;

                    case "Satiety_Up":
                        tempType = eBagItemType.Satiety_Up;
                        break;

                    case "Mana_Up":
                        tempType = eBagItemType.Mana_Up;
                        break;

                    case "Thirst_Up":
                        tempType = eBagItemType.Thirst_Up;
                        break;

                    case "Recovery":
                        tempType = eBagItemType.Recovery;
                        break;

                    case "Dmg_Up":
                        tempType = eBagItemType.Dmg_Up;
                        break;
                }

                return tempType;
            }

            /// <summary>
            /// 파트 아이템
            /// </summary>
            void LoadPartsITem()
            {
                TextAsset textAsset = (TextAsset)Resources.Load(PartsItemXmlFile);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(textAsset.text);

                XmlNodeList all_node = xmlDoc.SelectNodes("dataroot/ITEM");
                foreach (XmlNode node in all_node)
                {
                    partsItem.id = Int32.Parse(node.SelectSingleNode("ID").InnerText); //아이디
                    partsItem.type = TypeSettig(node.SelectSingleNode("Type").InnerText);     //아이템 타입               
                    partsItem.name = node.SelectSingleNode("Name").InnerText; //아이템 이름
                    partsItem.nLevel = Int32.Parse(node.SelectSingleNode("Level").InnerText); //아이템 레벨                    
                    partsItem.isExplosion = PropertyCheack(node.SelectSingleNode("Explosion").InnerText); //폭발 속성
                    partsItem.fMinRage= float.Parse(node.SelectSingleNode("ExplosionMinArea").InnerText); //폭발 최소 범위
                    partsItem.fMaxRage = float.Parse(node.SelectSingleNode("ExplosionMaxArea").InnerText); //최대 범위                    
                    partsItem.isStun = PropertyCheack(node.SelectSingleNode("Stun").InnerText); //기절 효과 속성
                    partsItem.fStunMinPer = float.Parse(node.SelectSingleNode("StunMinPer").InnerText); //최소
                    partsItem.fStunMaxPer = float.Parse(node.SelectSingleNode("StunMaxPer").InnerText); //최대
                    partsItem.dmgMinUp = Int32.Parse(node.SelectSingleNode("DmgMinUp").InnerText);
                    partsItem.dmgMaxUp = Int32.Parse(node.SelectSingleNode("DmgMaxUp").InnerText);
                    partsItem.accUp = float.Parse(node.SelectSingleNode("AccUp").InnerText);
                    partsItem.price = Int32.Parse(node.SelectSingleNode("Price").InnerText);
                    partsItem.tip = node.SelectSingleNode("Tip").InnerText;
                    partsItem.path = node.SelectSingleNode("Path").InnerText;

                    PartsItemList.Add(partsItem);
                }

            }
          

            /// <summary>
            /// 서브 아이템(수류탄 등)
            /// </summary>
            void LoadSubITem()
            {
                TextAsset textAsset = (TextAsset)Resources.Load(subItemXmlFile);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(textAsset.text);

                XmlNodeList all_node = xmlDoc.SelectNodes("dataroot/ITEM");
                foreach (XmlNode node in all_node)
                {
                    subgItem.id = Int32.Parse(node.SelectSingleNode("ID").InnerText);
                    subgItem.type = TypeSettig(node.SelectSingleNode("Type").InnerText);
                    subgItem.itemType = SubItemTypeCheck(node.SelectSingleNode("ItemType").InnerText);
                    subgItem.name = node.SelectSingleNode("Name").InnerText;
                    subgItem.minDmg = Int32.Parse(node.SelectSingleNode("MinDmg").InnerText);
                    subgItem.maxDmg = Int32.Parse(node.SelectSingleNode("MaxDmg").InnerText);
                    subgItem.count = Int32.Parse(node.SelectSingleNode("Count").InnerText);
                    subgItem.price = Int32.Parse(node.SelectSingleNode("Price").InnerText);
                    subgItem.tip = node.SelectSingleNode("Tip").InnerText;
                    subgItem.path = node.SelectSingleNode("Path").InnerText;

                    SubItemList.Add(subgItem);
                }

            }

            private eSubItemType SubItemTypeCheck(string type)
            {
                switch (type)
                {
                    case "Grenade":
                        tempSubType = eSubItemType.Grenade;
                        break;

                    case "Turret":
                        tempSubType = eSubItemType.Turret;
                        break;
                }

                return tempSubType;
            }

        }

    }
}
