using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 특정 구역에 플레이어가 도착하면
/// 적을 생성 시키고
/// 적을 제거 시키면 다음 지역 이동 가능
/// 
/// m_objEnemyGroup - 적 캐릭터를 각 위치에 지정해 놓고 그룹으로 묶어 놓은다
/// m_objEnemy - 활성화 상태를 확인하기 위해 적 정보를 가져오게 함
/// m_objCollision - 처음에는 플레이어가 지나가지 못 하게 막아놓고(투명 콜라이더) 
///                  적 처치 시 비활성화 시킴
/// 
/// </summary>
public class EnemyWave : MonoBehaviour
{
    /// <summary>
    /// 유니티에서 오브젝트 관리를 위해
    /// 그룹으로 만들어 가져오고
    /// 적 캐릭터 상태를 파악하기 위해
    /// 다시 하나씩 정보를 가져온다
    /// </summary>
    [SerializeField, Header("활성화 시킬 적 캐릭터 묶음")]
    GameObject m_objEnemyGroup;
    //그룹안의 적 캐릭터들 (풀링을 사용하지 않는 이유는
    //생성 위치를 지정해 놓기 위해서(풀링으로도 가능하지만 간단하게 구현함)
    //완료 후에 메모리 사용 체크해보고 풀링으로 교체
    CharactersData[] m_objEnemy;


    [SerializeField, Header("적 처치 시 막아놓은 콜라이더 비활성화")]
    GameObject m_objCollision;

    bool m_isEnter = false;

    int m_nCount = 0;

    private void Start()
    {
        //생성되는 적의 수를 파악하고 생존여부 확인을 위해 
        m_objEnemy = m_objEnemyGroup.GetComponentsInChildren<CharactersData>();       

        m_objEnemyGroup.SetActive(false); //처음에는 비활성화
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!m_isEnter)
        {
            if(other.tag.Equals("Player"))
            {
                m_isEnter = true;
                m_objEnemyGroup.SetActive(true);
            }

        }

    }

    /// <summary>
    /// 적이 생성되고
    /// 활성화 된 수를 파악하여
    /// 다음 지역을 이동 가능하게 할지 결정
    /// </summary>
    private void Update()
    {        
        if(m_isEnter)
        {
            m_nCount = m_objEnemy.Length; //생성된 적의 수 파악

            for (int i=0;i< m_objEnemy.Length;i++)
            {
                if (m_objEnemy[i].FHP <=0)
                    m_nCount--; //비활성화 된 적이 있으면 감소
            }

            if(m_nCount == 0)
            {
                m_objCollision.SetActive(false);
            }

        }

    }


}
