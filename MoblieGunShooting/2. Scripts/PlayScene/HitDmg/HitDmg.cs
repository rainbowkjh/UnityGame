using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace DmgManager
    {
        public class HitDmg : MonoBehaviour
        {
            float dmg;

            CharactersData charData;

            private void Start()
            {
                charData = GetComponent<CharactersData>();
            }

            /// <summary>
            /// 일반 타격
            /// </summary>
            /// <param name="dmg"></param>
            public void HitDamage(float dmg)
            {
                charData.Hp -= dmg;

                if(charData.Hp <=0)
                {
                    charData.Hp = 0;
                    //캐릭터 쓰러지는 애니메이션 추가
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

        }

    }
}
