using Black.Manager;
using Black.Weapone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터의 무기를 관리
/// 
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class WeaponeManager : MonoBehaviour
        {
            /// <summary>
            /// 무기 장착 위치 무기의 데이터를 가져온다
            /// </summary>
            GameObject[] weaponeEquipPos = new GameObject[2];

            WeaponeData[] weaponeData = new WeaponeData[2];

            [SerializeField,Header("미사용 무기 오브젝트 무기 교체 시 비/활성화")]
            GameObject[] weaponeObj;

            /// <summary>
            /// 현재 무기 상태
            /// 교체할때 사용
            /// </summary>
            bool isGun = false;

            //무기 변경 후 애니메이션 값 반환
            int aniId = 0;

            public bool IsGun { get => isGun; set => isGun = value; }

            private void Start()
            {
                weaponeEquipPos[0] = GameObject.FindGameObjectWithTag("WeaponeEquip0");
                weaponeEquipPos[1] = GameObject.FindGameObjectWithTag("WeaponeEquip1");

                weaponeData[0] = weaponeEquipPos[0].GetComponentInChildren<WeaponeData>();
                weaponeData[1] = weaponeEquipPos[1].GetComponentInChildren<WeaponeData>();

                weaponeEquipPos[1].SetActive(false); //사용 무기
                weaponeObj[1].SetActive(true); //미사용 무기
            }


            /// <summary>
            /// 무기 교체 활성화
            /// </summary>
            public int WeaponeChange()
            {
                IsGun = !IsGun;

                if(IsGun)
                {
                    //사용 무시 활성화
                    weaponeEquipPos[0].SetActive(false);
                    weaponeEquipPos[1].SetActive(true);

                    //미 사용 무기
                    weaponeObj[0].SetActive(true);
                    weaponeObj[1].SetActive(false);

                    //무기 데이터에서 무기 타입을 가져온다(애니메이션 결정)
                    if (weaponeEquipPos[1].transform.childCount != 0)
                    {
                        //나중에 문제가 될수 있는 부분으로 코드를 남김
                        //aniId = weaponeEquipPos[1].GetComponentInChildren<WeaponeData>().WeaponeAniID;
                        aniId = weaponeData[1].WeaponeAniID;
                    }
                    else
                    {
                        aniId = 0;
                    }

                }
                else
                {
                    //사용 무시 활성화
                    weaponeEquipPos[0].SetActive(true);
                    weaponeEquipPos[1].SetActive(false);

                    //미사용
                    weaponeObj[0].SetActive(false);
                    weaponeObj[1].SetActive(true);

                    if (weaponeEquipPos[0].transform.childCount != 0)
                    {
                        //aniId = weaponeEquipPos[0].GetComponentInChildren<WeaponeData>().WeaponeAniID;
                        aniId = weaponeData[0].WeaponeAniID;
                    }
                    else
                    {
                        aniId = 0;
                    }
                }

                return aniId;
            }

        }
    }
}