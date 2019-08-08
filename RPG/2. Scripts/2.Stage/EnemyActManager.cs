using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Manager
    {
        public class EnemyActManager : MonoBehaviour
        {
            [SerializeField, Header("생성 시킬 적 캐릭터 그룹")]
            GameObject[] enemyGroup;

            bool isEnter = false;

            [SerializeField, Header("적 그룹이 여러개일때 생성 딜레이")]
            float fDelay = 5.0f;


            private void Start()
            {
                EnemyDis();          
            }

            private void LateUpdate()
            {
                if(isEnter)
                {
                    StartCoroutine(ActEnemy(fDelay));
                }
            }

            /// <summary>
            /// 플레이어가 감지 되면
            /// 적 생성 활성화
            /// </summary>
            /// <param name="other"></param>
            private void OnTriggerEnter(Collider other)
            {                
                if(other.transform.CompareTag("Player"))
                {
                    isEnter = true;
                }
            }

            /// <summary>
            /// 초기화 및 상황에 맞게
            /// 해당 그룹 비활성화
            /// </summary>
            void EnemyDis()
            {
                for(int i=0;i<enemyGroup.Length;i++)
                {
                    enemyGroup[i].SetActive(false);
                }
            }


            IEnumerator ActEnemy(float delay)
            {
                for(int i=0; i< enemyGroup.Length;i++)
                {
                    yield return new WaitForSeconds(delay);
                    enemyGroup[i].SetActive(true);                    
                }
            }

        }
        //class End
    }
}
