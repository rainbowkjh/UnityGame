using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Black.Effect;
using Black.CameraUtil;

/// <summary>
/// 특정 지역에 접근하면
/// 효과음을 바꾼다
/// 애니메이션으로 움직이는 차량에 해당
/// </summary>
namespace Black
{
    namespace Car
    {
        [Serializable]
        public enum TrooperState
        {
            Idel, Excel, Break,
        }

        [Serializable]
        public enum ExplosionType
        {
            Timer, NoTimer,
            //일정 시간 후, 즉시 폭발
        }

        [RequireComponent(typeof(ExplosionAfter)),
            RequireComponent(typeof(SmokeEffect))]
        public class TrooperCar : MonoBehaviour
        {

            AudioSource _audio;
            [SerializeField, Header("0:Idel, 1:Excel, 2:Brake")]
            AudioClip[] _sfx;

            /// <summary>
            /// 애니메이션 미사용
            /// 코드로 제어
            /// </summary>
            TrooperState carState;

            float carSpeed;
            Transform nextPos;

            /// <summary>
            /// 차량 폭발 시
            /// 기능 비활성화 또는 오브젝트 비활성화
            /// </summary>
            [SerializeField, Header("탑승 캐릭터??")]
            GameObject charObj;

            [SerializeField, Header("차량 폭발 옵션")]
            ExplosionType explosionType;

            ExplosionAfter explosion;

            /// <summary>
            /// 자동차 이펙트
            /// </summary>
            SmokeEffect smokeEffect;

            [SerializeField, Header("폭발 시 카메라 효과")]
            shakeCamera shakeCam;

            /// <summary>
            /// 자동차 수명 상태
            /// </summary>
            bool isCarLive = true;

            #region Set,Get
            public TrooperState CarState
            {
                get
                {
                    return carState;
                }

                set
                {
                    carState = value;
                }
            }

            public float CarSpeed
            {
                get
                {
                    return carSpeed;
                }

                set
                {
                    carSpeed = value;
                }
            }

            public Transform NextPos
            {
                get
                {
                    return nextPos;
                }

                set
                {
                    nextPos = value;
                }
            }
            #endregion

            private void Start()
            {
                _audio = GetComponent<AudioSource>();
                _audio.PlayOneShot(_sfx[0]);

                explosion = GetComponent<ExplosionAfter>();
                smokeEffect = GetComponent<SmokeEffect>();
            }

            private void Update()
            {
                //폭발 되기 전 상태 효과음 변경
                if (!explosion.IsExplosion)
                {
                    //정차 상태
                    if (CarState == TrooperState.Idel)
                    {
                        _audio.PlayOneShot(_sfx[0]);
                        
                        //타이어 연기 이팩트
                        smokeEffect.TireSmokeStop();
                    }

                    //드라이브 상태
                    if (CarState == TrooperState.Excel)
                    {
                        _audio.PlayOneShot(_sfx[1]);
                        CarNextMove();

                        //Debug.Log("Tire Smoke");
                        smokeEffect.TireSmokePlay();
                    }

                    //브레이크
                    if (CarState == TrooperState.Break)
                    {
                        _audio.PlayOneShot(_sfx[2]);

                        smokeEffect.TireSmokePlay();
                    }
                }

                //폭발 시 캐릭터 및 연기 활성화
                if (explosion.IsExplosion
                    && isCarLive)
                {
                    isCarLive = false;

                    //폭발 연기 재생 시킴
                    smokeEffect.SmokePlay();

                    StartCoroutine(shakeCam.ShakeCamera(0.2f, 0.5f, 0.5f));

                    if (charObj != null)
                    {
                        charObj.SetActive(false);
                    }
                }

                //폭발 옵션 확인
                if (explosion.IsTimerStart)
                {
                    ExplosionOption();
                }

                Voulme();
            }

            #region 애니메이션 제어 시 차량 효과음(사용안함)
            //private void OnTriggerEnter(Collider other)
            //{
            //    //대기
            //    if (other.tag.Equals("Idle"))
            //    {
            //        CarState = TrooperState.Idel;
            //    }

            //    //이동
            //    if (other.tag.Equals("Excel"))
            //    {
            //        CarState = TrooperState.Excel;
            //    }

            //    //브레이크
            //    if(other.tag.Equals("Brake"))
            //    {
            //        CarState = TrooperState.Break;
            //    }
            //}
            #endregion

            /// <summary>
            /// 타이머 폭발, 즉시 폭발 등 옵션에 맞게 실행
            /// </summary>
            private void ExplosionOption()
            {
                switch (explosionType)
                {
                    case ExplosionType.Timer:
                        explosion.ExplosionTimer();
                        break;
                    case ExplosionType.NoTimer:
                        explosion.ExplosionPlay();
                        break;
                }
            }

            /// <summary>
            /// 효과음 볼륨 조절
            /// </summary>
            private void Voulme()
            {
                _audio.volume = GameManager.INSTANCE.volume.sfx / 4;
            }

            /// <summary>
            /// 탑승 차량을 다음 위치로 보냄
            /// </summary>
            void CarNextMove()
            {
                if (NextPos != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                        nextPos.position, CarSpeed * Time.deltaTime);
                }
            }

            /// <summary>
            /// 브레이크 효과음 후 정차 효과음
            /// </summary>
            /// <returns></returns>
            public IEnumerator CarBrake()
            {
                carState = TrooperState.Break;
                yield return new WaitForSeconds(0.5f);
                carState = TrooperState.Idel;
            }


        }
    }
}