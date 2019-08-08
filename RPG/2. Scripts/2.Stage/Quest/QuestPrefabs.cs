using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 퀘스트 목록에 들어갈
/// 퀘스트 내용 오브젝트
/// 아이디 값으로
/// 퀘스트 완효 시 삭제 시킨다
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class QuestPrefabs : MonoBehaviour
        {
            private int id; //퀘스트 아이디
            [SerializeField]
            private Text questText; //내용 출력

            public int Id { get => id; set => id = value; }
            public Text QuestText { get => questText; set => questText = value; }

            /// <summary>
            /// 퀘스트를 세팅 시킨다
            /// </summary>
            /// <param name="id"></param>
            /// <param name="quest"></param>
            public void TextSetting(int id, string quest)
            {
                this.Id = id;
                QuestText.text = quest;
            }

        }

    }
}
