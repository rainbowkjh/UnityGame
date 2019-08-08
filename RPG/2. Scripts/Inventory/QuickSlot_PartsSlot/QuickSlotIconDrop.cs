using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 총 무기 사용 할때 사용하는
/// 퀵 메뉴 슬롯
/// 회복 아이템 또는 수류탄 등 보조 무기 장착
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class QuickSlotIconDrop : MonoBehaviour, IDropHandler
        {            
            eItemType itemSlot;
            eItemType subItemSlot;

            Characters.PlayerCtrl player;
            InventoryData playerInven;

            void Start()
            {
                itemSlot = eItemType.Bag;
                subItemSlot = eItemType.Pouch2;

                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>();
                playerInven = GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<InventoryData>();
            }

            public void OnDrop(PointerEventData eventData)
            {
                if (transform.childCount == 0)
                {
                    if(itemSlot == IconDrag.draggingItem.GetComponent<IconDrag>().ItemType)
                    {
                        IconDrag.draggingItem.transform.SetParent(this.transform);

                        //소모 아이템인 경우 무게를 늘린다
                        BagItemData data = IconDrag.draggingItem.GetComponent<BagItemData>();
                        player.FWeight += (data.NCount * data._BagItem._Weight);
                        
                        playerInven.WeightTextPrint();
                    }

                    if(subItemSlot == IconDrag.draggingItem.GetComponent<IconDrag>().ItemType)
                    {
                        IconDrag.draggingItem.transform.SetParent(this.transform);
                    }
                }
            }
        }

    }
}

