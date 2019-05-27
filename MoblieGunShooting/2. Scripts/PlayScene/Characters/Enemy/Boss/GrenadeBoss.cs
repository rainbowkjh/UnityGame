using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 수류탄 던지는 좀비 보스전
/// 플레이어처럼 이동 경로를 지정해 주고
/// 이동경로를 따라 움직인다(네비 사용 안함)
/// 
/// 수류탄을 던지면서 이동경로를 따라 움직임(플레이어 추적)
/// 특정 구간에서 플레이어에게 빠르게 접근 시키고 근접 공격
/// (피격 당하면 공격 정지)
/// 수류탄 날아오는 속도는 사격 해서 터트릴수 있는 속도
/// 또는 슬로우 모션을 준다(타임 스케일)
/// 
/// 일반 공격과 수류탄 던지는 애니메이션은 같다(애니메이션이 부족해서...)
/// 
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class GrenadeBoss : EnemyCtrl
        {
            /// <summary>
            /// 이동 상태
            /// </summary>
            bool isRun = false;

            /// <summary>
            /// 플레이어의 위치
            /// </summary>
            Transform targetTr;

            [SerializeField, Header("일반 공격 범위")]
            float meleeDis = 3.0f;

            [SerializeField, Header("수류탄 공격 범위")]
            float grenadeDis = 7.0f;

            [SerializeField, Header("보스전 종료 후 스테이지 완료 변환")]
            StageManager stageManager;

            [SerializeField, Header("수류탄 장착 위치")]
            Transform grenadePosTr;

            [SerializeField, Header("수류탄 풀링")]
            MemoryPooling pooling;

            
            protected override void Start()
            {
                base.Start();
                nav.enabled = false; //네비를 끈다

                targetTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }

            private void Update()
            {

                if (IsLive)
                {
                    //다음 위치로 이동 
                    if (NextMove != null && !IsStop)
                    {
                        transform.position = Vector3.MoveTowards(transform.position,
                                           NextMove.position, Speed * Time.deltaTime);

                        isRun = true;
                    }


                    float dis = Vector3.Distance(targetTr.position, transform.position);


                    #region 이동 제어 변경 되어 사용 안함
                    //범위에서 벗어나면 이동 상태
                    //if (dis > grenadeDis)
                    //{
                    //    //Debug.Log("EnemyMove");
                    //    AttackCancle(); //공격 상태 초기화 ..이거 활성화 될때 초기화 되는거;;

                    //    IsStop = false;

                    //    if (NextMove != null)
                    //    {
                    //        transform.position = Vector3.MoveTowards(transform.position,
                    //                   NextMove.position, Speed * Time.deltaTime);
                    //    }

                    //    isRun = true;
                    //}
                    #endregion

                    //일반 공격 범위에서 벗어나고
                    //수류탄 공격 범위일때
                    if (dis > meleeDis && dis < grenadeDis)
                    {
                        // Debug.Log("Grenade");
                        AttackDelayTime(); //공격                        
                        transform.LookAt(new Vector3(targetTr.position.x, transform.position.y, targetTr.position.z));

                        ////수류탄 던짐
                        //GreadeAttack();

                        IsStop = true;
                        isRun = false;
                    }

                    //일반 공격
                    if (dis < meleeDis)
                    {
                        // Debug.Log("Melee");

                        transform.LookAt(new Vector3(targetTr.position.x, transform.position.y, targetTr.position.z));
                        AttackDelayTime(); //공격
                        IsStop = true;
                        isRun = false;
                    }
                }

                if(!IsLive && Hp == 0 &&!isDead)
                {
                    isDead = true;
                    isRun = false;

                    Ani.DeadAni();
                    stageManager.StageEnd();

                }

                Ani.RunAni(isRun);
            }


            void GreadeAttack()
            {
                GameObject obj = pooling.GetObjPool(pooling.nGrenadeMax, pooling.GrenadeList);

                if (obj != null)
                {
                    obj.transform.position = grenadePosTr.position;
                    obj.transform.rotation = grenadePosTr.rotation;
                    obj.SetActive(true);

                    obj.GetComponent<Rigidbody>().AddForce(obj.transform.forward * 10, ForceMode.Impulse);
                }
            }

            protected override IEnumerator AttackDelay()
            {
                
                delayTime = 0;
                IsFire = true;
                AttackWait = false;
                Ani.AttackAni();

                //수류탄 던짐
                GreadeAttack();

                yield return new WaitForSeconds(1.0f);
                IsFire = false;
            }
        }

    }
}
