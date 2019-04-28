using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapone
{
    public class OneShotGun : GunCtrl
    {
        protected override void Start()
        {
            base.Start();
        }


        void Update()
        {
            if(player.State != Characters.CharState.Dead
                && !player.IsRoll
                && !GameManager.INSTANCE.gameSystem.isPause)
            {
                Fire();
                Reload();
            }            
        }
    }

}
