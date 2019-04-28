using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;


/// <summary>
/// 플레이 씬으로 넘어가기 전
/// 세팅하는 씬에서 플레이어를 조작 한다
/// 나중에 플레이 캐릭터를 3명??으로 변경 할수 있기 떄문에
/// 싱글턴을 사용하지 않는다
/// 
/// 기능적으로 이동하는 것과 데이터 값 초기화 정도 이므로
/// 스크립트 하나로 끝냄
/// </summary>
namespace MainScene
{
    namespace MainPlayer
    {
        public class MainPlayerCtrl : CharactersData
        {
            Animator ani;

            readonly int hashWalk = Animator.StringToHash("Walk");
            readonly int hashUse = Animator.StringToHash("Use");

            public object InvetoryList { get; private set; }

            void Start()
            {
                ani = GetComponent<Animator>();              
            }

            
            void Update()
            {
                Move();
            }

      

            void Move()
            {
                float h = Input.GetAxisRaw("Horizontal");

                PlayerMove(h);
            }

            public void PlayerMove(float h)
            {
                if (State != CharState.Crouch
                    && !IsRoll)
                {

                    Vector3 dir = new Vector3(h, 0, 0).normalized;
                    bool isMove = false;

                    if (h > 0 || h < 0)
                    {
                        PlayerRot(h);

                        //State = CharState.Walk;
                        State = CharState.Run;
                        isMove = true;
                    }

                    else if (h == 0)
                    {
                        State = CharState.Idle;
                        isMove = false;
                    }

                    transform.Translate(dir * FSpeed * Time.deltaTime, Space.World);

                    //WalkAni(isMove);
                    ani.SetBool(hashWalk, isMove);
                }

            }

            /// <summary>
            /// 플레이어가 바라보는 방향을 결정한다
            /// </summary>
            /// <param name="h"></param>
            private void PlayerRot(float h)
            {
                if (h > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                if (h < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                }

            }


        }
    }

}
