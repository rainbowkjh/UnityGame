using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Black
{
    namespace MovePosObj
    {   
        public class EnemyWave : MonoBehaviour
        {
            [SerializeField, Header("Wave Time, 0.01은 한명씩 나옴")]
            float waveTime = 0;

            bool isStart = false;
            bool isEnd = false;

            [SerializeField,Header("EnemyCreate")]
            EnemyCreate[] createEnemy;

            
            private void Update()
            {
                if(isStart)
                {
                    if (waveTime > 0)
                    {
                        waveTime -= Time.deltaTime * 1;

                        //적을 생성 시킨다
                        for (int i = 0; i < createEnemy.Length; i++)
                        {
                            createEnemy[i].EnemyAct();                            
                        }


                    }

                    else if (waveTime <= 0)
                    {
                        waveTime = 0;
                        isStart = false;
                        isEnd = true;
                    }
                }
            }



            private void OnTriggerEnter(Collider other)
            {
                if(!isStart && !isEnd)
                {
                    if (other.tag.Equals("Player"))
                    {
                        //Wave 시작
                        isStart = true;

                        GameManager.INSTANCE.NEnemyCount += createEnemy.Length;
                    }
                }                

            }

        }

    }
}
