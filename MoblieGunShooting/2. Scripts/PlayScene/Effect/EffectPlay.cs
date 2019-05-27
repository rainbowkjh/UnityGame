using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스모크 이펙트
/// 플레이어가 들어오면 재생
/// 벗어나면 정지
/// </summary>
namespace Black
{
    namespace Effect
    {
        public class EffectPlay : MonoBehaviour
        {
            ParticleSystem particle;

            private void Start()
            {
                particle = GetComponent<ParticleSystem>();
            }

            private void OnTriggerEnter(Collider other)
            {
                if(other.tag.Equals("Player"))
                {
                    particle.Play();
                }
            }

            private void OnTriggerExit(Collider other)
            {
                if (other.tag.Equals("Player"))
                {
                    particle.Stop();
                }
            }

        }

    }
}
