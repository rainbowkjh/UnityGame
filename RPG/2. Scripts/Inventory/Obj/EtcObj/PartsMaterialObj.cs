using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 파츠 업그래이드 포인트
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class PartsMaterialObj : EtcObjData
        {
            private void OnTriggerEnter(Collider other)
            {
                if (other.transform.tag.Equals("Player"))
                {
                    player.GetComponent<Characters.ItemLootPar>().ItemLoot(0);

                    player.NPartsMaterial += Random.Range(MinValue, MaxValue);

                    Destroy(gameObject);
                }
            }
        }

    }
}
