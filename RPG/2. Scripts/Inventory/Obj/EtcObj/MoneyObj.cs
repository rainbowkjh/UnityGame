using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 돈 습득
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class MoneyObj : EtcObjData
        {
            private void OnTriggerEnter(Collider other)
            {
                if(other.transform.tag.Equals("Player"))
                {
                    player.GetComponent<ItemLootPar>().ItemLoot(0);

                    player.NMoney += Random.Range(MinValue, MaxValue);

                    Destroy(gameObject);
                }
            }
        }

    }
}
