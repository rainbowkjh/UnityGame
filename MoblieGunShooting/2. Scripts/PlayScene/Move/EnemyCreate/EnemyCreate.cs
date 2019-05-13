using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public class EnemyCreate : MonoBehaviour
        {
            [SerializeField,Header("Pooling Manager")]
            MemoryPooling pool;

            /// <summary>
            /// 적 생성
            /// (플레이어가 특정 지역에 도착하면
            /// 생성 시킨다)
            /// </summary>
            public void EnemyAct()
            {
                GameObject obj = pool.GetObjPool(pool.nZombieMax, pool.ZombieList);

                if (obj != null)
                {
                    obj.transform.position = transform.position;
                    obj.transform.rotation = transform.rotation;
                    obj.SetActive(true);
                    
                }
            }


        }

    }
}

