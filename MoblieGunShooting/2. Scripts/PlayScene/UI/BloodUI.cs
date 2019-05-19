using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 피격 시 혈흔 ui
/// 코루틴으로 구현 시
/// 쉐이크 카메라와 같이
/// 코루틴을 사용 해야 하기 때문에
/// 애니메이션으로 구현 함
/// </summary>
namespace Black
{
    namespace UI
    {
        public class BloodUI : MonoBehaviour
        {
            Animator ani;

            readonly int hashHit = Animator.StringToHash("Hit");

            private void Start()
            {
                ani = GetComponent<Animator>();
            }


            public void HitBloodAni()
            {
                ani.SetTrigger(hashHit);
            }

        }

    }
}
