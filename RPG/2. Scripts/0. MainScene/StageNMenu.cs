using Black.Characters;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 로비 씬에서
/// 스테이지 선택 UI
/// </summary>
namespace Black
{
    namespace MainScene
    {
        public class StageNMenu : MonoBehaviour
        {
            [SerializeField, Header("스테이지 선택 이미지")]
            GameObject[] stageSpr;

            int stageIndex = 0; //선택 중인 스테이지

            [SerializeField]
            LoadingManager loading;

            [SerializeField]
            PlayerCtrl player;


            //AudioSource _audio;
            //[SerializeField, Header("0업그래이드 1 취소")]
            //AudioClip[] _sfx;


            [SerializeField, Header("스테이지 선택 창")]
            CanvasGroup cg;

            [SerializeField, Header("로드 중 UI 숨김")]
            CanvasGroup[] uiObjs;

            [SerializeField, Header("로딩 중 숨김 퀘스트 UI Obj")]
            GameObject questUiObj;

            private void Start()
            {
                SpriteAct();

                cg.alpha = 0.0f;
                cg.interactable = false;
                cg.blocksRaycasts = false;

            }


            /// <summary>
            /// 스테이지 선택 버튼
            /// </summary>
            public void StageRightBtn()
            {
                //현재 스테이지가 클리어 되었을 경우만
                //다음 스테이지 선택 가능
                if(player.StageList[stageIndex])
                {
                    stageIndex++;

                    if (stageIndex >= stageSpr.Length)
                        stageIndex = stageSpr.Length - 1;

                    SpriteAct();
                }
                
            }

            public void StageLeftBtn()
            {
                stageIndex--;

                if (stageIndex <= 0)
                    stageIndex = 0;

                SpriteAct();
            }

            /// <summary>
            /// 스테이지 이미지를 보여준다
            /// </summary>
            private void SpriteAct()
            {
                for(int i=0;i<stageSpr.Length;i++)
                {
                    stageSpr[i].SetActive(false);
                }

                stageSpr[stageIndex].SetActive(true);
            }

            /// <summary>
            /// 스테이지를 시작한다
            /// 스테이지 씬으로 이동
            /// </summary>
            public void StageStartBtn()
            {
                //버튼 UI숨기기!
                for(int i=0;i< uiObjs.Length;i++)
                {
                    uiObjs[i].alpha = 0;
                    uiObjs[i].blocksRaycasts = false;
                    uiObjs[i].interactable = false;

                }

                questUiObj.SetActive(false);

                GameManager.INSTANCE.isSceneMove = true;
                GameManager.INSTANCE._DataManager.QuickSave(player);
                string stageName = stageSpr[stageIndex].name;
                StartCoroutine(loading.LoadStage(stageName));
                //UnityEngine.SceneManagement.SceneManager.LoadScene(stageName);
            }

            /// <summary>
            /// 마을로 돌아간다
            /// </summary>
            public void SafeAreMove()
            {
                //버튼 UI숨기기!
                for (int i = 0; i < uiObjs.Length; i++)
                {
                    uiObjs[i].alpha = 0;
                    uiObjs[i].blocksRaycasts = false;
                    uiObjs[i].interactable = false;

                }

                questUiObj.SetActive(false);

                GameManager.INSTANCE.isSceneMove = true;
                GameManager.INSTANCE._DataManager.QuickSave(player);
                
                StartCoroutine(loading.LoadStage("SafeArea"));
                //UnityEngine.SceneManagement.SceneManager.LoadScene(stageName);
            }

            /// <summary>
            /// 창 닫기
            /// </summary>
            public void CloseBtn()
            {

                player.IsInven = false;
                player.InventoryInit();

                cg.alpha = 0.0f;
                cg.interactable = false;
                cg.blocksRaycasts = false;

            }
        }

    }
}
