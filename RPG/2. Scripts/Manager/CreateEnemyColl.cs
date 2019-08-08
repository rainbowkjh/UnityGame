using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 콜라이더 접근 시
/// 적을 활성화 시킨다
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class CreateEnemyColl : MonoBehaviour
        {

            [SerializeField, Header("생성 시킬 적 캐릭터의 그룹")]
            GameObject[] enemyGroupObj;

            private void Start()
            {
                for(int i=0; i<enemyGroupObj.Length;i++)
                {
                    enemyGroupObj[i].SetActive(false);
                }
            }

            private void OnTriggerEnter(Collider other)
            {
                if(other.tag.Equals("Player"))
                {
                    for (int i = 0; i < enemyGroupObj.Length; i++)
                    {
                        enemyGroupObj[i].SetActive(true);
                    }
                }
            }
        }

    }
}
