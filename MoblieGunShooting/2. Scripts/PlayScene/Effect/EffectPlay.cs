using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
