using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이벤트 연출 시 사용되는 헬기
/// HP가 필요 없다
/// </summary>
namespace Black
{
    namespace Veh
    {
        public class HelicopterCtrl : VehCtrl
        {


            [SerializeField, Header("0 앞 / 1 뒤 플로펠러")]
            AirscrewCtrl[] aircrew;

        
            [SerializeField, Header("0 날개 회전 1 폭발")]
            AudioClip[] _sfx;

  

            private void Update()
            {
                if(IsLive)
                {
                    if(isMove)
                    {
                        if (!_audio.isPlaying)
                        {
                            //헬기 날개 회전 효과음
                            Manager.GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);
                        }

                        //날개 회전
                        for (int i = 0; i < aircrew.Length; i++)
                        {
                            aircrew[i].AirsCrewRot();
                        }

                        transform.position = Vector3.MoveTowards(transform.position, NextMovePos.position,
                                    MoveSpeed * Time.deltaTime);
                    }
                    
                    if(IsShake)
                    {
                        float dis = Vector3.Distance(player.transform.position, this.transform.position);
                       // Debug.Log("Dis : " + dis);

                        //카메라를 심하게 흔들음
                        if(dis <= 30)
                        {

                            StartCoroutine(shakeCam.ShakeCamAct(0.5f, 1f, 1f));
                            
                            //Debug.Log("Shake 1");
                            
                        }

                        //카메라를 약하게 흔듬
                        if (dis > 30 && dis <= 50)
                        {
                            StartCoroutine(shakeCam.ShakeCamAct(0.5f, 0.5f, 0.5f));
                        }

                        
                    }

                }

                //헬기 터짐
                else if(!isLive)
                {
                    //효과음

                    if(IsRot)
                    {
                        transform.Rotate(Vector3.up * ExplosionRotSpeed * Time.deltaTime);
                    }

                    if(IsSlow)
                    {
                        //Debug.Log("Slow");
                        //Manager.GameManager.INSTANCE.PlayerSpeed = 0.5f;
                        Time.timeScale = 0.3f;
                    }


                    StartCoroutine(ObjDis());
                }



            }


     

        }
        //class End
    }
}

