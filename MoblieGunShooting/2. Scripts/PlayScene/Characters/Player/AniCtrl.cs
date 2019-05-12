using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Black
{
    namespace Characters
    {
        public class AniCtrl : MonoBehaviour
        {
            Animator ani;

            readonly int hashWeaponeID = Animator.StringToHash("WeaponeID");
            readonly int hashFire = Animator.StringToHash("Fire");
            readonly int hashReload = Animator.StringToHash("Reload");

            private void Start()
            {
                ani = GetComponent<Animator>();

            }

            public void WeaponeChange(int id)
            {
                ani.SetInteger(hashWeaponeID, id);
            }

            public void FireAni()
            {
                ani.SetTrigger(hashFire);
            }

            public void ReloadAni()
            {
                ani.SetTrigger(hashReload);
            }
        }

    }
}

