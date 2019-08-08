using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이벤트 제어
/// 현재 이벤트가 끝나고
/// 다음 이벤트를 시작 시
/// 생성 되는 오브젝트를 
/// 한번에 생성 시키지 않고
/// 시간차를 두고 생성 시켜주는 역할
/// (오브젝트 비활성화는 스크립트를 따로(먼저) 만들었었고
/// 이건 생성만 제어한다)
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class CreateObjCtrl : MonoBehaviour
        {

            [SerializeField, Header("오브젝트 생성 시간 차")]
            float createTime;

            [SerializeField, Header("생성 시킬 오브젝트 또는 묶음들")]
            GameObject[] createObj;

            [SerializeField, Header("카메라 흔들림 효과")]
            bool isShake = false;
            [SerializeField]
            ShakeCamera shakeCam;

            bool isCreate = false;

            private void Start()
            {                
                //다른 곳에서 오브젝트를 비활성화 시키겠지만
                //이벤트 외 다른 곳에 사용 할때를 대비해서
                //다시 비활성화 시킨다
                //참고로 콜라이도로 오브젝트 를 시간차를 두고 생성 시키는 것은 있다
                //플레이어가 특정 지역 도착 시 적 생성하는거..
                for (int i = 0; i < createObj.Length; i++)
                {
                    createObj[i].SetActive(false);
                }
                
            }

            private void Update()
            {
                if (!isCreate)
                    StartCoroutine(CreateDelay(createTime));
            }

            IEnumerator CreateDelay(float delay)
            {
                isCreate = true;
                for (int i=0;i<createObj.Length;i++)
                {
                    createObj[i].SetActive(true);

                    if(isShake)
                    {
                        StartCoroutine(shakeCam.ShakeCamAct(0.2f, 0.5f, 0.5f));                        
                    }

                    yield return new WaitForSeconds(delay);
                }
            }

        }

    }
}
