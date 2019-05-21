using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 각 스테이지에 관련된 데이터
/// 다음 스테이지 정보와 마지막 지점 도착 시
/// 다음 스테이지 버튼을 활성화(스테이지 분기점 선택)
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class StageManager : MonoBehaviour
        {
            [SerializeField, Header("다음 스테이지 선택 버튼")]
            GameObject nextStageObj;

            private void Start()
            {
                //스테이지 선택 버튼을 숨긴다
                nextStageObj.SetActive(false);
            }

            /// <summary>
            /// 플레이어가 마지막 지점 도착 시 호출
            /// </summary>
            public void NextStageSelection()
            {
                nextStageObj.SetActive(true);
            }

            /// <summary>
            /// 선택한 스테이지 씬으로 이동
            /// 플레이어가 마지막 지점 도착 시
            /// 활성화 된 버튼에서
            /// 버튼 클릭 시 호출
            /// 
            /// 각 스테이지 버튼에 다음 스테이지 이름을 적어서 적용
            /// </summary>
            public void StageSelectionBtn(string stageName)
            {
                SceneManager.LoadScene(stageName);
            }


        }

    }
}
