using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ExplosionAfter의 타이머를 작동 시킨다
/// 작동 시킬 오브젝트의 태그값을 검색 시킴
/// </summary>
namespace Black
{
    namespace Effect
    {
        public class ExplosionTimerStart : MonoBehaviour
        {
            [SerializeField, Header("폭발 시킬 오브젝트의 태그 값")]
            string objTag;

            bool isEnter = false;

            private void OnTriggerEnter(Collider other)
            {
                if(!isEnter)
                {
                    if (other.tag.Equals(objTag))
                    {
                        isEnter = true;
                        other.GetComponent<ExplosionAfter>().IsTimerStart = true;
                    }
                }
                
            }

        }

    }
}
