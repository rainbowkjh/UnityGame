using _Item;
using CameraRig;
using Characters;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 특정 지역 도달 시 활성화
/// 보스 캐릭터 클리어 시 
/// 아이템 습득과 스테이지 종료
/// 
/// 보스 캐릭터가 나오고
/// 전투가 시작하기 전에 간단한 이벤트 연출
/// 
/// m_objBoss - 여러명이 될수 있기 때문에 배열로 선언
///           - HP가 0이 되거나 비활성화 되면 클리어 된것으로 하여
///           - 아이템을 받고 클리어가 되도록 함
/// </summary>

public class BossAct : MonoBehaviour
{
    [SerializeField, Header("Boss 캐릭터")]
    GameObject[] m_objBoss;

    //[SerializeField, Header("보스전 이벤트 카메라")]
    //GameObject m_objCam;

    /// <summary>
    /// 플레이어 정보 저장을 위해
    /// </summary>
    [SerializeField,Header("클리어 시 플레이어 정보 저장")]
    CharactersData playerData;

    /// <summary>
    /// 보스전이 시작하면 HP UI를 활성화 시킨다
    /// </summary>
    [SerializeField, Header("Boss HP UI")]
    GameObject[] m_objHPUI;

    /// <summary>
    /// 인벤토리에 아이템 생성 시 아이템 데이터 초기화를 위해 인벤을 활성화 시켜준다
    /// </summary>
    [SerializeField, Header("활성화 시켜줄 인벤")]
    GameObject m_InventoryObj;

    [SerializeField, Header("클리어 후 로비 이동 버튼")]
    GameObject m_LobbyBtn;

    /// <summary>
    /// 보스전 시작(클리어 조건 확인)
    /// </summary>
    bool isStart = false;

    /// <summary>
    /// 클리어 시 아이템을 받고
    /// 씬을 넘어감
    /// </summary>
    bool isClear = false;

    bool isSave = false;

    [SerializeField, Header("스테이지 클리어 보상(아이템 아이콘 프리펩)")]
    GameObject m_IconPefabs;

    [SerializeField, Header("스테이지 클리어 보상(아이템 이름")]
    string[] m_ClearItemName;

    [SerializeField, Header("Boss Event Cam")]
    CameraEventAct m_EventCam;


    private void Start()
    {        
        BossUIOff();
        BossInit();

        m_InventoryObj = GameObject.Find("GameManager/Canvas/Setting/WeaponeBtnManager");
        m_InventoryObj.SetActive(false);

        m_LobbyBtn.SetActive(false);
    }

    private void Update()
    {
        //보스전이 시작하면
        //보스 캐릭터의 생존 상태를 확인 한다
        if(isStart)
        {
            //클리어 조건을 검사하기 위해
            //true값으로 변경 시킨다
            isClear = true;

            for (int i = 0; i < m_objBoss.Length; i++)
            {
                //한명이라도 생존 상태이면
                //클리어 상태를 false로 변경
                if (m_objBoss[i].GetComponent<CharactersData>().FHP > 0)
                {
                    isClear = false;
                    break;
                }
            }

            //위 조건에 걸리지 않으면
            //클리어 된 것으로 함
            if(isClear)
            {
                StartCoroutine(StageClear());
            }
        }

    }


    /// <summary>
    /// 플레이어 도착 감지
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(!isStart)
        {
            if (other.tag.Equals("Player"))
            {
                BossCreate();
                StartCoroutine(m_EventCam.BossEventCam());
            }
        }
    }

    /// <summary>
    /// Boss 활성화
    /// </summary>
    void BossCreate()
    {
        for(int i=0;i< m_objBoss.Length;i++)
        {
            m_objBoss[i].SetActive(true);
            BossUIOn(i);

            isStart = true;
        }
    }

   void BossInit()
    {
        for(int i=0;i<m_objBoss.Length;i++)
        {
            m_objBoss[i].SetActive(false);
        }
    }

    #region UI
    void BossUIOff()
    {
        for(int i=0;i<m_objHPUI.Length;i++)
        {
            m_objHPUI[i].SetActive(false);
        }
    }

    void BossUIOn(int index)
    {
        m_objHPUI[index].SetActive(true);
        //Debug.Log("Boss HP UI 활성화");
    }
    #endregion

    IEnumerator StageClear()
    {
    
        if(!isSave)
        {
            isSave = true;

            //인벤을 연다
            //(인벤 활성화 시 아이템 정보 로드가 됨..
            //인벤 열기 전 아이템을 넣으면(부정 행위??)
            //인벤을 열떄 증발 ㅋㅋ)
            //인벤을 열고 스테이지 클리어 보상 아이템을 받도록 한다
            m_InventoryObj.SetActive(true);
            

            bool isInven = InventoryList.INVENTORY.InvetoryCheck(); //인벤에 아이템 습득 가능한지 확인

            #region 아이템 습득
            ////습득 가능하면 아이템 얻음
            if (isInven)
            {
                GameObject obj = Instantiate(m_IconPefabs);

                //보상 받을 아이템의 이름을 뽑는다
                int rand = Random.Range(0, m_ClearItemName.Length - 1);

                obj.GetComponent<ItemData>().SItemName = m_ClearItemName[rand]; //이름을 적용 시키고 인벤에 넣는다
                obj.GetComponent<ItemData>().isEquip = false;
                obj.GetComponent<ItemData>().ItemSetting();
                

                InventoryList.INVENTORY.AddItem(obj.GetComponent<ItemData>());

                Debug.Log("습득 아이템 : " + m_ClearItemName[rand]);
                Debug.Log("아이템 습득 완료: " + obj.GetComponent<ItemData>().SItemName);

            }
            #endregion

            #region 캐릭터 저장(스테이지 시작할때 코드와 같다..;; 중복 코드이니 수정)
            //캐릭터 정보를 저장 후 메인으로 넘어간다
            //게임이 시작 되기전에 플레이어 데이터를 저장한다
            if (GameManager.INSTANCE.isMale)
            {
                if (InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() != null)
                {
                    //무기 장착 슬롯으 0 인덱스
                    GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                        InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>().WeaponeData, 1);

                    Debug.Log("남 무기 장착 저장");
                }

                else if (InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() == null)
                {
                    GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                        null, 1);

                    Debug.Log("남 무기 없음 저장");
                }
                //Debug.Log("데이터 저장");
            }

            else
            {
                if (InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() != null)
                {
                    GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                    InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>().WeaponeData, 0);

                    Debug.Log("여 무기 장착 저장");
                }

                else if (InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() == null)
                {
                    GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                        null, 0);

                    Debug.Log("여 무기 없음 저장");
                }
            }
            #endregion

        }


        yield return new WaitForSeconds(3.0f);

        //  SceneManager.LoadScene("Main");
        m_LobbyBtn.SetActive(true);
    }

    public void LobbySceneLoad()
    {
        m_InventoryObj.SetActive(false);
        GameManager.INSTANCE.gameSystem.isPlayScene = false;
        SceneManager.LoadScene("Main");
    }


}
