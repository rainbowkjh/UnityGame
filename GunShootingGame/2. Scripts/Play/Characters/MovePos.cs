
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 다음 이동 위치를 지정
/// 이동 위치가 될 트랜스폼에 적용 시킨다
/// </summary>
public class MovePos : MonoBehaviour
{
    [SerializeField]
    private Transform nextPos;

    /// <summary>
    /// 이동 후 적 등장 이벤트 등 캐릭터의 이동을 정지 시킨다
    /// </summary>
    public bool isEvent = false;

    #region Set,Get
    public Transform NextPos
    {
        get
        {
            return nextPos;
        }

        set
        {
            nextPos = value;
        }
    }
    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (isEvent)
            {
                //Debug.Log("플레이어 도착");
                other.GetComponent<Characters.PlayerCtrl>().CharactersData.EState = Characters.CharState.Idle;
                other.transform.rotation = Quaternion.Slerp(other.transform.rotation,
                                                            this.transform.rotation,
                                                            1.0f * Time.deltaTime);
            }

            if (!isEvent && NextPos != null)
            {
                //Debug.Log("플레이어 이동");
                other.GetComponent<Characters.PlayerCtrl>().NextPos = NextPos;
                other.GetComponent<Characters.PlayerCtrl>().CharactersData.EState = Characters.CharState.Move;
            }

        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            if(isEvent)
            {
                other.GetComponent<Characters.PlayerCtrl>().CharactersData.EState = Characters.CharState.Idle;
            }

            if (!isEvent && NextPos != null)
            {
                Debug.Log("플레이어 도착");               
                other.GetComponent<Characters.PlayerCtrl>().NextPos = NextPos;
            }            
        }
    } 
         */


}
