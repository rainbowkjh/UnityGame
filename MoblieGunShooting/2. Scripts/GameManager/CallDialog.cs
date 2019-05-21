using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 파싱해서 가져온 텍스트 내용들을
/// 불러오는 스크립트로
/// 택스트를 출력 시킬 위치의 콜라이더에 적용 시킨다
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class CallDialog : MonoBehaviour
        {
            [SerializeField, Header("출력 시킬 택스트 리스트의 인덱스 값")]
            int dialogIndex;

            [SerializeField, Header("파싱하여 적용 시킨 텍스트 리스트")]
            DialogManager dialogManager;

            bool isEnter = false; //플레이어가 근접 했는지
            bool isPrint = false; //출력 후 텍스트 지움
            
            private void Update()
            {
                if(isEnter && isPrint)
                {
                    isPrint = false;
                    StartCoroutine(DialogPrint());
                }
            }

            IEnumerator DialogPrint()
            {
                //효과음 추가
                dialogManager.SfxPlay();

                dialogManager.DialogText.text = dialogManager.DialogList[dialogIndex];

                yield return new WaitForSeconds(3.0f);

                dialogManager.DialogText.text = "";
            }


            /// <summary>
            /// 플레이어가 접근하면 텍스트 출력
            /// </summary>
            /// <param name="other"></param>
            private void OnTriggerEnter(Collider other)
            {
                if(other.tag.Equals("Player"))
                {
                    if (!isEnter)
                    {
                        isEnter = true;
                        isPrint = true;
                    }
                        
                }
            }



        }

    }
}
