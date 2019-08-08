using Black.Inventory;
using Black.Manager;
using Black.Weapone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적 캐릭터 컨트롤
/// 플레이어가 특정 위치에 가면
/// 활성화 되어 나타나며
/// 무조건 플레이어를 추적한다
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class EnemyCtrl : CharactersData
        {
            //플레이어 추적을 위한 위치 값
            Transform targetTr;
            protected PlayerCtrl player;

            [SerializeField, Header("광역 피해 받을때 / 데미지 수치 출력 위치")]
            Transform hitInfo;

            [SerializeField, Header("공격 범위")]
            float attackDis;

            [SerializeField, Header("달리기 범위")]
            float runDis;

            ParsingData parsingData;

            public Transform _HitInfo { get => hitInfo; set => hitInfo = value; }
            public Transform TargetTr { get => targetTr; set => targetTr = value; }
            public float AttackDis { get => attackDis; set => attackDis = value; }
            public EnemyWeaponeCtrl EnemyWeapone { get => enemyWeapone; set => enemyWeapone = value; }
            public ParticleSystem DisableEffect { get => disableEffect; set => disableEffect = value; }
            public float RunDis { get => runDis; set => runDis = value; }

            [SerializeField, Header("무기 스크립트")]
            EnemyWeaponeCtrl enemyWeapone;

            protected AudioSource _audio;
            [SerializeField, Header("0 등장 효과음, 1 사라질때 효과음")]
            protected AudioClip[] _sfx;
            [SerializeField, Header("사라질때 이펙트")]
            ParticleSystem disableEffect;

           protected Manager.QuestClear questBoss;

            //적 쓰러질때 드랍 아이템
            [SerializeField]
            AmmoObj ammo;
            [SerializeField]
            MoneyObj money;
            [SerializeField]
            PartsMaterialObj partsMat;
            [SerializeField]
            MaterialObj material;

            [SerializeField]
            ItemObj bagItemObj;
            [SerializeField]
            PartsObj partsItemObj;
            [SerializeField]
            SubItemObj subItemObj;

            [SerializeField, Header("떨어뜨릴 최대 수치")]
            int dropMaxMoney;
            [SerializeField, Header("떨어뜨릴 최대 수치")]
            int dropMaxAmmo;
            [SerializeField, Header("떨어뜨릴 최대 수치")]
            int dropMaxMaterial;
            [SerializeField, Header("떨어뜨릴 최대 수치")]
            int dropMaxParts;
            /// <summary>
            /// 캐릭터 등장 효과음
            /// Enable에 넣으면 코드 순서가 어긋나는거 같다;;
            /// Move 함수에서 에러 발생으로
            /// 간단한 해결 법으로 변수를 만들어 제어
            /// </summary>
            protected bool isStart = true;

            /// <summary>
            /// 아이템이 계속 드랍 되는것을 막는다
            /// </summary>
            bool isItemDrop = false;

            [SerializeField, Header("Main Boss 사용")]
            protected bool isBoss = false;

            [SerializeField, Header("Sub Boss 사용")]
            protected bool isSubBoss = false;


           virtual protected void Start()
            {
                TargetTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
                player = targetTr.GetComponent<PlayerCtrl>();
                _audio = GetComponent<AudioSource>();
                parsingData = GameObject.Find("ParsingData").GetComponent<ParsingData>();

                questBoss = GetComponent<Manager.QuestClear>();
            }
                    

            /// <summary>
            /// 적 캐릭터를 이동 상태로 한다        
            /// </summary>
           protected void EnemyMove(Transform tr)
            {
                //피격으로 경직 되지 않았을때 이동
                if(!IsHit && IsLive)
                {
                    Nav.destination = tr.position; //타겟을 목적지로 지정
                    Nav.isStopped = false;
                    Nav.speed = FSpeed * 1;
                    IsRun = false;
                    IsWalk = true;
                }
                
            }

            protected void EnemyRun(Transform tr)
            {
                //피격으로 경직 되지 않았을때 이동
                if (!IsHit && IsLive)
                {
                    Nav.destination = tr.position; //타겟을 목적지로 지정
                    Nav.isStopped = false;
                    Nav.speed = FSpeed * 2;
                    IsRun = true;
                    IsWalk = false;
                }

            }

            /// <summary>
            /// 제자리에 정지 시킨다
            /// </summary>
            public void EnemyStop()
            {              
                Nav.isStopped = true;
                Nav.velocity = Vector3.zero;
                //IsRun = false;
                IsWalk = false;
            }
            //
           protected void DropItem()
            {
                if(!isItemDrop)
                {
                    isItemDrop = true;

                    int ran = Random.Range(0, 20);

                    //Money
                    if (ran < 2)
                    {
                        MoneyObj obj = Instantiate(money);
                        obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
                        obj.MinValue = 50;
                        obj.MaxValue = dropMaxMoney;
                    }
                    //ammo
                    else if (ran >= 2 && ran < 6)
                    {
                        AmmoObj obj = Instantiate(ammo);
                        obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
                        obj.MinValue = 50;
                        obj.MaxValue = dropMaxAmmo;
                    }
                    //Material
                    else if (ran >= 6 && ran < 8)
                    {
                        MaterialObj obj = Instantiate(material);
                        obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
                        obj.MinValue = 50;
                        obj.MaxValue = dropMaxMaterial;
                    }
                    //PartsMaterial
                    else if (ran >= 8 && ran <= 10)
                    {
                        PartsMaterialObj obj = Instantiate(partsMat);
                        obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
                        obj.MinValue = 50;
                        obj.MaxValue = dropMaxParts;
                    }
                    //Bag
                    else if (ran > 10 && ran <= 13)
                    {
                        ItemObj obj = Instantiate(bagItemObj);
                        obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);

                        int itemID = Random.Range(0, parsingData.BagItemList.Count);

                        for (int j = 0; j < parsingData.BagItemList.Count; j++) //리스트를 돌면서 해당 아이디의 아이템의 값을 가져와 적용
                        {
                            if (parsingData.BagItemList[j].id.Equals(itemID))
                            {
                                //해당 아이디의 아이템 이름(아이템 이름 지정이 아니고
                                //아이템을 생성하기 위해 이름을 작성 시키는 부분)을 가져와 적용 시킨다
                                //적용 시킨후 Start를 접근하면서 초기화 시킨다
                                obj.GetComponent<ItemObj>().itemName = parsingData.BagItemList[j].name;

                            }
                        }

                    }
                    //Parts
                    else if (ran > 13 && ran <= 15)
                    {
                        PartsObj obj = Instantiate(partsItemObj);
                        obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);

                        int itemID = Random.Range(0, parsingData.PartsItemList.Count);

                        for (int j = 0; j < parsingData.PartsItemList.Count; j++) //리스트를 돌면서 해당 아이디의 아이템의 값을 가져와 적용
                        {
                            if (parsingData.PartsItemList[j].id.Equals(itemID))
                            {
                                obj.GetComponent<PartsObj>().itemName = parsingData.PartsItemList[j].name; //해당 아이디의 아이템 이름을 가져온다

                            }
                        }
                    }
                    //Sub
                    else if (ran > 15 && ran <= 17)
                    {
                        SubItemObj obj = Instantiate(subItemObj);
                        obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);

                        int itemID = Random.Range(0, parsingData.SubItemList.Count);

                        for (int j = 0; j < parsingData.SubItemList.Count; j++) //리스트를 돌면서 해당 아이디의 아이템의 값을 가져와 적용
                        {
                            if (parsingData.SubItemList[j].id.Equals(itemID))
                            {
                                obj.GetComponent<SubItemObj>().itemName = parsingData.SubItemList[j].name; //해당 아이디의 아이템 이름을 가져온다                                
                            }
                        }
                    }
                }
                
                
            }
            //
        
            public void EnableObj()
            {
                DisableEffect.Play();
                Manager.GameManager.INSTANCE.SFXPlay(_audio, _sfx[1]);
            }
            

        }
        //
    }
}

