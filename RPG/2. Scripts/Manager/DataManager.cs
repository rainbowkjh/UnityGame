using Black.Characters;
using Black.Inventory;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// 세이브 로드 관리
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class DataManager : MonoBehaviour
        {
            [SerializeField]
            private string[] dataPath;

            [SerializeField]
            private string dataQuickSlot;

            public object BinaryFommatter { get; private set; }

            /// <summary>
            /// 저장 슬롯 생성
            /// </summary>
            /// <param name="slot"></param>
            public void Initialize(int slot)
            {
                dataPath[slot] = Application.dataPath + "/DataFile/data" + slot + ".bat";
                dataQuickSlot = Application.dataPath + "/DataFile/QuickSavedata.bat";
            }


            /// <summary>
            /// 플레이어 정보 저장
            /// </summary>
            /// <param name="player"></param>
            public void Save(PlayerCtrl player, int saveSlot)
            {
                #region 저장할때 인벤 정보를 가져온다
                //DataManager가 GameManager와 같이 묶여 있어서
                //싱글턴처럼 사용 됨
                //인벤 정보를 가지고 있지 않는 씬에서 에러 발생 방지
                InventoryData invenData = GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<InventoryData>();
                ChestManager chestData = GameObject.FindGameObjectWithTag("ChestData").GetComponent<ChestManager>();

                #endregion

                BinaryFormatter bf = new BinaryFormatter();

                FileStream file = File.Create(dataPath[saveSlot]);

                PlayerData saveData = new PlayerData();

                #region 플레이어 캐릭터 정보 저장
                saveData.charName = player.CharName;

                saveData.nHp = player.NHp;
                saveData.nMaxHp = player.NMaxHp;
                saveData.fMana = player.FMana;
                saveData.fMaxMana = player.FMaxMana;
                saveData.fThirst = player.FThirst;
                saveData.fMaxThirst = player.FMaxThirst;
                saveData.fSatiety = player.FSatiety;
                saveData.fMaxSatiety = player.FMaxSatiety;

                saveData.nMaxUpgradeHp = player.NMaxUpgradeHp;
                saveData.fMaxUpgradeMana = player.FMaxUpgradeMana;
                saveData.fMaxUpgradeThirst = player.FMaxUpgradeThirst;
                saveData.fMaxUpgradeSatiety = player.FMaxUpgradeSatiety;

                saveData.fSpeed = player.FSpeed;

                saveData.fWeight = player.FWeight;
                saveData.fMaxWeight = player.FMaxWeight;
                saveData.nAmmo = player.NAmmo;
                saveData.nMaxAmmo = player.NMaxAmmo;

                saveData.nMoney = player.NMoney;
                saveData.nMaterial = player.NMaterial;
                saveData.nPartsMaterial = player.NPartsMaterial;

                saveData.UseSkillMana1 = player.UseMana[0];
                saveData.UseSkillMana2 = player.UseMana[1];
                saveData.UseSkillMana3 = player.UseMana[2];

                saveData.stageClearList = player.StageList;
                #endregion

                //세이브를 여러번 하다보면 아이템이 중복 저장 될수 있기 때문에 삭제를 먼저 해준다
                #region 저장 슬롯에 저장되어 있는 아이템 정보 초기화
                saveData.weaponeParts1_1.Clear();
                saveData.weaponeParts1_2.Clear();
                saveData.weaponeParts1_3.Clear();

                saveData.weaponeParts2_1.Clear();
                saveData.weaponeParts2_2.Clear();
                saveData.weaponeParts2_3.Clear();

                saveData.quickSlot_1.Clear();
                saveData.quickSlot_2.Clear();
                saveData.quickSlot_3.Clear();

                saveData.upgradeSlot.Clear();

                saveData.playerBagInven.Clear();
                saveData.playerPartsInven.Clear();
                saveData.playerSubInven.Clear();

                saveData.chestBagInven.Clear();
                saveData.chestPartsInven.Clear();
                saveData.chestSubInven.Clear();
                #endregion

                #region 슬롯에 있는 아이템 정보 저장
                //player
                BagItemDataSave(saveData.playerBagInven, invenData.BagInvenList);
                PartsItemDataSave(saveData.playerPartsInven, invenData.PartsInvenList);
                SubItemDataSave(saveData.playerSubInven, invenData.SubInvenList);

                //Chest
                BagItemDataSave(saveData.chestBagInven, chestData.ChestBagInven);
                PartsItemDataSave(saveData.chestPartsInven, chestData.ChestPartsInven);
                SubItemDataSave(saveData.chestSubInven, chestData.ChestSubInven);

                //upgrade
                PartsItemDataSave(saveData.upgradeSlot, chestData.UpgradeSlot);

                //Weapone
                PartsItemDataSave(saveData.weaponeParts1_1, chestData.WeaponePartsSlot1_1);
                PartsItemDataSave(saveData.weaponeParts1_2, chestData.WeaponePartsSlot1_2);
                PartsItemDataSave(saveData.weaponeParts1_3, chestData.WeaponePartsSlot1_3);


                PartsItemDataSave(saveData.weaponeParts2_1, chestData.WeaponePartsSlot2_1);
                PartsItemDataSave(saveData.weaponeParts2_2, chestData.WeaponePartsSlot2_2);
                PartsItemDataSave(saveData.weaponeParts2_3, chestData.WeaponePartsSlot2_3);

                //quick Slot
                QuickSlotDataSave(saveData.quickSlot_1, chestData.QuickSlot_1);
                QuickSlotDataSave(saveData.quickSlot_2, chestData.QuickSlot_2);
                QuickSlotDataSave(saveData.quickSlot_3, chestData.QuickSlot_3);
                #endregion

                //   Debug.Log("Bag Inven Count : " + saveData.playerBagInven.Count);
                bf.Serialize(file, saveData);
                file.Close();
            }

            /// <summary>
            /// 로비에서 스테이지로 넘어 갈때 저장
            /// </summary>
            /// <param name="player"></param>
            public void QuickSave(PlayerCtrl player)
            {
                #region 저장할때 인벤 정보를 가져온다
                //DataManager가 GameManager와 같이 묶여 있어서
                //싱글턴처럼 사용 됨
                //인벤 정보를 가지고 있지 않는 씬에서 에러 발생 방지
                InventoryData invenData = GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<InventoryData>();
                ChestManager chestData = GameObject.FindGameObjectWithTag("ChestData").GetComponent<ChestManager>();

                #endregion

                BinaryFormatter bf = new BinaryFormatter();

                FileStream file = File.Create(dataQuickSlot);

                PlayerData saveData = new PlayerData();

                #region 플레이어 캐릭터 정보 저장
                saveData.charName = player.CharName;

                saveData.nHp = player.NHp;
                saveData.nMaxHp = player.NMaxHp;
                saveData.fMana = player.FMana;
                saveData.fMaxMana = player.FMaxMana;
                saveData.fThirst = player.FThirst;
                saveData.fMaxThirst = player.FMaxThirst;
                saveData.fSatiety = player.FSatiety;
                saveData.fMaxSatiety = player.FMaxSatiety;

                saveData.fSpeed = player.FSpeed;

                saveData.nMaxUpgradeHp = player.NMaxUpgradeHp;
                saveData.fMaxUpgradeMana = player.FMaxUpgradeMana;
                saveData.fMaxUpgradeThirst = player.FMaxUpgradeThirst;
                saveData.fMaxUpgradeSatiety = player.FMaxUpgradeSatiety;

                saveData.fWeight = player.FWeight;
                saveData.fMaxWeight = player.FMaxWeight;
                saveData.nAmmo = player.NAmmo;
                saveData.nMaxAmmo = player.NMaxAmmo;

                saveData.nMoney = player.NMoney;
                saveData.nMaterial = player.NMaterial;
                saveData.nPartsMaterial = player.NPartsMaterial;

                saveData.UseSkillMana1 = player.UseMana[0];
                saveData.UseSkillMana2 = player.UseMana[1];
                saveData.UseSkillMana3 = player.UseMana[2];

                saveData.stageClearList = player.StageList;
                #endregion

                //세이브를 여러번 하다보면 아이템이 중복 저장 될수 있기 때문에 삭제를 먼저 해준다
                #region 저장 슬롯에 저장되어 있는 아이템 정보 초기화
                saveData.weaponeParts1_1.Clear();
                saveData.weaponeParts1_2.Clear();
                saveData.weaponeParts1_3.Clear();

                saveData.weaponeParts2_1.Clear();
                saveData.weaponeParts2_2.Clear();
                saveData.weaponeParts2_3.Clear();

                saveData.quickSlot_1.Clear();
                saveData.quickSlot_2.Clear();
                saveData.quickSlot_3.Clear();

                saveData.upgradeSlot.Clear();

                saveData.playerBagInven.Clear();
                saveData.playerPartsInven.Clear();
                saveData.playerSubInven.Clear();

                saveData.chestBagInven.Clear();
                saveData.chestPartsInven.Clear();
                saveData.chestSubInven.Clear();
                #endregion

                #region 슬롯에 있는 아이템 정보 저장
                //player
                BagItemDataSave(saveData.playerBagInven, invenData.BagInvenList);
                PartsItemDataSave(saveData.playerPartsInven, invenData.PartsInvenList);
                SubItemDataSave(saveData.playerSubInven, invenData.SubInvenList);

                //Chest
                BagItemDataSave(saveData.chestBagInven, chestData.ChestBagInven);
                PartsItemDataSave(saveData.chestPartsInven, chestData.ChestPartsInven);
                SubItemDataSave(saveData.chestSubInven, chestData.ChestSubInven);

                //upgrade
                PartsItemDataSave(saveData.upgradeSlot, chestData.UpgradeSlot);

                //Weapone
                PartsItemDataSave(saveData.weaponeParts1_1, chestData.WeaponePartsSlot1_1);
                PartsItemDataSave(saveData.weaponeParts1_2, chestData.WeaponePartsSlot1_2);
                PartsItemDataSave(saveData.weaponeParts1_3, chestData.WeaponePartsSlot1_3);


                PartsItemDataSave(saveData.weaponeParts2_1, chestData.WeaponePartsSlot2_1);
                PartsItemDataSave(saveData.weaponeParts2_2, chestData.WeaponePartsSlot2_2);
                PartsItemDataSave(saveData.weaponeParts2_3, chestData.WeaponePartsSlot2_3);

                //quick Slot
                QuickSlotDataSave(saveData.quickSlot_1, chestData.QuickSlot_1);
                QuickSlotDataSave(saveData.quickSlot_2, chestData.QuickSlot_2);
                QuickSlotDataSave(saveData.quickSlot_3, chestData.QuickSlot_3);
                #endregion

                //   Debug.Log("Bag Inven Count : " + saveData.playerBagInven.Count);
                bf.Serialize(file, saveData);
                file.Close();
            }

            /// <summary>
            /// 새로 시작 
            /// 파싱 데이터를 적용 시킨다
            /// </summary>
            /// <returns></returns>
            public PlayerData NewData()
            {
                PlayerData data = new PlayerData();

                ParsingData parsingData = GameObject.Find("ParsingData").GetComponent<ParsingData>();

                data.id = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].id;
                data.charName = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].charName;

                data.nHp = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nHp;
                data.nMaxHp = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nMaxHp;
                data.fMana = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMana;
                data.fMaxMana = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxMana;
                data.fThirst = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fThirst;
                data.fMaxThirst = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxThirst;
                data.fSatiety = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fSatiety;
                data.fMaxSatiety = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxSatiety;

                data.fSpeed = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fSpeed;

                data.nMaxUpgradeHp = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nMaxUpgradeHp;
                data.fMaxUpgradeMana = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxUpgradeMana;
                data.fMaxUpgradeThirst = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxUpgradeThirst;
                data.fMaxUpgradeSatiety = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxUpgradeSatiety;

                data.fWeight = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fWeight;
                data.fMaxWeight = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxWeight;

                data.nAmmo = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nAmmo;
                data.nMaxAmmo = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nMaxAmmo;

                data.nMoney = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nMoney;
                data.nMaterial = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nMaterial;
                data.nPartsMaterial = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nPartsMaterial;

                data.UseSkillMana1 = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].UseSkillMana1;
                data.UseSkillMana2 = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].UseSkillMana2;
                data.UseSkillMana3 = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].UseSkillMana3;

                data.stageClearList = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].stageClearList;

                return data;
            }


            /// <summary>
            /// 저장된 파일 변환            
            /// </summary>
            /// <returns></returns>
            public PlayerData Load(int index)
            {
                if (File.Exists(dataPath[index]))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(dataPath[index], FileMode.Open);

                    PlayerData data = (PlayerData)bf.Deserialize(file);

                    file.Close();

                    return data;
                }

                //로드 데이터가 없으면 파싱 데이터를 로드
                else
                {
                    PlayerData data = new PlayerData();

                    //ParsingData parsingData =  GameObject.Find("ParsingData").GetComponent<ParsingData>();

                    //data.id = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].id;
                    //data.charName = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].charName;

                    //data.nHp = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nHp;
                    //data.nMaxHp = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nMaxHp;
                    //data.fMana = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMana;
                    //data.fMaxMana = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxMana;
                    //data.fThirst = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fThirst;
                    //data.fMaxThirst = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxThirst;
                    //data.fSatiety = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fSatiety;
                    //data.fMaxSatiety = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxSatiety;

                    //data.nMaxUpgradeHp = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nMaxUpgradeHp;
                    //data.fMaxUpgradeMana = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxUpgradeMana;
                    //data.fMaxUpgradeThirst = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxUpgradeThirst;
                    //data.fMaxUpgradeSatiety = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxUpgradeSatiety;

                    //data.fWeight = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fWeight;
                    //data.fMaxWeight = parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].fMaxWeight;

                    //data.nAmmo= parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nAmmo;
                    //data.nMaxAmmo= parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nMaxAmmo;

                    //data.nMoney= parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nMoney;
                    //data.nMaterial= parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nMaterial;
                    //data.nPartsMaterial= parsingData.PlayerList[GameManager.INSTANCE.CurPlayerIndenx].nPartsMaterial;
                    //return data;

                    data.charName = "";

                    return data;

                }
            }

            /// <summary>
            /// 로비에서 스테이지로 갈때 임시 저장한 내용을 불러온다
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public PlayerData QuickLoad()
            {
                if (File.Exists(dataQuickSlot))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(dataQuickSlot, FileMode.Open);

                    PlayerData data = (PlayerData)bf.Deserialize(file);

                    file.Close();

                    return data;
                }

                //로드 데이터가 없으면 파싱 데이터를 로드
                else
                {
                    PlayerData data = new PlayerData();

                    data.charName = "";

                    return data;

                }
            }

            #region 인벤 데이터 저장
            /// <summary>
            /// Bag
            /// 템플릿으로?? 한번에??
            /// </summary>
            /* 이름이 같지만 하나는 구조체 하나는 클래스로 서로 연동이 안되서
             * 템플릿으로 한번에 하기 힘들꺼 같다
             void InvenDataSave<T>(List<T> saveInven, List<GameObject> itemList) where T : new()
            {
                T item = new T(); //where T : new()

                //슬롯을 돌면서
                for (int i=0; i<itemList.Count;i++)
                {
                    //슬롯의 하위 오브젝트가 있음면
                    if(itemList[i].transform.childCount != 0)
                    {
                        Debug.Log("save Item Info : " + itemList[i].GetComponentInChildren<Black.Inventory.BagItemData>());

                        //저장된 정보를 저장
                        item = itemList[i].GetComponentInChildren<T>();
                        saveInven.Add(item);
                    }
                    
                }
            }
                 */


            /// <summary>
            /// 소모 아이템 저장
            /// </summary>
            void BagItemDataSave(List<BagItemData> saveInven, List<GameObject> itemList)
            {
                Black.Manager.BagItemData item = new Black.Manager.BagItemData(); //where T : new()
                Black.Inventory.BagItemData slotData = new Inventory.BagItemData();

                //슬롯을 돌면서
                for (int i = 0; i < itemList.Count; i++)
                {
                    //슬롯의 하위 오브젝트가 있음면
                    if (itemList[i].transform.childCount != 0)
                    {
                        //Debug.Log("save Item Info : " + itemList[i].GetComponentInChildren<Black.Inventory.BagItemData>());

                        //저장된 정보를 저장
                        slotData = itemList[i].GetComponentInChildren<Inventory.BagItemData>();

                        item.id = slotData._BagItem._Id;
                        item.type = slotData._BagItem._Type;
                        item.useType = slotData._BagItem._UseType;
                        item.name = slotData._BagItem._Name;
                        item.value = slotData._BagItem._Value;
                        item.weight = slotData._BagItem._Weight;
                        item.count = slotData.NCount; //개수(데이터의 개수가 아닌 실제 개수 값을 넘김)
                        item.price = slotData._BagItem._Price;
                        item.tip = slotData._BagItem._Tip;
                        item.path = slotData._BagItem._Path;

                        saveInven.Add(item);
                    }

                }
            }

            /// <summary>
            /// 파츠 인벤
            /// </summary>
            /// <param name="saveInven"></param>
            /// <param name="itemList"></param>
            void PartsItemDataSave(List<PartsItemData> saveInven, List<GameObject> itemList)
            {
                Black.Manager.PartsItemData item = new Black.Manager.PartsItemData(); //where T : new()
                Black.Inventory.PartsItemData slotData = new Inventory.PartsItemData();

                //슬롯을 돌면서
                for (int i = 0; i < itemList.Count; i++)
                {
                    //슬롯의 하위 오브젝트가 있음면
                    if (itemList[i].transform.childCount != 0)
                    {
                        //Debug.Log("save Item Info : " + itemList[i].GetComponentInChildren<Black.Inventory.PartsItemData>());

                        //저장된 정보를 저장
                        slotData = itemList[i].GetComponentInChildren<Inventory.PartsItemData>();

                        item.id = slotData._PartsItem._Id;
                        item.type = slotData._PartsItem._Type;
                        item.name = slotData._PartsItem._Name;
                        item.nLevel = slotData._PartsItem._NLevel;
                        item.nMaxLevel = slotData._PartsItem._NMaxLevel;

                        item.isExplosion = slotData._PartsItem._IsExplosion;
                        item.fMinRage = slotData._PartsItem._FMinRage;
                        item.fMaxRage = slotData._PartsItem._FMaxRage;
                        item.fExplosionArea = slotData._PartsItem._FExplosionArea;

                        item.isStun = slotData._PartsItem._IsStun;
                        item.fStunMinPer = slotData._PartsItem._FStunMinPer;
                        item.fStunMaxPer = slotData._PartsItem._FStunMaxPer;
                        item.fStunPer = slotData._PartsItem._FStunPer;

                        item.dmgMinUp = slotData._PartsItem._DmgMinUp;
                        item.dmgMaxUp = slotData._PartsItem._DmgMaxUp;
                        item.dmgUp = slotData._PartsItem._DmgUp;

                        item.accUp = slotData._PartsItem._AccUp;

                        item.count = slotData.NCount; //개수(데이터의 개수가 아닌 실제 개수 값을 넘김)

                        item.price = slotData._PartsItem._Price;
                        item.tip = slotData._PartsItem._Tip;
                        item.path = slotData._PartsItem._Path;

                        saveInven.Add(item);
                    }

                }
            }

            /// <summary>
            /// 서브 아이템 인벤
            /// </summary>
            /// <param name="saveInven"></param>
            /// <param name="itemList"></param>
            void SubItemDataSave(List<SubItemData> saveInven, List<GameObject> itemList)
            {
                Black.Manager.SubItemData item = new Black.Manager.SubItemData(); //where T : new()
                Black.Inventory.SubItemData slotData = new Inventory.SubItemData();

                //슬롯을 돌면서
                for (int i = 0; i < itemList.Count; i++)
                {
                    //슬롯의 하위 오브젝트가 있음면
                    if (itemList[i].transform.childCount != 0)
                    {
                     //   Debug.Log("save Item Info : " + itemList[i].GetComponentInChildren<Black.Inventory.SubItemData>());

                        //저장된 정보를 저장
                        slotData = itemList[i].GetComponentInChildren<Inventory.SubItemData>();

                        item.id = slotData._SubItem._Id;
                        item.type = slotData._SubItem._Type;
                        item.itemType = slotData._SubItem._ItemType;
                        item.name = slotData._SubItem._Name;
                        item.minDmg = slotData._SubItem._MinDmg;
                        item.maxDmg = slotData._SubItem._MaxDmg;

                        item.count = slotData.NCount; //개수(데이터의 개수가 아닌 실제 개수 값을 넘김)

                        item.price = slotData._SubItem._Price;
                        item.tip = slotData._SubItem._Tip;
                        item.path = slotData._SubItem._Path;

                        saveInven.Add(item);
                    }

                }
            }

            /// <summary>
            /// 퀵 슬롯은 아이템의 종류룰 파악해야 된다
            /// </summary>
            /// <param name="player"></param>
            /// <param name="itemList"></param>
            void QuickSlotDataSave(List<QuickSlotData> saveInven, List<GameObject> itemList)
            {
                //퀵 슬롯 루프를 돈다
                for (int i = 0; i < itemList.Count; i++)
                {
                    //저장 시킬 아이템이 있는지 확인
                    if (itemList[i].transform.childCount != 0)
                    {

                        if (itemList[i].GetComponentInChildren<Inventory.BagItemData>())
                        {

                            QuickSlotData item = new QuickSlotData();
                            //슬롯에 있는 아이템 정보를 가져온다
                            Inventory.BagItemData slotData = itemList[i].GetComponentInChildren<Inventory.BagItemData>();
                            
                            item.bagItem.id = slotData._BagItem._Id;
                            item.bagItem.type = slotData._BagItem._Type;
                            item.bagItem.useType = slotData._BagItem._UseType;
                            item.bagItem.name = slotData._BagItem._Name;
                            item.bagItem.value = slotData._BagItem._Value;
                            item.bagItem.weight = slotData._BagItem._Weight;
                            item.bagItem.count = slotData.NCount; //개수(데이터의 개수가 아닌 실제 개수 값을 넘김)
                            item.bagItem.price = slotData._BagItem._Price;
                            item.bagItem.tip = slotData._BagItem._Tip;
                            item.bagItem.path = slotData._BagItem._Path;


                            saveInven.Add(item);

                        }

                        //있으면 타입을 확인
                        if (itemList[i].GetComponentInChildren<Inventory.SubItemData>())
                        {
                            QuickSlotData item = new QuickSlotData();
                            //슬롯에 있는 아이템 정보를 가져온다
                            Inventory.SubItemData  slotData = itemList[i].GetComponentInChildren<Inventory.SubItemData>();

                            //임시 변수에 저장
                            item.subItem.id = slotData._SubItem._Id;
                            item.subItem.type = slotData._SubItem._Type;
                            item.subItem.itemType = slotData._SubItem._ItemType;
                            item.subItem.name = slotData._SubItem._Name;
                            item.subItem.minDmg = slotData._SubItem._MinDmg;
                            item.subItem.maxDmg = slotData._SubItem._MaxDmg;

                            item.subItem.count = slotData.NCount; //개수(데이터의 개수가 아닌 실제 개수 값을 넘김)

                            item.subItem.price = slotData._SubItem._Price;
                            item.subItem.tip = slotData._SubItem._Tip;
                            item.subItem.path = slotData._SubItem._Path;

                            saveInven.Add(item);

                        }

                    }
                }

                
            }
            #endregion



        }

    }
}
