using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이펙트 오브젝트가
/// 비활성화 되지 않을 것을 대비해
/// 비활성화 시킨다
///  
/// 오브젝트를 비활성화 시키기 떄문에
/// 상황에 따라 다른 오브젝트도 비활성화 하는데 사용 가능
/// </summary>
namespace Black
{
    namespace EffectCtrl
    {
        public class EffectDis : MonoBehaviour
        {
            public float effectOffTime = 1;

            private void LateUpdate()
            {
                if (gameObject.activeSelf)
                    StartCoroutine(EffectFalse(effectOffTime));
            }

            IEnumerator EffectFalse(float delay)
            {
                yield return new WaitForSeconds(delay);
                gameObject.SetActive(false);
            }
        }

    }
}
