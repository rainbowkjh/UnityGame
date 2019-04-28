using _Item;
using Characters;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// 데이터를 저장 로드 한다
/// </summary>
namespace Manager
{
    namespace GameData
    {
        public class DataManager : MonoBehaviour
        {
            private string[] dataPath = new string[2]; //캐릭터 성별 선택 버튼이 세이브 슬롯역할

            /// <summary>
            /// 세이브 슬롯을 생성
            /// </summary>
            public void Initialized()
            {
                for(int i=0; i<dataPath.Length;i++)
                {
                    //dataPath[i] = Application.persistentDataPath + "gameData" + i + ".dat";
                    dataPath[i] = Application.dataPath + "gameData" + i + ".dat";
                }
            }


           public void Save(CharactersData data, List<WeaponeGameData> itemList,
                            WeaponeGameData equipWeaponeList, int saveSlot)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(dataPath[saveSlot]);

                //캐릭터 정보 저장
                CharactersGameData saveData = new CharactersGameData();

                saveData.NLevel = data.NLevel;
                saveData.SName = data.sName;
                saveData.FMaxHP = data.FMaxHP;
                saveData.FHP = data.FHP;
                saveData.FMaxMana = data.FMaxMana;
                saveData.FMana = data.FMana;                
                saveData.FExp = data.FExp;
                saveData.FNextExp = data.FNextExp;
                                
                saveData.WeaponeList = itemList;
                saveData.EquipWeapone = equipWeaponeList;

                bf.Serialize(file, saveData);
                file.Close();

            }

            public CharactersGameData Load(int saveSlot)
            {
                //저장된 파일이 있으면 데이터를 리턴한다
                if (File.Exists(dataPath[saveSlot]))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(dataPath[saveSlot], FileMode.Open);
                    CharactersGameData data = (CharactersGameData)bf.Deserialize(file);

                    file.Close();

                    //Debug.Log("세이브 파일 로드 : " + dataPath[saveSlot].ToString());

                    return data;
                }
                //없으면 새로 생성
                else
                {
                    CharactersGameData data = new CharactersGameData();
                    //Debug.Log("초기 세팅값");
                    return data;
                }
                
            }


        }

    }
}
