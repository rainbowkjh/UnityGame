using Characters;
using Manager;
using Manager.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인벤토리 싱글턴으로 만들어
/// 메인->스테이지->메인 이동에도
/// 데이터 접근을 쉽게 만듬
/// </summary>
namespace _Item
{
    public class InventoryList : MonoBehaviour
    {
        public static InventoryList INVENTORY = null;

        [SerializeField, Header("인벤토리 슬롯")]
        List<GameObject> itemList;

        [SerializeField, Header("아이템 장착 시 아이콘 장착 위치, 0 무기, 1~2 아이템")]
        Transform[] m_trEquipTr;

        [SerializeField, Header("장착 슬롯에 생성 시킬 아이콘 프리펩")]
        private GameObject m_EquipIcon;

        [SerializeField, Header("인벤 아이콘 프리펩")]
        GameObject m_InvenIcon;

        [Header("게임 시작 시 저장 시킬 플레이어")]
        CharactersData playerData;

        //이 변수는 여러군데에서 반복 되는데..
        //나중에 싱글턴에서 관리하는 식으로 여러번 만들어 사용하지 않도록 수정!
        int m_nParsingIndex = 0; //파싱된 리스트에서 가져올 인덱스 값


        #region Set,Get
        public Transform[] TrEquipTr
        {
            get
            {
                return m_trEquipTr;
            }

            set
            {
                m_trEquipTr = value;
            }
        }

        public GameObject EquipIcon
        {
            get
            {
                return m_EquipIcon;
            }

            set
            {
                m_EquipIcon = value;
            }
        }
        #endregion

        private void Awake()
        {
            //Debug.Log("인벤토리");
            // 싱글 턴
            if (INVENTORY == null)
                INVENTORY = this;
            if (INVENTORY != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            //========================
        }


        private void OnEnable()
        {
            _Item.InventoryList.INVENTORY.InvetoryLoad();
            //InvetoryLoad();
        }

        private void OnDisable()
        {
            InvenSave();
        }

        /// <summary>
        /// 게임이 시작하면 저장 슬롯에서 인벤토리 정보를 불러온다
        /// </summary>
        public void InvetoryLoad()
        {
            if (GameManager.INSTANCE.isMale)
            {
                m_nParsingIndex = 1;
            }
            if (!GameManager.INSTANCE.isMale)
            {
                m_nParsingIndex = 0;
            }

            CharactersGameData loadData = new CharactersGameData();
            //Debug.Log("로드 캐릭터 : " + loadData.ToString());

            loadData = GameManager.INSTANCE.gameData.Load(m_nParsingIndex);
            //Debug.Log("로드 캐릭터 : " + loadData.SName);
            
            if(loadData != null)
            {
                #region 아이템 삭제
                //적용 시킬 인벤토리안의 아이템을 우선 삭제한다 (중복 생성 방지)
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (itemList[i].transform.childCount != 0)
                    {
                        Destroy(itemList[i].transform.GetChild(0).gameObject);
                        itemList[i].transform.DetachChildren(); //이 코드 작성 안할경우 안지워질수 있음
                    }
                }

                if (m_trEquipTr[0].transform.childCount != 0)
                {
                    Destroy(m_trEquipTr[0].transform.GetChild(0).gameObject);
                    m_trEquipTr[0].transform.DetachChildren(); //이 코드 작성 안할경우 안지워질수 있음
                }
                #endregion

                #region 로드 아이템 적용
                //로드한 인벤토리 정보 중 아이템을 가져온다
                //Debug.Log("로드한 인벤 크기 : " + loadData.WeaponeList.Count);

                for (int i = 0; i < loadData.WeaponeList.Count; i++)
                {
                    GameObject obj = Instantiate(m_InvenIcon);
                    obj.transform.SetParent(itemList[i].transform);
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localRotation = Quaternion.identity;
                    obj.transform.localScale = new Vector3(1, 1, 1);

                    //Debug.Log("로드한 인벤 크기 : " + loadData.WeaponeList[i].WeaponeName);

                    obj.GetComponent<ItemData>().WeaponeData = loadData.WeaponeList[i];

                    //Debug.Log("저장된 무기 : " + obj.GetComponent<ItemData>().WeaponeData.WeaponeName);

                    obj.GetComponent<ItemData>().isEquip = true;
                    obj.GetComponent<ItemData>().SpriteApply();

                    //사용중인 무기 아이콘
                    if (obj.GetComponent<ItemData>().WeaponeData.IsUse) //(로드할때 사용중인 무기인지 구별)
                    {
                        obj.GetComponent<ItemData>().UseIcon.SetActive(true); //로드할때 사용 중인 무기 표시가 나오지 않는다 버그;;;
                                                                              //Debug.Log("아이템 사용 표시 상태 : " + obj.GetComponent<ItemData>().UseIcon.activeSelf);
                    }

                }

                //장착된 아이템 정보            
                if (loadData.EquipWeapone != null)
                {
                    if (loadData.EquipWeapone.WeaponeName != "")
                    {
                        GameObject obj = Instantiate(m_EquipIcon);
                        obj.transform.SetParent(m_trEquipTr[0].transform);
                        obj.transform.localPosition = Vector3.zero;
                        obj.transform.localRotation = Quaternion.identity;
                        obj.transform.localScale = new Vector3(1, 1, 1);

                        obj.GetComponent<ItemData>().WeaponeData = loadData.EquipWeapone;
                        obj.GetComponent<ItemData>().isEquip = true;
                        obj.GetComponent<ItemData>().SpriteApply();
                    }
                }


                #endregion
            }


        }


