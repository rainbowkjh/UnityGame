using Black.Characters;
using Black.MovePosObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 차량에 접근하면
/// 탑승 위치로 상속 시키고
/// 무기 전환
/// </summary>
namespace Black
{
    namespace Car
    {
        public class PlayerDrive : MonoBehaviour
        {
            [SerializeField, Header("탑승 위치")]
            Transform drivePos;

            /// <summary>
            /// 탑승 상태
            /// </summary>
            bool isOnBoard = false;

            [SerializeField,Header("차량 대기를 해제한다 ")]
            MovePos movePos;

            private void OnTriggerEnter(Collider other)
            {
                if(!isOnBoard)
                {
                    if(other.tag.Equals("Player"))
                    {
                        //Debug.Log("탑승");

                        //탑승 위치로 상속
                        other.transform.SetParent(drivePos);
                        other.transform.localPosition = Vector3.zero; //위치를 다시 잡는다

                        other.GetComponent<PlayerCtrl>().IsDrive = true;                        

                        movePos.IsEvent = false;
                    }

                   
                }
            }
        }

    }
}
