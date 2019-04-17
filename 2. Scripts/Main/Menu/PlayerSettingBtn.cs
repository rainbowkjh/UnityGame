using Characters;
using Manager;
using Manager.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainScene
{
    namespace Menu
    {
        /// <summary>
        /// 플레이어 선택 버튼
        /// </summary>
        public class PlayerSettingBtn : MonoBehaviour
        {
            /// <summary>
            /// 처음 시작시에 크기가 정해지고 변동이 없기 떄문에
            /// 배열을 사용한다
            /// </summary>
            [SerializeField, Header("캐릭터 안에 변경 시킬 외형 오브젝트")]
            GameObject[] m_objMaleSkin;
            [SerializeField, Header("캐릭터 안에 변경 시킬 외형 오브젝트")]
            GameObject[] m_objFemaleSkin;
            [SerializeField, Header("헤어가 없는 외형이 있어서.../ 0 남 1 여")]
            GameObject[] m_objHair;

            //우선 캐릭터 테이블 없이 하나의 데이터로만 적용한다
            //즉, 캐릭터 변경에도 같은 능력치(1차 완성이 끝나면 캐릭터별 능력치를 지정해준다)
            [SerializeField, Header("플레이어의 데이터를 가져오기 위해 파싱 데이터")]
            WeaponeParsing m_Parsing;
            int m_nParsingIndex = 0; //파싱된 리스트에서 가져올 인덱스 값

            [SerializeField, Header("파싱 데이터를 적용 시킬 플레이어 캐릭터")]
            CharactersData m_PlayerData;

            [SerializeField, Header("캐릭터 능력치를 보여줄 택스트")]
            Text m_txtPlayerInfo;

            private void Start()
            {
                
                SkinInit();
                PlayerParsingDataApply();              
            }

            #region 외형 초기화
            void SkinInit()
            {
                MaleSkinFalse();
                FemaleSkinFalse();

                //해당하는 오브젝트 활성화
                if (GameManager.INSTANCE.isMale)
                    m_objMaleSkin[GameManager.INSTANCE.nPlayerIndex].SetActive(true);
                else
                    m_objFemaleSkin[GameManager.INSTANCE.nPlayerIndex].SetActive(true);

                HairObj();
            }

            /// <summary>
            /// 헤어가 없는 외형이 있어서
            /// 선택에 따라 활성화 및 비활성화 시킨다
            /// </summary>
            void HairObj()
            {
                //0번쨰 외형은 헤어가 있기 떄문에
                //0이 아닌 외형 중에서 남, 여 확인 후 활성화
                if (GameManager.INSTANCE.nPlayerIndex != 0)
                {
                    if (GameManager.INSTANCE.isMale)
                    {
                        m_objHair[0].SetActive(true);
                        m_objHair[1].SetActive(false);
                    }
                    else
                    {
                        m_objHair[0].SetActive(false);
                        m_objHair[1].SetActive(true);
                    }

                }
                else
                {
                    m_objHair[0].SetActive(false);
                    m_objHair[1].SetActive(false);
                }
            }

            void MaleSkinFalse()
            {
                for (int i = 0; i < m_objMaleSkin.Length; i++)
                {
                    m_objMaleSkin[i].SetActive(false);
                }
            }

            void FemaleSkinFalse()
            {
                for (int i = 0; i < m_objFemaleSkin.Length; i++)
                {
                    m_objFemaleSkin[i].SetActive(false);
                }
            }

            #endregion

            #region 성별 선택 버튼
            /// <summary>
            /// 남 선택 버튼 클릭 시
            /// </summary>
            public void GenderMaleBtn()
            {   
                GameManager.INSTANCE.isMale = true;

                PlayerParsingDataApply();

                //외형의 종류가 서로 다르기 떄문에
                //성별 바꾸고 외형 바꾸고 하다 인덱스가 넘어 
                //발생 할수 있는 에러를 방지하기 위해 0으로 바꾼다
                GameManager.INSTANCE.nPlayerIndex = 0;                
                SkinInit();
            }

            /// <summary>
            /// 여 선택 버튼
            /// </summary>
            public void GenderFameleBtn()
            {
                GameManager.INSTANCE.isMale = false;

                PlayerParsingDataApply();

                GameManager.INSTANCE.nPlayerIndex = 0;
                SkinInit();
            }

            private void PlayerInfoText()
            {
                m_txtPlayerInfo.text = "LEVLE " + m_PlayerData.NLevel + "\n" 
                                       + "Name " + m_PlayerData.sName + "\n" 
                                       + "HP " + m_PlayerData.FHP + " / " + m_PlayerData.FMaxHP + "\n"
                                       + "Mana " + m_PlayerData.FMana + " / " + m_PlayerData.FMaxMana + "\n"
                                       + "Exp " + m_PlayerData.FExp + " / " + m_PlayerData.FNextExp + "\n";
            }

            private void PlayerParsingDataApply()
            {
                if (GameManager.INSTANCE.isMale)
                {
                    m_nParsingIndex = 1;
                }
                if (!GameManager.INSTANCE.isMale)
                {
                    m_nParsingIndex = 0;
                }
                
                //선택된 성별이 저장 슬롯 역할을 하기 때문에
                //성별에 따라 해당하는 슬롯의 데이터를 가져온다
                CharactersGameData loadData = GameManager.INSTANCE.gameData.Load(m_nParsingIndex);

                //Debug.Log("로드 데이터 캐릭터 이름 : " + loadData.SName);

                //캐릭터의 이름이 없으면 로드 데이터가 없는 것으로 확인하여
                //파싱된 초기값을 설정

                if (loadData.SName=="")
                {
                    //Debug.Log("Init Data");

                    m_PlayerData.NLevel = m_Parsing.CharList[m_nParsingIndex].NLevel;
                    m_PlayerData.sName = m_Parsing.CharList[m_nParsingIndex].SName;
                    m_PlayerData.FMaxHP = m_Parsing.CharList[m_nParsingIndex].FMaxHP;
                    m_PlayerData.FHP = m_Parsing.CharList[m_nParsingIndex].FHP;
                    m_PlayerData.FMaxMana = m_Parsing.CharList[m_nParsingIndex].FMaxMana;
                    m_PlayerData.FMana = m_Parsing.CharList[m_nParsingIndex].FMana;                    
                    m_PlayerData.FExp = m_Parsing.CharList[m_nParsingIndex].FExp;
                    m_PlayerData.FNextExp = m_Parsing.CharList[m_nParsingIndex].FNextExp;
                }

                else
                {
                    //Debug.Log("Load Data");

                    m_PlayerData.NLevel = loadData.NLevel;
                    m_PlayerData.sName = loadData.SName;
                    m_PlayerData.FMaxHP = loadData.FMaxHP;
                    m_PlayerData.FHP = loadData.FHP;
                    m_PlayerData.FMaxMana = loadData.FMaxMana;
                    m_PlayerData.FMana = loadData.FMana;                    
                    m_PlayerData.FExp = loadData.FExp;
                    m_PlayerData.FNextExp = loadData.FNextExp;
                
                    //Debug.Log("Load Data HP : " + m_PlayerData.FHP);
                }

                PlayerInfoText(); //변경되 데이터를 출력 해준다
            }

            #endregion

            #region 외형 변경
            public void LeftBtn()
            {
                if(GameManager.INSTANCE.nPlayerIndex > 0)
                {
                    GameManager.INSTANCE.nPlayerIndex--;
                    SkinInit();
                }
            }

            public void RightBtn()
            {           
                if(GameManager.INSTANCE.isMale)
                {
                    if (GameManager.INSTANCE.nPlayerIndex < m_objMaleSkin.Length-1)
                    {
                        GameManager.INSTANCE.nPlayerIndex++;
                    }
                }
                else
                {
                    if (GameManager.INSTANCE.nPlayerIndex < m_objFemaleSkin.Length - 1)
                    {
                        GameManager.INSTANCE.nPlayerIndex++;
                    }
                }
                SkinInit();
            }
            #endregion

        }

    }
}
