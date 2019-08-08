using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Characters
    {
        [RequireComponent(typeof(Animator))]
        public class CharactersAniCtrl : MonoBehaviour
        {

            Animator ani;

            readonly int hashWeaponeID = Animator.StringToHash("WeaponeID");
            readonly int hashFire = Animator.StringToHash("Fire");
            readonly int hashReload = Animator.StringToHash("Reload");
            readonly int hashRun = Animator.StringToHash("Run");
            readonly int hashWalk = Animator.StringToHash("Walk");
            readonly int hashRoll = Animator.StringToHash("Roll");
            readonly int hashSkill1 = Animator.StringToHash("Skill1");
            readonly int hashSkill2 = Animator.StringToHash("Skill2");
            readonly int hashSkill3 = Animator.StringToHash("Skill3");
            readonly int hashAttack = Animator.StringToHash("Attack");
            readonly int hashGrenade = Animator.StringToHash("Grenade");
            readonly int hashHit = Animator.StringToHash("Hit");
            readonly int hashDown = Animator.StringToHash("Down");
            readonly int hashStun = Animator.StringToHash("Stun");
            readonly int hashChestOpen = Animator.StringToHash("ChestOpen");
            readonly int hashAttackID = Animator.StringToHash("AttackID");
          
            
            private void Start()
            {
                ani = GetComponent<Animator>();
            }


            public void AniWeaponeID(int id)
            {
                ani.SetInteger(hashWeaponeID, id);
            }

            public void AniFire()
            {
                ani.SetTrigger(hashFire);
            }

            public void AniReload()
            {
                ani.SetTrigger(hashReload);
            }

            public void AniRun(bool run)
            {
                ani.SetBool(hashRun, run);
            }

            public void AniWalk(bool walk)
            {
                ani.SetBool(hashWalk, walk);
            }

            public void AniRoll()
            {
                ani.SetTrigger(hashRoll);
            }

            public void AniSkill1()
            {
                ani.SetTrigger(hashSkill1);
            }

            public void AniSkill2()
            {
                ani.SetTrigger(hashSkill2);
            }

            public void AniSkill3()
            {
                ani.SetTrigger(hashSkill3);
            }

            public void AniAttack()
            {
                ani.SetTrigger(hashAttack);
            }

            public void AniGrenade()
            {
                ani.SetTrigger(hashGrenade);
            }

            public void AniHit()
            {
                ani.SetTrigger(hashHit);
            }

            public void AniDown()
            {
                ani.SetTrigger(hashDown);
            }

            public void AniStun(bool stun)
            {
                ani.SetBool(hashStun, stun);
            }

            public void AniChestOpen()
            {
                ani.SetTrigger(hashChestOpen);
            }

            public void AniAttackID(int atk)
            {
                ani.SetInteger(hashAttackID, atk);
            }

        }

    }
}
