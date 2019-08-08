using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Black
{
    namespace Inventory
    {
        public class BagItemStoreInven : StoreBase, IStoreItemCreate
        {
            [SerializeField, Header("Sell / Buy Window")]
            GameObject sellbuyObj;
            bool isSell = false;
            bool isBuy = false;

            [SerializeField,Header("구매/판매 버튼 울러서 활성화 된 창의 택스트")]
            Text sellbuyText;

            [SerializeField, Header("아이템 가격")]
            Text priceText;

            [SerializeField,Header("BagItem 구매 판매 슬롯")]
            BagStoreItemDrop itemSlot;
                        
            InventoryData playerInven;

            public bool IsBuy { get => isBuy; set => isBuy = value; }

            void Start()
            {
                cg = GetComponent<CanvasGroup>();
                cg.alpha = 0.0f;
                cg.interactable = false;
                cg.blocksRaycasts = false;

                SellBuyWindowAct();
                
                ItemCreate();
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>();
                playerInven = GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<InventoryData>();
            }

            /// <summary>
            /// Bag 아이템을 하나씩 생성하여
            /// 상점 인벤에 장착 시킨다
            /// 슬롯 자체를 버튼으로 한다
            /// </summary>
            public void ItemCreate()
            {
                for (int i = 0; i < storeSlots.Count; i++)
                {
                    BagItemData data = storeSlots[i].GetComponent<BagItemData>();

                    data._BagItem._Id = parsingData.BagItemList[i].id;
                    data._BagItem._Type = parsingData.BagItemList[i].type;
                    data._BagItem._UseType = parsingData.BagItemList[i].useType;
                    data._BagItem._Name = parsingData.BagItemList[i].name;
                    data._BagItem._Value = parsingData.BagItemList[i].value;
                    data._BagItem._Weight = parsingData.BagItemList[i].weight;
                    data._BagItem._Count = parsingData.BagItemList[i].count;
                    data._BagItem._Price = parsingData.BagItemList[i].price;
                    data._BagItem._Tip = parsingData.BagItemList[i].tip;
                    data._BagItem._Path = parsingData.BagItemList[i].path;

                    data.NCount = data._BagItem._Count;
                    data.CountText();
                    data.IconSpr.sprite = Resources.Load<Sprite>(data._BagItem._Path);

                }
            }

            /// <summary>
            /// 상점을 닫는다
            /// </summary>
            public void CloseBtn()
            {
                //구매 밒 판매 창이 닫혀 있을때 창을 닫을수 있다
                if(sellbuyObj.activeSelf==false)
                {
                    player.IsInven = false;
                    player.InventoryInit();

                    cg.alpha = 0.0f;
                    cg.interactable = false;
                    cg.blocksRaycasts = false;
                }
                
            }

            /// <summary>
            /// 판매 버튼을 누르면
            /// 아이템 팔기 창이 활성화
            /// </summary>
            public void SellWindowBtn()
            {
                //구매 상태가 아닐때 클릭하면 판매 창이 열린다 (슬롯이 비워 있을때)
                //창이 비활성화 일때
                if (!IsBuy && itemSlot.transform.childCount == 0
                    && sellbuyObj.activeSelf == false)
                {
                    isSell = !isSell; //판매 상태 true이면 창을 활성화

                    sellbuyText.text = "Sell";
                    priceText.text = "0";

                    sellbuyObj.SetActive(true);
                }
                
            }

            /// <summary>
            /// 구매 버튼을 누르면
            /// 아이템 팔기 창이 활성화
            /// </summary>
            public void BuyWindowBtn(int itemID)
            {
                //플레이어의 인벤에 공간이 있을 경우
                //창을 연다
                if(playerInven.BagSlotCheck())
                {
                    if (!isSell && itemSlot.transform.childCount == 0
                       && sellbuyObj.activeSelf == false)
                    {
                        IsBuy = !IsBuy;//구매 상태 true이면 창을 활성화

                        sellbuyText.text = "Buy";

                        sellbuyObj.SetActive(true);

                        BuyWinIconCreate(itemID);
                        playerInven.WeightTextPrint();
                    }
                }
            }

            #region 아이템 구매 시 아이템 구매 창에 생성
            private void BuyWinIconCreate(int id)
            {
                //구매 슬롯 확인
                if(itemSlot.transform.childCount == 0)
                {
                    //아이콘 생성
                    BagItemData icon = Instantiate(GameManager.INSTANCE.BagItemIcon, itemSlot.transform);
                    
                    //상점 슬롯에 있는 아이템 중 선택된 아이템 정보를 가져와 적용
                    BagItemData data = storeSlots[id].GetComponent<BagItemData>();

                    icon._BagItem._Id = data._BagItem._Id;
                    icon._BagItem._Type = data._BagItem._Type;
                    icon._BagItem._UseType = data._BagItem._UseType;
                    icon._BagItem._Name = data._BagItem._Name;
                    icon._BagItem._Value = data._BagItem._Value;
                    icon._BagItem._Weight = data._BagItem._Weight;
                    icon._BagItem._Count = data._BagItem._Count;
                    icon._BagItem._Price = data._BagItem._Price;
                    icon._BagItem._Tip = data._BagItem._Tip;
                    icon._BagItem._Path = data._BagItem._Path;

                    icon.NCount = data._BagItem._Count;
                    icon.CountText();
                    icon.IconSpr.sprite = Resources.Load<Sprite>(icon._BagItem._Path);

                    icon.GetComponent<IconDrag>().enabled = false; //아이템 드래그 기능은 끈다

                    player.FWeight += icon._BagItem._Weight * icon.NCount; //아이템 1개의 무게를 증가 시킨다

                    priceText.text = icon._BagItem._Price.ToString(); //가격 출력
                }
            }
            #endregion


            /// <summary>
            /// 구매/판매 창을 닫는다
            /// </summary>
            public void SellBuyWindowDis()
            {
                //슬롯 안에 있는 아이템 제거
                BagItemData data = itemSlot.GetComponentInChildren<BagItemData>();

                //구매 중일때 창을 닫으면 닫아지지만
                //판매 중일떄는 아이템을 수동으로 뺴야  된다
                if (itemSlot.transform.childCount == 0 && !IsBuy)
                {
                    isSell = false;
                    IsBuy = false;

                    if (data != null)
                        data._BagItem._Count = 1; //다시 아이템 세팅 값으로 변경 (창을 닫을때)
                    sellbuyObj.SetActive(false);
                }                

                if(IsBuy)
                {                   

                    if (itemSlot.transform.childCount != 0)
                    {  
                        //적용 된 무게를 다시 감소 시키고
                        player.FWeight -= data._BagItem._Weight * data.NCount;
                        //제거
                        Destroy(data.gameObject);

                        isSell = false;
                        IsBuy = false;
                        sellbuyObj.SetActive(false);
                    }

                    else if(itemSlot.transform.childCount == 0)
                    {
                        isSell = false;
                        IsBuy = false;
                        sellbuyObj.SetActive(false);
                    }

                    priceText.text = "0";
                }

                playerInven.WeightTextPrint();
            }

            /// <summary>
            /// 상태에 따라 활성화 및 비활성화
            /// </summary>
            private void SellBuyWindowAct()
            {
                if (isSell)
                {
                    sellbuyObj.SetActive(true);
                }
                else
                {
                    sellbuyObj.SetActive(false);
                }
            }

            /// <summary>
            /// 최종적으로 아이템을 판다
            /// </summary>
            public void Sell_Buy_Btn()
            {
                //슬롯 안에 있는 아이템의 데이터
                BagItemData data = itemSlot.GetComponentInChildren<BagItemData>();

                
                if(data != null)
                {
                    //판매                
                    if (isSell && !IsBuy)
                    {
                        player.NMoney +=((data.NCount * data._BagItem._Price) / GameManager.INSTANCE.SellPrice);

                        //판매한 만큼 무게를 줄인다
                      //  player.FWeight -= (data._BagItem._Weight * data.NCount);

                        //판매 슬롯에 장착 했을때 아이템의 총 수량에 판매 시킨는 수량을 뺀다
                        data._BagItem._Count -= data.NCount;

                        //아이템의 남은 수량
                        data.NCount = data._BagItem._Count;
                        data.CountText();
                        
                        //만약에 아이템을 전부 판매를 했으면 아이템을 삭제 시킨다
                        if(data.NCount == 0)
                        {
                            Destroy(data.gameObject);
                            priceText.text = "0";
                        }
                        else
                        {
                            //아이템이 남아 있으면 판매 가격을 최신화
                            priceText.text = ((data.NCount * data._BagItem._Price) / GameManager.INSTANCE.SellPrice).ToString("N0");
                        }

                    }

                    //구매
                    else if (!isSell && IsBuy)
                    {
                        //무게가 넘으면 구매가 안된다
                        if (player.FWeight > player.FMaxWeight)
                        {
                            //구매 불가  UI
                        }
                        else
                        {
                            for (int i = 0; i < playerInven.BagInvenList.Count; i++)
                            {
                                //플레이어의 빈 슬롯에 아이템을 추가 시킨다
                                if (playerInven.BagInvenList[i].transform.childCount == 0)
                                {
                                    //무게는 아이템 개수 선택 시 바로 적용 되기 때문에 아이템만 생성
                                    BagItemData create = Instantiate(GameManager.INSTANCE.BagItemIcon, playerInven.BagInvenList[i].transform);


                                    create._BagItem._Id = data._BagItem._Id;
                                    create._BagItem._Type = data._BagItem._Type;
                                    create._BagItem._UseType = data._BagItem._UseType;
                                    create._BagItem._Name = data._BagItem._Name;
                                    create._BagItem._Value = data._BagItem._Value;
                                    create._BagItem._Weight = data._BagItem._Weight;
                                    create._BagItem._Count = data.NCount;
                                    create._BagItem._Price = data._BagItem._Price;
                                    create._BagItem._Tip = data._BagItem._Tip;
                                    create._BagItem._Path = data._BagItem._Path;

                                    create.NCount = create._BagItem._Count;
                                    create.CountText();
                                    create.IconSpr.sprite = Resources.Load<Sprite>(create._BagItem._Path);

                                    player.NMoney -= (create._BagItem._Price * create.NCount); //소지금 감소

                                    Destroy(data.gameObject); //데이터를 넘기고 구매 아이템은 삭제

                                    priceText.text = "0";
                                    break;
                                }
                            }
                        }
                    }
                }
                
                playerInven.WeightTextPrint();
            }

             /// <summary>
            /// 아이템의 개수를 줄인다
            /// </summary>
            public void LeftBtn()
            {
                //아이템의 수량과 가격 측정
                BagItemData data = itemSlot.GetComponentInChildren<BagItemData>();

                if(data  != null)
                {
                    if (data.NCount > 1)
                    {
                        data.NCount--;

                        //판매는 무게를 마지막에 처리한다
                        if (IsBuy)
                            player.FWeight -= data._BagItem._Weight;

                    }

                    playerInven.WeightTextPrint();
                    data.CountText();

                    PriceValueText(data);
                }
               
            }

            /// <summary>
            /// 아이템의 개수를 증가 시킨다
            /// </summary>
            public void RightBtn()
            {
                //아이템의 수량과 가격 측정
                BagItemData data = itemSlot.GetComponentInChildren<BagItemData>();

                if(data != null)
                {
                    if (player.NMoney >= data._BagItem._Price &&
                             player.FWeight < player.FMaxWeight && IsBuy)
                    {
                        data.NCount++;

                        player.FWeight += data._BagItem._Weight;

                        //만약 무게를 넘으면 다시 하나를 감소 시킨다
                        if (player.FWeight > player.FMaxWeight)
                        {
                            data.NCount--;
                            if (IsBuy)
                                player.FWeight -= data._BagItem._Weight;
                        }

                    }

                    else if (isSell)
                    {
                        if (data.NCount < data._BagItem._Count)
                        {
                            data.NCount++;
                        }
                    }

                    playerInven.WeightTextPrint();
                    data.CountText();

                    PriceValueText(data);
                }
                
              
            }

            /// <summary>
            /// 가격 출력 함수
            /// </summary>
            /// <param name="data"></param>
            void PriceValueText(BagItemData data)
            {
                if (isSell) //판매 가격
                {
                    priceText.text = ((data._BagItem._Price * data.NCount) / GameManager.INSTANCE.SellPrice).ToString("N0");
                }
                    

                else if (IsBuy) //구매 가격
                {
                    priceText.text = (data._BagItem._Price * data.NCount).ToString();
                }
                    
            }

        }
        //class End
    }

}
