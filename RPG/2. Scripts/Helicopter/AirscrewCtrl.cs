using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 헬리콥터의 프로펠러 회전
/// 앞, 뒤 프로펠러에 적용하고
/// 방향에 맞게 회전 각을 설정해준다
/// 회전은 헬리콤터 컨트롤에서..
/// </summary>
namespace Black
{
    namespace Veh
    {
        public class AirscrewCtrl : MonoBehaviour
        {
            [SerializeField, Header("회전 축")]
            Vector3 airscrewAxis;
            [SerializeField, Header("회전 속도")]
            private float rotSpeed = 150;

            public void AirsCrewRot()
            {
                transform.Rotate(airscrewAxis, Time.deltaTime * rotSpeed);
            }
        }

    }
}
