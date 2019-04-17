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
                //게임이 시작 되기전에 플레이어 데이터를 저장한다
                if (GameManager.INSTANCE.isMale)
                {
                    if(InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() != null)
                    {
                        //무기 장착 슬롯으 0 인덱스
                        GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                            InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>().WeaponeData, 1);                        
                    }

                    else if(InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() == null)
                    {
                        GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                            null, 1);
                    }
                    Debug.Log("데이터 저장");
                }

                else
                {
                    if (InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() != null )
                    {
                        GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                        InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>().WeaponeData, 0);
                    }
                    
                    else if(InventoryList.INVENTORY.TrEquipTr[0].GetComponentInChildren<ItemData>() == null)
                    {
                        GameManager.INSTANCE.gameData.Save(playerData, InventoryList.INVENTORY.GetWeaponeItem(),
                            null, 0);
                    }
                }

                gameObject.SetActive(false);
                SceneManager.LoadScene("Stage" + m_nStageIndex);
            }
        }

    }
}
