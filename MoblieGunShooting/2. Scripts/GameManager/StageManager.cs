using Black.Characters;
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

            [SerializeField, Header("게임이 시작 되면 업그래이드 메뉴를 활성화 할지 선택")]
            bool isUpgradeWindow = false;

            [SerializeField, Header("업그래이드 메뉴")]
            GameObject upgradeMenuObj;

            [SerializeField, Header("스테이지 시작 또는 종료")]
            bool isStart = false;

            [SerializeField, Header("스테이지 (이벤트)시작 시 배경음 변경")]
            BgmManager bgm;

            [SerializeField, Header("Boss전이 끝나면 플레이어를 정지 시킨다, 데이터 저장 시 필요")]
            PlayerCtrl player;

            [SerializeField, Header("데이터 저장 시 무기 정보")]
            WeaponeManager weapone;

            [SerializeField, Header("아이템 정보")]
            ItemManager item;

            [SerializeField, Header("Load Manager")]
            LoadingManager load;
                        
            #region Set,Get
            public bool IsStart
            {
                get
                {
                    return isStart;
                }

                set
                {
                    isStart = value;
                }
            }
            #endregion

            private void Start()
            {
                //스테이지 선택 버튼을 숨긴다
                nextStageObj.SetActive(false);
                UpgradeMenuAct();
                
                //전 스테이지에서 적 제거 수를 초기화 해준다
                //(충돌 방지_이벤트 성으로 적을 생성 후 처치 했을때
                //제거 수가 -가 된다
                GameManager.INSTANCE.NEnemyCount = 0;
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
                GameManager.INSTANCE.BtnSfx();

                GameManager.INSTANCE.system.isPause = false;
                //데이터 저장
                GameManager.INSTANCE.SaveGameData(player, weapone, item, stageName);
                //SceneManager.LoadScene(stageName);
                StartCoroutine(load.SceneLoad(stageName));
            }

            /// <summary>
            /// Start에서 메뉴 활성화를 확인하고 실행
            /// </summary>
            void UpgradeMenuAct()
            {
                if(isUpgradeWindow)
                {
                    GameManager.INSTANCE.GamePause();
                    upgradeMenuObj.SetActive(true);
                }
            }

            /// <summary>
            /// 업그래이드 메뉴에서 창 닫기 버튼
            /// </summary>
            public void UpgradeCloseBtn()
            {
                GameManager.INSTANCE.GamePlay();
                StageStart(); //스테이지 시작

                bgm._BgmIndexChange(1);

                isUpgradeWindow = false;
                upgradeMenuObj.SetActive(false);
            }

            /// <summary>
            /// 스테이지 시작 종료에 따라
            /// NPC  행동 및 그 밖에 필요에 따라 제어한다
            /// 
            /// 스테이지 시작 버튼은 주로
            /// 무기 업그레이드 종료 버튼을 누르는 곳에서 호출
            /// </summary>
            public void StageStart()
            {
                IsStart = true;
            }

            public void StageEnd()
            {
                if (IsStart)
                    StartCoroutine(NextBtnAct());
            }

            IEnumerator NextBtnAct()
            {
                IsStart = false;
                player.IsStop = true;

                GameManager.INSTANCE.system.isPause = true;

                yield return new WaitForSeconds(3.0f);

                nextStageObj.SetActive(true);
            }

        }

    }
}
