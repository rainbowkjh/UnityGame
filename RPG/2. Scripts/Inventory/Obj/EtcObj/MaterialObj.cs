using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 업그래이드 포인트 습득
/// 캐릭터와 무기 업그래이드
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class MaterialObj : EtcObjData
        {
            private void OnTriggerEnter(Collider other)
            {
                if (other.transform.tag.Equals("Player"))
                {
                    player.GetComponent<Characters.ItemLootPar>().ItemLoot(0);

                    player.NMaterial += Random.Range(MinValue, MaxValue);

                    Destroy(gameObject);
                }
            }
        }

    }
}
