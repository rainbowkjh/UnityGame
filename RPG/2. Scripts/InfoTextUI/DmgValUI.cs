using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Black
{
    namespace PlayerUI
    {
        public class DmgValUI : MonoBehaviour
        {
            [SerializeField]
            Text dmgText;

            private void LateUpdate()
            {
                //가끔 비활성화가 되지 않아 강제로 비활성화 시킨다
                StartCoroutine(DisUI());
            }

            IEnumerator DisUI()
            {
                yield return new WaitForSeconds(1.0f);
                gameObject.SetActive(false);
            }

            /// <summary>
            /// 데미지를 출력한다
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="dmg"></param>
            public IEnumerator DmageValueUI<T>(T dmg)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Dmage ");
                sb.Append(dmg);
                                
                dmgText.text = sb.ToString();
                dmgText.fontSize = 15;

                yield return new WaitForSeconds(0.5f);
                dmgText.fontSize = 5;

                //yield return new WaitForSeconds(0.5f);
                //gameObject.SetActive(false); //크기를 작게 만들고 비활성화 시킨다
            }


        }

    }
}
