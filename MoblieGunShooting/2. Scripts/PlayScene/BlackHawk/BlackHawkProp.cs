using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 블랙호크의 프로펠러 회전
/// </summary>
namespace Black
{
    namespace Car
    {
        public class BlackHawkProp : MonoBehaviour
        {
            [SerializeField, Header("회전 속도")]
            float rotSpeed;

            [SerializeField, Header("회전 각_본체 y 90, 꼬리 x 90")]
            Vector3 rot;

            private void Update()
            {
                //프로펠러 작동 상태가 되면(헬기 본체에서 값을 가져온다)
                transform.Rotate(rot * rotSpeed * Time.deltaTime);

            }
        }

    }
}
