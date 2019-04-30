using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class ZombieCtrl : EnemyCtrl
    {
        protected override void Awake()
        {
            base.Awake();
            PlayerTr = GameObject.Find("PlayerCtrl").GetComponent<Transform>();
        }

        private void Update()
        {
            if(CharactersData.IsLive)
            {
                EnemyMove();

                if (IsTargeting)
                    EnemyAttack();

                //특정 지역 도착 시 생성되는 적 말고
                //엑스트라 용으로 만든 적이 해당
                if(IsAttack)
                {
                    EnemyAttack();
                    transform.LookAt(PlayerTr);
                }
                    
            }
            
        }


    }

}
