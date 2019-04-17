using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어를 다른 공간으로 이동 시킨다
/// 층간 이동 등
/// </summary>


public class AreaMove : MonoBehaviour
{
    [SerializeField, Header("이동 시킬 좌표")]
    Transform m_MovePos;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                other.transform.position = m_MovePos.position;
                //other.transform.rotation = m_MovePos.rotation;
            }
        }
    }

}
