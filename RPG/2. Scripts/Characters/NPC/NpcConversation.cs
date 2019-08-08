using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// NPC의 대화를 관리 한다
/// 
/// XML파일의 이름을 검색해서
/// 해당 캐릭터의 대화 차례로 보고
/// 간단한 동작과 대화창을 출력한다
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class NpcConversation : MonoBehaviour
        {
            [SerializeField, Header("대화 하는 캐릭터")]
            NpcCtrl[] npc;

            [Header("창 활성화 시간")]
            public float logTime;

            [SerializeField]
            GameObject textObj;
            [SerializeField, Header("로그 내용을 작성 시킬 텍스트")]
            Text logText;

            DialogDataParsing dialog;

            bool isEnter = false;
            bool isLogStart = false;

            [SerializeField, Header("대화 리스트 시작 아이디")]
            int startID;

            [SerializeField, Header("대화 리스트 끝 아이디")]
            int endID;

            PlayerCtrl player;

            //Quest----
            [SerializeField, Header("대화가 끝나면 퀘스트 생성")]
            bool isQuest = false;
            QuestManager manager;
            [SerializeField, Header("생성 시킬 퀘스트 아이디")]
            int questID;


            private void Start()
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
                dialog = GameObject.Find("StageManager").GetComponent<DialogDataParsing>();
                manager = GameObject.Find("StageManager").GetComponent<QuestManager>();
                textObj.SetActive(false);

                if(GameManager.INSTANCE.IsTest)
                    manager.QuestCreate(questID);
            }


            private void LateUpdate()
            {
                if(isLogStart)
                {
                    isLogStart = false;

                    StartCoroutine(NPCLog(3));
                }
            }

            
            private IEnumerator NPCLog(float delay)
            {
                textObj.SetActive(true);
                //logText.text = dialog.DiaLogList[logID].log;
                StringBuilder sb = new StringBuilder();

                //디아로그 리스트
                for (int i = startID; i <= endID; i++)
                {
                    if (dialog.DiaLogList[i].talker.Equals("Player"))
                    {
                        textObj.SetActive(true);

                        sb.Append("<color=#00ff00>");
                        sb.Append(dialog.DiaLogList[i].talker);
                        sb.Append("</color>");

                        sb.Append(" : ");
                        sb.Append(dialog.DiaLogList[i].log);

                        logText.text = sb.ToString();

                        yield return new WaitForSeconds(delay);
                        textObj.SetActive(false);
                        logText.text = null;

                        if (sb != null)
                            sb.Clear();
                    }

                    //npc
                    for (int j = 0; j < npc.Length; j++)
                    {
                        //련재 리스트의 아이디 값의 대화자의 이름과
                        //이름이 일치하는 NPC를 찾는다
                        if (dialog.DiaLogList[i].talker.Equals(npc[j].CharName))
                        {
                            textObj.SetActive(true);

                            sb.Append("<color=#0000ff>");
                            sb.Append(dialog.DiaLogList[i].talker);
                            sb.Append("</color>");

                            sb.Append(" : ");
                            sb.Append(dialog.DiaLogList[i].log);

                            logText.text = sb.ToString();

                            int ran = Random.Range(0, 1);

                            ///대화 캐릭터 애니메이션
                            npc[j].GetComponent<NpcCtrl>().AniTalk(true, ran);

                            yield return new WaitForSeconds(delay);
                            textObj.SetActive(false);
                            logText.text = null;

                            if (sb != null)
                                sb.Clear();

                            npc[j].GetComponent<NpcCtrl>().AniTalk(false, ran);
                        }
                    }
                }

                //퀘스트 생성 
                manager.QuestCreate(questID);
            }



            private void OnTriggerEnter(Collider other)
            {
                if(!isEnter)
                {
                    if(other.transform.CompareTag("Player"))
                    {
                        isEnter = true;

                        isLogStart = true;

                        player.Stop();
                    }
                }
            }

        }

    }
}
