using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 특정 캐릭터 처치 시 퀘스트 완료
/// 그 특정 캐릭터에 적용 시킨다
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class QuestClear : MonoBehaviour
        {
            [SerializeField, Header("현재 스테이지(완료 시 다음 스테이지 열림 0부터")]
            int nCurStage;

            [SerializeField, Header("클리어 퀘스트 아이디")]
            int questID;

            QuestManager manager;
            StageManager stage;
            BgmManager bgmManager;

            bool isClear = false;

            [SerializeField, Header("퀘스트 클리어 후 배경음 변경")]
            bool isBgmChange = false;
            [SerializeField, Header("Bgm Index")]
            int bgmIndex = 0;

            Characters.PlayerCtrl player;

            private void Start()
            {
                bgmManager = GameObject.Find("BgmManager").GetComponent<BgmManager>();
                stage = GameObject.Find("StageManager").GetComponent<StageManager>();
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>();
                manager = stage.GetComponent<QuestManager>();
            }

            /// <summary>
            /// 적 캐릭터에서
            /// 이 스크립트가 있는 캐릭터가 쓰러지면
            /// 함수를 호출 시킨다
            /// </summary>
            /// <param name="questID"></param>
            public void BossDown()
            {
                //현재 스테이지가 클리어 하지 못했을 경우
                if(!player.StageList[nCurStage])
                {
                    player.StageList[nCurStage] = true; //현재 스테이지 완료

                    bool nextStage = false;
                    //다음 스테이지 미 클리어 상태로 추가
                    //반복 플레이 시 목록이 더 추가 되지 않도록 한다
                    player.StageList.Add(nextStage); 

                }

                stage.IsClear = true; //메인 퀘스트 완료
                manager.QuestDelete(questID);

                if (isBgmChange)
                    bgmManager.BgmIndexPlay(bgmIndex);

            }

            /// <summary>
            /// 위와 같지만 서브 퀘스트 완료
            /// </summary>
            public void SubQuestBossDown()
            {
                if(!isClear)
                {
                    isClear = true;
                    manager.QuestDelete(questID);
                }
                
            }
        }
        //class End
    }
}
