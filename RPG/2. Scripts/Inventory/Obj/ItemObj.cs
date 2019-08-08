using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  회복 아이템 종류 오브젝트
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class ItemObj : ItemObjData, IItemObj
        {
          
            BagItem _bagItem = new BagItem();

            public BagItem _BagItem { get => _bagItem; set => _bagItem = value; }

            override protected void Start()
            {
                base.Start();
                DataInit();
            }

            private void OnTriggerEnter(Collider other)
            {
                if(other.tag.Equals("Player"))
                {
                    if(inven.BagSlot(_BagItem))
                    {
                        player.GetComponent<Characters.ItemLootPar>().ItemLoot(1);

                        Destroy(this.gameObject);
                    }
                }
            }
            

            //아이템 오브젝트에 생성되는 데이터
            public void DataInit()
            {
                for (int i = 0; i < ItemTable.BagItemList.Count; i++)
                {
                    if (itemName.Equals(ItemTable.BagItemList[i].name))
                    {

                        _BagItem._Id = ItemTable.BagItemList[i].id;
                        _BagItem._Type = ItemTable.BagItemList[i].type;
                        _BagItem._UseType = ItemTable.BagItemList[i].useType;
                        _BagItem._Name = ItemTable.BagItemList[i].name;
                        _BagItem._Value = ItemTable.BagItemList[i].value;
                        _BagItem._Weight = ItemTable.BagItemList[i].weight;
                        _BagItem._Count = ItemTable.BagItemList[i].count;
                        _BagItem._Price = ItemTable.BagItemList[i].price;
                        _BagItem._Tip = ItemTable.BagItemList[i].tip;
                        _BagItem._Path = ItemTable.BagItemList[i].path;


                        itemIconSpr.sprite = Resources.Load<Sprite>(_BagItem._Path);
                    }
                }

            }

        }

    }
}
