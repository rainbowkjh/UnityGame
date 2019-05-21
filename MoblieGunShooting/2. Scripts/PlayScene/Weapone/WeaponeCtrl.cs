using Black.CameraUtil;
using Black.Characters;
using Black.DmgManager;
using Black.ItemData;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Black
{
    namespace Weapone
    {
        [RequireComponent(typeof(AudioSource))]
        public class WeaponeCtrl : MonoBehaviour
        { 
            #region WeaponeData
            int id = 0;
            string weaponeName = "";
            string itemIconPath = ""; //아이템 아이콘 경로

            int weaponeState = 0; //무기 형태 0 권총 1 AR 2 SG

            //데미지 최소 수치1 ~최소 수치2 중 랜덤 결정
            //최대치도 같음 (같은 무기라도 최소에서 최대 데미지가 다르다)
            //결정된 수치에서 공격할때 또 랜덤 데미지를 적용
            float m_fMinDmgF = 50;
            float m_fMinDmgE = 100;

            float m_fMaxDmgF = 150;
            float m_fMaxDmgE = 200;

            //무기마다 실제 적용되는 데미지
            float m_fMinDmg = 0;
            float m_MaxDmg = 0;

            //탄창안의 최대 탄 수
            int m_nMag = 30;
            int m_nCurBullet = 30; // 현재 탄 수

            bool m_isUse = false; //로드 할때 사용 중이였던 무기 였는지 확인 후 표시

            #endregion

            #region Set,Get
            public int Id
            {
                get
                {
                    return id;
                }

                set
                {
                    id = value;
                }
            }

            public string WeaponeName
            {
                get
                {
                    return weaponeName;
                }

                set
                {
                    weaponeName = value;
                }
            }

            public float FMinDmgF
            {
                get
                {
                    return m_fMinDmgF;
                }

                set
                {
                    m_fMinDmgF = value;
                }
            }

            public float FMinDmgE
            {
                get
                {
                    return m_fMinDmgE;
                }

                set
                {
                    m_fMinDmgE = value;
                }
            }

            public float FMaxDmgF
            {
                get
                {
                    return m_fMaxDmgF;
                }

                set
                {
                    m_fMaxDmgF = value;
                }
            }

            public float FMaxDmgE
            {
                get
                {
                    return m_fMaxDmgE;
                }

                set
                {
                    m_fMaxDmgE = value;
                }
            }

            public int NMag
            {
                get
                {
                    return m_nMag;
                }

                set
                {
                    m_nMag = value;
                }
            }

            public string ItemIconPath
            {
                get
                {
                    return itemIconPath;
                }

                set
                {
                    itemIconPath = value;
                }
            }

            public int NCurBullet
            {
                get
                {
                    return m_nCurBullet;
                }

                set
                {
                    m_nCurBullet = value;
                }
            }

            public float FMinDmg
            {
                get
                {
                    return m_fMinDmg;
                }

                set
                {
                    m_fMinDmg = value;
                }
            }

            public float MaxDmg
            {
                get
                {
                    return m_MaxDmg;
                }

                set
                {
                    m_MaxDmg = value;
                }
            }

            public bool IsUse
            {
                get
                {
                    return m_isUse;
                }

                set
                {
                    m_isUse = value;
                }
            }

            public int WeaponeState
            {
                get
                {
                    return weaponeState;
                }

                set
                {
                    weaponeState = value;
                }
            }
            #endregion

            private float fireRate = 0.01f; //발사간격
            private float fireTime = 0.0f; //시간 흐름

            ParsingData parsingData;
            [SerializeField, Header("적용 시킬 아이템 이름(테이블에 있는 이름)")]
            string weaponeDataName;

            private PlayerCtrl playerCtrl;

            AudioSource _audio;
            [SerializeField,Header("0 사격, 1 재장전, 2 탄 없음")]
            AudioClip[] _sfx;

            [SerializeField, Header("Muzzle")]
            ParticleSystem muzzleEffect;

            [SerializeField, Header("HitEffectObj")]
            GameObject hitEffect;

            ParticleSystem[] hitParticle;

            [SerializeField, Header("Shake Cam")]
            shakeCamera shakeCam;

            [SerializeField, Header("재장전 딜레이 권총,소총 1.5, 샷건 0.5")]
            float reloadSpeed = 1.5f;

            private void Awake()
            {
                parsingData = GameObject.Find("ParSingData").GetComponent<ParsingData>();
                
            }

            private void Start()
            {
             //   Debug.Log("WeaponeCtrl");
                ItemSearch(weaponeDataName); //무기 데이터 적용 
                playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                _audio = GetComponent<AudioSource>();

                hitParticle = hitEffect.GetComponentsInChildren<ParticleSystem>();
            }

            void ItemSearch(string name)
            {
                for(int i=0; i< parsingData.WeaponeList.Count;i++)
                {
                    if (parsingData.WeaponeList[i].WeaponeName.Equals(name))
                    {
                        Id = parsingData.WeaponeList[i].Id;
                        WeaponeName = parsingData.WeaponeList[i].WeaponeName;
                        itemIconPath = parsingData.WeaponeList[i].ItemIconPath;

                        WeaponeState = parsingData.WeaponeList[i].WeaponeState;

                        BulletRate(WeaponeState);

                        FMinDmgF = parsingData.WeaponeList[i].FMinDmgF;
                        FMinDmgE = parsingData.WeaponeList[i].FMinDmgE;
                        FMinDmg = Random.Range(FMinDmgF, FMinDmgE);

                        FMaxDmgF = parsingData.WeaponeList[i].FMaxDmgF;
                        FMaxDmgE = parsingData.WeaponeList[i].FMaxDmgE;
                        MaxDmg = Random.Range(FMaxDmgF, FMaxDmgE);

                        NMag = parsingData.WeaponeList[i].NMag;
                        NCurBullet = parsingData.WeaponeList[i].NCurBullet;
                    }
                }
            }

            /// <summary>
            /// 무기 별 연사 속도           
            /// </summary>
            /// <param name="state"></param>
            void BulletRate(int state)
            {

                //권총 연사 속도
                if (state == 0)
                {
                    fireRate = 0.2f;
                }
                //AR
                if (state == 1)
                {
                    fireRate = 0.01f;
                }
                //SG
                if (state == 2)
                {
                    fireRate = 0.5f;
                }
                if(state == 3)
                {
                    fireRate = 0.01f;
                }

            }

            /// <summary>
            /// 공격
            /// </summary>
            public void Fire()
            {   
                if (playerCtrl.IsLive &&
                    !playerCtrl.IsReload)
                {
                    #region pc 사격 버그_ 효과음과 파티클 실행과 동시에 종료

                    if (Time.time >= fireTime)
                    {
                        if (NCurBullet > 0)
                        {
                            NCurBullet--;

                            fireTime = Time.time + fireRate + Random.Range(0.0f, 0.3f);

                            playerCtrl.AniCtrl.FireAni();

                            //Debug.Log("SFX 실행 상태 : " + _audio.isPlaying);
                            //Debug.Log("Particle 실행 상태 1: " + muzzleEffect.isPlaying);

                            //소리 크기 값을 가져온다
                            _audio.volume = GameManager.INSTANCE.volume.sfx;
                            _audio.PlayOneShot(_sfx[0]);
                            muzzleEffect.Play();

                            StartCoroutine(shakeCam.ShakeCamera(0.05f, 0.1f, 0.2f));

                            //공격 데미지
                            if (WeaponeState == 0 || WeaponeState == 1 || WeaponeState == 3)
                                AttackDmg();

                            if (WeaponeState == 2)
                                AttackDmgEx();

                            playerCtrl.AimUI.FireAimAni(); //에임이 벌어진다

                            playerCtrl.PlayerUI.AmmoInfo(NCurBullet, NMag);
                        }

                        //탄이 비어있다는 효과음
                        else if (NCurBullet == 0)
                        {
                            if (Time.time >= fireTime)
                            {
                                fireTime = Time.time + fireRate + Random.Range(0.1f, 0.3f);

                                _audio.volume = GameManager.INSTANCE.volume.sfx;
                                _audio.PlayOneShot(_sfx[2]);
                            }
                        }

                    }
                    
                    #endregion

                }
            }

            /// <summary>
            /// 재장전
            /// </summary>
            public void Reload()
            {
                if (playerCtrl.IsLive && !playerCtrl.IsReload
                    && NCurBullet < NMag)
                {
                    StartCoroutine(ReloadDelay());
                }
            }

            /// <summary>
            /// 재장전 중 공격 또는 무기 교체를 못하도록 한다
            /// </summary>
            /// <returns></returns>
            IEnumerator ReloadDelay()
            {
                playerCtrl.IsReload = true;
                playerCtrl.AniCtrl.ReloadAni();

                _audio.volume = GameManager.INSTANCE.volume.sfx;
                _audio.PlayOneShot(_sfx[1]);

                if (weaponeState != 2)
                    NCurBullet = NMag;

                //샷건은 한 발만 장전
                if(weaponeState == 2)
                {
                    NCurBullet++;
                    if (NCurBullet >= NMag)
                        NCurBullet = NMag;
                }

                playerCtrl.PlayerUI.AmmoInfo(NCurBullet, NMag);
                yield return new WaitForSeconds(reloadSpeed);
                playerCtrl.IsReload = false;
            }

            /// <summary>
            /// 공격 데미지
            /// 권총과 소총
            /// 사정거리 무한
            /// 탄 퍼짐 없음
            /// </summary>
            void AttackDmg()
            {
                RaycastHit hit;
                int layerMask = 1 << 8;
                layerMask = ~layerMask;

                if (Physics.Raycast(playerCtrl.FirePosTr.position, playerCtrl.FirePosTr.forward,
                    out hit, Mathf.Infinity, layerMask))
                {
                    HitDamageValue(hit);
                    HitItemPickUp(hit);
                }
            }

            /// <summary>
            /// 데미지 적용 부분
            /// </summary>
            /// <param name="hit"></param>
            void HitDamageValue(RaycastHit hit)
            {
                Debug.DrawLine(playerCtrl.FirePosTr.position, hit.point, Color.red);

                HitEffectPlay(hit.point);

                float dmg = Random.Range(FMinDmg, MaxDmg);

                playerCtrl.PlayerUI.DmgInfoprint(dmg);
                //StartCoroutine(playerCtrl.PlayerUI.DmgInfo(dmg));

                if (hit.transform.GetComponent<HitDmg>())
                {
                    if (!hit.transform.GetComponent<HitDmg>().IsHeadDmg)
                        hit.transform.GetComponent<HitDmg>().HitDamage(dmg);

                    if (hit.transform.GetComponent<HitDmg>().IsHeadDmg)
                        hit.transform.GetComponent<HitDmg>().HeadDamage(dmg);

                    playerCtrl.PlayerUI.EnemyHpInfo(hit.transform.GetComponent<CharactersData>().Hp,
                        hit.transform.GetComponent<CharactersData>().MaxHp);
                }
            }

            /// <summary>
            /// 아이템을 공격하면 습득한다
            /// 샷건은 제외(중복 습득 가능성이 있기 때문에..)
            /// </summary>
            /// <param name="hit"></param>
            void HitItemPickUp(RaycastHit hit)
            {
                HitEffectPlay(hit.point);

                if(hit.transform.tag.Equals("Grenade"))
                {
                    playerCtrl.GetComponent<ItemManager>().NGrenadeCount++;
                    playerCtrl.GetComponent<ItemManager>().GrenadeCountText();

                    hit.transform.gameObject.SetActive(false);
                }

                if(hit.transform.tag.Equals("Recovery"))
                {
                    playerCtrl.GetComponent<ItemManager>().NRecoveryCount++;
                    playerCtrl.GetComponent<ItemManager>().RecoveryCountText();

                    hit.transform.gameObject.SetActive(false);
                }
            }

            /// <summary>
            /// 샷건(탄 퍼짐)
            /// 사정거리
            /// </summary>
            void AttackDmgEx()
            {
                RaycastHit hit;
                int layerMask = 1 << 8;
                layerMask = ~layerMask;

                //8번 랜덤 방향으로 발사
                for (int i = 0; i < 8; i++)
                {
                    if (Physics.Raycast(playerCtrl.FirePosTr.position, playerCtrl.FirePosTr.forward + Random.onUnitSphere * 0.2f,
                    out hit, 500))
                    {
                        HitDamageValue(hit);

                    }

                }

            }

            /// <summary>
            /// 사격 지점 이펙트 효과
            /// </summary>
            /// <param name="hitPos"></param>
            void HitEffectPlay(Vector3 hitPos)
            {
                hitEffect.transform.position = hitPos;

                for(int i=0;i< hitParticle.Length;i++)
                {
                    hitParticle[i].Play();
                }
            }


            /// <summary>
            /// PC 사격 효과음 및 파티클
            /// 버그 테스트
            /// </summary>
            /// <param name="i"></param>
            public void TestSfx(int i)
            {
                Debug.Log("Test Sfx");
                //_audio.PlayOneShot(_sfx[i]);
                //muzzleEffect.Play();
                StartCoroutine(ReloadDelay());
            }


        }

    }
}
