using Black.Veh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 우선 정해진 좌표로 날아 다니는 것만 연출
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class SoulExCtrl : VehCtrl
        {
            
            [SerializeField, Header("효과음, 0 공중 날아다니는 소리")]
            AudioClip[] _sfx;

            private void Update()
            {
                if (IsLive)
                {
                    if (isMove)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, NextMovePos.position,
                                    MoveSpeed * Time.deltaTime);

                        //바라보는 방향을 바꾼다
                        transform.rotation = Quaternion.Slerp(transform.rotation, NextMovePos.rotation,
                            25 * Time.deltaTime);
                    }

                    if (IsShake)
                    {
                        float dis = Vector3.Distance(player.transform.position, this.transform.position);
                        // Debug.Log("Dis : " + dis);

                        //카메라를 심하게 흔들음
                        if (dis <= 30)
                        {

                            StartCoroutine(shakeCam.ShakeCamAct(0.5f, 1f, 1f));

                            //Debug.Log("Shake 1");

                        }

                        //카메라를 약하게 흔듬
                        if (dis > 30 && dis <= 50)
                        {
                            StartCoroutine(shakeCam.ShakeCamAct(0.5f, 0.5f, 0.5f));
                        }

                        if (IsSlow)
                        {
                            //Debug.Log("Slow");
                            //Manager.GameManager.INSTANCE.PlayerSpeed = 0.5f;
                            Time.timeScale = 0.3f;
                        }
                    }

                }

                //캐릭터 쓰러짐
                else if (!isLive)
                {
                    //if (IsRot)
                    //{
                    //    transform.Rotate(Vector3.up * ExplosionRotSpeed * Time.deltaTime);
                    //}
                    isMove = false;
                    
                    if (IsSlow)
                    {
                        //Debug.Log("Slow");
                        //Manager.GameManager.INSTANCE.PlayerSpeed = 0.5f;
                        Time.timeScale = 0.3f;
                    }


                    StartCoroutine(ObjDis());
                }


            }
            //

           
        }

    }
}
