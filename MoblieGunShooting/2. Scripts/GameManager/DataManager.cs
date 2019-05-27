using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Black
{
    namespace Manager
    {
        /// <summary>
        /// 저장할 플레이어의 정보
        /// </summary>
        [Serializable]
        public class GameData
        {
            //로드 할 멥의 씬 이름
            public string sceneName = "Stage0";

            //캐릭터 부분-------------------------
            public float hp = 1000;
            public float maxHp = 1000;
            public float speed = 5;
            public bool isLive = true;
            public bool isFire = false;
            public bool isReload = false;
            public bool isStop = false;

            //무기 -------------------------------
            //public WeaponeData[] weaponeData = new WeaponeData[4];
            public List<WeaponeData> weaponeList = new List<WeaponeData>();

            //아이템 & 업그래이드 포인트-------------
            public int nGrenadeCount = 0;
            public int nRecoveryCount = 0;
            //최대 소지량
            public int nMaxGrenadeCount = 3;
            public int nMaxRecorveryCount = 3;
            //업그래이드 레벨(10까지)
            public int grenadeLevel = 1;
            public int recoveryLevel = 1;
            //데미지와 회복량은 업그래이드로 올릴 수 있다            
            public float greadeDMG = 100;
            public float recoveryHP = 100;
            //포인트
            public int nUpgradePoint = 0;
        }


        [Serializable]
        public class WeaponeData
        {
            public int id = 0;
            public string weaponeName = "";
            public string itemIconPath = ""; //아이템 아이콘 경로

            public int weaponeState = 0; //무기 형태 0 권총 1 AR 2 SG

            public int nLevel = 1; //일정 레벨이 오르면 외형이 변경된다
            public int nMaxLevel = 30; //레벨 최대

            //데미지 최소 수치1 ~최소 수치2 중 랜덤 결정
            //최대치도 같음 (같은 무기라도 최소에서 최대 데미지가 다르다)
            //결정된 수치에서 공격할때 또 랜덤 데미지를 적용
            public float m_fMinDmgF = 50;
            public float m_fMinDmgE = 100;

            public float m_fMaxDmgF = 150;
            public float m_fMaxDmgE = 200;

            //무기마다 실제 적용되는 데미지
            public float m_fMinDmg = 0;
            public float m_MaxDmg = 0;

            //탄창안의 최대 탄 수
            public int m_nMag = 30;
            public int m_nCurBullet = 30; // 현재 탄 수

        }

        public class DataManager : MonoBehaviour
        {
            private string dataPath;

            //데이터 저장
            //[SerializeField]
            GameData data = new GameData();


            /// <summary>
            /// 저장 경로 초기화
            /// </summary>
            public void Initialize()
            {
                dataPath = Application.persistentDataPath + "/gameSave.dat";
                Debug.Log("Path : " + dataPath);
            }

            /// <summary>
            /// 저장하기
            /// </summary>
            /// <param name="data"></param>
            public void Save(GameData saveData)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(dataPath);

                ////데이터 저장
                //GameData data = new GameData();

                data.sceneName = saveData.sceneName;
                //캐릭터
                data.hp = saveData.hp;
                data.maxHp = saveData.maxHp;
                data.speed = saveData.speed;
                data.isLive = saveData.isLive;
                data.isFire = saveData.isFire;
                data.isReload = saveData.isReload;
                data.isStop = saveData.isStop;

                //Debug.Log("weaponeLength " + data.weaponeData.Length);

                //무기
                data.weaponeList.Clear(); //리스트를 한번 정리해준다
                for (int i = 0; i < saveData.weaponeList.Count; i++)
                {
                    //Debug.Log("saveData : " + saveData.weaponeData[i].id);
                    //Debug.Log("data : " + data.weaponeData[i].id);

                    WeaponeData weapone = new WeaponeData();

                    weapone.id = saveData.weaponeList[i].id;
                    weapone.weaponeName = saveData.weaponeList[i].weaponeName;
                    weapone.weaponeState = saveData.weaponeList[i].weaponeState;
                    weapone.nLevel = saveData.weaponeList[i].nLevel;
                    weapone.nMaxLevel = saveData.weaponeList[i].nMaxLevel;
                    weapone.m_fMinDmg = saveData.weaponeList[i].m_fMinDmg;
                    weapone.m_MaxDmg = saveData.weaponeList[i].m_MaxDmg;
                    weapone.m_nMag = saveData.weaponeList[i].m_nMag;
                    weapone.m_nCurBullet = saveData.weaponeList[i].m_nCurBullet;

                    data.weaponeList.Add(weapone);
                    //weapone = null;
                }

                //아이템
                data.nGrenadeCount = saveData.nGrenadeCount;
                data.nRecoveryCount = saveData.nRecoveryCount;
                data.nMaxGrenadeCount = saveData.nMaxGrenadeCount;
                data.nMaxRecorveryCount = saveData.nMaxRecorveryCount;
                data.grenadeLevel = saveData.grenadeLevel;
                data.recoveryLevel = saveData.recoveryLevel;
                data.greadeDMG = saveData.greadeDMG;
                data.recoveryHP = saveData.recoveryHP;

                data.nUpgradePoint = saveData.nUpgradePoint;

                bf.Serialize(file,data);
                file.Close();

                //data = null;
            }

            /// <summary>
            /// 로드하기
            /// </summary>
            /// <returns></returns>
            public GameData Load()
            {
                //저장 데이터 확인
                if(File.Exists(dataPath))
                {
                    //Debug.Log("Load Data");

                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(dataPath, FileMode.Open);
                    GameData loadData = (GameData)bf.Deserialize(file);
                    file.Close();

                    return loadData;
                }
                //저장 데이터가 없으면
                else
                {
                    Debug.Log("New Data");

                    GameData newData = new GameData();
                    return newData;
                }
            }

            public GameData NewGame()
            {
                GameData newData = new GameData();
                return newData;
            }
        }

    }
}
