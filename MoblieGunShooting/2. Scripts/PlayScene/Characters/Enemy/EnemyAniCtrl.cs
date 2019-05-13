using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Characters
    {
        [RequireComponent(typeof(Animator))]
        public class EnemyAniCtrl : MonoBehaviour
        {
            Animator ani;

            private void Start()
            {
                ani = GetComponent<Animator>();
            }
        }

    }
}
