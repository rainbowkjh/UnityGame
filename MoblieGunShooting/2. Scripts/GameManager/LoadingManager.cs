using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Black
{
    namespace Manager
    {
        public class LoadingManager : MonoBehaviour
        {
            [SerializeField, Header("게임 카메라")]
            GameObject camObj;

            [SerializeField, Header("로딩 카메라")]
            GameObject loadingCam;

            [SerializeField, Header("Load Fill")]
            Image loadBar;

            [SerializeField, Header("로드 중에 비활성화 시킬 UI")]
            GameObject[] uiObj;

            private void Start()
            {
                 PlayCamAct();

                loadBar.fillAmount = 0;
            }

            /// <summary>
            /// 플레이 캠을 활성화
            /// </summary>
            void PlayCamAct()
            {
                camObj.SetActive(true);
                loadingCam.SetActive(false);
            }

            /// <summary>
            /// 로딩 카메라 활성화
            /// </summary>
            void LoadingCamAct()
            {
                camObj.SetActive(false);
                loadingCam.SetActive(true);
            }

            void UIDisable()
            {
                for(int i=0;i<uiObj.Length;i++)
                {
                    uiObj[i].SetActive(false);
                }
            }

            /// <summary>
            /// 로드
            /// </summary>
            /// <param name="stageName"></param>
            /// <returns></returns>
           public IEnumerator SceneLoad(string stageName)
            {
                LoadingCamAct();
                UIDisable();

                yield return null;

                AsyncOperation async = Application.LoadLevelAsync(stageName);

                while(!async.isDone)
                {
                    yield return null;
                    loadBar.fillAmount = async.progress;
                }
            }

            /// <summary>
            /// 플레이어 게임오버 시 버튼
            /// </summary>
            public void MainBtn()
            {
                StartCoroutine(SceneLoad("Main"));
            }

        }

    }
}

