
using Black.Car;
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
            bool isTempEvent; //이벤트 재사용

            /// <summary>
            /// 콜라이더에 도착했는지 확인
            /// </summary>
            bool isEnter = false;

            [SerializeField, Header("캐릭터 이동속도를 변경 시킨다")]
            float moveSpeed = 5;

            [SerializeField, Header("적 생성 지역 오브젝트")]
            GameObject enemyArea =null;

            EnemyCreate[] enemyCreate;

            [SerializeField, Header("차량 탑승 및 AI등")]
            private string tagName;

            [SerializeField, Header("다음 목표물 이동 타이머")]
            float moveTimer = 0;
            float tempMoveTimer;

            private void Start()
            {
                //생성 시킬 적이 있으면
                if (enemyArea != null)
                {
                    enemyCreate = enemyArea.GetComponentsInChildren<EnemyCreate>();                    
                }
                isTempEvent = isEvent;
                tempMoveTimer = moveTimer;
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

            private void OnTriggerExit(Collider other)
            {
                if(other.tag.Equals("Player"))
                {
                    isEvent = isTempEvent;
                    moveTimer = tempMoveTimer;
                }
            }

            private void OnTriggerStay(Collider other)
            {
                if(IsPlayerPos)
                {
                    if (other.transform.tag.Equals("Player"))
                    {
                        //차량 탑승을 안하고 있을때만
                        if (!other.GetComponent<PlayerCtrl>().IsDrive)
                        {
                            //이벤트 발생 지점일 경우 정지
                            if (isEvent && moveTimer ==0)
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

                            if(isEvent && moveTimer > 0)
                            {
                                moveTimer -= Time.deltaTime * 1;

                                StartCoroutine(CharStopTimer(other));

                                if (moveTimer <=0)
                                {
                                    moveTimer = 0;
                                    StartCoroutine(CharMove(other));
                                }
                            }
                        }

                    }
                }
                

              else  if(other.transform.tag.Equals("Enemy"))
                {
                    //Debug.Log("Enemy");

                    //정지 공격
                    if(isEvent)
                    {
                        //Debug.Log("Attack");
                        other.GetComponent<CharactersData>().IsStop = true; //정지
                    }

                    //이동
                    if(!isEvent)
                    {

                        //Debug.Log("Move");
                        if (nextPos != null)
                            StartCoroutine(CharMove(other));
                    }

                    if (isEvent && moveTimer > 0)
                    {
                        moveTimer -= Time.deltaTime * 1;

                        StartCoroutine(CharStopTimer(other));

                        if (moveTimer <= 0)
                        {
                            moveTimer = 0;
                            StartCoroutine(CharMove(other));
                            isEvent = false;
                        }
                    }

                    other.transform.rotation = Quaternion.Slerp(other.transform.rotation,
                               this.transform.rotation, 10 * Time.deltaTime);
                }

                if(other.tag.Equals(tagName))
                {
                    
                    float rot;
                    if(tagName.Equals("BlackHawk"))
                    {
                        rot = 5; //블랙호크의 회전 속도
                    }
                    else
                    {
                        rot = 25;
                    }

                    if(isEvent)
                    {
                        CarStop(other);
                    }

                    if(!isEvent)
                    {
                        //Debug.Log("Tag : " + tagName);
                        CarMove(other, rot);
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
                    GameManager.INSTANCE.NEnemyCount += enemyCreate.Length; //생성된 적의 수를 담는다
                    isEnter = true;

                    yield return new WaitForSeconds(1.0f);

                    //해당 캐릭터를 정지 시킴
                    coll.GetComponent<CharactersData>().IsStop = true;

                    //적을 생성 시킴
                    CreateEnemyAct();
                }
            }

            IEnumerator CharStopTimer(Collider other)
            {
                if (!isEnter)
                {
                    isEnter = true;

                    yield return new WaitForSeconds(1.0f);

                    //해당 캐릭터를 정지 시킴
                    other.GetComponent<CharactersData>().IsStop = true;

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

            /// <summary>
            /// 탑승 차량 이동
            /// 차량 또는 헬기의 방향 전환 속도
            /// </summary>
            /// <param name="coll"></param>
            void CarMove(Collider coll, float rotSpeed)
            {
                TrooperCar car = coll.GetComponent<TrooperCar>();

                car.transform.rotation = Quaternion.Slerp(car.transform.rotation,
                    this.transform.rotation, rotSpeed * Time.deltaTime);

                if(nextPos !=null)
                {
                    car.CarState = TrooperState.Excel;
                    car.CarSpeed = moveSpeed;
                    car.NextPos = nextPos;
                }                

            }

            /// <summary>
            /// 탑승차량 정지
            /// </summary>
            /// <param name="coll"></param>
            void CarStop(Collider coll)
            {
                TrooperCar car = coll.GetComponent<TrooperCar>();

                car.CarState = TrooperState.Break;
            }

        }

    }
}

