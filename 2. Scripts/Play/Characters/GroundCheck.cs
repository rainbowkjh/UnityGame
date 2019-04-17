using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 캐릭터가 땅을 밟고 있는지 체크
/// (레이를 바닥 방향으로 날려서 땅을 밟고 있는지 체크)
/// (레이의 길이가 너무 길면, 점프를 뛰기 시작할때도 레이에 걸리기 때문에
///     점프를 더 할 수 있는 상태가 되기 때문에 캐릭터의 발 부분과 비슷한 길이로 조절해 준다)
/// </summary>
namespace Characters
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField,Header("캐릭터 그라운드 체크 후 애니메이션 변경")]
        AniCtrl ani;

        [SerializeField, Header("바닥 레이로 체크, 레이 시작 지점")]
        Transform m_trRay;

        bool isDropAni = false;

        private void LateUpdate()
        {
            RaycastHit hit;
                        
            //캐릭터가 땅에 있을때
            //착지 애니메이션 실행
            if (Physics.Raycast(m_trRay.position, m_trRay.transform.forward, out hit, 1))
            {
                Debug.DrawLine(m_trRay.position, hit.point, Color.yellow); //레이 작동 확인

                if (hit.transform.tag.Equals("Ground"))
                {                    
                    //  Debug.Log("Ground");
                    //점프 애니메이션
                    ani.GroundAni(true);
                    ani.IsJump = false; // <- 점프랑 충돌 발생으로 2~3단 점프가 된다(레이 길이를 1로 줄여서 해결)

                    isDropAni = false;

                }

            }

            //점프 안하고 내려갈때
            else
            {                   
                ani.IsJump = true;
                ani.GroundAni(false);

                //연속으로 트리거 동작 하지 못하도록 함
                if(!isDropAni)
                {
                    isDropAni = true;
                    ani.DropAni();                    
                }
                
            }
        }



    }

}
