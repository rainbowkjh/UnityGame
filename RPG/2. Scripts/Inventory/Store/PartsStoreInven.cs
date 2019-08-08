using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 파츠를 판매하는 상점의 인벤토리
/// 게임 시작 시 랜덤으로
/// 랜덤의 수량의 랜덤의 아이템을 생성 시킨다
/// 새로고침을 눌러 수동으로 아이템을 최신화 하지만
/// 돈을 사용해야 한다
/// </summary>
namespace Black
{
    namespace Inventory
    {
        /// <summary>
        /// 파일을 따로 만들지 않고 이 곳에 만들었음(파일이 너무 많아져서;;)
        /// 파츠와 보조 무기 아이템 상점의 공통 부분을
        /// 상위 클래스로 만듬
        /// </summary>
        public class StoreBase : MonoBehaviour
        {
           protected CanvasGroup cg;

            [SerializeField]
            protected List<GameObject> storeSlots;

            [SerializeField, Header("파싱데이터에서 아이템 데이터가 필요")]
            protected ParsingData parsingData;

            protected Characters.PlayerCtrl player;
        }

        public interface IStoreItemCreate
        {
          void ItemCreate();
        }

        public class PartsStoreInven : StoreBase, IStoreItemCreate
        {
            
            
            private void Start()
            {
                cg = GetComponent<CanvasGroup>();
                cg.alpha = 0.0f;
                cg.interactable = false;
                cg.blocksRaycasts = false;

                ItemCreate();
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>();
            }

            /// <summary>
            /// 상점 인벤에 아이템을 생성 시킨다
            /// </summary>
            public void ItemCreate()
            {
                //최소 3개에서 인벤의 크기 만큼 랜덤 값
                //인벤에 아이템을 생성 시킬 개수이다
                int rand = Random.Range(3, storeSlots.Count);

                for(int i=0; i<rand;i++)
                {
                    //슬롯에 아이템이 있으면 삭제
                    if(storeSlots[i].transform.childCount != 0)
                    {
                        PartsItemData item = storeSlots[i].transform.GetComponentInChildren<PartsItemData>();
                        Destroy(item.gameObject);
                    }


                    //슬롯이 비워 있으면
                    if(storeSlots[i].transform.childCount == 0)
                    {
                        //파츠 아이템의 아이디 값을 받아온다
                        //아이디 값이 리스트의 크기 값과 같다
                        int randID = Random.Range(0, parsingData.PartsItemList.Count);

                        GameObject obj = Instantiate(GameManager.INSTANCE.PartsItemIcon, storeSlots[i].transform).gameObject;

                        PartsItemData data = obj.GetComponent<PartsItemData>();
                        //아이고야...
                        //아이템 데이터 적용 ㅋㅋㅋ
                        //ㅡㅡ...
                        data._PartsItem._Id = parsingData.PartsItemList[randID].id;                        
                        data._PartsItem._Type = parsingData.PartsItemList[randID].type;
                        data._PartsItem._Name = parsingData.PartsItemList[randID].name;
                        data._PartsItem._NLevel = parsingData.PartsItemList[randID].nLevel;
                        data._PartsItem._NMaxLevel = Random.Range(5, 30); //업그래이드 최대치는 랜덤

                        data._PartsItem._IsExplosion = parsingData.PartsItemList[randID].isExplosion;
                        data._PartsItem._FMinRage = parsingData.PartsItemList[randID].fMinRage;
                        data._PartsItem._FMaxRage = parsingData.PartsItemList[randID].fMaxRage;
                        data._PartsItem._FExplosionArea = Random.Range(data._PartsItem._FMinRage, data._PartsItem._FMaxRage);

                        data._PartsItem._IsStun = parsingData.PartsItemList[randID].isStun;
                        data._PartsItem._FStunMinPer = parsingData.PartsItemList[randID].fStunMinPer;
                        data._PartsItem._FStunMaxPer = parsingData.PartsItemList[randID].fStunMaxPer;
                        data._PartsItem._FStunPer = Random.Range(data._PartsItem._FStunMinPer, data._PartsItem._FStunMaxPer);

                        data._PartsItem._DmgMinUp = parsingData.PartsItemList[randID].dmgMinUp;
                        data._PartsItem._DmgMaxUp = parsingData.PartsItemList[randID].dmgMaxUp;
                        data._PartsItem._DmgUp = Random.Range(data._PartsItem._DmgMinUp, data._PartsItem._DmgMaxUp);

                        data._PartsItem._AccUp = parsingData.PartsItemList[randID].accUp;
                        data._PartsItem._Count = parsingData.PartsItemList[randID].count;
                        data._PartsItem._Price = parsingData.PartsItemList[randID].price;
                        data._PartsItem._Tip = parsingData.PartsItemList[randID].tip;
                        data._PartsItem._Path = parsingData.PartsItemList[randID].path;

                        data.IsStoreItem = true; //상점 아이템으로 분류

                        data.IconSpr.sprite = Resources.Load<Sprite>(data._PartsItem._Path);
                        data.LevelText();
                    }
                }
            }

            /// <summary>
            /// 아이템을 슬롯에서 제거
            /// </summary>
           void ItemDelete()
            {
                for(int i=0; i<storeSlots.Count;i++)
                {
                    //슬롯에 아이템이 존재하면
                    if(storeSlots[i].transform.childCount != 0)
                    {
                        //아이템을 가져와 삭제한다
                        PartsItemData Obj = storeSlots[i].transform.GetComponentInChildren<PartsItemData>();
                        Destroy(Obj.gameObject);
                    }
                }
            }

            /// <summary>
            /// 새로 고침 버튼
            /// </summary>
            public void RefreshBtn()
            {
                if(player.NMoney >=500)
                {
                    //슬롯들을 삭제하고
                    //ItemDelete();
                    //처음 초기화처럼 생성
                    ItemCreate();

                    player.NMoney -= 500;
                }
                
            }

            public void CloseBtn()
            {
                player.IsInven = false;
                player.InventoryInit();

                cg.alpha = 0.0f;
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }

          
        }

    }
}
