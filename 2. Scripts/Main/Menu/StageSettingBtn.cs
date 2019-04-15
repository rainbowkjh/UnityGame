using _Item;
using Characters;
using Manager;
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

            [SerializeField, Header("인벤토리 정보")]
            InventoryList inventory;

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
                    //Debug.Log("인벤 저장 전");
                    ////인벤 정보를 저장한다
                    //playerData.ItemList = inventory.GetInventoryItem();
                    //Debug.Log("저장 후");

                    GameManager.INSTANCE.gameData.Save(playerData, 1);
                    Debug.Log("데이터 저장");
                }

                else
                {   
                    GameManager.INSTANCE.gameData.Save(playerData, 0);
                }

                SceneManager.LoadScene("Stage" + m_nStageIndex);
            }
        }

    }
}
