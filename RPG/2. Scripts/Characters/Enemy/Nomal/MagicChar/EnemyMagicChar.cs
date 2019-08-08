using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Black
{
    namespace Characters
    {
        public class EnemyMagicChar : EnemyCtrl
        {

            void Update()
            {
                if (IsLive && !IsAttack && !IsStun && player.IsLive)
                {
                    float dis = Vector3.Distance(this.transform.position, TargetTr.position);

                    //공격 범위보다 멀리 있을때 타겟 추적
                    if (dis > AttackDis)
                    {
                        EnemyMove(TargetTr); //목적지를 세팅 해주고 이동 시킨다
                    }

                    if (dis <= AttackDis)
                    {
                        if (!IsAttack)
                        {
                            MagicRandum();
                            StartCoroutine(EnemyWeapone.EnemyAttack(this));
                        }

                    }

                    AniCtrl.AniWalk(IsWalk);
                    AniCtrl.AniRun(IsRun);
                }

                if(!IsLive)
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

            /// <summary>
            /// 우선 임시로 50%확률로
            /// 두가지 마법 중 하나로 세팅을 하게 해준다
            /// 재활용 생각해서 마법과 확률울 선택 할수 있도록 변경!!
            /// </summary>
            void MagicRandum()
            {
                int ran = Random.Range(0, 10);
                EnemyWeapone.IsStun = false;

                //파이어볼 세팅
                if (ran < 5)
                {
                    //효과음
                    Manager.GameManager.INSTANCE.SFXPlay(EnemyWeapone._Audio, EnemyWeapone._Sfx[0]);

                    EnemyWeapone.BulletType = Manager.eProjectilesType.FireBall;
                }

                //기절 효과 마법 세팅
                else if(ran >=5)
                {
                    //효과음
                    Manager.GameManager.INSTANCE.SFXPlay(EnemyWeapone._Audio, EnemyWeapone._Sfx[1]);
                    EnemyWeapone.IsStun = true;
                    EnemyWeapone.BulletType = Manager.eProjectilesType.CometBlue;
                }
            }

        }

    }
}
