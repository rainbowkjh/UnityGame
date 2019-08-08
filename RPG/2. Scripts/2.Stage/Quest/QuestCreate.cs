using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 퀘스트를 생성 시킨다
/// 생성 시킬 타이밍(?)에 맞혀 호출 시킨다
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class QuestCreate : MonoBehaviour
        {
            QuestManager manager;

            [SerializeField, Header("정해진 오브젝트 감지 시 생성")]
            bool isEnterQuest = false;

            [SerializeField, Header("감지항 대상 태그")]
            string objTag;

            [SerializeField, Header("생성 시킬 퀘스트 아이디")]
            int questID;

            private void Start()
            {
                manager = GameObject.Find("StageManager").GetComponent<QuestManager>();
            }


            private void OnTriggerEnter(Collider other)
            {
                if (isEnterQuest)
                {
                    if (other.transform.CompareTag(objTag))
                    {
                        manager.QuestCreate(questID);
                    }
                }
            }




        }

    }
}
