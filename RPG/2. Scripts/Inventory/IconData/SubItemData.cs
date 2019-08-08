using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Text;

namespace Black
{
    namespace Inventory
    {
        [Serializable]
        public class SubItem
        {
            private int id;
            private eItemType type;
            private eSubItemType itemType;
            private string name;
            private int minDmg;
            private int maxDmg;
            private int count;
            private int price;
            private string tip;
            private string path;

            public int _Id { get => id; set => id = value; }
            public eItemType _Type { get => type; set => type = value; }
            public string _Name { get => name; set => name = value; }
            public int _MinDmg { get => minDmg; set => minDmg = value; }
            public int _MaxDmg { get => maxDmg; set => maxDmg = value; }
            public int _Count { get => count; set => count = value; }
            public int _Price { get => price; set => price = value; }
            public string _Tip { get => tip; set => tip = value; }
            public string _Path { get => path; set => path = value; }
            public eSubItemType _ItemType { get => itemType; set => itemType = value; }
            
        }

        public class SubItemData : ItemDataClass, IPointerEnterHandler, IPointerExitHandler
        {
            SubItem _subItem = new SubItem();
             
            ParsingData ItemTable;

            [SerializeField, Header("아이템 정보 창")]
            GameObject itemInfoObj;
            [SerializeField]
            Text itemInfo;


            public SubItem _SubItem { get => _subItem; set => _subItem = value; }

            private void Start()
            {
                itemInfoObj.SetActive(false);
            }

            public void OnPointerEnter(PointerEventData eventData)
            {
                itemInfoObj.SetActive(true);

                StringBuilder sb = new StringBuilder();
                sb.Append(_SubItem._Name);
                sb.Append("\n");

                sb.Append("Tip ");
                sb.Append(_SubItem._Tip);
                sb.Append("\nDmage ");
                sb.Append(_SubItem._MinDmg); //최소 데미지
                sb.Append(" / ");
                sb.Append(_SubItem._MaxDmg);
                
                sb.Append("\nBuy / Sell Price ");
                sb.Append(_SubItem._Price);
                sb.Append(" / ");
                sb.Append(_SubItem._Price / 2);

                itemInfo.text = sb.ToString();

            }

            public void OnPointerExit(PointerEventData eventData)
            {
                itemInfoObj.SetActive(false);
            }

            //private void Start()
            //{
            //    ItemTable = GameObject.Find("ParsingData").GetComponent<ParsingData>();
            //}


        }

    }
}
