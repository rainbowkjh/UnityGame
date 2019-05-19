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
/// ㅡㅡ 다시 생각해보니..
/// bool 값 하나 줘서 상황에 맞게 접근 시켜셔
/// 감소 시켰으면 되었다 ㅋㅋㅋ
/// 너무 멀리 생각 함;;;
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
                if (charData.Hp > 0)
                    charData.Hp -= dmg;                

                if (charData.Hp <= 0 &&
                    charData.IsLive)
                {
                    charData.Hp = 0;
                    charData.IsLive = false;
                    
                    //캐릭터 쓰러지는 애니메이션 추가
                    //EnemyCtrl에서 제어

                    //Enemy 추가되는 내용==============
                    GameManager.INSTANCE.NEnemyCount--;
                 

                    StartCoroutine(EnemyDisable());
                }
            }


 
        }

    }
}
