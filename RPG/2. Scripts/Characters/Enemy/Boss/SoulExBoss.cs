using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Characters
    {
        public class SoulExBoss : EnemyCtrl
        {
            [SerializeField]
            BossUi hpUi;

            bool isTeleport = false; //플레이어에게 순간 이동

            //protected override void Start()
            //{
            //    base.Start();

            //    //if (hpUi != null)
            //    //{
            //    //    hpUi.UiAct();
            //    //    hpUi.HPFill(NHp, NMaxHp);
            //    //}
                    
            //}

            private void Update()
            {
                if (IsLive && !IsAttack && !IsStun && player.IsLive)
                {
                    if (hpUi != null)
                    {
                        hpUi.UiAct();
                        hpUi.HPFill(NHp, NMaxHp);
                    }

                    //플레이어와의 거리
                    float dis = Vector3.Distance(this.transform.position, TargetTr.position);

                    //HP에 따른 헹동
                    if (NHp > (int)NMaxHp * 0.6f) //일반적인 AI와 같다
                    {

                        //공격 범위보다 멀리 있을때 타겟 추적
                        if (dis > AttackDis)
                        {
                            EnemyMove(TargetTr); //목적지를 세팅 해주고 이동 시킨다
                        }

                        if (dis <= AttackDis)
                        {
                            if (!IsAttack)
                            {
                                EnemyWeapone.BulletType = Manager.eProjectilesType.FireBall; //파이어 볼 공격
                                StartCoroutine(EnemyWeapone.EnemyAttack(this));
                            }

                        }

                        
                    }

                    else if (NHp <= (int)NMaxHp * 0.6f && NHp > (int)NMaxHp * 0.3f)
                    {
                        //공격 범위보다 멀리 있을때 타겟 추적
                        if (dis > AttackDis)
                        {
                            StartCoroutine(Teleport());
                        }

                        if (dis <= AttackDis)
                        {
                            if (!IsAttack)
                            {
                                EnemyWeapone.BulletType = Manager.eProjectilesType.FireBall; //파이어 볼 공격
                                StartCoroutine(EnemyWeapone.EnemyAttack(this, 0.5f));
                            }
                        }
                    }

                    else if (NHp <= (int)NMaxHp * 0.3f)
                    {

                        //공격 범위보다 멀리 있을때 타겟 추적
                        if (dis > AttackDis)
                        {
                            StartCoroutine(Teleport());
                        }

                        if (dis <= AttackDis)
                        {
                            if (!IsAttack)
                            {
                                EnemyWeapone.BulletType = Manager.eProjectilesType.FireBall; //파이어 볼 공격
                                StartCoroutine(EnemyWeapone.EnemyAttack(this, 0.3f));
                            }

                        }
                    }


                    AniCtrl.AniWalk(IsWalk);
                    AniCtrl.AniRun(IsRun);

                }

                if (!IsLive)
                {
                    DropItem();
                    if (questBoss != null)
                    {
                        if (isBoss)
                            questBoss.BossDown();

                        if (isSubBoss)
                            questBoss.SubQuestBossDown();
                    }
                }
            }
            //

            IEnumerator Teleport()
            {
                if(!isTeleport)
                {
                    isTeleport = true;
                    EnemyWeapone.Muzzle.Play(); //이펙트 실행
                    yield return new WaitForSeconds(2.0f);

                    transform.position = TargetTr.position; //
                    yield return new WaitForSeconds(1.0f);
                    isTeleport = false;
                }                
            }
            //
        }
        //class End
    }
}
