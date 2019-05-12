using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 매니져(싱글턴)
/// </summary>

namespace Black
{
    namespace Manager
    {

        public class GameManager : MonoBehaviour
        {
            public static GameManager INSTANCE = null;

            private void Awake()
            {
                ManagerInit();   
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

            /// <summary>
            /// 싱글턴 세팅
            /// </summary>
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

 
        }

    }
}
