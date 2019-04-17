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
        [SerializeField, Header("파손되는 오브젝트")]
        bool isObs = false;

        [SerializeField, Header("오브젝트의 내구도(0이되면 부서진다")]
        float m_fDurability = 500.0f;

        [SerializeField, Header("파괴 될떄 이펙트")]
        ParticleSystem m_parEffect;


        [SerializeField, Header("Hit Effect")]
        GameObject m_effectObj;
        ParticleSystem[] m_hitEffect;


        private void Start()
        {
            if (m_effectObj)
                m_hitEffect = m_effectObj.GetComponentsInChildren<ParticleSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag.Equals("Bullet"))
            {
                other.gameObject.SetActive(false);

                if(m_fDurability > 0)
                {
                    HitEffect();

                    //플레이어의 탄에만 내구력이 감소
                    if (isObs && other.GetComponent<BulletCtrl>().IsPlayerBullet)
                    {
                        m_fDurability -= other.GetComponent<BulletCtrl>().FDmg;

                        if(m_fDurability<=0)
                        {
                            StartCoroutine(ObjDisable());
                        }
                    }
                }
                
            }
        }

        IEnumerator ObjDisable()
        {
            if(!m_parEffect.isPlaying)
            {
                m_parEffect.Play();
            }

            yield return new WaitForSeconds(1.0f);

            Destroy(gameObject);
        }

        void HitEffect()
        {
            if(m_effectObj)
            {
                for (int i = 0; i < m_hitEffect.Length; i++)
                {
                    m_hitEffect[i].Play();
                }
            }
            
        }


    }

}
