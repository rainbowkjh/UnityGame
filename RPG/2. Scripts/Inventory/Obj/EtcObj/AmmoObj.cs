using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어가 접근하면
/// 플레이어의 탄을 증가 시킨다
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class AmmoObj : EtcObjData
        {

            //private void Start()
            //{
            //    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
            //}


            private void OnTriggerEnter(Collider other)
            {
                if(other.transform.tag.Equals("Player"))
                {

                    if(player.NAmmo < player.NMaxAmmo)
                    {
                        player.GetComponent<ItemLootPar>().ItemLoot(0);

                        int ranAmmo = Random.Range(MinValue, MaxValue);

                        player.NAmmo += ranAmmo;

                        if (player.NAmmo > player.NMaxAmmo)
                            player.NAmmo = player.NMaxAmmo;

                        Destroy(gameObject);
                    }
                }
            }
        }

    }
}
