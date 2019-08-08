using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Inventory
    {
        public class PartsObj : ItemObjData, IItemObj
        {
            PartsItem partsItem = new PartsItem();

            public PartsItem _PartsItem { get => partsItem; set => partsItem = value; }

            override protected void Start()
            {
                base.Start();
                DataInit();
            }

            private void OnTriggerEnter(Collider other)
            {
                if (other.tag.Equals("Player"))
                {
                    if (inven.PartsSlot(_PartsItem))
                    {
                        player.GetComponent<Characters.ItemLootPar>().ItemLoot(2);
                        Destroy(this.gameObject);
                    }
                }
            }


            public void DataInit()
            {
                for (int i = 0; i < ItemTable.PartsItemList.Count; i++)
                {
                    if (itemName.Equals(ItemTable.PartsItemList[i].name))
                    {
                        _PartsItem._Id = ItemTable.PartsItemList[i].id;
                        _PartsItem._Type = ItemTable.PartsItemList[i].type;
                        _PartsItem._Name = ItemTable.PartsItemList[i].name;
                        _PartsItem._NLevel = ItemTable.PartsItemList[i].nLevel;
                        _PartsItem._NMaxLevel = Random.Range(5, 30); //업그래이드 최대치는 랜덤

                        _PartsItem._IsExplosion = ItemTable.PartsItemList[i].isExplosion;
                        _PartsItem._FMinRage = ItemTable.PartsItemList[i].fMinRage;
                        _PartsItem._FMaxRage = ItemTable.PartsItemList[i].fMaxRage;
                        _PartsItem._FExplosionArea = Random.Range(_PartsItem._FMinRage, _PartsItem._FMaxRage);

                        _PartsItem._IsStun = ItemTable.PartsItemList[i].isStun;
                        _PartsItem._FStunMinPer = ItemTable.PartsItemList[i].fStunMinPer;
                        _PartsItem._FStunMaxPer = ItemTable.PartsItemList[i].fStunMaxPer;
                        _PartsItem._FStunPer = Random.Range(_PartsItem._FStunMinPer, _PartsItem._FStunMaxPer);
                            
                        _PartsItem._DmgMinUp = ItemTable.PartsItemList[i].dmgMinUp;
                        _PartsItem._DmgMaxUp = ItemTable.PartsItemList[i].dmgMaxUp;
                        _PartsItem._DmgUp = Random.Range(_PartsItem._DmgMinUp, _PartsItem._DmgMaxUp);

                        _PartsItem._AccUp = ItemTable.PartsItemList[i].accUp;
                        _PartsItem._Count = ItemTable.PartsItemList[i].count;
                        _PartsItem._Price = ItemTable.PartsItemList[i].price;
                        _PartsItem._Tip = ItemTable.PartsItemList[i].tip;
                        _PartsItem._Path = ItemTable.PartsItemList[i].path;

                        itemIconSpr.sprite = Resources.Load<Sprite>(_PartsItem._Path);
                    }
                }
            }


        }

    }
}
