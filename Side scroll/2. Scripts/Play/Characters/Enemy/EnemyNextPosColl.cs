using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적 캐릭터가 접근하면
/// 다음 위치 인덱스 값으로 변경 시켜준다
/// </summary>
namespace Characters
{
    public class EnemyNextPosColl : MonoBehaviour
    {

        [SerializeField, Header("1부터 시작, 다음 위치 값, 마지막 위치는 자기 자신")]
        int nNextPos = 1;


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Enemy"))
            {

                other.GetComponent<EnemyMovePos>().NextPos = nNextPos;

            }
        }
    }

}
