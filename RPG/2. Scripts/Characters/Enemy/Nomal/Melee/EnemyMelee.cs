using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 근접 공격하는 적 캐릭터
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class EnemyMelee : EnemyCtrl
        {
            void Update()
            {
                if(isStart)
                {
                    Manager.GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);
                    isStart = false;
                }

                if (IsLive && !IsAttack && !IsStun && player.IsLive)
                {
                    float dis = Vector3.Distance(this.transform.position, TargetTr.position);

                    //달리기 범위
                    if (dis > RunDis)
                    {
                        EnemyRun(TargetTr);
                    }


                    //공격 범위보다 멀리 있을때 타겟 추적
                    else if (dis > AttackDis && dis <= RunDis)
                    {
                        EnemyMove(TargetTr); //목적지를 세팅 해주고 이동 시킨다
                    }


                    else if (dis <= AttackDis)
                    {
                        if (!IsAttack)
                        {
                            Manager.GameManager.INSTANCE.SFXPlay(EnemyWeapone._Audio, EnemyWeapone._Sfx[Random.Range(0, EnemyWeapone._Sfx.Length)]);
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
        }

    }
}
