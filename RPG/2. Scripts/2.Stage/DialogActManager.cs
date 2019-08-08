using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어를 감지하면
/// 해당 아이디의 로그 내용을 출력한다
/// (출력 효과음, 잠시 후 비활성화 등..)
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class DialogActManager : MonoBehaviour
        {
            [Header("로그 리스트에서 출력 시킬 아이디 값")]
            public int logID;
            [Header("창 활성화 시간")]
            public float logTime;

            [SerializeField, Header("출력 시킬 텍스트 오브젝트")]
            GameObject textObj;
            [SerializeField, Header("로그 내용을 작성 시킬 텍스트")]
            Text logText;

            DialogDataParsing dialog;

            bool isEnter = false; //코루틴을 호출하기 위해(한번만 출력하기 때문에 false로 돌리지 않는다)
            bool isLog = false; //로그 출력 유무

            private void Start()
            {
              
                dialog = GameObject.Find("StageManager").GetComponent<DialogDataParsing>();
                textObj.SetActive(false);
            }


            private void LateUpdate()
            {
                if(isEnter && !isLog)
                {
                    isLog = true;
                    StartCoroutine(DiaLogText(logTime));
                }
            }

            private void OnTriggerEnter(Collider other)
            {
                if(!isEnter)
                {
                    if(other.transform.CompareTag("Player"))
                    {
                        isEnter = true;
                    }
                }
            }

            IEnumerator DiaLogText(float delay)
            {
                textObj.SetActive(true);
                //logText.text = dialog.DiaLogList[logID].log;
                StringBuilder sb = new StringBuilder();

                if (dialog.DiaLogList[logID].talker.Equals("Player"))
                {
                    sb.Append("<color=#00ff00>");
                    sb.Append(dialog.DiaLogList[logID].talker);
                    sb.Append("</color>");
                }

                else
                {
                    sb.Append("<color=#ff0000>");
                    sb.Append(dialog.DiaLogList[logID].talker);
                    sb.Append("</color>");
                }

                sb.Append(" : ");
                sb.Append(dialog.DiaLogList[logID].log);

                logText.text = sb.ToString();

                yield return new WaitForSeconds(delay);
                textObj.SetActive(false);
                logText.text = null;
            }
        }
        //class End
    }
}
