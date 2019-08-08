using Black.Characters;
using Black.EventCam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

/// <summary>
/// 스테이지에서
/// 이벤트 연출 시
/// 게임 카메라를 잠시 비활성화 시키고
/// 해당 이벤트 카메라를 활성화 시킨다
/// 
/// 이벤트 연출은 상황에 맞게
/// 스크립트를 새로 작성 한다
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class EventCamCtrl : MonoBehaviour
        {
            /// <summary>
            /// 카메라가 확대하면 포스트 값을 변경 시켜준다
            /// </summary>
            PostProcessingBehaviour post;


            [SerializeField, Header("메인 카메라(이벤트 연출 시 비활성화)")]
            GameObject camObj;

            [SerializeField, Header("이벤트 카메라")]
            GameObject eventCamObj;

            [SerializeField, Header("이벤트 연출 오브젝트들")]
            GameObject eventObj;

            [SerializeField, Header("이벤트 후 오브젝트를 남길지 결정")]
            bool isEventObjAct = true;

            bool isEnter = false; //이벤트 콜라이더에 플레이어 감지

            [SerializeField, Header("이벤트 연출 시 캐릭터 정지")]
            bool isStop = false;

            PlayerCtrl player;

            [SerializeField, Header("퀘스트가 해당하는지 확인")]
            bool isQuest = false;
            [SerializeField, Header("퀘스트와 연관 있다면 아이디")]
            int questID = 0;

            [SerializeField, Header("이벤트 연출 후 적 생성")]
            EnemyActManager questEnemy = null;

            QuestManager manager;

            [SerializeField, Header("이벤트 발생 시 배경음 변경")]
            bool isBgmChange = false;

            [SerializeField, Header("배경음 변경 인덱스")]
            int bgmIndex;

            [SerializeField, Header("이벤트 끝나고 배경음 변경")]
            bool isBgmChangeEnd = false;

            [SerializeField, Header("배경음 변경 인덱스")]
            int bgmIndexEnd;

            //[SerializeField, Header("배경음 정지 후 이벤트 끝나면 재생")]
            //bool isBgmStop = false;

            BgmManager bgmManager;

            private void Start()
            {
                bgmManager = GameObject.Find("BgmManager").GetComponent<BgmManager>();
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                manager = GameObject.Find("StageManager").GetComponent<QuestManager>();
                MainCamAct();

                if (questEnemy)
                    questEnemy.enabled = false;
                eventObj.SetActive(false);
                
            }


            private void OnTriggerEnter(Collider other)
            {
                if (!isEnter)
                {
                    //퀘스트가 있는 지역이면
                    //퀘스트가 적용 되었는지 확인(퀘스트 중인 경우에만 이벤트 발생)
                    if (isQuest)
                    {
                        //퀘스트 아이디를 리스트에서 검색해본다
                        if (manager.QuestCheck(questID))
                        {
                            if (other.transform.CompareTag("Player"))
                            {
                                BgmPlay();

                                isEnter = true;

                                if (isStop)
                                    player.Stop();

                                GameManager.INSTANCE.IsEvent = true;
                                GameManager.INSTANCE.isSceneMove = true; //필드 아이템 UI가 에러가 나므로,,

                                camObj.SetActive(false);
                                eventCamObj.SetActive(true);
                                eventCamObj.GetComponentInParent<CamTargetZoom>().IsEventStart = true;
                                //Debug.Log("Event Start Send");


                                if (questEnemy)
                                    questEnemy.enabled = true;
                                eventObj.SetActive(true);
                            }
                        }

                    }
                    //플레이어가 감지되면
                    //이벤트 무조건 발생.
                    else
                    {
                        if (other.transform.CompareTag("Player"))
                        {
                            BgmPlay();

                            isEnter = true;

                            if (isStop)
                                player.Stop();

                            GameManager.INSTANCE.IsEvent = true;
                            GameManager.INSTANCE.isSceneMove = true; //필드 아이템 UI가 에러가 나므로,,

                            camObj.SetActive(false);
                            eventCamObj.SetActive(true);
                            eventCamObj.GetComponentInParent<CamTargetZoom>().IsEventStart = true;
                            //Debug.Log("Event Start Send");


                            if (questEnemy)
                                questEnemy.enabled = true;
                            eventObj.SetActive(true);
                        }
                    }

                }

            }
            
            /// <summary>
            /// 배경음 교체
            /// </summary>
            private void BgmPlay()
            {
                if(isBgmChange)
                {
                    bgmManager.BgmIndexPlay(bgmIndex);
                }
            }


            /// <summary>
            /// 이벤트가 끝나면
            /// 카메라를 원래데로 돌려 놓은다
            /// </summary>
            public void MainCamAct()
            {
                if(isBgmChangeEnd)
                    bgmManager.BgmIndexPlay(bgmIndexEnd);

                eventCamObj.SetActive(false);
                //Debug.Log("Event Off 2 ");
                camObj.SetActive(true);
                //Debug.Log("Main On ");

                if (!isEventObjAct)
                    eventObj.SetActive(false);
                GameManager.INSTANCE.IsEvent = false;
                GameManager.INSTANCE.isSceneMove = false;

            }

        }
        //class End
    }
}
