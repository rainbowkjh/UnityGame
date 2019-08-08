using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 업그래이드 슬롯에 사용한다
/// (PartsUpgrade-> Slot)
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class IconDropEx : MonoBehaviour, IDropHandler
        {
            [SerializeField, Header("슬롯 타입")]
            eItemType slotType;

            [SerializeField, Header("업그레이드 정보")]
            Text itemInfo;

            private void LateUpdate()
            {
                if (transform.childCount == 0)
                {
                    itemInfo.text = ""; //슬롯이 비어 있으면 내용을 삭제
                }
            }

            public void OnDrop(PointerEventData eventData)
            {
                if (transform.childCount == 0)
                {
                    //슬롯과 아이템 타입이 같으면 슬롯에 장착
                    if (slotType == IconDrag.draggingItem.GetComponent<IconDrag>().ItemType)
                    {
                        IconDrag.draggingItem.transform.SetParent(this.transform);

                        PartsItemData data = this.transform.GetComponentInChildren<PartsItemData>();

                        ItemInfoPrint(data);
                    }
                        
                }
            }

            /// <summary>
            /// 아이템 정보
            /// </summary>
            /// <param name="data"></param>
            public void ItemInfoPrint(PartsItemData data)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("Name ");
                sb.Append(data._PartsItem._Name);
                sb.Append("\n");

                sb.Append("Level ");
                sb.Append(data._PartsItem._NLevel);
                sb.Append(" / ");
                sb.Append(data._PartsItem._NMaxLevel);
                sb.Append("\n");

                //폭발 속성
                sb.Append("Explosion Range ");
                sb.Append(data._PartsItem._FExplosionArea.ToString("N2"));
                if(data._PartsItem._IsExplosion) //폭발 속성이 있으면 업그래이드 수치를 보여준다
                {
                    sb.Append("<color=#ff0000>");
                    sb.Append("  +");                    
                    sb.Append(PartsUpgradeManager.EXPLOSIONRANGEUPGRADE);
                    sb.Append("</color>");
                }                
                sb.Append("\n");

                //기절 속성
                sb.Append("Stun Percent ");
                sb.Append(data._PartsItem._FStunPer.ToString("N2"));
                if(data._PartsItem._IsStun)
                {
                    sb.Append("<color=#ff0000>");
                    sb.Append("  +");
                    sb.Append(PartsUpgradeManager.STUNPERCENTUPGRADE);
                    sb.Append("</color>");
                }
                sb.Append("\n");

                //데미지
                sb.Append("Dmage Up ");
                sb.Append(data._PartsItem._DmgUp);
                sb.Append("<color=#ff0000>");
                sb.Append("  +");
                sb.Append(PartsUpgradeManager.DAMAGEUPGRADE);
                sb.Append("\n");
                sb.Append("</color>");

                //업그래이드 자원
                sb.Append("Parts UpgradePoint ");
                sb.Append(data._PartsItem._NLevel * PartsUpgradeManager.USEMATERAIL); //업그래이드 자원 XMl에 추가할것(수치 변경 시 한번에 해결)
                sb.Append("\n");

                sb.Append("Money ");
                sb.Append(data._PartsItem._NLevel * PartsUpgradeManager.USEMONEY);

                itemInfo.text = sb.ToString();
            }

        }

    }
}