using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 보스전에서 플레이어가
/// 특정 지역에 가면
/// 보스 캐릭터를 지정한 위치로
/// 순간 이동 시킨다
/// </summary>
namespace Black
{
    namespace MovePosObj
    {
        public class BossTeleportation : MonoBehaviour
        {
            [SerializeField, Header("순간 이동 시킬 캐릭터")]
            GameObject teleportationObj;

            [SerializeField, Header("순간 이동 시킬 위치")]
            Transform teleportationTr;

            private void OnTriggerEnter(Collider other)
            {
                if(other.tag.Equals("Player"))
                {
                    teleportationObj.transform.position = teleportationTr.position;
                }
            }


        }

    }
}
