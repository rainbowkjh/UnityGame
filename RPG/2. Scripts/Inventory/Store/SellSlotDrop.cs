using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 아이템 상점에서
/// Sell 슬롯에 적용해준다
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class SellSlotDrop : MonoBehaviour,IDropHandler
        {
            Characters.PlayerCtrl playerCtrl;

            private void Start()
            {
                playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>();
            }

            /// <summary>
            /// 아이템 삭제 슬롯
            /// </summary>
            /// <param name="eventData"></param>
            public void OnDrop(PointerEventData eventData)
            {
               if(transform.childCount==0)
                {
                    //파츠 아이템 삭제 슬롯 일 경우
                    if (IconDrag.draggingItem.GetComponent<PartsItemData>())
                    {
                        //상점의 아이템이 아닌경우
                        if(!IconDrag.draggingItem.GetComponent<PartsItemData>().IsStoreItem)
                        {
                            IconDrag.draggingItem.transform.SetParent(this.transform);
                            
                            PartsItemData item = IconDrag.draggingItem.GetComponent<PartsItemData>();

                            playerCtrl.NMoney += (item._PartsItem._Price / 2);

                            //아이템을 삭제 한다
                            Destroy(item.gameObject);
                        }
                        
                    }
                    
                    if(IconDrag.draggingItem.GetComponent<SubItemData>())
                    {
                        if (!IconDrag.draggingItem.GetComponent<SubItemData>().IsStoreItem)
                        {
                            IconDrag.draggingItem.transform.SetParent(this.transform);

                            SubItemData item = IconDrag.draggingItem.GetComponent<SubItemData>();

                            playerCtrl.NMoney += (item._SubItem._Price / 2);

                            //아이템을 삭제 한다
                            Destroy(item.gameObject);
                        }
                        
                    }

                }
            }
        }

    }
}

