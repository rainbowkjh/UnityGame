using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 퀘스트 관리
/// 스테이지 매니저 오브젝트에 넣는다
/// 리스트로 관리 하며 완료 시 리스트에서
/// 퀘스트 아이디를 추적 하여 제거
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class QuestManager : MonoBehaviour
        {

            [SerializeField, Header("퀘스트 목표(파싱하지 않음, 이곳에 작성)")]
            string[] questStr;

            [SerializeField, Header("퀘스트 내용 출력 텍스트 프리펩")]
            QuestPrefabs questTextPrefab;

            [SerializeField, Header("퀘스트 목록(위에 프리펩을 생성 시킬 위치")]
            Transform questList;

            List<QuestPrefabs> quests = new List<QuestPrefabs>();
            bool isClear = false;

            AudioSource _audio;
            [SerializeField, Header("퀘스트 생성 효과음")]
            AudioClip[] _sfx;

            public List<QuestPrefabs> Quests { get => quests; set => quests = value; }

            private void Start()
            {
                _audio = GetComponent<AudioSource>();
            }

            /// <summary>
            /// id는 questStr의 인덱스로
            /// 퀘스트의 아이디 값이다
            /// </summary>
            /// <param name="id"></param>
            public void QuestCreate(int id)
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                QuestPrefabs data = Instantiate(questTextPrefab, questList);
                data.TextSetting(id, questStr[id]);

                Quests.Add(data);
            }

            /// <summary>
            /// 퀘스트 완료 시 목록 삭제
            /// </summary>
            /// <param name="id"></param>
            public void QuestDelete(int id)
            {
                //if(!isClear)
                //{
                //    isClear = true;

                for (int i = 0; i < Quests.Count; i++)
                {
                    if (id.Equals(Quests[i].Id))
                    {
                        if (Quests[i] != null)
                            Destroy(Quests[i].gameObject);
                    }
                }
                // }

            }

            /// <summary>
            /// 퀘스트가 활성화 상태인지 확인
            /// 활성화 된 퀘스트의 아이디와
            /// 현재 진행 중인 퀘스트 아이디와 확인
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public bool QuestCheck(int id)
            {
                bool isCheck = false;
                for(int i=0;i<Quests.Count;i++)
                {
                    if(id.Equals(Quests[i].Id))
                    {
                        isCheck = true;
                        break;
                    }
                }
                return isCheck;
            }
                 

        }
        //class End
    }
}
