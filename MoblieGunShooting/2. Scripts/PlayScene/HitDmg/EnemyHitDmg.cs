using Black.Manager;
using Black.MovePosObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적이 비활성화 되면
/// 생존 수를 감소 시키는 코드가 추가 됨
/// 다른 부분은 같기 때문에
/// 상속을 받아 추가 시킴
/// 
/// </summary>
namespace Black
{
    namespace DmgManager
    {
        public class EnemyHitDmg : HitDmg
        {
            
            override public void HitDamage(float dmg)
            {
                charData.Hp -= dmg;

                if (charData.Hp <= 0 &&
                    charData.IsLive)
                {
                    charData.IsLive = false;
                    charData.Hp = 0;
                    //캐릭터 쓰러지는 애니메이션 추가

                    //Enemy 추가되는 내용==============
                    GameManager.INSTANCE.NEnemyCount--;
                 

                    StartCoroutine(EnemyDisable());
                }
            }


 
        }

    }
}
