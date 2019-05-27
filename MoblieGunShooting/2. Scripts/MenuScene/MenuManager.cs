using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 메뉴에서 게임으로 넘어가는 메뉴의 버튼을 관리
/// </summary>
namespace Black
{
    namespace Main
    {
        namespace Manager
        {
            public class MenuManager : MonoBehaviour
            {
                [SerializeField, Header("로드 버튼 활성화")]
                GameObject loadBtn;

                [SerializeField, Header("LoadManager")]
                LoadingManager load;



                private void Start()
                {
                    LoadBtnAct();
                  
                }

                /// <summary>
                /// 로드 버튼 활성화(저장된 파일이 있으면
                /// 로들 할 스테이지 이름이 Stage0이 아닌 것을 활용해서
                /// 버튼을 활성화 할지 결정)
                /// </summary>
                void LoadBtnAct()
                {                    
                    GameManager.INSTANCE.LoadGameData();
                    if (GameManager.INSTANCE.LoadStage().Equals("Stage0"))
                    {
                        loadBtn.SetActive(false);
                    }
                    else
                    {
                        loadBtn.SetActive(true);
                    }
                }

                /// <summary>
                /// 게임 시작 버튼
                /// </summary>
                public void GameStart()
                {
                    GameManager.INSTANCE.BtnSfx();

                    GameManager.INSTANCE.NewGame();
                    // SceneManager.LoadScene("Stage1");
                    StartCoroutine(load.SceneLoad("Stage0"));
                                       
                }

                /// <summary>
                /// 게임 로드 버튼
                /// </summary>
                public void GameLoad()
                {
                    GameManager.INSTANCE.BtnSfx();

                    GameManager.INSTANCE.LoadGameData();
                    //SceneManager.LoadScene(GameManager.INSTANCE.LoadStage());

                    string stageName = GameManager.INSTANCE.LoadStage();
                    StartCoroutine(load.SceneLoad(stageName));
                }

            }

        }
    }
}
