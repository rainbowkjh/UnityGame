using Black.Characters;
using Black.DmgManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDZombieAttackColl : MonoBehaviour
{
    [SerializeField]
    EnemyCtrl enemyCtrl;

    bool isHit = false;

    /// <summary>
    /// 활성화 될때마다 초기화
    /// </summary>
    private void OnEnable()
    {
        isHit = false;
    }

    /// <summary>
    /// 데미지가 연달아서 들어가는 것을 막기 위해
    /// isHit 변수 사용 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (enemyCtrl.IsFire)
            {
                if(!isHit)
                {
                    isHit = true;

                    other.GetComponent<HitDmg>().HitDamage(enemyCtrl.Dmg());
                    PlayerCtrl player = other.GetComponent<PlayerCtrl>();

                    //HP UI
                    player.PlayerUI.CurHP(player.Hp, player.MaxHp);
                }
                
            }
        }
    }
}
