using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    [SerializeField, Header("활성화 시킬 적 캐릭터")]
    List<GameObject> enemyObj = new List<GameObject>();

    public List<GameObject> EnemyObj
    {
        get
        {
            return enemyObj;
        }

        set
        {
            enemyObj = value;
        }
    }

    private void Start()
    {
        //비활성화
        DisObj();
    }


    /// <summary>
    /// 초기화(비활성화)
    /// </summary>
    void DisObj()
    {
        for(int i=0; i<EnemyObj.Count;i++)
        {
            EnemyObj[i].SetActive(false);
        }
    }

    /// <summary>
    /// 바로 활성화
    /// </summary>
    public void ActObj()
    {
        for (int i = 0; i < EnemyObj.Count; i++)
        {
            EnemyObj[i].SetActive(true);
        }
    }

    /// <summary>
    /// 바로 활성화 시키지 않고 약간의 딜레이를 준다
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    public IEnumerator DelayActObj(float delay)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < EnemyObj.Count; i++)
        {
            EnemyObj[i].SetActive(false);
        }
    }

    /// <summary>
    /// 종료 시 리스트 정리
    /// </summary>
    private void OnDestroy()
    {
        EnemyObj.Clear();
    }

}
