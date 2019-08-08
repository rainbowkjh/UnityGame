using Black.Veh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이벤트 캐릭터 제어로
/// 주로 좌표? 콜라이더를 통해서
/// 캐릭터의 행동을 결정 짓도록 한다
/// 
/// VehCtrl이 원래는 탑승차량 생각해서 만들었는데
/// 이벤트 연출시 캐릭터에도 비슷하게 사용 되서
/// 같이 사용 
/// </summary>
/// 
namespace Black
{
    namespace Characters
    {
        public class SwatCtrl : VehCtrl
        {
           
            Animator ani;

            readonly int hashMove = Animator.StringToHash("Move");
            readonly int hashFire = Animator.StringToHash("Fire");
            readonly int hashDown = Animator.StringToHash("Down");

            float fireRate = 0.1f;
            bool isDownAni = false;

            //무기 관련
            [SerializeField, Header("총구 화염")]
            ParticleSystem muzzlePar;
                        
            [SerializeField, Header("효과음, 0 사격")]
            AudioClip[] _sfx;

            protected override void Start()
            {
                base.Start();
                ani = GetComponent<Animator>();            
            }

            private void Update()
            {
                if (IsLive)
                {
                    if (isMove)
                    {
                        transform.position = Vector3.MoveTowards(transform.position,NextMovePos.position,
                                    MoveSpeed * Time.deltaTime);
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


                    }

                    if(IsAttack)
                    {
                        AniFire();
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
                    AniDown();

                    if (IsSlow)
                    {
                        //Debug.Log("Slow");
                        //Manager.GameManager.INSTANCE.PlayerSpeed = 0.5f;
                        Time.timeScale = 0.3f;
                    }


                    StartCoroutine(ObjDis());
                }


                AniMove(isMove); //
            }
            //

                /// <summary>
                /// 공격 애니메이션
                /// </summary>
            public void AniFire()
            {
                if(Time.time >= fireRate)
                {
                    fireRate = Time.time + fireRate;

                    Manager.GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);
                    muzzlePar.Play();
                    
                    ani.SetTrigger(hashFire);
                }
            }

            /// <summary>
            /// 이동 애니
            /// </summary>
            /// <param name="ismove"></param>
            public void AniMove(bool ismove)
            {
                ani.SetBool(hashMove, isMove);
            }

            /// <summary>
            /// 쓰러지는 애니
            /// </summary>
            public void AniDown()
            {
                if(!isDownAni)
                {
                    isDownAni = true;
                    ani.SetTrigger(hashDown);
                }
            }

        }
        //
    }
}
