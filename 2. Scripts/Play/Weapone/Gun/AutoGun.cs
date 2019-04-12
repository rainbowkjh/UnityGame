using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapone
{
    public class AutoGun : GunCtrl
    {
        
        protected override void Start()
        {
            base.Start();
        }

        
        void Update()
        {
            Fire();
            Reload();
        }
    }

}

