using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Inventory
    {
        public class SubItemObj : ItemObjData, IItemObj
        {
            SubItem subItem = new SubItem();

            public SubItem _SubItem { get => subItem; set => subItem = value; }

           
            override protected void Start()
            {
                base.Start();
                DataInit();
            }

            private void OnTriggerEnter(Collider other)
            {
                if (other.tag.Equals("Player"))
                {
                    if (inven.SubItemSlot(_SubItem))
                    {
                        player.GetComponent<Characters.ItemLootPar>().ItemLoot(3);
                        Destroy(this.gameObject);
                    }
                }
            }


            public void DataInit()
            {
                for (int i = 0; i < ItemTable.SubItemList.Count; i++)
                {
                    if (itemName.Equals(ItemTable.SubItemList[i].name))
                    {
                        _SubItem._Id = ItemTable.SubItemList[i].id;
                        _SubItem._Type = ItemTable.SubItemList[i].type;
                        _SubItem._ItemType = ItemTable.SubItemList[i].itemType;
                        _SubItem._Name = ItemTable.SubItemList[i].name;
                        _SubItem._MinDmg = ItemTable.SubItemList[i].minDmg;
                        _SubItem._MaxDmg = ItemTable.SubItemList[i].maxDmg;
                        _SubItem._Count = ItemTable.SubItemList[i].count;
                        _SubItem._Price = ItemTable.SubItemList[i].price;
                        _SubItem._Tip = ItemTable.SubItemList[i].tip;
                        _SubItem._Path = ItemTable.SubItemList[i].path;

                        itemIconSpr.sprite = Resources.Load<Sprite>(_SubItem._Path);
                    }
                }
            }


        }

    }
}
