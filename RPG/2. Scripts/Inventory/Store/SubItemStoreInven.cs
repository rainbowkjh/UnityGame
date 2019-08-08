using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 파츠 상점과 같은 원리
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class SubItemStoreInven : StoreBase, IStoreItemCreate
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

                for (int i = 0; i < rand; i++)
                {
                    //슬롯에 아이템이 있으면 삭제
                    if (storeSlots[i].transform.childCount != 0)
                    {
                        SubItemData item = storeSlots[i].transform.GetComponentInChildren<SubItemData>();
                        Destroy(item.gameObject);
                    }


                    //슬롯이 비워 있으면
                    if (storeSlots[i].transform.childCount == 0)
                    {
                        //파츠 아이템의 아이디 값을 받아온다
                        //아이디 값이 리스트의 크기 값과 같다
                        int randID = Random.Range(0, parsingData.SubItemList.Count);

                        GameObject obj = Instantiate(GameManager.INSTANCE.SubItemIcon, storeSlots[i].transform).gameObject;

                        SubItemData data = obj.GetComponent<SubItemData>();
                        //아이고야...
                        //아이템 데이터 적용 ㅋㅋㅋ
                        //ㅡㅡ...
                        data._SubItem._Id = parsingData.SubItemList[randID].id;
                        data._SubItem._Type = parsingData.SubItemList[randID].type;
                        data._SubItem._ItemType = parsingData.SubItemList[randID].itemType;
                        data._SubItem._Name = parsingData.SubItemList[randID].name;
                        data._SubItem._MinDmg = parsingData.SubItemList[randID].minDmg;
                        data._SubItem._MaxDmg = parsingData.SubItemList[randID].maxDmg;
                        data._SubItem._Count = parsingData.SubItemList[randID].count;
                        data._SubItem._Price = parsingData.SubItemList[randID].price;
                        data._SubItem._Tip = parsingData.SubItemList[randID].tip;
                        data._SubItem._Path = parsingData.SubItemList[randID].path;

                        data.IconSpr.sprite = Resources.Load<Sprite>(data._SubItem._Path);
                        data.IsStoreItem = true; //상점 아이템으로 분류

                        data.NCount = data._SubItem._Count;
                        data.CountText();
                    }
                }
            }

            /// <summary>
            /// 아이템을 슬롯에서 제거
            /// </summary>
            void ItemDelete()
            {
                for (int i = 0; i < storeSlots.Count; i++)
                {
                    //슬롯에 아이템이 존재하면
                    if (storeSlots[i].transform.childCount != 0)
                    {
                        //아이템을 가져와 삭제한다
                        SubItemData Obj = storeSlots[i].transform.GetComponentInChildren<SubItemData>();
                        Destroy(Obj.gameObject);
                    }
                }
            }

            /// <summary>
            /// 새로 고침 버튼
            /// </summary>
            public void RefreshBtn()
            {
                if (player.NMoney >= 500)
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
