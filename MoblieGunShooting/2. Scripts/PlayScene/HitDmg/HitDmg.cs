using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 엑스트라 적 또는 플레이어에 해당
/// 엑스트라 - 플레이어가 접근해서 생성 되는 적이 아니고
///           다른 방법으로 존재하는적
///           (주로 플레이어가 이동하면서 싸우게 되는 적, 보스 급)
/// </summary>
namespace Black
{
    namespace DmgManager
    {
        public class HitDmg : MonoBehaviour
        {
            float dmg;

            protected CharactersData charData;

            private void Start()
            {
                charData = GetComponent<CharactersData>();
            }

            /// <summary>
            /// 일반 타격
            /// </summary>
            /// <param name="dmg"></param>
            virtual public void HitDamage(float dmg)
            {
                charData.Hp -= dmg;

                if(charData.Hp <= 0 &&
                    charData.IsLive)
                {
                    charData.IsLive = false;

                    charData.Hp = 0;
                    //캐릭터 쓰러지는 애니메이션 추가

                    //생성되는 적이 아니라 엑스트라 적
                    if(this.transform.tag.Equals("Enemy"))
                    {
                        StartCoroutine(EnemyDisable());
                    }

                }
            }

            /// <summary>
            /// 헤드 샷 데미지 
            /// 1.5배
            /// </summary>
            /// <param name="dmg"></param>
            public void HeadDamager(float dmg)
            {
                float damage = dmg * 1.5f;

                charData.Hp -= damage;
            }


            protected IEnumerator EnemyDisable()
            {
                yield return new WaitForSeconds(1.5f);
                gameObject.SetActive(false);
            }

        }

    }
}
