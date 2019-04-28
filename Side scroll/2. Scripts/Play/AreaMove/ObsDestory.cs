using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 특정 지역 이동 못하게 막아놓은 장애물을 파괴한다(비활성화)
/// 플레이어가 콜라이더를 접근하면 약간의 딜레이 후 비활성화
/// 폭발 연출도 가능 
/// </summary>

public class ObsDestory : MonoBehaviour
{
    [SerializeField, Header("비활성화 시킬 오브젝트")]
    GameObject m_objOBS;

    [SerializeField, Header("폭발 효과 후 장애물 제거 시 사용할 이펙트")]
    ParticleSystem m_parEffect;

    /// <summary>
    /// 한번 실행되고 나면 실행하지 않도록 한다
    /// </summary>
    bool isEnter = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            if (!isEnter)
                StartCoroutine(ObsDisable());
        }
    }


    IEnumerator ObsDisable()
    {
        isEnter = true;

        yield return new WaitForSeconds(3.0f);

        //이펙트가 있으면
        if(m_parEffect!= null)
        {
            //이펙트가 실행중이지 않을때
            if(!m_parEffect.isPlaying)
            {
                //이펙트 실행
                m_parEffect.Play();
            }
        }

        yield return new WaitForSeconds(1.5f);

        m_objOBS.SetActive(false);
    }

}
