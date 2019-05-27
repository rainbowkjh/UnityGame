using Black.Characters;
using Black.DmgManager;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// NPC 전용 무기 스크립트
/// </summary>
namespace Black
{
    namespace Weapone
    {
        public class NPCWeapone : MonoBehaviour
        {
            [SerializeField,Header("스테이지 난이도에 따라 데미지 값 조절")]
            float minDmg = 50;

            [SerializeField]
            float maxDmg = 100;

            [SerializeField, Header("탄창")]
            int mag = 30;

            int curBullet = 0;

            [SerializeField, Header("Muzzle")]
            ParticleSystem muzzlePar;

            AudioSource _audio;

            [SerializeField, Header("0 사격 효과음, 1 재장전")]
            AudioClip[] _sfx;

            [SerializeField]
            Transform firePos;

            /// <summary>
            /// 사격 간격
            /// </summary>
            float fireRate = 0.01f;
            float fireTime = 0.0f;

            #region Set,Get
            public int CurBullet
            {
                get
                {
                    return curBullet;
                }

                set
                {
                    curBullet = value;
                }
            }
            #endregion

            private void Awake()
            {
                CurBullet = mag;
                _audio = GetComponent<AudioSource>();
            }

            public void Fire(NpcCtrl npc)
            {
                if(Time.time >= fireTime)
                {
                    if(CurBullet >0 )
                    {
                        //사격 애니메이션 실행
                        npc.AniCtrl.FireAni();

                        CurBullet--; //탄 감소
                        fireTime = Time.time + fireRate + Random.Range(0.1f, 0.3f);


                        _audio.volume = GameManager.INSTANCE.volume.sfx;
                        _audio.PlayOneShot(_sfx[0]);
                        muzzlePar.Play();

                        //적 타격 데미지
                        RaycastHit hit;
                        int layerMask = 1 << 8;
                        layerMask = ~layerMask;

                        if (Physics.Raycast(firePos.position, firePos.forward,
                            out hit, Mathf.Infinity, layerMask))
                        {
                            Debug.DrawLine(firePos.position, hit.point, Color.red);

                            float dmg = Random.Range(minDmg, maxDmg);
               

                            if (hit.transform.GetComponent<HitDmg>())
                            {
                                if (!hit.transform.GetComponent<HitDmg>().IsHeadDmg)
                                    hit.transform.GetComponent<HitDmg>().HitDamage(dmg);

                                if (hit.transform.GetComponent<HitDmg>().IsHeadDmg)
                                    hit.transform.GetComponent<HitDmg>().HeadDamage(dmg);

                            }

                        }
                    }
                    
                }
            }

            public IEnumerator Reload(NpcCtrl npc)
            {
                //AI의 Reload 상태 값 변경과 애니메이션 실행
                npc.IsReload = true;
                npc.AniCtrl.ReloadAni();

                _audio.volume = GameManager.INSTANCE.volume.sfx * 0.7f;
                _audio.PlayOneShot(_sfx[1]);

                CurBullet = mag;

                yield return new WaitForSeconds(3f);
                //재장전 상태 종료
                npc.IsReload = false;

            }

        }

    }
}
