using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 캐릭터의 기본 데이터
/// </summary>
namespace Black
{
    namespace Characters
    {
        [RequireComponent(typeof(CharactersAniCtrl)),
            RequireComponent(typeof(NavMeshAgent))]
        public class CharactersData : MonoBehaviour
        {
            private string charName;
            private bool isLive = true;
            [SerializeField] private int nHp = 500;
            [SerializeField] private int nMaxHp = 1000;
            private int nOriginHP; //HP감소 디버프 걸렸을 경우 원래 값 돌리기 위해
            [SerializeField] private float fMana = 50;
            [SerializeField] private float fMaxMana = 100;
            [SerializeField] private float fThirst = 500;
            [SerializeField] private float fMaxThirst = 1000;
            [SerializeField] private float fSatiety = 500;
            [SerializeField] private float fMaxSatiety = 1000;
            [SerializeField] private float fSpeed = 1;
            //최대 수치 (업그래이드를 하고 아이템을 사용해야 최대 수치 증가) 
            private int nMaxUpgradeHp = 1000;
            private float fMaxUpgradeMana = 100;
            private float fMaxUpgradeThirst = 1000;
            private float fMaxUpgradeSatiety = 1000;

            private float fWeight = 0.0f; //소모 아이템 인벤 련재 무게
             private float fMaxWeight = 0.0f; //소모 아이템 인벤 최대 무게(업그래이드 가능 항목)
             private float fOriginWeight = 0.0f; //소모 아이템 인벤 최대 무게(업그래이드 가능 항목)
            private int nAmmo = 0; //사용 중인 탄의 수
            private int nMaxAmmo = 200; //최대 습득 탄의 수, 업그래이드 가능 항목

             private int nMoney = 0; //소지금
             private int nMaterial = 0; //업그래이드 재료
             private int nPartsMaterial = 0; //파츠 업그래이드 재료

            
            private bool isRun = false;
            private bool isWalk = false;
            private bool isRoll = false; //구르기 상태
            private bool isSkill = false; //스킬 사용
            private bool isReload = false; //재장전 중
            private bool isAttack = false; //일반 공격 
            private bool isStun = false; //기절 상태
            private bool isHit = false; //피격 시 정지 시키기 위해
            private bool isChestOpen = false; //상자 열기 상태            

            private int nBuffDmg = 0; //버프 받을때 데미지 증가량(적용 수치 업그래이드는 CharWeaponeDmg에서 한다)

            private CharactersAniCtrl aniCtrl;
            private NavMeshAgent nav;
            private ConditionEffect condition;

            #region Set Get
            public string CharName { get => charName; set => charName = value; }
            public bool IsLive { get => isLive; set => isLive = value; }
            public int NHp
            {
                get => nHp;
                set
                {
                    nHp = value;
                    if (nHp >= NMaxHp)
                        nHp = nMaxHp;
                }
            }
            public int NMaxHp
            {
                get => nMaxHp;
                set
                {
                    nMaxHp = value;

                    if (nMaxHp >= nMaxUpgradeHp)
                        nMaxHp = nMaxUpgradeHp;
                }
            }
            public float FSpeed { get => fSpeed; set => fSpeed = value; }

            public bool IsRun { get => isRun; set => isRun = value; }
            public bool IsRoll { get => isRoll; set => isRoll = value; }
            public bool IsSkill { get => isSkill; set => isSkill = value; }

            public CharactersAniCtrl AniCtrl { get => aniCtrl; set => aniCtrl = value; }
            public NavMeshAgent Nav { get => nav; set => nav = value; }
            public bool IsWalk { get => isWalk; set => isWalk = value; }
            public bool IsReload { get => isReload; set => isReload = value; }
            public float FThirst
            {
                get => fThirst;
                set
                {
                    fThirst = value;
                    if (fThirst >= fMaxThirst)
                        fThirst = fMaxThirst;
                }
            }
            public float FMaxThirst
            {
                get => fMaxThirst;
                set
                {
                    fMaxThirst = value;
                    if (fMaxThirst >= fMaxUpgradeThirst)
                        fMaxThirst = fMaxUpgradeThirst;
                }
            }
            public float FSatiety
            {
                get => fSatiety;
                set
                {
                    fSatiety = value;
                    if (fSatiety >= fMaxSatiety)
                        fSatiety = fMaxSatiety;
                }
            }
            public float FMaxSatiety
            {
                get => fMaxSatiety;
                set
                {
                    fMaxSatiety = value;
                    if (fMaxSatiety >= fMaxUpgradeSatiety)
                        fMaxSatiety = fMaxUpgradeSatiety;
                }
            }
            public float FMana
            {
                get => fMana;
                set
                {
                    fMana = value;
                    if (fMana >= fMaxMana)
                        fMana = fMaxMana;
                }
            }
            public float FMaxMana
            {
                get => fMaxMana;
                set
                {
                    fMaxMana = value;

                    if (fMaxMana >= fMaxUpgradeMana)
                        fMaxMana = fMaxUpgradeMana;
                }
            }
            public bool IsAttack { get => isAttack; set => isAttack = value; }
            public bool IsStun { get => isStun; set => isStun = value; }
            public int NBuffDmg { get => nBuffDmg; set => nBuffDmg = value; }
            public bool IsHit { get => isHit; set => isHit = value; }
            public float FWeight { get => fWeight; set => fWeight = value; }
            public float FMaxWeight { get => fMaxWeight; set => fMaxWeight = value; }
            public int NAmmo { get => nAmmo; set => nAmmo = value; }
            public int NMaxAmmo { get => nMaxAmmo; set => nMaxAmmo = value; }
            public bool IsChestOpen { get => isChestOpen; set => isChestOpen = value; }
            public int NMoney { get => nMoney; set => nMoney = value; }
            public int NMaterial { get => nMaterial; set => nMaterial = value; }
            public int NPartsMaterial { get => nPartsMaterial; set => nPartsMaterial = value; }
            public int NMaxUpgradeHp { get => nMaxUpgradeHp; set => nMaxUpgradeHp = value; }
            public float FMaxUpgradeMana { get => fMaxUpgradeMana; set => fMaxUpgradeMana = value; }
            public float FMaxUpgradeThirst { get => fMaxUpgradeThirst; set => fMaxUpgradeThirst = value; }
            public float FMaxUpgradeSatiety { get => fMaxUpgradeSatiety; set => fMaxUpgradeSatiety = value; }
            public float FOriginWeight { get => fOriginWeight; set => fOriginWeight = value; }
            public int NOriginHP { get => nOriginHP; set => nOriginHP = value; }
            public ConditionEffect Condition { get => condition; set => condition = value; }


            #endregion

            private void Awake()
            {
                aniCtrl = GetComponent<CharactersAniCtrl>();
                Nav = GetComponent<NavMeshAgent>();

                condition = GetComponent<ConditionEffect>();
            }

            public IEnumerator AttackDelay(float delay)
            {
                yield return new WaitForSeconds(0.5f);
                IsAttack = true;

                AniCtrl.AniAttack();
                yield return new WaitForSeconds(delay);
                IsAttack = false;
            }

          
        }

    }
}

