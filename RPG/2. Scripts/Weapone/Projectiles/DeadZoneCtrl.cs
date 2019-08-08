using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// DeadZone 제어
/// </summary>
namespace Black
{
    namespace Weapone
    {
        public class DeadZoneCtrl : MonoBehaviour
        {
            [SerializeField, Header("속도"), Range(0.0f, 5.0f)]
            float scaleSpeed;

            [SerializeField, Header("On 크기 커짐, Off 작아짐")]
            bool isScale = true;

            [SerializeField, Header("크기 설정")]
            float scaleValue;

            //현재 크기
            float curScale = 0;

            // Start is called before the first frame update
            void Start()
            {
                if(isScale)
                {
                    transform.localScale = new Vector3(0, 0, 0);
                }

                else if(!isScale)
                {
                    transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
                }
                
            }

            // Update is called once per frame
            void Update()
            {
                if(isScale)
                {
                    //세팅 한 값보다 적은 동안 크게 만든다
                    if(curScale < scaleValue)
                    {
                        curScale += scaleSpeed * Time.deltaTime;
                        transform.localScale = new Vector3(curScale, curScale, curScale);
                    }

                    //같아지면 오브젝트 삭제
                    if (curScale == scaleValue)
                    {
                        gameObject.SetActive(false);
                        //Destroy(gameObject);
                    }
                        

                }

                if(!isScale)
                {
                    if (scaleValue > 0)
                    {
                        scaleValue -= scaleSpeed * Time.deltaTime;
                        transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
                    }

                    if (scaleValue == 0)
                    {
                        gameObject.SetActive(false);
                        //Destroy(gameObject);
                    }
                        
                }
            }
        }

    }
}
