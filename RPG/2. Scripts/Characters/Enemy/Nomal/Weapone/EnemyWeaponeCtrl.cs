using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Black
{
    namespace Weapone
    {
        public class EnemyWeaponeCtrl : WeaponeData
        {
     
            /// <summary>
            /// Enemy Attack
            /// </summary>
           public IEnumerator EnemyAttack(EnemyCtrl enemy, float attackDelay = 1.5f)
            {
                enemy.IsAttack = true; //공격 상태
                enemy.EnemyStop(); //공격 범위에 들어오면 정지한다
                enemy.Nav.transform.LookAt(enemy.TargetTr); //타겟을 바라본다
                enemy.AniCtrl.AniAttack(); //공격 애니메이션
                yield return new WaitForSeconds(0.5f);

                Muzzle.Play(); //공격 이펙트(총구 화염 또는 공격 효과)
                BulletSetting(false); //공격 이펙트(데미지 적용)

                float ran = Random.Range(0.0f, attackDelay);
                yield return new WaitForSeconds(ran);
                enemy.IsAttack = false; //공격 상태 해제
            }

        }

    }
}
