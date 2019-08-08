using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 회복 아이템 상점에
/// 어아탬울 사고 파는 역할을 하는
/// 슬롯에 적용해주는 스크립트
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class BagStoreItemDrop : MonoBehaviour, IDropHandler
        {

            [SerializeField, Header("슬롯 타입")]
            Manager.eItemType slotType;

            [SerializeField, Header("아이템 판매 시 가겨을 보여준다/ 구매는 BagItemStoreINven에서..")]
            Text priceText;

            [SerializeField]
            BagItemStoreInven bagStore;

            Characters.PlayerCtrl player;

            private void Start()
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>();
            }

            /// <summary>
            /// 아이템 판매 할때
            /// 슬롯에 직접 놓는다
            /// </summary>
            /// <param name="eventData"></param>
            public void OnDrop(PointerEventData eventData)
            {
                if (slotType == IconDrag.draggingItem.GetComponent<IconDrag>().ItemType)
                {

                    if (transform.childCount == 0 && !bagStore.IsBuy)
                    {
                        //드래그 중인 아이템의 상위 오브젝트로 잡는다
                        //슬롯에 아이템 장착
                        //IconDrag.draggingItem.transform.SetParent(this.transform);

                        BagItemData data = IconDrag.draggingItem.GetComponent<BagItemData>();

                        data.transform.SetParent(this.transform);

                        //아이템의 개수를 임시 저장 시킨다
                        //  data._BagItem._Count 이것은 원래
                        //파싱 데이터 가져올때 무조건 1이여야 하는 값이지만
                        //판매 시 판매전 수량을 알아야 하기 때문에
                        //이곳에 임시 저장 후
                        //판매가 끝나면 다시 1로 만들어 준다
                        data._BagItem._Count = data.NCount;

                        priceText.text = ((data.NCount * data._BagItem._Price) / Manager.GameManager.INSTANCE.SellPrice).ToString("N0");

                    }
                }

            }


        }
        //class End
    }
}
