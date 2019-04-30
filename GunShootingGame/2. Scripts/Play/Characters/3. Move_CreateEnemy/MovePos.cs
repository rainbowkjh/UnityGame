
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
    private Transform nextPos = null;

    /// <summary>
    /// 이동 후 적 등장 이벤트 등 캐릭터의 이동을 정지 시킨다
    /// </summary>
    public bool isEvent = false;

    /// <summary>
    /// 플레이어의 이동 경로인지 확인
    /// </summary>
    public bool isPlayer = false;

    private bool isEnter = false;

    CreateEnemy createEnemy;
    bool isEnemy = true;

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

    private void Start()
    {
        createEnemy = GetComponent<CreateEnemy>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(isPlayer)
        {
            if (other.tag.Equals("Player"))
            {
                if (isEvent)
                {
                    //Debug.Log("플레이어 도착");
                    other.GetComponent<Characters.PlayerCtrl>().CharactersData.EState 
                        = Characters.CharState.Idle;
                    other.transform.rotation = Quaternion.Slerp(other.transform.rotation,
                                                                this.transform.rotation,
                                                                1.0f * Time.deltaTime);

                    //활성화 시킬 적(스크립트)이 있는지 확인
                    var temp = GetComponent<CreateEnemy>();

                    if (temp != null)
                    {
                        if(!isEnter)
                        {
                            isEnter = true;
                            GetComponent<CreateEnemy>().ActObj();
                        }
                        
                    }

                    //적이 비활성화 되어 있으면 다음으로 이동 시킨다
                    if (!EnemyCheck())
                    {
                        isEvent = false;
                        return;
                    }

                }


                if (!isEvent && NextPos != null)
                {
                    //회전
                    other.transform.rotation = Quaternion.Slerp(other.transform.rotation,
                                                 this.transform.rotation,
                                                 10.0f * Time.deltaTime);

                    //Debug.Log("플레이어 이동");
                    other.GetComponent<Characters.PlayerCtrl>().NextPos = NextPos;
                    other.GetComponent<Characters.PlayerCtrl>().CharactersData.EState 
                        = Characters.CharState.Move;
                }

            }
        }
        
        else if(!isPlayer)
        {
            if(other.tag.Equals("Enemy"))
            {
                if (isEvent)
                {
                    other.GetComponent<Characters.EnemyCtrl>().CharactersData.EState
                        = Characters.CharState.Idle;
                    other.transform.rotation = Quaternion.Slerp(other.transform.rotation,
                                                                this.transform.rotation,
                                                                1.0f * Time.deltaTime);

                    other.GetComponent<Characters.EnemyCtrl>().IsTargeting = true;

                }

                if (!isEvent && NextPos != null)
                {                 
                    other.GetComponent<Characters.EnemyCtrl>().NextPos = NextPos;
                    other.GetComponent<Characters.EnemyCtrl>().CharactersData.EState
                        = Characters.CharState.Move;
                }
            }
        }

    }

    /// <summary>
    /// 활성화 되어 있는 적을 확인 후 이벤트 종료
    /// </summary>
    /// <returns></returns>
    bool EnemyCheck()
    {
        if (createEnemy == null)
            return false;

        for (int i = 0; i < createEnemy.EnemyObj.Count; i++)
        {
            isEnemy = false;

            //한명이라도 적이 있으면(활성화) true
            if (createEnemy.EnemyObj[i].activeSelf == true)
            {
                isEnemy = true;
                return isEnemy; //true
            }
        }

        return isEnemy; //false
    }


}
