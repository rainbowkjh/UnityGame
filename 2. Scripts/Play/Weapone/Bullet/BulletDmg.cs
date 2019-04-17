using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapone;
using Characters;
using Manager;

/// <summary>
/// 탄 데미지
/// 같은 소속?의 탄은 맞지 않는다
/// (적끼리 타격 없음)
/// </summary>

public class BulletDmg : MonoBehaviour
{

    CharactersData data;
    AniCtrl ani;
    PlayUI.CharHP_UI ui;

    [SerializeField, Header("피격 받는 오브젝트가 플레이어인지 확인")]
    bool m_isPlayer = false; //같은 팀의 무기는 맞지 않도록 하기 위해

    [SerializeField, Header("Hit Dmg UI Pool")]
    MemoryPooling m_Pool;

    [SerializeField, Header("타격 받을때 이펙트")]
    GameObject m_parHit;
    ParticleSystem[] effect;

    private void Start()
    {
        data = GetComponent<CharactersData>();
        ani = GetComponent<AniCtrl>();
        ui = GetComponent<PlayUI.CharHP_UI>();

        m_Pool = GameObject.Find("Manager/MemoryPool").GetComponent<MemoryPooling>();

        effect = m_parHit.GetComponentsInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Bullet"))
        {
            //피격 대상(스크립트 적용 대상)이  플레이어이면
            if (m_isPlayer)
            {
                //탄이 플레이어의 탄이 아닐때
                if(!other.GetComponent<BulletCtrl>().IsPlayerBullet)
                {
                    BulletHit(other);
                }
            }

            if (!m_isPlayer)
            {
                if (other.GetComponent<BulletCtrl>().IsPlayerBullet)
                {
                    BulletHit(other);
                }
            }
        }

    }

    void BulletHit(Collider other)
    {        
        other.gameObject.SetActive(false);

        if (data.FHP > 0 && !data.IsRoll) //구르기를 하고 있을떄는 대미지를 안받는다
        {
            HitEffect();

            data.FHP -= other.GetComponent<BulletCtrl>().FDmg;

            GameObject Obj = m_Pool.GetObjPool(m_Pool.nMaxDmgUICount, m_Pool.DmgUIList);
            
            if (Obj!=null)
            {
                Obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);
                //Obj.transform.LookAt(
                //        new Vector3(this.transform.position.x, this.transform.position.y, 
                //        Camera.main.transform.position.z));

                Obj.transform.rotation = Quaternion.identity;

                Obj.GetComponent<DmgPrint>().FDmg = other.GetComponent<BulletCtrl>().FDmg;
                Obj.SetActive(true);
            }

            Obj = null;

            //ui가 있으면 HP상태를 동기화 해준다(플레이어 또는 보스가 해당, 일반 적도 구현 계획 중)
            if (ui != null)
                ui.HPBarUI(data);

            //hit Ani
        }

        if (data.FHP <= 0)
        {
            if (data.State != CharState.Dead)
            {
                //Debug.Log("Down");
                data.State = CharState.Dead;
                ani.LiveAni(false);
                ani.DownAni();

                //플레이어의 경우 콜라이더를 끄면 땅으로 떨어지기 때문에 적 캐릭터만 끈다
                if(this.gameObject.tag.Equals("Enemy"))
                {
                    //해당 캐릭터의 콜라이더를 끈다
                    this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                }
                
            }

        }
        
    }

    void HitEffect()
    {
        for(int i=0;i< effect.Length;i++ )
        {
            effect[i].Play();
        }
    }

}
