using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Car
    {
        public class SmokeEffect : MonoBehaviour
        {
            [SerializeField, Header("바퀴 연기 이팩트")]
            ParticleSystem[] tireSmoke;

            [SerializeField, Header("폭발 후 연기")]
            ParticleSystem smoke;

            /// <summary>
            /// 타이어 연기 이펙트 정지
            /// </summary>
            public void TireSmokeStop()
            {
                for(int i=0;i<tireSmoke.Length;i++)
                {
                    tireSmoke[i].Stop();
                }
            }

            /// <summary>
            /// 타이어 이펙트 재생
            /// </summary>
            public void TireSmokePlay()
            {
                for (int i = 0; i < tireSmoke.Length; i++)
                {
                    if (!tireSmoke[i].isPlaying)
                        tireSmoke[i].Play();
                }
            }

            /// <summary>
            /// 폭발 후 연기 이펙트 정지
            /// </summary>
            public void SmokeStop()
            {
                smoke.Stop();
            }

            /// <summary>
            /// 폭발 후 연기 이펙트 재생
            /// </summary>
            public void SmokePlay()
            {
                if (!smoke.isPlaying)
                    smoke.Play();
            }
        }

    }
}

