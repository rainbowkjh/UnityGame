using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// 게임 매니져(싱글턴)
/// </summary>

namespace Black
{
    namespace Manager
    {
        /// <summary>
        /// 게임의 기본 메뉴와 관련 된 기능
        /// </summary>
        [Serializable]
        public struct GameSystem
        {
            public bool isPause;
        }

        /// <summary>
        /// 사운드 볼륨 조절
        /// </summary>
        [Serializable]
        public struct GameSFX
        {
            public float bgm;
            public float sfx;
        }

        public class GameManager : MonoBehaviour
        {
            public static GameManager INSTANCE = null;
                        
            public GameSystem system;
            public GameSFX volume;

            /// <summary>
            /// 적이 생성되면 적의 수를 저장 시킨다
            /// 0이 되면 다음으로 이동 시키는 역할
            /// </summary>
            int nEnemyCount = 0;

            #region Set,Get
            public int NEnemyCount
            {
                get
                {
                    return nEnemyCount;
                }

                set
                {
                    nEnemyCount = value;
                }
            }
            #endregion

            private void Awake()
            {
                ManagerInit();
                SystemInit();
            }

            /// <summary>
            /// 프로그램이 종료 되면
            /// 싱글턴을 제거 
            /// </summary>
            private void OnDestroy()
            {
                INSTANCE = null;
                Destroy(gameObject);
            }

            #region 싱글턴
            private void ManagerInit()
            {
                if(INSTANCE == null)
                {
                    INSTANCE = this;
                }

                if(INSTANCE != this)
                {
                    Destroy(gameObject);
                }

                DontDestroyOnLoad(gameObject);
            }
            #endregion

            private void SystemInit()
            {
                system.isPause = false;

                volume.bgm = 1.0f;
                volume.sfx = 1.0f;
            }
 
            /// <summary>
            /// 게임 정지
            /// </summary>
            public void GamePause()
            {
                system.isPause = true;
                Time.timeScale = 0;
            }

            public void GamePlay()
            {
                system.isPause = false;
                Time.timeScale = 1;
            }


            //private void Update()
            //{
            //    if (Input.GetKeyDown(KeyCode.Q))
            //        SceneManager.LoadScene("Stage1");
            //}

        }

    }
}
