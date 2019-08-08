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

            #region 로딩 관련
            [SerializeField]
            GameObject loadObj;

            [SerializeField]
            Slider loadFill;

            [SerializeField]
            GameObject mainCam;

            bool isLoadStart = false;

            public bool IsLoadStart { get => isLoadStart; set => isLoadStart = value; }
            public GameObject MainCam { get => mainCam; set => mainCam = value; }
            public Slider LoadFill { get => loadFill; set => loadFill = value; }
            public GameObject LoadObj { get => loadObj; set => loadObj = value; }
            #endregion


            public IEnumerator LoadStage(string nextScene)
            {

                MainCam.SetActive(false);
                LoadObj.SetActive(true);
                yield return null;

                AsyncOperation async = Application.LoadLevelAsync(nextScene);

                while (!async.isDone)
                {
                    yield return null;

                    LoadFill.value = async.progress;
                }

            }
        }

    }

}

