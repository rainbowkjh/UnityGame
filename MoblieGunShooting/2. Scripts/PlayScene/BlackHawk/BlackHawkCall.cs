using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 블랙호크 콜이지만
/// 다른 탑승 차량도 불러오는 기능(NPC 캐릭터도 불러오도록 한다)
/// -플레이어가 특정 위치에 도착하면
/// 차량을 활성화 시켜 차량을 이동 위치에 따라
/// 움직이도록 한다
/// </summary>
namespace Black
{
    namespace Car
    {
        public class BlackHawkCall : MonoBehaviour
        {
            [SerializeField, Header("활성화 시킬 오브젝트")]
            GameObject[] actObj;

            [SerializeField, Header("감지 시킬 오브젝트 태그")]
            string tagName;

            bool isEnter = false;

            private void OnTriggerEnter(Collider other)
            {
                if(!isEnter)
                {
                    if(other.tag.Equals(tagName))
                    {
                        for(int i=0;i<actObj.Length;i++)
                        {
                            actObj[i].SetActive(true);
                        }
                        
                    }
                }
            }
        }

    }
}
