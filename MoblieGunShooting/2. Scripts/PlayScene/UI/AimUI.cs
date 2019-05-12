using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace UI
    {
        public class AimUI : MonoBehaviour
        {
            Animator ani;

            readonly int hashFire = Animator.StringToHash("Fire");
            readonly int hashMove = Animator.StringToHash("Move");

            private void Start()
            {
                ani = GetComponent<Animator>();   
            }

            public void FireAimAni()
            {
                ani.SetTrigger(hashFire);
            }

            public void MoveAimAni(bool state)
            {
                ani.SetBool(hashMove, state);
            }

        }

    }
}
