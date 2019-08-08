using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 스테이지 매니져로
/// 로비 제외 스테이지 씬에
/// 적용 시킨다
/// 
/// 스테이지 클리어 조건
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class StageManager : MonoBehaviour
        {
            [SerializeField]
            PlayerCtrl player;


            public bool isTest = false;

            [SerializeField, Header("스테이지 클리어 졸건 달성")]
            bool isClear = false;

            public bool IsClear { get => isClear; set => isClear = value; }

            private void Awake()
            {
                if(!isTest)
                {
                    //로비에서 저장된 데이터를 불러온다
                    GameManager.INSTANCE._DataManager.QuickLoad();
                    Time.timeScale = 1.0f;

                }                
            }
            
            //private void LateUpdate()
            //{
            //    //스테이지 종료 테스트
            //    if(Input.GetKeyDown(KeyCode.Alpha0))
            //    {
            //        StageExit();
            //    }
            //}

            /// <summary>
            /// 스테이지가 종료 되면 퀵 세이브
            /// </summary>
            private void StageExit()
            {
                //스테이지에서 로비로 이동
                GameManager.INSTANCE.isSceneMove = true;
                GameManager.INSTANCE._DataManager.QuickSave(player);

                SceneManager.LoadScene("SafeArea");
            }

         

        }
        //class End
    }
}