        /// <summary>
        /// 아이템을 인벤이 넣을수 있는지 확인한다
        /// </summary>
        /// <returns></returns>
        public bool InvetoryCheck()
        {
            for(int i=0;i<itemList.Count;i++)
            {
                //아이템 슬롯 안에 아이템이 없으면
                //true를 반환하여 아이템을 넣을수 있게 한다
                if(itemList[i].transform.childCount == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddItem(ItemData item)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                //아이템 슬롯 안에 아이템이 없으면
                //true를 반환하여 아이템을 넣을수 있게 한다
                if (itemList[i].transform.childCount == 0)
                {
                    item.transform.SetParent(itemList[i].transform);
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localRotation = Quaternion.identity;
                    item.transform.localScale = new Vector3(1, 1, 1);

                    item.isEquip = true;               
                    //item.WeaponeData.IsUse = false;

                    break;
                }
            }

        }

        public List<WeaponeGameData> GetWeaponeItem()
        {
            List<WeaponeGameData> returnItem = new List<WeaponeGameData>();
            WeaponeGameData obj = new WeaponeGameData();

            //인벤토리를 검색한다
            for (int i = 0; i < itemList.Count; i++)
            {
                //아이템이 있으면
                if (itemList[i].transform.childCount != 0)
                {
                    //반환 시킬 리스트에 추가
                    obj = itemList[i].GetComponentInChildren<ItemData>().WeaponeData;
                    returnItem.Add(obj);
                }
            }

            //Debug.Log("저장된 인벤 아이템 개수 : " + returnItem.Count);

            //아이템을 모아둔 리스트를 반환 시킨다
            return returnItem; 
        }

        public void InvenSave()
        {
            
            if(!GameManager.INSTANCE.gameSystem.isPlayScene)
            {
                var obj = GameObject.Find("Main_PLAYER");
                playerData = obj.GetComponent<CharactersData>();


                //게임이 시작 되기전에 플레이어 데이터를 저장한다
                if (GameManager.INSTANCE.isMale)
                {
                    if (InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() != null)
                    {
                        //무기 장착 슬롯으 0 인덱스
                        GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                            InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>().WeaponeData, 1);

                        //Debug.Log("남 무기 장착 저장");
                    }

                    else if (InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() == null)
                    {
                        GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                            null, 1);

                        // Debug.Log("남 무기 없음 저장");
                    }
                    //Debug.Log("데이터 저장");
                }

                else
                {
                    if (InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() != null)
                    {
                        GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                        InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>().WeaponeData, 0);

                        //  Debug.Log("여 무기 장착 저장");
                    }

                    else if (InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() == null)
                    {
                        GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                            null, 0);

                        //  Debug.Log("여 무기 없음 저장");
                    }
                }


            }

            
        }

    }

}

