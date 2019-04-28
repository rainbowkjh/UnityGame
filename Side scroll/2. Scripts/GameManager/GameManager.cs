using Characters;
using Manager.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{    
    /// <summary>
    /// 게임과 관련된 기능 묶음
    /// </summary>
    public struct GameSystem
    {
        public bool isPause; //이벤트 및 특정 상황에서 정지한다
        public bool isPlayScene; //플레이씬인지 로비 씬인지 구별
        
    }

    public class GameManager : MonoBehaviour
    {

        public static GameManager INSTANCE = null;

        /// <summary>
        /// 메인에서 무시 선택 시 임시 저장 데이터
        /// 플레이 씬으로 넘어가면 데이터를 적용 시켜준다
        /// </summary>
        WeaponeGameData weaponeData;

        /// <summary>
        /// 무기와 마찬가지로 임시 저장 후
        /// 플레이 씬에서 적용 시킨다
        /// </summary>
        CharactersGameData playerData;
        
        /// <summary>
        /// 플레이어를 선택할때 사용되는 값
        /// 선택 후 다음 씬으로 넘기고
        /// 캐릭터를 초기화 할때 외형을 다시 세팅 해주어야
        /// 하기 때문에 싱글턴에 저장 시킨다
        /// </summary>
        public int nPlayerIndex = 0;
        public bool isMale = false; //성별 선택(처음 작업 시 여성 캐릭터를 넣어서 false를 기본값으로 한다;;

        public DataManager gameData;

        public GameSystem gameSystem;

        private void Awake()
        {
            GameManagerSingleTone();
            gameData.Initialized();
        }


        #region 싱글턴
        void GameManagerSingleTone()
        {
            if (INSTANCE == null)
                INSTANCE = this;

            if (INSTANCE != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }
        #endregion

        /// <summary>
        /// 게임과 관련 된 기능 초기화
        /// </summary>
        void GameSetttingInit()
        {
            gameSystem.isPause = false;
            gameSystem.isPlayScene = false;
        }


    }

}

