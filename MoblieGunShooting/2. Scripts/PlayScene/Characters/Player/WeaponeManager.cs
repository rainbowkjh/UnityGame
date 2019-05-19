using Black.Weapone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Black
{
    namespace Characters
    {
        public class WeaponeManager : MonoBehaviour
        {
            [SerializeField,Header("무기 장착 위치")]
            GameObject[] weaponePos;

    
            #region Set,Get
            public GameObject[] WeaponePos
            {
                get
                {
                    return weaponePos;
                }

                set
                {
                    weaponePos = value;
                }
            }



            #endregion
            //private void Start()
            //{
            //    WeaponeChange(0);
            //}

            /// <summary>
            /// 무기 활성화
            /// </summary>
            /// <param name="index"></param>
            public void WeaponeChange(int index)
            {
                for(int i=0;i<WeaponePos.Length;i++)
                {
                    weaponePos[i].SetActive(false);
                }

                weaponePos[index].SetActive(true);
            }


            /// <summary>
            /// 소총 장착 시 트랜스폼 값
            /// 무기를 처음부터 소지하고
            /// 다른 무기로 교체 기능을 삭제 하면서
            /// 사용하지 않는다
            /// </summary>
            public void WeaponeEquip(int index)
            {
                //무기 장착 위치에 오브젝트가 비어 있지 않으면
                if(WeaponePos[index].transform.childCount !=0)
                {
                    //Pistol 장착 위치
                    if (WeaponePos[index].GetComponentInChildren<WeaponeCtrl>().WeaponeState == 0)
                    {
                        WeaponePos[index].transform.localPosition = new Vector3(0.06f, 0f, 0f);
                        WeaponePos[index].transform.localRotation = Quaternion.Euler(353.45f, 91.96f, 89.77f);
                    }

                    //AR
                    if (WeaponePos[index].GetComponentInChildren<WeaponeCtrl>().WeaponeState == 1)
                    {
                        WeaponePos[index].transform.localPosition = new Vector3(0.20f, 0f, 0.09f);
                        WeaponePos[index].transform.localRotation = Quaternion.Euler(357.6f, 90.9f, 89.9f);
                    }

                    //SG
                    if (WeaponePos[index].GetComponentInChildren<WeaponeCtrl>().WeaponeState == 2)
                    {
                       WeaponePos[index].transform.localPosition = Vector3.zero;
                        WeaponePos[index].transform.localRotation = Quaternion.Euler(357.6f, 90.9f, 89.9f);
                    }
                }


            }


            #region 무기 정보를 가져온다(무기 교체 시 UI 정보 최신화)
            public string GetWeaponeName(int index)
            {
                return weaponePos[index].GetComponentInChildren<WeaponeCtrl>().WeaponeName;
            }

            public float GetWeaponeMinDmg(int index)
            {
                return weaponePos[index].GetComponentInChildren<WeaponeCtrl>().FMinDmg;
            }

            public float GetWeaponeMaxDmg(int index)
            {
                return weaponePos[index].GetComponentInChildren<WeaponeCtrl>().MaxDmg;
            }

            public int GetWeaponeAmmo(int index)
            {
                return weaponePos[index].GetComponentInChildren<WeaponeCtrl>().NCurBullet;
            }

            public int GetWeaponeMaxAmmo(int index)
            {
                return weaponePos[index].GetComponentInChildren<WeaponeCtrl>().NMag;
            }
            #endregion
        }

    }
}
