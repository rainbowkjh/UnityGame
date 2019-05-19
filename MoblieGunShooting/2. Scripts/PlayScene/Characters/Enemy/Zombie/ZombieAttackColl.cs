using Black.Characters;
using Black.DmgManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackColl : MonoBehaviour
{
    [SerializeField]
    EnemyCtrl enemyCtrl; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            if(enemyCtrl.IsFire)
            {
                other.GetComponent<HitDmg>().HitDamage(enemyCtrl.Dmg());
                PlayerCtrl player = other.GetComponent<PlayerCtrl>();
                
                //HP UI
                player.PlayerUI.CurHP(player.Hp, player.MaxHp);
            }
        }
    }
}
