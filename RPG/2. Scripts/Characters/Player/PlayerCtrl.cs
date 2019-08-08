using Black.Manager;
using Black.PlayerUI;
using Black.Weapone;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 캐릭터 제어
/// 
/// 메인에서 선택된 캐릭터를 
/// Scene에 생성 시킴
/// 
/// 키 입력 코드 제어
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class PlayerCtrl : CharactersData
        {
            [SerializeField, Header("마우스 클릭 유효 범위")]
            public LayerMask layerMask;

            [SerializeField]
            private WeaponeManager weaponeM;
            RaycastHit hit;

            bool isRunMod = false;

            [SerializeField, Header("스킬 이펙트")]
            ParticleSystem[] skillEffect;

            /// <summary>
            /// 스킬 마나 사용량
            /// 파싱에 받는다
            /// </summary>
            private float[] useMana = new float[3];

            [SerializeField, Header("인벤토리")]
            CanvasGroup invenCG;
            bool isInven = false;

            [SerializeField, Header("소모품 퀵 슬롯")]
            UseItemSlot useSlot;

            #region Set, Get
            public WeaponeManager WeaponeM { get => weaponeM; set => weaponeM = value; }
            public bool IsInven { get => isInven; set => isInven = value; }
            public MemoryPooling Pool { get => pool; set => pool = value; }
            public Transform GrenadeThrowPos { get => GrenadeThrow; set => GrenadeThrow = value; }
            public Transform _DmgUI { get => dmgUI; set => dmgUI = value; }
            public ShakeCamera ShakeCam { get => shakeCam; set => shakeCam = value; }
            public float[] UseMana { get => useMana; set => useMana = value; }
            public List<bool> StageList { get => stageList; set => stageList = value; }
            #endregion

            //Pool
            MemoryPooling pool;

            [SerializeField, Header("수류탄 던지는 위치")]
            Transform GrenadeThrow;

            [SerializeField,Header("InvenBtn에 있는 UI")]
            InfoTextUI infoUI;

            [SerializeField,Header("캐릭터 전용 무기")]
            CharWeaponeCtrl charWeaponeCtrl;

      
            [SerializeField, Header("데미지 출력 위치")]
            Transform dmgUI;

            [SerializeField]
            ShakeCamera shakeCam;

            [SerializeField, Header("메뉴")]
            GameObject escMenuObj;

            UIBar fillUI;
            [SerializeField, Header("스킬 사용 시 쿨타임 UI")]
            Image[] skillFill;

            [SerializeField]
            GameOverUI overUI;

            PlayerState stateCtrl;

            /// <summary>
            /// 스테이지 클리어 상태 저장
            /// </summary>
            private List<bool> stageList = new List<bool>();

            private void Start()
            {
                InventoryInit();
                Pool = GameObject.Find("MemoryPool").GetComponent<MemoryPooling>();
                stateCtrl = GetComponent<PlayerState>();
                escMenuObj.SetActive(false);

                fillUI = GetComponent<UIBar>();
                PlayerDataInit();

                fillUI.HpBar();
                fillUI.ManaBar();
                fillUI.ThirstBar();
                fillUI.SatietyBar();
            }

            void Update()
            {
                EscMenuCall(); //Esc Menu

                infoUI.PlayerInfo(); //UI

                if(!GameManager.INSTANCE.isMenu)
                {
                    if (IsLive && !IsRoll && !IsSkill && !IsInven && !IsStun && !IsChestOpen 
                        && !GameManager.INSTANCE.IsEvent)
                    {
                        TestCheat(); //Cheat

                        PlayerLook(); //정지 상태에서 바라보는 방향
                        PlayerMove(); //캐릭터 이동
                        PlayerStop(); //정지
                        PlayerWeaponeChange(); //무기 교체
                        PlayerRoll(); //구르기(회피)
                        PlayerNoWeapone(); //무기 미장착 기본 공격 및 스킬

                        QuickSlotItem(); //퀵 슬롯 아이템 사용                 
                    }

                    if (IsLive && !IsRoll && !IsSkill
                        && !GameManager.INSTANCE.IsEvent)
                    {
                        InvenKey(); //인벤 사용                
                    }

                    //마나 증가
                    stateCtrl.ManaIncrease(1);
                    //배고품 수치와 목마름 수치 감소
                    stateCtrl.ThirstDecrease();
                    stateCtrl.SatietyDecreaseHP();

                    fillUI.HpBar();
                    fillUI.ManaBar();
                    fillUI.ThirstBar();
                    fillUI.SatietyBar();

                }
                
                if(!IsLive)
                {
                    AniCtrl.AniWeaponeID(0); //쓰러질때 들고 있는 총 애니메이션 버그로 첫번째 무기로 교체
                    Stop();
                    overUI.GameOverAct();
                }

                AniCtrl.AniWalk(IsWalk);
                AniCtrl.AniRun(IsRun);

            }

            /// <summary>
            /// 캐릭터 이동
            /// 마우스 클릭 부분
            /// </summary>
            void PlayerMove()
            {
                if(!IsAttack && 
                    !GameManager.INSTANCE.isSceneMove)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        isRunMod = !isRunMod;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                        {
                            MouseEffect(hit); //마우스 클릭 이펙트

                            Nav.isStopped = false; //네비를 이동 시킨다
                            Nav.destination = hit.point; //목적지

                            if (isRunMod) //캐릭터 달리기 상태 체크(애니메이션)
                            {
                                IsWalk = false;
                                IsRun = true;

                                Nav.speed = FSpeed * 2f;
                            }
                            else
                            {
                                IsWalk = true;
                                IsRun = false;

                                Nav.speed = FSpeed * 1f;
                            }
                            
                        }
                    }

                }

                //목마름 상태 0이면 달리지 못한다
                if (FThirst <= 0)
                {
                    isRunMod = false;
                }
            }

            /// <summary>
            /// 마우스 클릭 지점을 매개변수로 받아서
            /// 해당 위치에 이펙트를 실행 시킨다
            /// 0.5초후 비활성화 시킴
            /// </summary>
            /// <param name="hit"></param>
            void MouseEffect(RaycastHit hit)
            {
                ParticleSystem obj = Pool.GetParticlePool(Pool.mouseCount, Pool.mouseEffectList); //사용할수 있는 파티클 확인 

                if(obj != null)
                {
                    obj.transform.position = hit.point; //위치를 지정 
                    obj.gameObject.SetActive(true); //이펙트(풀링) 활성화
                    obj.Play(); //이펙트 실행

                    StartCoroutine(Pool.ParticleFalse(obj, 0.5f)); //비 활성화(클릭 여러번 할 경우 바로 사용 할수 있도록)
                }
            }

            /// <summary>
            /// 플레이어 정지
            /// </summary>
            void PlayerStop()
            {
                if (Vector3.Distance(transform.position, hit.point) <= 1.0f) //목적지까지 거리가 1이하일때
                { 
                    Stop(); //정지 시킨다
                }
                //목적지를 향해 달리는 동안
                else
                {
                    if(isRunMod)
                    {
                        FThirst -= 2 * Time.deltaTime; //달리는 동안 목마름 수치 빠르게 감소
                    }
                }
            }

            /// <summary>
            /// 캐릭터 애니메이션 정지 상태 변경
            /// 이동을 정지(네비 이동 상태 정지)
            /// </summary>
            public void Stop()
            {
                IsWalk = false;
                IsRun = false;
                Nav.velocity = Vector3.zero;
                Nav.isStopped = true;
            }

            /// <summary>
            /// 무기 교체
            /// </summary>
            void PlayerWeaponeChange()
            {
                if (Input.GetKeyDown(KeyCode.Z) && !IsReload && !IsAttack)
                {
                    AniCtrl.AniWeaponeID(weaponeM.WeaponeChange());
                }
            }

            /// <summary>
            /// 정지 상태에서 마우스 오른쪽 클릭 시 바라보는 방향
            /// </summary>
            void PlayerLook()
            {
                //씬 이동 중이지 않을때
                //씬 이동 중 메인 카메라가 비 활성화 이기 때문에
                //에러 발생
                if(!GameManager.INSTANCE.isSceneMove)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Input.GetMouseButton(1))
                    {
                        if (!IsRun && !IsWalk)
                        {
                            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                            {
                                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                            }
                        }
                    }

                }

            }

            /// <summary>
            /// 캐릭터 구리기(회피)
            /// </summary>
            void PlayerRoll()
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        transform.LookAt(hit.point);
                    }

                    StartCoroutine(PlayerRollDelay());
                    Stop();
                }
            }

            /// <summary>
            /// 구르기 애니메이션 후 상태 전환
            /// </summary>
            /// <returns></returns>
            public IEnumerator PlayerRollDelay()
            {
                IsRoll = true;
                AniCtrl.AniRoll();
                Condition.RollEffect();

                yield return new WaitForSeconds(1.5f);
                IsRoll = false;
                Condition.RollEffectStop();
            }

            #region 총 미장착 공격
            /// <summary>
            /// 기본 공격
            /// </summary>
            void PlayerNoWeapone()
            {
                if (!WeaponeM.IsGun)
                {
                    charWeaponeCtrl.Attack();
                    SkillInput();
                }
            }


            /// <summary>
            /// 스킬 사용
            /// </summary>
            void SkillInput()
            {
                if(!IsSkill)
                {
                    if (Input.GetKeyDown(KeyCode.Q) 
                        && stateCtrl.UseMana(UseMana[0]))
                    {
                        //마우스 방향을 바라본다
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                        {
                            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                        }

                        skillEffect[0].Play(); //스킬 사용 이펙트                        
                        StartCoroutine(PlayerSkill1(1.5f)); //스킬 애니메이션 실행                        
                        StartCoroutine(charWeaponeCtrl.BulletSetting(true, 0)); //스킬 데미지 주는 이펙트 실행(파티클 트리거로 데미지를 준다)
                        StartCoroutine(ShakeCam.ShakeCamAct(0.1f, 0.5f, 0.5f)); //스킬 사용 시 카메라 흔들림

                        Stop();
                    }

                    if (Input.GetKeyDown(KeyCode.W)
                        && stateCtrl.UseMana(UseMana[1]))
                    {
                        //마우스 방향을 바라본다
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                        {
                            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                        }

                        skillEffect[1].Play();
                        StartCoroutine(PlayerSkill2(1.5f));
                        StartCoroutine(charWeaponeCtrl.BulletSetting(true, 1));
                        StartCoroutine(ShakeCam.ShakeCamAct(0.1f, 0.5f, 0.5f)); //스킬 사용 시 카메라 흔들림

                        Stop();
                    }

                    if (Input.GetKeyDown(KeyCode.E)
                        && stateCtrl.UseMana(UseMana[2]))
                    {
                        //마우스 방향을 바라본다
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                        {
                            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                        }

                        skillEffect[2].Play();
                        StartCoroutine(PlayerSkill3(1.5f));
                        StartCoroutine(charWeaponeCtrl.BulletSetting(true, 2));
                        StartCoroutine(ShakeCam.ShakeCamAct(0.1f, 0.5f, 0.5f)); //스킬 사용 시 카메라 흔들림

                        Stop();
                    }
                }
                
            }

            /// <summary>
            /// 스킬 사용
            /// </summary>
            /// <param name="delay"></param>
            /// <returns></returns>
            public IEnumerator PlayerSkill1(float delay)
            {
                IsSkill = true;
                skillFill[0].fillAmount = 0;
                AniCtrl.AniSkill1();

                yield return new WaitForSeconds(delay);

                IsSkill = false;
                skillFill[0].fillAmount = 1;
            }
            
            public IEnumerator PlayerSkill2(float delay)
            {
                IsSkill = true;
                skillFill[1].fillAmount = 0;
                AniCtrl.AniSkill2();
                yield return new WaitForSeconds(delay);
                IsSkill = false;
                skillFill[1].fillAmount = 1;
            }

            public IEnumerator PlayerSkill3(float delay)
            {
                IsSkill = true;
                skillFill[2].fillAmount = 0;
                AniCtrl.AniSkill3();
                yield return new WaitForSeconds(delay);
                IsSkill = false;
                skillFill[2].fillAmount = 1;
            }

            public IEnumerator PlayerGrenade(float delay)
            {
                IsSkill = true;
                AniCtrl.AniGrenade();
                yield return new WaitForSeconds(delay);
                IsSkill = false;
            }


            #endregion

            #region Inventory
            //인벤 키 입력
            void InvenKey()
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    Stop();
                    IsInven = !IsInven;

                    InventoryInit();
                }
            }

            //키입력 시 인벤 활성화
            public void InventoryInit()
            {
                invenCG.alpha = IsInven ? 1.0f : 0.0f;
                invenCG.interactable = IsInven;
                invenCG.blocksRaycasts = IsInven;
            }
            #endregion

            #region 퀵 슬롯 (A,S,D 입력 부분)
            private void QuickSlotItem()
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    useSlot.UseItem(0);
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    useSlot.UseItem(1);
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    useSlot.UseItem(2);
                }
            }
            #endregion

            /// <summary>
            /// 게임 중 메뉴 호출
            /// </summary>
            void EscMenuCall()
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    GameManager.INSTANCE.isMenu = !GameManager.INSTANCE.isMenu;

                    if (Manager.GameManager.INSTANCE.isMenu)
                        escMenuObj.SetActive(true);
                    else
                        escMenuObj.SetActive(false);
                }
            }

            /// <summary>
            /// 플레이어 데이터 적용
            /// </summary>
            void PlayerDataInit()
            {
                NMaxUpgradeHp = GameManager.INSTANCE.loadPlayerData.nMaxUpgradeHp;
                FMaxUpgradeMana = GameManager.INSTANCE.loadPlayerData.fMaxUpgradeMana;
                FMaxUpgradeThirst = GameManager.INSTANCE.loadPlayerData.fMaxUpgradeThirst;
                FMaxUpgradeSatiety = GameManager.INSTANCE.loadPlayerData.fMaxUpgradeSatiety;

                CharName = GameManager.INSTANCE.loadPlayerData.charName;

                NMaxHp = GameManager.INSTANCE.loadPlayerData.nMaxHp;
                NHp = GameManager.INSTANCE.loadPlayerData.nHp;

                NOriginHP = NMaxHp;

                FMaxMana = GameManager.INSTANCE.loadPlayerData.fMaxMana;
                FMana = GameManager.INSTANCE.loadPlayerData.fMana;

                FMaxThirst = GameManager.INSTANCE.loadPlayerData.fMaxThirst;
                FThirst = GameManager.INSTANCE.loadPlayerData.fThirst;

                FMaxSatiety = GameManager.INSTANCE.loadPlayerData.fMaxSatiety;
                FSatiety = GameManager.INSTANCE.loadPlayerData.fSatiety;

                FWeight = GameManager.INSTANCE.loadPlayerData.fWeight;
                FMaxWeight = GameManager.INSTANCE.loadPlayerData.fMaxWeight;

                FSpeed = GameManager.INSTANCE.loadPlayerData.fSpeed;

                NAmmo = GameManager.INSTANCE.loadPlayerData.nAmmo;
                NMaxAmmo = GameManager.INSTANCE.loadPlayerData.nMaxAmmo;

                NMoney = GameManager.INSTANCE.loadPlayerData.nMoney;
                NMaterial = GameManager.INSTANCE.loadPlayerData.nMaterial;
                NPartsMaterial = GameManager.INSTANCE.loadPlayerData.nPartsMaterial;

                UseMana[0] = GameManager.INSTANCE.loadPlayerData.UseSkillMana1;
                UseMana[1] = GameManager.INSTANCE.loadPlayerData.UseSkillMana2;
                UseMana[2] = GameManager.INSTANCE.loadPlayerData.UseSkillMana3;

                stageList = GameManager.INSTANCE.loadPlayerData.stageClearList; 

                FOriginWeight = FMaxWeight;

                //무게를 적용하고 인벤에 출력 시킨다
                GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<Inventory.InventoryData>().WeightTextPrint();
            }

            #region 테스트 시 능력치 등 상승
            void TestCheat()
            {
                if(Input.GetKeyDown(KeyCode.O))
                {
                    NMaxHp = 100000;
                    FMaxMana = 100000;
                    FMaxSatiety = 100000;
                    FMaxThirst = 10000;

                    NHp = NMaxHp;
                    FMana = FMaxMana;
                    FSatiety = FMaxSatiety;
                    FThirst = FMaxThirst;
                }

                if(Input.GetKeyDown(KeyCode.P))
                {
                    NMoney += 10000000;
                    NMaterial += 10000;
                    NPartsMaterial += 10000;
                }
            }
            #endregion
        }

    }

}

