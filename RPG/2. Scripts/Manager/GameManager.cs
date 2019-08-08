using Black.Characters;
using Black.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Black
{
    namespace Manager
    {
        [Serializable]
        public struct BgmCtrl
        {            
            public float bgmVolume;
            public float sfxVolume;
        }


        public class GameManager : MonoBehaviour
        {

            public static GameManager INSTANCE = null;

            public BgmCtrl Bgm;
            private bool isPostOn = true; //포스트 프로세싱 옵션

            public bool isMenu = false; //Esc 메뉴
            private bool isEvent = false; //이벤트 중인지 확인
            public bool isSafeArea = false; //안전 지역인지 확인


            [SerializeField, Header("작업 중일때 퀘스트 빨리 생성")]
            bool isTest = false;

            //public BgmCtrl Bgm { get => bgm; set => bgm = value; }
            public int CurPlayerIndenx { get => curPlayerIndenx; set => curPlayerIndenx = value; }
            public DataManager _DataManager { get => dataManager; set => dataManager = value; }
            public int NSlotCount { get => nSlotCount; set => nSlotCount = value; }
            public Inventory.BagItemData BagItemIcon { get => bagItemIcon; set => bagItemIcon = value; }
            public Inventory.PartsItemData PartsItemIcon { get => partsItemIcon; set => partsItemIcon = value; }
            public Inventory.SubItemData SubItemIcon { get => subItemIcon; set => subItemIcon = value; }
            public int SellPrice { get => sellPrice; set => sellPrice = value; }
            public bool IsEvent { get => isEvent; set => isEvent = value; }
            public float PlayerSpeed { get => playSpeed; set => playSpeed = value; }
            public bool IsPostOn { get => isPostOn; set => isPostOn = value; }
            public bool IsTest { get => isTest; set => isTest = value; }

            [SerializeField, Header("아이템 판매 시 감소 량 /sellPrice")]
            private int sellPrice = 2;

            //아이템 습득 시 데이터를 넘기면서 (GetComponent)
            //아이콘을 생성 시킨다(인벤에 여유 공간이 있을떼)
            [SerializeField]
            Inventory.BagItemData bagItemIcon;
            [SerializeField]
            Inventory.PartsItemData partsItemIcon;
            [SerializeField]
            Inventory.SubItemData subItemIcon;

            /// <summary>
            /// 캐릭터 선택 아이디, (메인에서는 카메라 이동 좌표도 포함)..
            /// 메인에서 캐릭터 아이디 값을 다음 씬으로 이동 할때
            /// 가지고 가서
            /// 캐릭터를 데이터를 적용 시키는데 사용
            /// (아직은 캐릭터가 하나이기 때문에 사용되지는 않고
            /// 미리 준비)
            /// 
            /// 이 값으로 캐릭터의 얼굴과 스킬 아이콘을 선택하도록 하면 될꺼 같다
            /// </summary>
            int curPlayerIndenx = 0;

            DataManager dataManager;
            public PlayerData loadPlayerData;

            [SerializeField, Header("세이브 슬롯 개수")]
            int nSlotCount = 3;

            /// <summary>
            /// 게임의 플레이 속도
            /// 소리, 움직임 등 제어
            /// </summary>
            float playSpeed = 1.0f;

            /// <summary>
            /// 스테이지에서 로비(반대도 포함)로 전환 할때
            /// 퀵 저장을 하는데
            /// 이것을 퀵 로드하기 위해
            /// 상태 변환 값을 설정
            /// </summary>
            public bool isSceneMove = false;

            private void Awake()
            {
                ManagerInit();

                _DataManager = GetComponent<DataManager>();

                //세이브 로드 슬롯을 초기화 한다
                for (int i = 0; i < NSlotCount; i++)
                {
                    _DataManager.Initialize(i);
                }


            }


            #region 싱글턴
            void ManagerInit()
            {
                if (INSTANCE == null)
                {
                    INSTANCE = this;
                }
                if (INSTANCE != this)
                {
                    Destroy(gameObject);
                }

                DontDestroyOnLoad(gameObject);
            }
            #endregion

            /// <summary>
            /// 설정된 볼륨 값으로 효과음 재생, 
            /// </summary>
            /// <param name="audio"></param>
            /// <param name="sfx"></param>
            public void SFXPlay(AudioSource audio, AudioClip sfx)
            {                
                audio.volume = Bgm.sfxVolume;
                audio.PlayOneShot(sfx);
            }

            /// <summary>
            /// 볼륨 세팅에 맞게 재생
            /// </summary>
            /// <param name="audio"></param>
            /// <param name="bmg"></param>
            public void BgmPlay(AudioSource audio, AudioClip bgm)
            {
                audio.Stop(); //재생 중인 배경음 정지

                audio.loop = true; //반복 재생
                //audio.volume = Bgm.bgmVolume;
                audio.clip = bgm; //배경음 적용
                audio.Play();
            }

            /// <summary>
            /// 로드 하여 캐릭터 데이터 임시 저장 후
            /// 캐릭터가 활성화 되면서 초기화를 시킨다
            /// </summary>
            public void LoadData(int saveIndex)
            {
                PlayerData data = _DataManager.Load(saveIndex);

                loadPlayerData = data;

            }

            /// <summary>
            /// 퀵로드
            /// </summary>
            public void QuickLoadData()
            {
                PlayerData data = _DataManager.QuickLoad();

                loadPlayerData = data;

            }

            /// <summary>
            /// 새로 시작
            /// 파싱 데이터를 적용 시킨다
            /// </summary>
            public void NewGame()
            {
                PlayerData data = _DataManager.NewData();
                loadPlayerData = data;
            }

            /// <summary>
            /// 데이터 저장
            /// </summary>
            /// <param name="player"></param>
            /// <param name="slotIndex"></param>
            public void SaveData(PlayerCtrl player, int slotIndex)
            {
                _DataManager.Save(player, slotIndex);
            }


            /// <summary>
            /// 인벤에 저장된 아이템 정보를
            /// 로드 시킨다(아이콘 생성)
            /// ChestManager, InventoryData에서 호출
            /// 
            /// 생성 시킬 슬롯 리스트, 아이템 저장 리스트
            /// </summary>
            public void LoadBagItem(List<GameObject> invenSlot, List<BagItemData> itemInfo)
            {
                for (int i = 0; i < itemInfo.Count; i++)
                {
                    //아이템을 추가 하기전에 남아 있는 아이콘이 있을 수 있기 때문에 초기화를 해준다
                    if (invenSlot[i].transform.childCount != 0)
                    {
                        Destroy(invenSlot[i].transform.GetComponentInChildren<GameObject>());
                    }

                    //아이콘을 인벤에 순서데로 생성 시킨다
                    Inventory.BagItemData data = Instantiate(BagItemIcon, invenSlot[i].transform);

                    data._BagItem._Id = itemInfo[i].id;
                    data._BagItem._Type = itemInfo[i].type;
                    data._BagItem._UseType = itemInfo[i].useType;
                    data._BagItem._Name = itemInfo[i].name;
                    data._BagItem._Value = itemInfo[i].value;
                    data._BagItem._Weight = itemInfo[i].weight;
                    data.NCount = itemInfo[i].count;
                    data._BagItem._Price = itemInfo[i].price;
                    data._BagItem._Tip = itemInfo[i].tip;
                    data._BagItem._Path = itemInfo[i].path;

                    data.CountText();
                    data.IconSpr.sprite = Resources.Load<Sprite>(data._BagItem._Path);
                }
            }

            /// <summary>
            /// 파츠 아이템을 로드하는 함수
            /// </summary>
            /// <param name="invenSlot"></param>
            /// <param name="itemInfo"></param>
            public void LoadPartsItem(List<GameObject> invenSlot, List<PartsItemData> itemInfo)
            {
                for (int i = 0; i < itemInfo.Count; i++)
                {
                    //아이템을 추가 하기전에 남아 있는 아이콘이 있을 수 있기 때문에 초기화를 해준다
                    if (invenSlot[i].transform.childCount != 0)
                    {
                        Destroy(invenSlot[i].transform.GetComponentInChildren<GameObject>());
                    }

                    //아이콘을 인벤에 순서데로 생성 시킨다
                    Inventory.PartsItemData data = Instantiate(PartsItemIcon, invenSlot[i].transform);

                    data._PartsItem._Id = itemInfo[i].id;
                    data._PartsItem._Type = itemInfo[i].type;
                    data._PartsItem._Name = itemInfo[i].name;
                    data._PartsItem._NLevel = itemInfo[i].nLevel;
                    data._PartsItem._NMaxLevel = itemInfo[i].nMaxLevel;

                    data._PartsItem._IsExplosion = itemInfo[i].isExplosion;
                    data._PartsItem._FMinRage = itemInfo[i].fMinRage;
                    data._PartsItem._FMaxRage = itemInfo[i].fMaxRage;
                    data._PartsItem._FExplosionArea = itemInfo[i].fExplosionArea;

                    data._PartsItem._IsStun = itemInfo[i].isStun;
                    data._PartsItem._FStunMinPer = itemInfo[i].fStunMinPer;
                    data._PartsItem._FStunMaxPer = itemInfo[i].fStunMaxPer;
                    data._PartsItem._FStunPer = itemInfo[i].fStunPer;

                    data._PartsItem._DmgMinUp = itemInfo[i].dmgMinUp;
                    data._PartsItem._DmgMaxUp = itemInfo[i].dmgMaxUp;
                    data._PartsItem._DmgUp = itemInfo[i].dmgUp;

                    data._PartsItem._AccUp = itemInfo[i].accUp;

                    data.NCount = itemInfo[i].count; //개수(데이터의 개수가 아닌 실제 개수 값을 넘김)

                    data._PartsItem._Price = itemInfo[i].price;
                    data._PartsItem._Tip = itemInfo[i].tip;
                    data._PartsItem._Path = itemInfo[i].path;

                    data.LevelText();
                    data.IconSpr.sprite = Resources.Load<Sprite>(data._PartsItem._Path);
                }
            }

            /// <summary>
            /// 보조 무기(수류탄) 로드함수
            /// </summary>
            /// <param name="invenSlot"></param>
            /// <param name="itemInfo"></param>
            public void LoadSubtsItem(List<GameObject> invenSlot, List<SubItemData> itemInfo)
            {
                for (int i = 0; i < itemInfo.Count; i++)
                {
                    //아이템을 추가 하기전에 남아 있는 아이콘이 있을 수 있기 때문에 초기화를 해준다
                    if (invenSlot[i].transform.childCount != 0)
                    {
                        Destroy(invenSlot[i].transform.GetComponentInChildren<GameObject>());
                    }

                    //아이콘을 인벤에 순서데로 생성 시킨다
                    Inventory.SubItemData data = Instantiate(subItemIcon, invenSlot[i].transform);

                    data._SubItem._Id = itemInfo[i].id;
                    data._SubItem._Type = itemInfo[i].type;
                    data._SubItem._ItemType = itemInfo[i].itemType;
                    data._SubItem._Name = itemInfo[i].name;
                    data._SubItem._MinDmg = itemInfo[i].minDmg;
                    data._SubItem._MaxDmg = itemInfo[i].maxDmg;

                    data.NCount = itemInfo[i].count; //개수(데이터의 개수가 아닌 실제 개수 값을 넘김)

                    data._SubItem._Price = itemInfo[i].price;
                    data._SubItem._Tip = itemInfo[i].tip;
                    data._SubItem._Path = itemInfo[i].path;

                    data.CountText();
                    data.IconSpr.sprite = Resources.Load<Sprite>(data._SubItem._Path);
                }
            }

            public void LoadQuickItem(List<GameObject> quickSlot, List<QuickSlotData> quickInfo)
            {
                for (int i = 0; i < quickInfo.Count; i++)
                {
                    if (quickSlot[i].transform.childCount != 0)
                    {
                        Destroy(quickSlot[i].transform.GetComponentInChildren<GameObject>());
                    }
                    //소모 아이템의 이름이 검색이 되면 아이템이 있는 것으로 본다
                    if (quickInfo[i].bagItem.name != null)
                    {
                        //아이콘을 인벤에 순서데로 생성 시킨다
                        Inventory.BagItemData data = Instantiate(BagItemIcon, quickSlot[i].transform);

                        data._BagItem._Id = quickInfo[i].bagItem.id;
                        data._BagItem._Type = quickInfo[i].bagItem.type;
                        data._BagItem._UseType = quickInfo[i].bagItem.useType;
                        data._BagItem._Name = quickInfo[i].bagItem.name;
                        data._BagItem._Value = quickInfo[i].bagItem.value;
                        data._BagItem._Weight = quickInfo[i].bagItem.weight;
                        data.NCount = quickInfo[i].bagItem.count;
                        data._BagItem._Price = quickInfo[i].bagItem.price;
                        data._BagItem._Tip = quickInfo[i].bagItem.tip;
                        data._BagItem._Path = quickInfo[i].bagItem.path;

                        data.CountText();
                        data.IconSpr.sprite = Resources.Load<Sprite>(data._BagItem._Path);
                    }
                    
                    //위에 소모 아이템이 없는 것으로 처리가 되면 보조 아이템을 생성
                    else
                    {
                        //아이콘을 인벤에 순서데로 생성 시킨다
                        Inventory.SubItemData data = Instantiate(subItemIcon, quickSlot[i].transform);

                        data._SubItem._Id = quickInfo[i].subItem.id;
                        data._SubItem._Type = quickInfo[i].subItem.type;
                        data._SubItem._ItemType = quickInfo[i].subItem.itemType;
                        data._SubItem._Name = quickInfo[i].subItem.name;
                        data._SubItem._MinDmg = quickInfo[i].subItem.minDmg;
                        data._SubItem._MaxDmg = quickInfo[i].subItem.maxDmg;

                        data.NCount = quickInfo[i].subItem.count; //개수(데이터의 개수가 아닌 실제 개수 값을 넘김)

                        data._SubItem._Price = quickInfo[i].subItem.price;
                        data._SubItem._Tip = quickInfo[i].subItem.tip;
                        data._SubItem._Path = quickInfo[i].subItem.path;

                        data.CountText();
                        data.IconSpr.sprite = Resources.Load<Sprite>(data._SubItem._Path);

                    }

                }

            }

            #region 태그 사용
            /// <summary>
            /// 태그 사용
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public string ObjTagSetting(eObjTag obj)
            {
                string str = null;
                switch (obj)
                {
                    case eObjTag.Helicopter:
                        str = "Helicopter";
                        break;
                    case eObjTag.Cop:
                        str = "Cop";
                        break;
                    case eObjTag.Swat:
                        str = "Swat";
                        break;
                    case eObjTag.SoulEx:
                        str = "SoulEx";
                        break;
                    case eObjTag.Player:
                        str = "Player";
                        break;
                    case eObjTag.Enemy:
                        str = "Enemy";
                        break;
                }

                return str;
            }
            #endregion

            //
        }
    }
}