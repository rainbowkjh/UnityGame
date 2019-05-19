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

            readonly int hashRun = Animator.StringToHash("Run");
            readonly int hashWalk = Animator.StringToHash("Walk");
            readonly int hashAttack = Animator.StringToHash("Attack");
            readonly int hashDead = Animator.StringToHash("Dead");
            readonly int hashEating = Animator.StringToHash("Eating");

            private void Start()
            {
                ani = GetComponent<Animator>();
            }

            public void RunAni(bool run)
            {
                ani.SetBool(hashRun, run);
            }

            public void WalkAni(bool walk)
            {
                ani.SetBool(hashWalk, walk);
            }

            public void AttackAni()
            {
                ani.SetTrigger(hashAttack);
            }

            public void DeadAni()
            {
                ani.SetTrigger(hashDead);
            }

            public void EatingAni(bool eat)
            {
                ani.SetBool(hashEating, eat);
            }
        }

    }
}
