using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 정해진 객체가 감지 되면
/// 다음 이동 위치를 목표로 잡아준다
/// </summary>
namespace Black
{
    namespace Veh
    {
        public class MovePosColl : MonoBehaviour
        {
            [SerializeField, Header("감지 할 오브젝트 태그")]
            eObjTag objTag;

            string tagStr;
            bool isEnter = false;

            [SerializeField, Header("다음 위치까지 이동속도 값")]
            float moveSpeed;

            [SerializeField, Header("다음 위치 인덱스 값")]
            int nextIndex;

            [SerializeField, Header("다음 이동 위치 콜라이더")]
            Transform nextPos;

            [SerializeField, Header("이 지점에서 공격 상태로 변경")]
            bool isAttack =false;

            [SerializeField, Header("슬로우 효과")]
            bool isSlow = false;

            [SerializeField, Header("생성 시킬 오브젝트")]
            bool isObjCreate = false;

            [SerializeField, Header("생성 시킬 오브젝트")]
            GameObject createObj;

            

            private void Start()
            {
                tagStr= GameManager.INSTANCE.ObjTagSetting(objTag);

                if (isObjCreate)
                    createObj.SetActive(false);
            }

            private void OnTriggerEnter(Collider other)
            {
                if(!isEnter)
                {
                    if(other.transform.CompareTag(tagStr))
                    {
                        VehCtrl ctrl = other.GetComponent<VehCtrl>();
                        ctrl.MoveSpeed = moveSpeed;

                        ctrl.IsAttack = isAttack;

                        if (isObjCreate)
                            createObj.SetActive(true);

                        if (isSlow)
                        {
                            ctrl.IsSlow = true;
                        }

                        if (nextIndex != -1)
                            ctrl.NextMovePos = nextPos;

                        if(nextIndex == -1)
                        {
                            ctrl.IsLive = false; //정지 또는 캐릭터 쓰러짐
                        }
                    }
                }
            }


        }

    }
}
