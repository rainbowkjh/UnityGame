using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어를 키보드로 조작한다
/// </summary>
namespace CtrlManager
{
    public class PlayerKeyInput : MonoBehaviour
    {
        PlayerCtrl player;

        bool isCrouchKey = false; //앉는 키 입력 상태

        void Start()
        {
            player = GetComponent<PlayerCtrl>();
        }


        void Update()
        {
            //플레이어가 생존해 있을때만 제어
            if(player.State != CharState.Dead)
            {               
                Move();
                Crouch();
                Jump();
                Roll();

                player.LiveAni(true);
            }
        }

        void Move()
        {            
            float h = Input.GetAxisRaw("Horizontal");

            player.PlayerMove(h);
        }

        void Crouch()
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                isCrouchKey = !isCrouchKey;
              //  Debug.Log("C 입력");
                player.PlayerCrouch(isCrouchKey);
            }
        }

        void Jump()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                player.PlayerJump();
            }
        }

        void Roll()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                player.PlayerRoll();
            }
        }

    }

}
