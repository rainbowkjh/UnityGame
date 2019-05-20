using Black.Characters;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 적을 생성 시킬 트랜스폼에 적용 시키는 스크립트
/// 
/// Area 오브젝트를 하나 만들고
/// 그 안에 생성 시킬 오브젝트들을 넣어준다
/// Area 오브젝트를 통해서 생성 위치를 검색하여 생성 시킴 
/// </summary>
namespace Black
{
    namespace MovePosObj
    {
        [Serializable]
        public enum EnemyType
        {
            zombie, SdZombie,
        }

        public class EnemyCreate : MonoBehaviour
        {
            [SerializeField,Header("생성 시킬 적의 타입")]
            EnemyType enemyType;

            [SerializeField,Header("Pooling Manager")]
            MemoryPooling pool;

            [SerializeField, Header("생성 시킬 적의 외형")]
            bool isZombie = true;
            [SerializeField, Header("좀비 외형")]
            ZombieSkin zombieSkin;
            [SerializeField, Header("적 외형")]
            EnemySkin enemySkin;

            GameObject obj;

            [SerializeField, Header("생성할 적의 HP")]
            float hp = 100;

            [SerializeField, Header("생성 할 적의 이동속도")]
            float speed = 5;

            private void EnemyTypeSetting()
            {
                //활성화 시킬 적의 타입을 확인
                switch (enemyType)
                {
                    case EnemyType.zombie:
                        obj = pool.GetObjPool(pool.nZombieMax, pool.ZombieList);
                        break;

                    case EnemyType.SdZombie:
                        obj = pool.GetObjPool(pool.nSdZombieMax, pool.sdZombieList);
                        break;

                }
            }

            /// <summary>
            /// 적 생성
            /// (플레이어가 특정 지역에 도착하면
            /// 생성 시킨다)
            /// </summary>
            public void EnemyAct()
            {
                EnemyTypeSetting();

                //활성화 가능하면 활성화 시킴
                if (obj != null)
                {
                    if (isZombie)
                    {
                        obj.GetComponent<ZombieSkinSetting>().EnemyTypeSetting(zombieSkin);
                    }
                    else if (!isZombie)
                    {
                        obj.GetComponent<EnemySkinSetting>().EnemyTypeSetting(enemySkin);
                    }

                    //적 캐릭터 활성화 데이터 초기화
                    obj.GetComponent<EnemyCtrl>().EnemyInit(true, hp, speed, false);

                    obj.transform.position = transform.position;
                    obj.transform.rotation = transform.rotation;
                    obj.SetActive(true);
                }
            }

            /// <summary>
            /// 생성 딜레이를 준다
            /// </summary>
            /// <returns></returns>
            public IEnumerator EnemyWaveAct()
            {
                yield return new WaitForSeconds(0.5f);

                EnemyTypeSetting();
                
                //활성화 가능하면 활성화 시킴
                if (obj != null)
                {
                    if (isZombie)
                    {
                        obj.GetComponent<ZombieSkinSetting>().EnemyTypeSetting(zombieSkin);
                    }
                    else if (!isZombie)
                    {
                        obj.GetComponent<EnemySkinSetting>().EnemyTypeSetting(enemySkin);
                    }

                    //적 캐릭터 활성화 데이터 초기화
                    obj.GetComponent<EnemyCtrl>().EnemyInit(true, hp, speed, false);

                    obj.transform.position = transform.position;
                    obj.transform.rotation = transform.rotation;
                    obj.SetActive(true);
                }
            }


        }
    }
}