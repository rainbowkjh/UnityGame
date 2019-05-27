using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC Ai의 애니메이션을 관리
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class NpcAniCtrl : MonoBehaviour
        { 
            Animator ani;

            readonly int hashFire = Animator.StringToHash("Fire");
            readonly int hashReload = Animator.StringToHash("Reload");
            readonly int hashDead = Animator.StringToHash("Dead");
            readonly int hashWalk = Animator.StringToHash("Walk");
            readonly int hashRun = Animator.StringToHash("Run");
            readonly int hashReady = Animator.StringToHash("Ready");

            private void Start()
            {
                ani = GetComponent<Animator>();
            }

            public void FireAni()
            {
                ani.SetTrigger(hashFire);
            }

            public void ReloadAni()
            {
                ani.SetTrigger(hashReload);
            }

            public void Dead()
            {
                ani.SetTrigger(hashDead);
            }

            public void WalkAni(bool walk)
            {
                ani.SetBool(hashWalk, walk);
            }

            public void RunAni(bool run)
            {
                ani.SetBool(hashRun, run);
            }

            public void ReadyAni(bool ready)
            {
                ani.SetBool(hashReady, ready);
            }
        }

    }
}
