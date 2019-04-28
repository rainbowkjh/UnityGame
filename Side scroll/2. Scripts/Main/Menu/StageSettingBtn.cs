using _Item;
using Characters;
using Manager;
using Manager.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 스테이지 선택 버튼
/// 좌, 우 클릭 으로 스테이지를 선택 하고
/// 선택할때 사용 한 인덱스 값을 이용하여
/// 플레이 할 스테이지 멥을 불러온다
/// </summary>
namespace MainScene
{
    namespace Menu
    {
        public class StageSettingBtn : MonoBehaviour
        {
            int m_nStageIndex = 0;
            [SerializeField,Header("선택할 스테이지 정보가 담긴 이미지")]
            GameObject[] m_StageInfo;

            [SerializeField, Header("게임 시작 시 저장 시킬 플레이어")]
            CharactersData playerData;

            //[SerializeField, Header("인벤토리 정보")]
            //InventoryList inventory;

            //[SerializeField,Header("장착슬롯의 정보를 가져와야 한다")]
            //WeaponeSettingBtn m_EquipWeapone;

            [SerializeField, Header("게임이 시작되기전 인벤 내용을 로드해준다")]
            GameObject m_InvenObj;

            private void Start()
            {
                SpriteAct();
            }

            /// <summary>
            /// 스테이지 인덱스 값을 변경 시킨다
            /// </summary>
            public void StageLeft()
            {
                if(m_nStageIndex>0)
                {
                    m_nStageIndex--;
                }
                SpriteAct();
            }

            public void StageRight()
            {
                if(m_nStageIndex < m_StageInfo.Length- 1)
                {
                    m_nStageIndex++;
                }
                SpriteAct();
            }

            /// <summary>
            /// 현재 인덱스에 해당하는 스테이지 정보 활성화
            /// </summary>
            void SpriteAct()
            {
                for(int i=0;i< m_StageInfo.Length;i++)
                {
                    m_StageInfo[i].SetActive(false);
                }

                m_StageInfo[m_nStageIndex].SetActive(true);
            }

            /// <summary>
            /// 선택된 스테이지 시작
            /// 플레이어 정보를 게임 메니져에 임시 저장
            /// 로드되면 다시 초기화 시킴
            /// </summary>
            public void StageStart()
            {
              //  m_InvenObj.SetActive(true); //인벤 정보를 저장하기 위해 활성화를 한번 해준다//인벤 닫을 때 저장하면서 필요 없어짐

                //게임이 시작 되기전에 플레이어 데이터를 저장한다
                if (GameManager.INSTANCE.isMale)
                {
                    if(InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() != null)
                    {
                        //무기 장착 슬롯으 0 인덱스
                        GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                            InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>().WeaponeData, 1);

                        //Debug.Log("남 무기 장착 저장");
                    }

                    else if(InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() == null)
                    {
                        GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                            null, 1);

                       // Debug.Log("남 무기 없음 저장");
                    }
                    //Debug.Log("데이터 저장");
                }

                else
                {
                    if (InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() != null )
                    {
                        GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                        InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>().WeaponeData, 0);

                      //  Debug.Log("여 무기 장착 저장");
                    }
                    
                    else if(InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() == null)
                    {
                        GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                            null, 0);

                      //  Debug.Log("여 무기 없음 저장");
                    }
                }


                gameObject.SetActive(false);
                GameManager.INSTANCE.gameSystem.isPlayScene = true;
                SceneManager.LoadScene("Stage" + m_nStageIndex);
            }
        }

    }
}
