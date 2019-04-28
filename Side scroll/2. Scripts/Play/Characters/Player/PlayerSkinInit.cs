using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// 메인에서 게임 메니져에 임시 저장 시킨 외형 정보를
    /// 게임이 시작하면 적용 시켜준다
    /// 
    /// 캐릭터 데이터도 받아오도록 추가
    /// </summary>
    public class PlayerSkinInit : MonoBehaviour
    {
        
        [SerializeField, Header("캐릭터 안에 변경 시킬 외형 오브젝트")]
        GameObject[] m_objMaleSkin;
        [SerializeField, Header("캐릭터 안에 변경 시킬 외형 오브젝트")]
        GameObject[] m_objFemaleSkin;
        [SerializeField, Header("헤어가 없는 외형이 있어서.../ 0 남 1 여")]
        GameObject[] m_objHair;

        void Start()
        {
            SkinInit();
        }

        //PlayerSettingBtn에 있는 코드와 같다
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

    }

}
