using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이 스크립트는
/// 특정 콜라이더에 적용하며ㅛ
/// 콜라이더에 정해진 오브텍트 감지히
/// 해당 캐릭터의 애니메이션읗을 실행
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class EventSoul : MonoBehaviour
        {
            [SerializeField]
            Animator ani;

            readonly int hashAttack = Animator.StringToHash("Attack");

            [SerializeField, Header("감지 할 오브젝트 태그")]
            string tagName;

            bool isEnter = false;

            public void AttackAni()             
            {
                ani.SetTrigger(hashAttack);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="other"></param>
            private void OnTriggerEnter(Collider other)
            {
                if(!isEnter)
                {
                    if(other.transform.CompareTag(tagName))
                    {
                        isEnter = true;
                        AttackAni();
                    }
                        
                }
            }

        }

    }
}
