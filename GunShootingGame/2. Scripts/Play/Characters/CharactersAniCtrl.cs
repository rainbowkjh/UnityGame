using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class CharactersAniCtrl : MonoBehaviour
    {
        protected Animator Ani;

        readonly int hashWeaponeState = Animator.StringToHash("WeaponeState");
        readonly int hashMove = Animator.StringToHash("Move");
        readonly int hashFire = Animator.StringToHash("Attack");
        readonly int hashReload = Animator.StringToHash("Reload");
        readonly int hashDown = Animator.StringToHash("Down");
        readonly int hashHit = Animator.StringToHash("Hit");


        protected void WeaponeSwapAni(int index)
        {
            Ani.SetInteger(hashWeaponeState, index);
        }

        protected void MoveAni(bool move)
        {
            Ani.SetBool(hashMove, move);
        }

        public void FireAni()
        {
            Ani.SetTrigger(hashFire);
        }

        public void ReloadAni()
        {
            Ani.SetTrigger(hashReload);
        }

        public void DownAni()
        {
            Ani.SetTrigger(hashDown);
        }

        public void HitAni()
        {
            Ani.SetTrigger(hashHit);
        }

    }


}
