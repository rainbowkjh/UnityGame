using Black.Characters;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기 데이터 관리(캐릭터 무기, 총 무기의 상위 클래스)
/// 
/// 무기 속성 추가 시
/// XML에 항목 추가
/// ParsingData에 관련 구조체이 항목 추가
/// 파싱 데이터 받아 리스트 생성
/// 
/// 해당 오브젝트에 데이터 적용
/// 
/// 각 해당하는 아이템의 아이콘 데이터 클래스에도 데이터 추가
/// 무기 속성의 경우
/// PartsItemData가 해당한다.
/// 
/// 그리고 아이콘을 슬롯에 장착 및 해제 시 수치 중첩 코드 추가
/// 
/// 밑에 클래스에 항목 추가
/// (속성의 경우 무기에서 속성 값을 탄에 전달하기 위해 추가 시킨다)
/// 밑에 탄 발사 부분에서
/// 속성 관련 데이터를 탄에 보냄
/// 
/// 탄 충돌 시 해당 속성 관련 실행 시킴
/// 
/// 아이템 정보 UI 항목 추가
/// 
/// </summary>
namespace Black
{
    namespace Weapone
    {
        public class WeaponeData : MonoBehaviour
        {
            [SerializeField, Header("전용 무기이므로 캐릭터를 연결 시킨다")]
            protected CharactersAniCtrl aniCtrl;
            [SerializeField]
            protected PlayerCtrl player;

           protected MemoryPooling pool;

            [SerializeField, Header("속성에 따라 오브젝트 바뀜(실제 적용)")]
            eProjectilesType bulletType;
            [SerializeField, Header("기본 발사체 세팅")]
            eProjectilesType bulletTypeOrigin; //초기값 저장

            protected GameObject obj;


            [SerializeField]
            ParticleSystem muzzle;

            [SerializeField,Header("발사체를 퍼트리기 위해 배열 사용, 한발 직진은 크기 1")]
            protected Transform[] firePos;

            [SerializeField] string weaponeName;
            [SerializeField] eWeaponeType weaponeType; //무기 타입
            [SerializeField] int weaponeAniID; //무기 타입에 따른 애니메이션 값
            [SerializeField] float fireRate; //사격 속도
            [SerializeField] float fAcc; //정확도
            [SerializeField] int nBullet; //현재 탄수
            [SerializeField] int nMaxMag; //재장전 후 탄 수
            [SerializeField] int nMinDmg; //최소 데미지
            [SerializeField] int nMaxDmg; //최대 데미지
            [SerializeField] float fBulletSpeed; //탄 속도
            [SerializeField] float fBulletDis; //탄 비활성화 시간

            // 속성
            [SerializeField] bool isExplosion; //폭발탄 속성
            [SerializeField] float fExplosionArea; //폭발 범위
            [SerializeField] bool isStun; //기절효과
            [SerializeField] float fStunPer; //기절효과 확률

            [SerializeField] int nExplosionSlot; //슬롯에 파트가 장착된 수(해제 시 상태 변경 전 체크하기 위해서.. 0이면 false)
            [SerializeField] int nStunSlot; //슬롯에 파트가 장착된 수(해제 시 상태 변경 전 체크하기 위해서.. 0이면 false)

            #region Set,Get
            public string WeaponeName { get => weaponeName; set => weaponeName = value; }
            public eWeaponeType WeaponeType { get => weaponeType; set => weaponeType = value; }
            public int WeaponeAniID { get => weaponeAniID; set => weaponeAniID = value; }
            public float FireRate { get => fireRate; set => fireRate = value; }
            public float FAcc { get => fAcc; set => fAcc = value; }
            public int NBullet { get => nBullet; set => nBullet = value; }
            public int NMaxMag { get => nMaxMag; set => nMaxMag = value; }
            public int NMinDmg { get => nMinDmg; set => nMinDmg = value; }
            public int NMaxDmg { get => nMaxDmg; set => nMaxDmg = value; }
            public float FBulletSpeed { get => fBulletSpeed; set => fBulletSpeed = value; }
            public bool IsExplosion { get => isExplosion; set => isExplosion = value; }
            public bool IsStun { get => isStun; set => isStun = value; }
            public float FStunPer { get => fStunPer; set => fStunPer = value; }

