using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Black
{
    namespace Inventory
    {
        public class ItemDataClass : MonoBehaviour
        {
            [SerializeField, Header("아이템 아이콘")]
            Image iconSpr;
            private bool isIcon = true;

            [SerializeField, Header("남은 수량 출력")]
            protected Text count;
            int nCount = 0;

            /// <summary>
            /// 상점의 아이템인지 결정한다
            /// 아이템 판매 시
            /// 상점의 아이템을 팔면 안됨
            /// </summary>
            bool isStoreItem = false;

            public bool IsIcon { get => isIcon; set => isIcon = value; }
            public Image IconSpr { get => iconSpr; set => iconSpr = value; }
            public int NCount { get => nCount; set => nCount = value; }
            public bool IsStoreItem { get => isStoreItem; set => isStoreItem = value; }

            public void CountText()
            {
                count.text = nCount.ToString();
            }
        }

    }
}
