using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라 이벤트
/// 보스전 시작 전, 클리어 후 카메라 제어
/// 
/// m_objCam - 플레이 카메라와 보스전 연출 카메라
/// m_objUI - 보스전 연출 카메라가 활성화가 되면
///            간단한 UI를 일정한 간격으로 활성화
/// 
/// </summary>
namespace CameraRig
{
    public class CameraEventAct : MonoBehaviour
    {
        [SerializeField,Header("0: 플레이 1: 보스전 이벤트 카메라")]
        GameObject[] m_objCam;

        [SerializeField, Header("보스전 간단한 연출 UI")]
        GameObject[] m_objUI;

        private void Start()
        {
            PlayCam();
            UIOff();
        }

        private void UIOff()
        {
            for (int i = 0; i < m_objUI.Length; i++)
            {
                m_objUI[i].SetActive(false);
            }
        }

        public IEnumerator BossEventCam()
        {
            GameManager.INSTANCE.gameSystem.isPause = true;
            
            m_objCam[0].SetActive(false);
            m_objCam[1].SetActive(true);

            yield return new WaitForSeconds(1.0f);

            for (int i = 0; i < m_objUI.Length; i++)
            {
                m_objUI[i].SetActive(true);
                yield return new WaitForSeconds(1.0f);
            }

            //플레이 카메라로 전환
            PlayCam();

        }

        public void PlayCam()
        {
            m_objCam[0].SetActive(true);
            m_objCam[1].SetActive(false);

            GameManager.INSTANCE.gameSystem.isPause = false;
            
            UIOff();
        }

    }

}