            public int NExplosionSlot { get => nExplosionSlot; set => nExplosionSlot = value; }
            public int NStunSlot { get => nStunSlot; set => nStunSlot = value; }
            public float FExplosionArea { get => fExplosionArea; set => fExplosionArea = value; }
            public eProjectilesType BulletType { get => bulletType; set => bulletType = value; }
            public eProjectilesType BulletTypeOrigin { get => bulletTypeOrigin; set => bulletTypeOrigin = value; }
            public float FBulletDis { get => fBulletDis; set => fBulletDis = value; }
            public ParticleSystem Muzzle { get => muzzle; set => muzzle = value; }
            public AudioSource _Audio { get => _audio; set => _audio = value; }
            public AudioClip[] _Sfx { get => _sfx; set => _sfx = value; }
            #endregion


            AudioSource _audio;
            [SerializeField,Header("0 공격, 1 스킬1 / 재장전 2 스킬2 / 탄 비웠음, 3 스킬3")]
            AudioClip[] _sfx;

            private void Start()
            {
                pool = GameObject.Find("MemoryPool").GetComponent<MemoryPooling>();
                _Audio = GetComponent<AudioSource>();
            }

            protected IEnumerator ReloadDelay()
            {
                GameManager.INSTANCE.SFXPlay(_Audio, _Sfx[1]);

                player.IsReload = true;
                aniCtrl.AniReload();

                yield return new WaitForSeconds(1.5f);
                player.IsReload = false;

                if(NBullet < NMaxMag && player.NAmmo > 0)
                {
                    while(NBullet < NMaxMag)
                    {
                        NBullet++;
                        player.NAmmo--;

                        if(player.NAmmo <=0 || NBullet >= NMaxMag)
                        {
                            break;
                        }
                    }
                }
                
            }

            /// <summary>
            /// 플레이어의 총 스크립트에서
            /// 발사 시 true값을 준다
            /// 적 캐릭터의 총 발사는 false를
            /// 주어 탄에 충돌 체크 시 해당 태그만 체크 하도록 한다.
            /// </summary>
            /// <param name="isPlayer"></param>
            public void BulletSetting(bool isPlayer)
            {
                for (int i = 0; i < firePos.Length; i++)
                {
                    switch (BulletType)
                    {
                        case eProjectilesType.BubbleBlue:
                            obj = pool.GetObjPool(pool.BubbleBluetMaxCount, pool.BubbleBlueList);
                            break;

                        case eProjectilesType.BubbleRose:
                            obj = pool.GetObjPool(pool.BubbleRoseMaxCount, pool.BubbleRoseList);
                            break;

                        case eProjectilesType.Feather:
                            obj = pool.GetObjPool(pool.FeatherMaxCount, pool.FeatherList);
                            break;

                        case eProjectilesType.CometBlue:
                            obj = pool.GetObjPool(pool.CometBlueMaxCount, pool.CometBlueList);
                            break;

                        case eProjectilesType.FireBall:
                            obj = pool.GetObjPool(pool.FireBallCount, pool.FireBallList);
                            break;

                        case eProjectilesType.Spark:
                            obj = pool.GetObjPool(pool.SparkCount, pool.SparkList);
                            break;

                        case eProjectilesType.AttackExplosion:
                            obj = pool.GetObjPool(pool.AttackExplosionCount, pool.AttackExplosionList);
                            break;
                    }

                    if (obj != null)
                    {
                        obj.transform.position = firePos[i].transform.position;
                        obj.transform.rotation = firePos[i].transform.rotation;
                        obj.SetActive(true);

                        ProjectilesMove objData = obj.GetComponent<ProjectilesMove>();

                        //탄의 속성값 적용
                        if (isPlayer)
                        {
                            objData.MinDmg = NMinDmg + player.NBuffDmg; //최소 데미지 + 버프 데미지(기본 0)
                            objData.MaxDmg = NMaxDmg + player.NBuffDmg; //최대 데미지 + 버프 데미지
                        }

                        else if (!isPlayer)
                        {
                            objData.MinDmg = NMinDmg; //최소 데미지 
                            objData.MaxDmg = NMaxDmg; //최대 데미지 
                        }
                        objData.MoveSpeed = FBulletSpeed; //속도
                        objData.IsExplosion = IsExplosion; //폭발 속성 유무 100%
                        objData.FExplosionArea = FExplosionArea; //폭발 범위
                        objData.IsStun = IsStun; //기절 속성 유무
                        objData.FStunPer = FStunPer; //기절 속성 확률
                        objData.FBulletDis = FBulletDis;

                        /*
                             obj.GetComponent<ProjectilesMove>().MinDmg = NMinDmg;
                             obj.GetComponent<ProjectilesMove>().MaxDmg = NMaxDmg;
                             obj.GetComponent<ProjectilesMove>().MoveSpeed = FBulletSpeed;
                         */


                        obj.GetComponent<ProjectilesMove>().IsPlayerBullet = isPlayer;

                    }


                }
            }


        }
    }
}