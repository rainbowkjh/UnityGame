using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Characters
    {
        
        public class EnemyCtrl : CharactersData
        {
            EnemyAniCtrl aniCtrl;

            void Start()
            {
                aniCtrl = GetComponent<EnemyAniCtrl>();
            }

            
            void Update()
            {

            }
        }

    }
}

