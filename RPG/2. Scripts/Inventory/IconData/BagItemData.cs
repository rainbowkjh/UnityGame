using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Text;


/// <summary>
/// 회복 아이템 등
/// 가방 인벤에 들어가는 아이템의 아이콘
/// 아이템 오브젝트를 습득 시
/// 데이터를 전달 받아 적용 시킨다
/// </summary>
namespace Black
{
    namespace Inventory
    {
        [Serializable]
        public class BagItem
        {
            private int id;
            private eItemType type;
            private eBagItemType useType;
            private string name;
            private int value;
            private float weight;
            private int count;
            private int price;
            private string tip;
            private string path;

            public int _Id { get => id; set => id = value; }
            public eItemType _Type { get => type; set => type = value; }
            public string _Name { get => name; set => name = value; }
            public int _Value { get => value; set => this.value = value; }
            public float _Weight { get => weight; set => weight = value; }
            public int _Count { get => count; set => count = value; }
            public int _Price { get => price; set => price = value; }
            public string _Tip { get => tip; set => tip = value; }
            public string _Path { get => path; set => path = value; }
            public eBagItemType _UseType { get => useType; set => useType = value; }            
        }

        public class BagItemData : ItemDataClass, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
        {
            //public string itemName;

            BagItem _bagItem = new BagItem();

            public BagItem _BagItem { get => _bagItem; set => _bagItem = value; }
       
            ParsingData ItemTable;

            BagItemUse bagItemUse;

            [SerializeField,Header("아이템 정보 창")]
            GameObject itemInfoObj; 
            [SerializeField]
            Text itemInfo;

            private void Start()
            {
                GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
                bagItemUse = playerObj.GetComponent<BagItemUse>();

                itemInfoObj.SetActive(false); 
            }

            /// <summary>
            /// 해당 아이템 아이콘을 
            /// 마우스 오른쪽 클릭 시
            /// 아이템 사용(회복 아이템)
            /// </summary>
            /// <param name="eventData"></param>
            public void OnPointerClick(PointerEventData eventData)
            {
                if(eventData.button == PointerEventData.InputButton.Right)
                {
                    bagItemUse.UseItemApply(this);

                   //NCount--; //개수를 줄인다
                    CountText(); //수를 줄이고 표시를 동기화
                

                    if (NCount <= 0) //0이 되면 아이콘 삭제
                    {
                        NCount = 0;
                        Destroy(gameObject);
                    }
                        
                }
            }

            /// <summary>
            /// 아이콘에 마우스가 들어오면
            /// </summary>
            /// <param name="eventData"></param>
            public void OnPointerEnter(PointerEventData eventData)
            {
                itemInfoObj.SetActive(true);

                StringBuilder sb = new StringBuilder();
                sb.Append(_BagItem._Name);
                sb.Append("\n");

                sb.Append("Tip ");
                sb.Append(_BagItem._Tip);
                sb.Append(" ");
                sb.Append(_BagItem._Value);

                sb.Append("\n");
                sb.Append("Weight ");
                sb.Append(_BagItem._Weight * NCount);
                sb.Append("\n");

                sb.Append("Count : ");
                sb.Append(NCount);
                sb.Append("\n");

                sb.Append("Buy / Sell Price ");
                sb.Append(_BagItem._Price * NCount);
                sb.Append(" / ");
                sb.Append((_BagItem._Price * NCount) / 2);

                itemInfo.text = sb.ToString();

            }

            /// <summary>
            /// 아이콘에서 마우스가 나감
            /// </summary>
            /// <param name="eventData"></param>
            public void OnPointerExit(PointerEventData eventData)
            {
                itemInfoObj.SetActive(false);
            }

            #region 오브젝트를 통해서 아이템 아이콘이 생성되어야 하는경우
            //오브젝트를 통해서 아이템 아이콘이 생성되어야 하는경우
            //밑에 수정해서 사용
            //현재는 오브젝트를 통해서만 아이템을 얻을수 있다

            //private void Start()
            //{
            //    //Debug.Log(this.name + "Start");

            //    ItemTable = GameObject.Find("ParsingData").GetComponent<ParsingData>();

            //    //if (!isIcon)
            //    //{
            //    //    DataInit();
            //    //}

            //}

            //아이템 오브젝트에 생성되는 데이터
            //public void DataInit()
            //{                
            //    for (int i = 0; i < ItemTable.BagItemList.Count; i++)
            //    {
            //        if (itemName.Equals(ItemTable.BagItemList[i].name))
            //        {

            //            _BagItem._Id = ItemTable.BagItemList[i].id;
            //            _BagItem._Type = ItemTable.BagItemList[i].type;
            //            _BagItem._Name = ItemTable.BagItemList[i].name;
            //            _BagItem._Value = ItemTable.BagItemList[i].value;
            //            _BagItem._Count = ItemTable.BagItemList[i].count;
            //            _BagItem._Price = ItemTable.BagItemList[i].price;
            //            _BagItem._Tip = ItemTable.BagItemList[i].tip;
            //            _BagItem._Path = ItemTable.BagItemList[i].path;
            //        }
            //    }

            //    if (isIcon)
            //        iconSpr.sprite = Resources.Load<Sprite>(_BagItem._Path);
            //}

            #endregion

        }

    }
}
