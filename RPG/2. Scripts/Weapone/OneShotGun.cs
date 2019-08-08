using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Weapone
    {

        public class OneShotGun : WeaponeData, IWeaponeCtrl
        {
            public void Fire()
            {
                if(Input.GetMouseButtonDown(1))
                {
                    aniCtrl.AniFire();
                }
            }

            public void Reload()
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    aniCtrl.AniReload();
                }
            }


            void Update()
            {
                Fire();
                Reload();
            }
        }

    }
}
