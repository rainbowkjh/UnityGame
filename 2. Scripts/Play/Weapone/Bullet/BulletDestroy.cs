using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 탄이 충돌 시 탄을 비활성화 시키는 기능
/// 주로 벽같은 오브젝트에 적용 시킨다
/// </summary>
namespace Weapone
{
    public class BulletDestroy : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag.Equals("Bullet"))
            {
                other.gameObject.SetActive(false);
            }
        }
    }

}
