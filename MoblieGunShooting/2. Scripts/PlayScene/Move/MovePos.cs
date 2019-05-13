
using Black.Characters;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// moveSpeed는 캐릭터를 다음 위치로 보낼때 
/// 이동 속도 값을 변경 시킨다(이벤트 연출 상황에 맞게 조절 - 달리는 구간, 걷는 구간 등)
/// 
/// 적을 생성 시킬 위치의 오브젝트(Transform)을 만들고
/// EnemyCreate를 적용 시킨다
/// 이 오브젝트를 enemyArea에 넣어주면 생성 위치가 지정된다
/// </summary>

namespace Black
{
    namespace MovePosObj
    {
        /// <summary>
        /// 플레이어의 다음 이동 위치를 지정
        /// 이동 위치가 될 트랜스폼에 적용 시킨다
        /// </summary>
        public class MovePos : MonoBehaviour
        {
            [SerializeField]
            private Transform nextPos = null;

            [SerializeField, Header("Player의 이동 경로인지 구분")]
            bool isPlayerPos = false;

            [SerializeField, Header("도착하면 이벤트 발생 상태(정지)")]
            bool isEvent = true;

            /// <summary>
            /// 콜라이더에 도착했는지 확인
            /// </summary>
            bool isEnter = false;

            [SerializeField, Header("캐릭터 이동속도를 변경 시킨다")]
            float moveSpeed = 5;

            [SerializeField, Header("적 생성 지역 오브젝트")]
            GameObject enemyArea =null;

            EnemyCreate[] enemyCreate;


            private void Start()
            {
                //생성 시킬 적이 있으면
                if (enemyArea != null)
                {
                    enemyCreate = enemyArea.GetComponentsInChildren<EnemyCreate>();
                    GameManager.INSTANCE.NEnemyCount = enemyCreate.Length; //생성된 적의 수를 담는다
                }
                    
            }


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

            public bool IsPlayerPos
            {
                get
                {
                    return isPlayerPos;
                }

                set
                {
                    isPlayerPos = value;
                }
            }

            public bool IsEvent
            {
                get
                {
                    return isEvent;
                }

                set
                {
                    isEvent = value;
                }
            }
            #endregion

            private void OnTriggerStay(Collider other)
            {
                if (other.transform.tag.Equals("Player"))
                {
                    //이벤트 발생 지점일 경우 정지
                    if (isEvent)
                    {
                        StartCoroutine(CharStop(other));

                        if (GameManager.INSTANCE.NEnemyCount == 0)
                            isEvent = false;

                    }

                    if (!isEvent)
                    {
                        if (nextPos != null)
                            StartCoroutine(CharMove(other));
                    }

                }

            }


            /// <summary>
            /// 콜라이더에 도착하면 정지 시킴
            /// (바로 정지 시키면 원하는 지점보다 전에 정지
            /// 하기 때문에 약간의 시간을 주고 정지 시킴)
            /// </summary>
            /// <param name="coll"></param>
            /// <returns></returns>
            IEnumerator CharStop(Collider coll)
            {
                if (!isEnter)
                {

                    isEnter = true;

                    yield return new WaitForSeconds(1.0f);

                    //해당 캐릭터를 정지 시킴
                    coll.GetComponent<CharactersData>().IsStop = true;

                    //적을 생성 시킴
                    CreateEnemyAct();
                }
            }

            void CreateEnemyAct()
            {
                for (int i = 0; i < enemyCreate.Length; i++)
                {
                    enemyCreate[i].EnemyAct();

                }
            }

            /// <summary>
            /// 다음 위치의 자표를 넘겨 주어 이동 시킨다
            /// </summary>
            /// <param name="coll"></param>
            /// <returns></returns>
            IEnumerator CharMove(Collider coll)
            {

                //해당 캐릭터를 이동 상태로 변경
                coll.GetComponent<CharactersData>().IsStop = false;

                yield return new WaitForSeconds(0.2f);

                coll.GetComponent<CharactersData>().Speed = moveSpeed;
                //다음 위치를 보낸다
                coll.GetComponent<CharactersData>().NextMove = nextPos;

            }


            /// <summary>
            /// 활성화 된 적이 있는지 확인
            /// </summary>
            /// <returns></returns>
            bool EnemyActCheck()
            {
                for(int i=0;i<enemyCreate.Length;i++)
                {
                    //한명이라도 활성화 이면 true
                    if(enemyCreate[i].gameObject.activeSelf == true)
                    {
                        return true;        
                    }
                }
                //활성화 적이 없으면 false
                return false;
            }


        }

    }
}

