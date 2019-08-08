using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 아이템 아이콘
/// (파싱에서 데이터를 적용 시킬때
/// 아이템의 타입을 결정해야
/// 인벤에 제대로 장착이 된다)
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class IconDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
        {
            private Transform itemTr;
            private Transform inventoryTr;
            public static GameObject draggingItem = null;
            private CanvasGroup cg;

            AudioSource _audio;
            [SerializeField, Header("0 아이콘 클릭, 1 놓을때")]
            AudioClip[] _sfx;

            //아이템 타입
            //[SerializeField, Header("아이템 타입")]
            eItemType itemType;
            //타입에 맞는 슬롯에 넣지 못할때 원위치
            Transform tempTr;

            public eItemType ItemType { get => itemType; set => itemType = value; }
            public bool CharWeaponeSlotEquip { get => charWeaponeSlotEquip; set => charWeaponeSlotEquip = value; }
            public bool GunWeaponeSlotEquip { get => gunWeaponeSlotEquip; set => gunWeaponeSlotEquip = value; }
            public Transform TempTr { get => tempTr; set => tempTr = value; }


            //파트 장착 슬롯에서 해제 시 데이터 값을 돌려 놓기 위해
            //어느 파트에 장착되었는지 확인
            bool charWeaponeSlotEquip = false; 
            bool gunWeaponeSlotEquip = false;

            #region 무게가 있는 아이템 코드
            bool playerWeight = false;
            bool weightItmeMove = false;
            #endregion

            #region 상점 아이템일 경우
            Characters.PlayerCtrl player;
            bool isItemBuy = false; //아이템을 살수 있는지 확인
            int tempValue = 0; //아이템 구매 임시 값 저장(구매 실패 시 되돌리기 위해)

            #endregion


            private void Start()
            {
                _audio = GetComponent<AudioSource>();

                itemTr = GetComponent<Transform>();

                player =  GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>();
                inventoryTr = GameObject.Find("GameCanvas/Inventory").GetComponent<Transform>();

                cg = GetComponent<CanvasGroup>();
                TypeSetting();
            }

            void TypeSetting()
            {
                if (GetComponent<BagItemData>() != null)
                {
                    itemType = eItemType.Bag;
                }

                if(GetComponent<PartsItemData>() != null)
                {
                    itemType = eItemType.Pouch1;
                }

                if (GetComponent<SubItemData>() != null)
                {
                    itemType = eItemType.Pouch2;
                }
            }

            /// <summary>
            /// 아이템 드래그할때
            /// </summary>
            /// <param name="eventData"></param>
            public void OnDrag(PointerEventData eventData)
            {
                //소지금 부족으로 널값이 되지 않았을때만 드래그
                if(draggingItem !=null)
                {

                    //아이템의 위치는 마우스를 따라간다
                    itemTr.position = Input.mousePosition;

                    //아이콘을 해제하기 위해 클릭 시
                    //능력치를 감소 시킨다
                    if (charWeaponeSlotEquip)
                    {
                        PartsItemData patrs = gameObject.GetComponent<PartsItemData>();
                        CharWeaponeDrop itemData = TempTr.GetComponent<CharWeaponeDrop>();
                        //데이터 적용
                        itemData.ItemDataSyn(patrs._PartsItem._DmgUp, patrs._PartsItem._IsExplosion, patrs._PartsItem._IsStun,
                            patrs._PartsItem._FExplosionArea, patrs._PartsItem._FStunPer);

                        charWeaponeSlotEquip = false;
                    }

                    if (gunWeaponeSlotEquip)
                    {
                        PartsItemData patrs = gameObject.GetComponent<PartsItemData>();
                        GunWeaponeDrop itemData = TempTr.GetComponent<GunWeaponeDrop>();
                        //데이터 적용
                        itemData.ItemDataSyn(patrs._PartsItem._DmgUp, patrs._PartsItem._IsExplosion, patrs._PartsItem._IsStun,
                            patrs._PartsItem._FExplosionArea, patrs._PartsItem._FStunPer);

                        gunWeaponeSlotEquip = false;
                    }

                    //소모 아이템인 경우 플레이어의 가방 무게를 줄인다
                    if (playerWeight)
                    {
                        //슬롯이 플레이어의 슬롯이면 무게를 줄인다
                        if (TempTr.GetComponent<IconDrop>())
                        {
                            if (TempTr.GetComponent<IconDrop>().IsPlayerSlot)
                            {
                                BagItemData data = draggingItem.GetComponent<BagItemData>();

                                //아이템의 무게에 개수 만큼 감소 시킨다
                                //GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>().FWeight -=
                                //(data._BagItem._Weight * data.NCount);

                                player.FWeight -= (data._BagItem._Weight * data.NCount);

                                GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<InventoryData>().WeightTextPrint();
                                weightItmeMove = true; //무게가 있는 아이템 이동 상태
                            }
                        }

                        else if(TempTr.GetComponent<QuickSlotIconDrop>())
                        {
                            BagItemData data = draggingItem.GetComponent<BagItemData>();

                            //아이템의 무게에 개수 만큼 감소 시킨다
                            //GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>().FWeight -=
                            //(data._BagItem._Weight * data.NCount);

                            player.FWeight -= (data._BagItem._Weight * data.NCount);

                            GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<InventoryData>().WeightTextPrint();
                            weightItmeMove = true; //무게가 있는 아이템 이동 상태
                        }

                        playerWeight = false; //무게가 연속으로 줄이는 것을 방지하기 위해 바로 변경

                    }
                }

            }

            /// <summary>
            /// 아이템 클릭
            /// </summary>
            /// <param name="eventData"></param>
            public void OnBeginDrag(PointerEventData eventData)
            {
                
                //상점 아이템이면
                if (gameObject.GetComponent<ItemDataClass>().IsStoreItem)
                {
                    //소지금이 적게 있으면 드래그를 못하게 한다
                    if (gameObject.GetComponent<BagItemData>())
                    {
                        BagItemData data = gameObject.GetComponent<BagItemData>();

                        if (player.NMoney < data._BagItem._Price)
                        {
                            draggingItem = null;
                            isItemBuy = false;
                        }

                        else
                        {
                            player.NMoney -= data._BagItem._Price;

                            tempValue = data._BagItem._Price; //아이템 가격을 임시 저장
                            isItemBuy = true;
                        }
                    }
                    else if (gameObject.GetComponent<PartsItemData>())
                    {
                        PartsItemData data = gameObject.GetComponent<PartsItemData>();

                        if (player.NMoney < data._PartsItem._Price)
                        {
                            draggingItem = null;
                            isItemBuy = false;
                        }

                        else
                        {
                            player.NMoney -= data._PartsItem._Price;

                            tempValue = data._PartsItem._Price; //아이템 가격을 임시 저장
                            isItemBuy = true;
                        }
                    }
                    else if (gameObject.GetComponent<SubItemData>())
                    {
                        SubItemData data = gameObject.GetComponent<SubItemData>();

                        if (player.NMoney < data._SubItem._Price)
                        {
                            draggingItem = null;
                            isItemBuy = false;
                        }

                        else
                        {
                            player.NMoney -= data._SubItem._Price;

                            tempValue = data._SubItem._Price; //아이템 가격을 임시 저장
                            isItemBuy = true;
                        }
                    }
                }

                //상점 아이템이 아니거나 아이템을 구매 할수 있을때
                if (!gameObject.GetComponent<ItemDataClass>().IsStoreItem
                    || isItemBuy)
                {
                    GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                    //임시로 위치를 저장한다
                    TempTr = this.transform.parent.GetComponent<Transform>();

                    this.transform.SetParent(inventoryTr);
                    draggingItem = this.gameObject;

                    //소모 아이템의 경우 무게를 줄이기 위해
                    if (draggingItem.GetComponent<BagItemData>())
                    {
                        playerWeight = true;
                    }

                    cg.blocksRaycasts = false;

                }


            }

            /// <summary>
            /// 아이템 클릭 해제
            /// </summary>
            /// <param name="eventData"></param>
            public void OnEndDrag(PointerEventData eventData)
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[1]);

                draggingItem = null;

                cg.blocksRaycasts = true;

              
                //아이템의 상위 오브젝트가 인벤토리이면(슬롯에 장착 안됨)
                if(itemTr.parent == inventoryTr)
                {
                    //원래 위치로 돌려 놓은다
                    itemTr.SetParent(TempTr);

                    //원래 위치로 돌아 갈때
                    //원래 위치의 데이터를 복귀 시킨다
                    //해당 데이터를 확인
                    if(TempTr.GetComponent<CharWeaponeDrop>())
                    {
                        PartsItemData patrs = gameObject.GetComponent<PartsItemData>();
                        CharWeaponeDrop itemData = TempTr.GetComponent<CharWeaponeDrop>();
                        //데이터 적용 상태로 다시 변경(아이콘 이동 실패)
                        itemData.ItemDataSynRe(patrs._PartsItem._DmgUp, patrs._PartsItem._IsExplosion, patrs._PartsItem._IsStun,
                            patrs._PartsItem._FExplosionArea,patrs._PartsItem._FStunPer);
                        charWeaponeSlotEquip = true;
                    }

                    else if(TempTr.GetComponent<GunWeaponeDrop>())
                    {
                        PartsItemData patrs = gameObject.GetComponent<PartsItemData>();
                        GunWeaponeDrop itemData = TempTr.GetComponent<GunWeaponeDrop>();
                        itemData.ItemDataSynRe(patrs._PartsItem._DmgUp, patrs._PartsItem._IsExplosion, patrs._PartsItem._IsStun,
                             patrs._PartsItem._FExplosionArea, patrs._PartsItem._FStunPer);
                        gunWeaponeSlotEquip = true;
                    }

                    //무게 다시 늘리기(아이템 이동 실패)
                    //마우스를 놓았는데
                    //무게가 있는 아이템인 경우
                    //다시 무게를 늘린다
                    if(weightItmeMove) 
                    {
                        BagItemData data = GetComponent<BagItemData>();

                        //  GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>().FWeight +=
                        //(data._BagItem._Weight * data.NCount); //개수의 곱만큼 무게를 증가

                        player.FWeight += (data._BagItem._Weight * data.NCount); //개수의 곱만큼 무게를 증가

                        GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<InventoryData>().WeightTextPrint();

                        weightItmeMove = false;
                    }

                    //구매 실패 시 임시 저장된 가격으로
                    //다시 소지금을 올려준다
                    if(isItemBuy)
                    {
                        player.NMoney += tempValue;
                    }
                }

             
                //마지막에는 무저건 flase로 변경(소몽 아이템 이동 관련 코드)
                if (playerWeight)
                    playerWeight = false;
                if (weightItmeMove) //이동 상태 해제
                    weightItmeMove = false;

                //임시 위치(트랜스폼)을 해제
                TempTr = null;
            }

        }



    }
}
