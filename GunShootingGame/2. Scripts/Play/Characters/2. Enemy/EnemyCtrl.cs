using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class EnemyCtrl : CharactersInfo
    {
        Transform nextPos;

        bool isAction = false;

        /// <summary>
        /// 공격 가능 상태
        /// </summary>
        bool isTargeting = false;
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

        public bool IsTargeting
        {
            get
            {
                return isTargeting;
            }

            set
            {
                isTargeting = value;
            }
        }

        public bool IsAttack
        {
            get
            {
                return isAttack;
            }

            set
            {
                isAttack = value;
            }
        }

        public Transform PlayerTr
        {
            get
            {
                return playerTr;
            }

            set
            {
                playerTr = value;
            }
        }
        #endregion

        [SerializeField, Header("제자리에서 공격한다(엑스트라 적 캐릭터??)")]
        private bool isAttack = false;
        [SerializeField, Header("제지리 공격 적이 플레이어를 바라본다")]
        Transform playerTr;

        [SerializeField, Header("Ray Pos")]
        Transform rayTr;

        virtual public void EnemyAttack()
        {
            if (CharactersData.EState == CharState.Idle && !isAction)
            {
                //Debug.Log("공격!");
                StartCoroutine(ActionDelay(Random.Range(2.0f, 5.0f)));
            }
        }

        IEnumerator ActionDelay(float delay)
        {
            //Debug.Log("공격 애니 실행");
            isAction = true;
            FireAni();

            yield return new WaitForSeconds(delay);
            isAction = false;
        }

        virtual protected void EnemyMove()
        {
            //다음 이동 위치가 있고 달리기 상태가 되면 이동
            if (NextPos != null && CharactersData.EState == CharState.Move)
            {

                this.transform.position = Vector3.MoveTowards(transform.position, NextPos.position,
                    CharactersData.FSpeed * Time.deltaTime);
            }

            //애니메이션
            if (CharactersData.EState == CharState.Move)
            {
                MoveAni(true);
            }
            else
            {
                MoveAni(false);
            }
        }


    

    }

}
