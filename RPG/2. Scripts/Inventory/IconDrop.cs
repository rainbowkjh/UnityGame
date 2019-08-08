using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 일반 인벤토리 슬롯
/// 타입이 동일해 아이템을 담는다
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class IconDrop : MonoBehaviour, IDropHandler
        {
            [SerializeField, Header("개수 분할 시 아이콘 생성")]
            GameObject BagIconCreate;

            [SerializeField, Header("슬롯 타입")]
            eItemType slotType;

            [SerializeField, Header("플레이어 가방 인벤 슬롯")]
            bool isPlayerSlot;

            Characters.PlayerCtrl player;

            private void Start()
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>();
            }

            public bool IsPlayerSlot { get => isPlayerSlot; set => isPlayerSlot = value; }

            public void OnDrop(PointerEventData eventData)
            {
                //슬롯이 비웠을때
                if (transform.childCount == 0)
                {
                    if(IconDrag.draggingItem != null)
                    {
                        //슬롯과 아이템 타입이 같으면 슬롯에 장착
                        if (slotType == IconDrag.draggingItem.GetComponent<IconDrag>().ItemType)
                        {

                            IconDrag.draggingItem.transform.SetParent(this.transform);

                            IconDrag.draggingItem.GetComponent<ItemDataClass>().IsStoreItem = false; //플레이어 아이템 소속 변경

                            //플레이어의 가방 슬롯의 경우
                            //아이템을 슬롯에 놓으면 무게를 증가 시킨다
                            //습득 시 무게 증가는 아이템 습득하는 부분에서 처리
                            //아이템 이동 시 해당
                            if (IsPlayerSlot)
                            {
                                if (IconDrag.draggingItem.GetComponent<BagItemData>())
                                {
                                    WeightCheckAdd();
                                }
                            }
                        }
                    }
                    

                }



                //슬롯에 아이템이 있으면
                else if (transform.childCount == 1)
                {
                    //슬롯과 아이템 타입이 같으면 슬롯에 장착
                    if (slotType == IconDrag.draggingItem.GetComponent<IconDrag>().ItemType)
                    {
                        //소모 아이템인지(이동 중 아이템, 슬롯에 있는 아이템) 확인
                        if (IconDrag.draggingItem.GetComponent<BagItemData>() &&
                        GetComponentInChildren<BagItemData>())
                        {

                            WeightCheck();

                            #region 함수로 만들기 전
                            //BagItemData dragItem = IconDrag.draggingItem.GetComponent<BagItemData>(); //이동 중인 아이템
                            //BagItemData slotItem = GetComponentInChildren<BagItemData>(); //하위 오브젝트(슬롯에 있는 아이템)

                            ////같은 아이템이 슬롯에 존재하면
                            //if (dragItem._BagItem._Name.Equals(slotItem._BagItem._Name))
                            //{
                            //    if (IsPlayerSlot)
                            //    {
                            //        Characters.PlayerCtrl player = GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>();

                            //        //임시로 플레이어의 무게에 이동 중인 아이템을 인벤에 넣었을때 무게를 구한다(현재 무게 + (추가 무게 +개수))
                            //        float tempWeight = player.FWeight + (dragItem._BagItem._Weight * dragItem.NCount);

                            //        //최대 무게를 넘지 않을 경우
                            //        //아이템 전부를 슬롯에서 받는다
                            //        if (tempWeight <= player.FMaxWeight)
                            //        {
                            //            player.FWeight = tempWeight; //임시 저장된 무게를 적용 시킨다
                            //            slotItem.NCount += dragItem.NCount; //슬롯에 있는 아이템의 개수 증가
                            //            Destroy(dragItem.gameObject); //이동 중이였던 아이템 삭제
                            //        }

                            //        //최대 무게를 넘는 경우
                            //        //무게이 맞게 개수를 가져온다
                            //        else if (tempWeight > player.FMaxWeight)
                            //        {
                            //            int i = 0;
                            //            //이동중인 아이템의 개수만큼
                            //            for (i = 1; i <= dragItem.NCount; i++)
                            //            {
                            //                //무게를 계산해본다
                            //                tempWeight = player.FWeight + (dragItem._BagItem._Weight * i);

                            //                //최대 무게치를 넘을 경우
                            //                if (tempWeight > player.FMaxWeight)
                            //                {
                            //                    //한개를 다시 빼고 루프 종료
                            //                    i--;

                            //                    //한개를 뺸 개수로 다시 계산하여 무게를 측정
                            //                    tempWeight = player.FWeight + (dragItem._BagItem._Weight * i);

                            //                    break;
                            //                }
                            //            }

                            //            //슬롯에 넣을 개수를 이동 아이콘 개수를 뺸다 
                            //            dragItem.NCount -= i;
                            //            //이동 시킨 아이콘 개수를 슬롯 아이콘 개수에 증가
                            //            slotItem.NCount += i;


                            //            //최종적으로 적용 
                            //            player.FWeight = tempWeight;

                            //            //이동 중인 아이템의 개수에 따라 개수 표시 및 삭제
                            //            if (dragItem.NCount != 0)
                            //                dragItem.CountText();
                            //            else if (dragItem.NCount == 0)
                            //                Destroy(dragItem.gameObject);

                            //            slotItem.CountText();
                            //        }
                            //    }


                            //    GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<InventoryData>().WeightTextPrint(); //무게 값




                            //    if (dragItem.NCount != 0)
                            //        dragItem.CountText(); //이동 중이였던 아이콤의 남은 개수
                            //}
                            #endregion
                        }
                    }
                        
                }

            }

            /// <summary>
            /// 비워있는 슬롯에 아이템을 놓읗때
            /// 가방 아이템의 경우
            /// 무게를 확인하여 이동 가능 개수만 이동
            /// 나머지는 윈위치
            /// </summary>
            void WeightCheckAdd()
            {
                //Characters.PlayerCtrl player = GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>();
                BagItemData dragItem = IconDrag.draggingItem.GetComponent<BagItemData>();                

                InventoryData inven = GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<InventoryData>();

                if (IsPlayerSlot)
                {
                    //같은 종류의 아이템이 있어서
                    //개수를 전부 합침
                    if (inven.bagItemEqual(dragItem))
                    {
                        dragItem.CountText();

                        if (dragItem.NCount == 0)
                            //아이템 표시는 bagItemEqual에서 처리
                            Destroy(dragItem.gameObject);

                        //개수가 남아 있으면 임시 위치(원래 위치로) 돌려 보낸다
                        else if (dragItem.NCount != 0)
                            dragItem.transform.SetParent(dragItem.GetComponent<IconDrag>().TempTr);
                    }

                    else if(!inven.bagItemEqual(dragItem))
                    {
                        //임시 무게 값
                        float tempWeight = player.FWeight + (dragItem._BagItem._Weight * dragItem.NCount);

                        int i = 0;
                        int tempCount = 0; // 0이 아니면 아이콘을 생성 시킨다(원래 위치로 돌려 보내기)
                        bool isCreate = false; //무게를 넘어 아이콘을 생성 시킨다

                        //이동중인 아이템의 개수만큼
                        for (i = 1; i <= dragItem.NCount; i++)
                        {
                            //무게를 계산해본다
                            tempWeight = player.FWeight + (dragItem._BagItem._Weight * i);

                            //최대 무게치를 넘을 경우
                            if (tempWeight > player.FMaxWeight)
                            {
                                //한개를 다시 빼고 루프 종료
                                i--;

                                //한개를 뺸 개수로 다시 계산하여 무게를 측정
                                tempWeight = player.FWeight + (dragItem._BagItem._Weight * i);

                                isCreate = true; //아이콘을 생성 시킬수 있도록 한다
                                break;
                            }
                                                       
                        }

                        
                        //아이템이 분할 되면 아이콘을 생성 시켜 원래 위치로 돌려 놓은다(나머지 개수)
                        if(isCreate)
                        {
                            //이동 시킨 아이템 개수에서 남은 개수를 저장
                            tempCount = dragItem.NCount - i;
                            //슬롯에 넣을 개수를 이동 아이콘 개수를 적용
                            dragItem.NCount = i;
                                                       
                            //새로 생성 후 데이터를 넘겨주고
                            GameObject obj = Instantiate<GameObject>(BagIconCreate);
                            obj.transform.SetParent(dragItem.GetComponent<IconDrag>().TempTr);

                            BagItemData data = obj.GetComponent<BagItemData>();

                            //개수를 적용 시킨다
                            data._BagItem = dragItem._BagItem;
                            data.IconSpr.sprite = Resources.Load<Sprite>(data._BagItem._Path);
                            data.NCount = tempCount;
                            data.CountText();
                        }


                        //최종적으로 적용 
                        player.FWeight = tempWeight;

                        //이동 시킨 아이콘의 개수가 남아 있으면 슬롯에 놓은다
                        if (dragItem.NCount > 0)
                            dragItem.transform.SetParent(this.transform);
                        else if (dragItem.NCount <= 0)
                            Destroy(dragItem.gameObject);
                        dragItem.CountText();


                        //player.FWeight += (dragItem._BagItem._Weight * dragItem.NCount);

                    }
                }
                

                //창고 슬롯이면
                if(!IsPlayerSlot)
                {
                    dragItem.transform.SetParent(this.transform);
                }


                GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<InventoryData>().WeightTextPrint(); //무게 값
                
            }

            /// <summary>
            /// 슬롯에 아이템 있을때
            /// 개수 및 무게 관리
            /// </summary>
            void WeightCheck()
            {
                BagItemData dragItem = IconDrag.draggingItem.GetComponent<BagItemData>(); //이동 중인 아이템
                BagItemData slotItem = GetComponentInChildren<BagItemData>(); //하위 오브젝트(슬롯에 있는 아이템)

                //같은 아이템이 슬롯에 존재하면
                if (dragItem._BagItem._Name.Equals(slotItem._BagItem._Name))
                {
                    if (IsPlayerSlot)
                    {
                        Characters.PlayerCtrl player = GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>();

                        //임시로 플레이어의 무게에 이동 중인 아이템을 인벤에 넣었을때 무게를 구한다(현재 무게 + (추가 무게 +개수))
                        float tempWeight = player.FWeight + (dragItem._BagItem._Weight * dragItem.NCount);

                        //최대 무게를 넘지 않을 경우
                        //아이템 전부를 슬롯에서 받는다
                        if (tempWeight <= player.FMaxWeight)
                        {
                            player.FWeight = tempWeight; //임시 저장된 무게를 적용 시킨다
                            slotItem.NCount += dragItem.NCount; //슬롯에 있는 아이템의 개수 증가
                            Destroy(dragItem.gameObject); //이동 중이였던 아이템 삭제
                        }

                        //최대 무게를 넘는 경우
                        //무게이 맞게 개수를 가져온다
                        else if (tempWeight > player.FMaxWeight)
                        {
                            int i = 0;
                            //이동중인 아이템의 개수만큼
                            for (i = 1; i <= dragItem.NCount; i++)
                            {
                                //무게를 계산해본다
                                tempWeight = player.FWeight + (dragItem._BagItem._Weight * i);

                                //최대 무게치를 넘을 경우
                                if (tempWeight > player.FMaxWeight)
                                {
                                    //한개를 다시 빼고 루프 종료
                                    i--;

                                    //한개를 뺸 개수로 다시 계산하여 무게를 측정
                                    tempWeight = player.FWeight + (dragItem._BagItem._Weight * i);

                                    break;
                                }
                            }

                            //슬롯에 넣을 개수를 이동 아이콘 개수를 뺸다 
                            dragItem.NCount -= i;
                            //이동 시킨 아이콘 개수를 슬롯 아이콘 개수에 증가
                            slotItem.NCount += i;


                            //최종적으로 적용 
                            player.FWeight = tempWeight;

                            dragItem.CountText();
                            //이동 중인 아이템의 개수에 따라 개수 표시 및 삭제
                            if (dragItem.NCount == 0)
                                Destroy(dragItem.gameObject);

                        }
                    }

                    //창고 슬롯에 아이콘 위에 놓을때
                    if (!IsPlayerSlot)
                    {
                        //슬롯에 있는 아이템 아이콘의 개수를 증가하고
                        //이동 중이던 아이콘을 삭제  (무게 감소는 아이콘을 집을때 처리)
                        
                        slotItem.NCount += dragItem.NCount;
                        slotItem.CountText();
                        
                        Destroy(dragItem.gameObject);
                    }


                    GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<InventoryData>().WeightTextPrint(); //무게 값

                    if (dragItem.NCount != 0)
                        dragItem.CountText(); //이동 중이였던 아이콤의 남은 개수

                    slotItem.CountText();
                }
            }


        }
    }
}