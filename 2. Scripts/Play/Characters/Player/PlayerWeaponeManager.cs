using Manager;
using Manager.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapone;

/// <summary>
/// 플레이 씬으로 넘어오면
/// 장착 무기를 생성 시킨다
/// 실수로 장착무기 없이 들어오면
/// 기본 무기를 생성 시켜준다
/// </summary>
namespace Characters
{
    public class PlayerWeaponeManager : MonoBehaviour
    {
        PlayerCtrl player;
        [SerializeField,Header("기본 무기")]
        GameObject m_DefaultWeaponeObj;
        
        WeaponeGameData m_Data = new WeaponeGameData();

        [SerializeField,Header("무기 오브젝트 프리펩을 가지고 있다가 해당 오브젝트를 생성")]
        GameObject[] weaponeObj;

        [SerializeField, Header("무기 생성 위치")]
        Transform m_trWeaponePos;

        private void Start()
        {
            player = GetComponent<PlayerCtrl>();
            WeaponeSetting();
        }

      void WeaponeSetting()
        {
            CharactersGameData load = GameManager.INSTANCE.gameData.Load(player.NParsingIndex);
            bool isEquip = false;

            //로드한 무기의 정보와 무기 오브잭트 배열을 검사하여 찾는다
            m_Data = load.EquipWeapone;

            if(m_Data != null)
            {
                //Debug.Log("로드 데이터 : " + m_Data.WeaponeName);
                //Debug.Log("셍성 시킬 오브젝트 길이 : " + weaponeObj.Length);
                //Debug.Log("셍성 시킬 오브젝트 0번째 무기이름 : " + weaponeObj[0].GetComponent<GunCtrl>().WeaponeNameSearch);


                for (int i = 0; i < weaponeObj.Length; i++)
                {
                    //같은 무기가 있으면 생성
                    //(프리펩에서 데이터를 불러오기 위해 적어주는 무기 이름과 검색해서 찾는다)
                    if (m_Data.WeaponeName.Equals(weaponeObj[i].GetComponent<GunCtrl>().WeaponeNameSearch))
                    {
                        GameObject obj = Instantiate(weaponeObj[i].gameObject);
                        obj.transform.SetParent(m_trWeaponePos);
                        obj.transform.localPosition = Vector3.zero;
                        obj.transform.localRotation = Quaternion.identity;

                        obj.transform.localScale = new Vector3(1, 1, 1);

                        obj.GetComponent<WeaponeData>().Id = m_Data.Id;
                        obj.GetComponent<WeaponeData>().WeaponeName = m_Data.WeaponeName;
                        obj.GetComponent<WeaponeData>().FMinDmg = m_Data.FMinDmg;
                        obj.GetComponent<WeaponeData>().MaxDmg = m_Data.MaxDmg;
                        obj.GetComponent<WeaponeData>().NMag = m_Data.NMag;
                        obj.GetComponent<GunCtrl>().isEquip = true; //

                        Debug.Log("무기 생성");
                        //Debug.Log("DMG : " + weaponeObj[i].GetComponent<WeaponeData>().FMinDmg
                        //    + " ~ " + weaponeObj[i].GetComponent<WeaponeData>().MaxDmg);


                        isEquip = true;
                    }
                }
                
            }

            //else
            if(m_Data == null || m_Data.WeaponeName=="")
            {
                if (!isEquip)
                {
                    Debug.Log("기본무기 사용");
                    m_DefaultWeaponeObj.SetActive(true);
                }

            }

        }

    }
}

