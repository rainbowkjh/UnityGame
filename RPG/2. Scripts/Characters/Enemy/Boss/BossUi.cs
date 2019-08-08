using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 보스 캐릭터가 생성 되면
/// HP UI를 활성화 시킨다
/// 
/// Boss 캐릭터에 적용 시키고
/// UI 를 연결 한다
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class BossUi : MonoBehaviour
        {
            [SerializeField, Header("UI Obj")]
            GameObject hpObj;

            [SerializeField, Header("UI Fill")]
            Image fill;

            [SerializeField, Header("UI Text")]
            Text hpText;

            private void Start()
            {
                hpObj.SetActive(false); //ui를 끈다
            }

            //UI 활성화
            public void UiAct()
            {
                hpObj.SetActive(true);
                fill.color = Color.green;
            }

            public void HPFill(int hp, int max)
            {
                fill.fillAmount = (float)hp / (float)max;

                //hp가 2/3 이상일떄 색
                if(hp > max * 0.6f)
                {
                    fill.color = Color.green;
                }

                if(hp > max * 0.3f && hp <= max *0.6f)
                {
                    fill.color = Color.yellow;
                }

                if (hp <= max * 0.3f)
                {
                    fill.color = Color.red;
                }

                StringBuilder sb = new StringBuilder();
                sb.Append(hp);
                sb.Append(" / ");
                sb.Append(max);

                hpText.text = sb.ToString();
            }

            public void UiOff()
            {
                hpObj.SetActive(false); //ui를 끈다
            }
        }

    }
}
