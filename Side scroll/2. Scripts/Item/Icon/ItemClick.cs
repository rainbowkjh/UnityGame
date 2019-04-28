using MainScene.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 아이콘을 클릭할떄 호출되는 클래스
/// </summary>
namespace _Item
{
    public class ItemClick : MonoBehaviour
    {
        /// <summary>
        /// 아이템의 정보를 확인하여
        /// 해당하는 정보창을 활성화 시킨다
        /// </summary>
        ItemData m_itemData;

        public static GameObject ITEMICON;

        #region Set,Get
        public ItemData ItemDataValue
        {
            get
            {
                return m_itemData;
            }

            set
            {
                m_itemData = value;
            }
        }
        #endregion

        private void Start()
        {
            ItemDataValue = GetComponent<ItemData>();
        }

        public void ItemIconClick()
        {
            //전에 클릭해서 연결 되었던 오브젝트를 해제
            if (ItemClick.ITEMICON != null)
                ItemClick.ITEMICON = null;

            if (ItemDataValue.ItemType == Weapone.ItemType.Weapone)
            {
                GameObject.Find("Canvas/Setting/WeaponeBtnManager")
                    .GetComponent<WeaponeSettingBtn>().WeaponeIconClick(ItemDataValue.WeaponeData);
            }

            if (ItemDataValue.ItemType == Weapone.ItemType.Item)
            {

            }

            //클릭한 오브젝트(삭제할때 필요)
            ItemClick.ITEMICON = this.gameObject;
        }
    }
}

