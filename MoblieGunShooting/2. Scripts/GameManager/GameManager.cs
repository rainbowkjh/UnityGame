using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Black.Characters;
using Black.Weapone;

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
            
            [SerializeField, Header("메뉴 등 버튼 효과음")]
            AudioClip[] _sfx;
            AudioSource _audio;

            /// <summary>
            /// 적이 생성되면 적의 수를 저장 시킨다
            /// 0이 되면 다음으로 이동 시키는 역할
            /// </summary>
            int nEnemyCount = 0;

            DataManager dataManager;
            
            GameData data = new GameData();

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

            public GameData playerData
            {
                get
                {
                    return data;
                }
                set
                {
                    data = value;
                }
            }

            #endregion

            private void Awake()
            {
                ManagerInit();
                SystemInit();

                dataManager = GetComponent<DataManager>();
                dataManager.Initialize();

                _audio = GetComponent<AudioSource>();
            }

            /// <summary>
            /// 프로그램이 종료 되면
            /// 싱글턴을 제거 
            /// </summary>
            //private void OnDestroy()
            //{
            //    INSTANCE = null;
            //    Destroy(gameObject);
            //}

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
 
            public void BtnSfx()
            {
                _audio.volume = GameManager.INSTANCE.volume.sfx;
                _audio.PlayOneShot(_sfx[0]);
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

            /// <summary>
            /// 게임 로드
            /// </summary>
            public void LoadGameData()
            {
                GameData loadData = dataManager.Load();

                data.sceneName = loadData.sceneName;
                //캐릭터
                data.hp = loadData.hp;
                data.maxHp = loadData.maxHp;
                data.speed = loadData.speed;
                data.isLive = loadData.isLive;
                data.isFire = loadData.isFire;
                data.isReload = loadData.isReload;
                data.isStop = loadData.isStop;

                //무기                    
                //Debug.Log("Weapone List : " + loadData.weaponeList.Count);
                for (int i = 0; i < loadData.weaponeList.Count; i++)
                {
                    WeaponeData weaponeData = new WeaponeData();

                    weaponeData.id = loadData.weaponeList[i].id;
                    weaponeData.weaponeName = loadData.weaponeList[i].weaponeName;
                    weaponeData.weaponeState = loadData.weaponeList[i].weaponeState;
                    weaponeData.nLevel = loadData.weaponeList[i].nLevel;
                    weaponeData.nMaxLevel = loadData.weaponeList[i].nMaxLevel;
                    weaponeData.m_fMinDmg = loadData.weaponeList[i].m_fMinDmg;
                    weaponeData.m_MaxDmg = loadData.weaponeList[i].m_MaxDmg;
                    weaponeData.m_nMag = loadData.weaponeList[i].m_nMag;
                    weaponeData.m_nCurBullet = loadData.weaponeList[i].m_nCurBullet;


                    data.weaponeList.Add(weaponeData);
                    weaponeData = null;
                   // Debug.Log("load : " + loadData.weaponeData[i].id);
                   // Debug.Log("data : " + data.weaponeData[i].id);
                }

                //아이템
                data.nGrenadeCount = loadData.nGrenadeCount;
                data.nRecoveryCount = loadData.nRecoveryCount;
                data.nMaxGrenadeCount = loadData.nMaxGrenadeCount;
                data.nMaxRecorveryCount = loadData.nMaxRecorveryCount;
                data.grenadeLevel = loadData.grenadeLevel;
                data.recoveryLevel = loadData.recoveryLevel;
                data.greadeDMG = loadData.greadeDMG;
                data.recoveryHP = loadData.recoveryHP;

                data.nUpgradePoint = loadData.nUpgradePoint;

                //loadData = null;
            }

            /// <summary>
            /// 새로 시작
            /// </summary>
            public void NewGame()
            {
                GameData loadData = dataManager.NewGame();

                data.sceneName = loadData.sceneName;
                //캐릭터
                data.hp = loadData.hp;
                data.maxHp = loadData.maxHp;
                data.speed = loadData.speed;
                data.isLive = loadData.isLive;
                data.isFire = loadData.isFire;
                data.isReload = loadData.isReload;
                data.isStop = loadData.isStop;

                //무기                    
                //Debug.Log("Weapone List : " + loadData.weaponeList.Count);
                for (int i = 0; i < loadData.weaponeList.Count; i++)
                {
                    WeaponeData weaponeData = new WeaponeData();

                    weaponeData.id = loadData.weaponeList[i].id;
                    weaponeData.weaponeName = loadData.weaponeList[i].weaponeName;
                    weaponeData.weaponeState = loadData.weaponeList[i].weaponeState;
                    weaponeData.nLevel = loadData.weaponeList[i].nLevel;
                    weaponeData.nMaxLevel = loadData.weaponeList[i].nMaxLevel;
                    weaponeData.m_fMinDmg = loadData.weaponeList[i].m_fMinDmg;
                    weaponeData.m_MaxDmg = loadData.weaponeList[i].m_MaxDmg;
                    weaponeData.m_nMag = loadData.weaponeList[i].m_nMag;
                    weaponeData.m_nCurBullet = loadData.weaponeList[i].m_nCurBullet;


                    data.weaponeList.Add(weaponeData);
                    weaponeData = null;
                    // Debug.Log("load : " + loadData.weaponeData[i].id);
                    // Debug.Log("data : " + data.weaponeData[i].id);
                }

                //아이템
                data.nGrenadeCount = loadData.nGrenadeCount;
                data.nRecoveryCount = loadData.nRecoveryCount;
                data.nMaxGrenadeCount = loadData.nMaxGrenadeCount;
                data.nMaxRecorveryCount = loadData.nMaxRecorveryCount;
                data.grenadeLevel = loadData.grenadeLevel;
                data.recoveryLevel = loadData.recoveryLevel;
                data.greadeDMG = loadData.greadeDMG;
                data.recoveryHP = loadData.recoveryHP;

                data.nUpgradePoint = loadData.nUpgradePoint;

            }

            /// <summary>
            /// 데이터 저장
            /// </summary>
            public void SaveGameData(PlayerCtrl player, WeaponeManager weaponeData,
                ItemManager itemData, string stageName)
            {
                data.sceneName = stageName;
                //캐릭터
                data.hp = player.Hp;
                data.maxHp = player.MaxHp;
                data.speed = player.Speed;
                data.isLive = player.IsLive;
                data.isFire = player.IsFire;
                data.isReload = player.IsReload;
                data.isStop = player.IsStop;

                //무기
                int length = weaponeData.WeaponePos.Length;
                data.weaponeList.Clear(); //리스트 정리

                //Debug.Log("Weapone List : " + weaponeData.WeaponePos.Length);

                for (int i = 0; i < length; i++)
                {
                    WeaponeCtrl weapone = weaponeData.WeaponePos[i].GetComponentInChildren<WeaponeCtrl>();
                    WeaponeData weaponeList = new WeaponeData();

                    //Debug.Log("weapone Info : " + weaponeData.WeaponePos[i].GetComponentInChildren<WeaponeCtrl>().name);

                    weaponeList.id = weapone.Id;
                    weaponeList.weaponeName = weapone.WeaponeName;
                    weaponeList.weaponeState = weapone.WeaponeState;
                    weaponeList.nLevel = weapone.NLevel;
                    weaponeList.nMaxLevel = weapone.NMaxLevel;
                    weaponeList.m_fMinDmg = weapone.FMinDmg;
                    weaponeList.m_MaxDmg = weapone.MaxDmg;
                    weaponeList.m_nMag = weapone.NMag;
                    weaponeList.m_nCurBullet = weapone.NCurBullet;

                    data.weaponeList.Add(weaponeList);

                    //Debug.Log("Weapone Data : " + data.weaponeList[i].weaponeName);
                    //weaponeList = null;
                }

                //아이템
                ItemManager item = player.GetComponent<ItemManager>();

                data.nGrenadeCount = item.NGrenadeCount;
                data.nRecoveryCount = item.NRecoveryCount;
                data.nMaxGrenadeCount = item.NMaxGrenadeCount;
                data.nMaxRecorveryCount = item.NMaxRecorveryCount;
                data.grenadeLevel = item.GrenadeLevel;
                data.recoveryLevel = item.RecoveryLevel;
                data.greadeDMG = item.GreadeDMG;
                data.recoveryHP = item.RecoveryHP;

                data.nUpgradePoint = item.NUpgradePoint;


                dataManager.Save(data);

                item = null;
            }

            /// <summary>
            /// 이벤트 스테이지의 경우
            /// 업그래이드 포인트와 아이템 정보만 저장하여 넘긴다
            /// </summary>
            /// <param name="save"></param>
            public void ItemSave(PlayerCtrl player, ItemManager itemData, string stageNam)
            {
                data.sceneName = stageNam;

                //아이템
                ItemManager item = player.GetComponent<ItemManager>();

                data.nGrenadeCount = item.NGrenadeCount;
                data.nRecoveryCount = item.NRecoveryCount;
                data.nMaxGrenadeCount = item.NMaxGrenadeCount;
                data.nMaxRecorveryCount = item.NMaxRecorveryCount;
                data.grenadeLevel = item.GrenadeLevel;
                data.recoveryLevel = item.RecoveryLevel;
                data.greadeDMG = item.GreadeDMG;
                data.recoveryHP = item.RecoveryHP;

                data.nUpgradePoint = item.NUpgradePoint;


                dataManager.Save(data);
            }

            /// <summary>
            /// 저장된 스테이지 정보를 반환 시킨다
            /// </summary>
            /// <returns></returns>
            public string LoadStage()
            {
                return data.sceneName;
            }

        }

    }
}
