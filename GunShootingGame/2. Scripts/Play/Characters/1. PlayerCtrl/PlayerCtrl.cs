using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class PlayerCtrl : CharactersInfo
    {

        private Transform nextPos;
        WeaponeCtrl weaponeCtrl;

        /// <summary>
        /// 리로드, 무기 교체 중 행동 제어
        /// </summary>
        bool isAction = false;

        public float fireRate = 0.01f;
        private float fireTime = 0.0f;


        #region Set,Get
        public Transform NextPos
        {
            get
            {
                return nextPos;
            }

            set
            {
                nextPos = value;
            }
        }
        #endregion

        protected override void Awake()
        {
            base.Awake();
            weaponeCtrl = GetComponent<WeaponeCtrl>();
            
        }

       
        void Update()
        {
            if (CharactersData.IsLive)
            {
                PlayerMove();
                Fire();
                Reload();

            }
            
           // WeaponeChangeInput();

        }


        /// <summary>
        /// 캐릭터 이동 제어
        /// </summary>
        void PlayerMove()
        {
            //다음 이동 위치가 있고 달리기 상태가 되면 이동
            if (NextPos != null && CharactersData.EState == CharState.Move)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, NextPos.position,
                CharactersData.FSpeed * Time.deltaTime);
            }

            //애니메이션
            if(CharactersData.EState == CharState.Move)
            {
                MoveAni(true);
            }
            else
            {
                MoveAni(false);
            }

        }

        /// <summary>
        /// 공격 입력
        /// </summary>
        void Fire()
        {
            if (Input.GetMouseButton(0) && !isAction)
            {
                if (weaponeCtrl.GetComponentInChildren<GunData>().Mag > 0)
                {
                    if (Time.time > fireTime)
                    {
                        fireTime = Time.time + fireRate + Random.Range(0.1f, 0.3f);

                        FireAni();
                        weaponeCtrl.GunFire();
                    }

                }
            }
                
        }

        /// <summary>
        /// 재장전 입력
        /// </summary>
        void Reload()
        {
            if (Input.GetMouseButtonDown(1) && !isAction)
            {
                ReloadAni();
                weaponeCtrl.GunReload();

                StartCoroutine(isActionDelay(2.5f));
                
            }            
        }

        /// <summary>
        /// 무기 교체 입력
        /// </summary>
        void WeaponeChangeInput()
        {

            if (Input.GetKeyDown(KeyCode.Alpha1) && !isAction)
            {
                weaponeCtrl.UseWeapone = 0;
                StartCoroutine(isActionDelay(1.0f));
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && !isAction)
            {
                weaponeCtrl.UseWeapone = 1;
                StartCoroutine(isActionDelay(1.0f));
            }

            weaponeCtrl.WeaponeChange();
            WeaponeSwapAni(weaponeCtrl.UseWeapone);
            
        }

        /// <summary>
        /// 행동 중 딜레이
        /// </summary>
        /// <returns></returns>
        IEnumerator isActionDelay(float delay)
        {
            isAction = true;
            yield return new WaitForSeconds(delay);
            isAction = false;
        }

    }

}
