using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 쓰러지면
/// 활성화
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class GameOverUI : MonoBehaviour
        {

            CanvasGroup cg;
            [SerializeField]
            GameObject mainBtn;

            [SerializeField]
            EscMenuManager escMenu;

            private void Start()
            {
                mainBtn.SetActive(false);
                cg = GetComponent<CanvasGroup>();
                cg.alpha = 0;
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }

            //점점 어두워지게한다
            public void GameOverAct()
            {
                if(cg.alpha != 1)
                {
                    cg.alpha += 0.1f * Time.deltaTime;
                }
                
                if(cg.alpha == 1)
                {
                    if (mainBtn.activeSelf == false)
                    {
                        mainBtn.SetActive(true);
                        cg.interactable = true;
                        cg.blocksRaycasts = true;
                    }
                        
                }
            }

            public void MainBtn()
            {
                //데이터를 저장하지는 않는다

                GameManager.INSTANCE.isSceneMove = true;
                //로딩 UI 추가!
                StartCoroutine(escMenu.Loading.LoadStage("Lobby"));

                escMenu.MenuClose(); //메뉴를 닫으면서 로딩이 멈춘다;;

                escMenu.MenuCg.alpha = 0; //메뉴 창만 투명하게 한다

            }

        }
        //end
    }
}
