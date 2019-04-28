using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Characters
{
    public class AniCtrl : CharactersData
    {
        Animator ani;

        protected readonly int hashLive = Animator.StringToHash("Live");
        protected readonly int hashDown = Animator.StringToHash("Down");
        protected readonly int hashWalk = Animator.StringToHash("Walk");
        protected readonly int hashRun = Animator.StringToHash("Run");
        protected readonly int hashCrouch = Animator.StringToHash("Crouch");
        protected readonly int hashFire = Animator.StringToHash("Fire");
        protected readonly int hashReload = Animator.StringToHash("Reload");
        protected readonly int hashGround = Animator.StringToHash("Ground");
        protected readonly int hashJump = Animator.StringToHash("Jump");
        protected readonly int hashDrop = Animator.StringToHash("Drop");
        protected readonly int hashRoll = Animator.StringToHash("Roll");


        protected virtual void Start()
        {
            ani = GetComponent<Animator>();
        }

        public void LiveAni(bool islive)
        {
            ani.SetBool(hashLive, islive);
        }

        public void DownAni()
        {
            ani.SetTrigger(hashDown);
        }

        protected void WalkAni(bool isWalk)
        {
            ani.SetBool(hashWalk, isWalk);
        }

        protected void RunAni(bool isRun)
        {
            ani.SetBool(hashRun, isRun);
        }

        protected void CrouchAni(bool isCrouch)
        {
            ani.SetBool(hashCrouch, isCrouch);
        }

        public void FireAni()
        {
            ani.SetTrigger(hashFire);
        }

        public void ReloadAni()
        {
            ani.SetTrigger(hashReload);
        }

        public void GroundAni(bool isGround)
        {
            ani.SetBool(hashGround, isGround);
        }

        public void JumpTrigger()
        {
            ani.SetTrigger(hashJump);
        }

        /// <summary>
        /// 점프 안뛰고 공중에 있을떄
        /// </summary>
        public void DropAni()
        {
            ani.SetTrigger(hashDrop);
        }

        public void RollAni()
        {
            ani.SetTrigger(hashRoll);
        }

    }

}