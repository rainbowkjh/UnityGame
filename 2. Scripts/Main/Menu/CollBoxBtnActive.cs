using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 각 콜라이더에 플레이어가 들어와서
/// 활성화 키를 입력하면
/// 해당 UI를 활성화 시킨다(캐릭터 선택, 무기 선택 등)
/// </summary>
namespace MainScene
{
    namespace Menu
    {
        public class CollBoxBtnActive : MonoBehaviour
        {
            [SerializeField, Header("활성화 시킬 UI오브젝트")]
            GameObject m_objUI;

            bool isAct = false;

            private void Start()
            {
                m_objUI.SetActive(false);
            }

            private void OnTriggerStay(Collider other)
            {
                if (!isAct)
                {
                    if (other.tag.Equals("Player"))
                    {
                        isAct = true;
                        m_objUI.SetActive(true);
                    }
                }

            }

            private void OnTriggerExit(Collider other)
            {
                if(other.tag.Equals("Player"))
                {
                    isAct = false;
                    m_objUI.SetActive(false);
                }
            }
        }
    }
}

