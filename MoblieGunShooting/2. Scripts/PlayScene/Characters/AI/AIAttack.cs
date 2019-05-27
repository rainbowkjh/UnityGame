using Black.DmgManager;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 제자리에서 레이에 적이 감지 되면 사격만 하는 AI
/// 기관총을 들고 있는 AI 캐릭터에 해당
/// (탑승 차량의 AI)
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class AIAttack : MonoBehaviour
        {
            [SerializeField,Header("FirePos")]
            Transform FirePos;

            [SerializeField, Header("Muzzle")]
            ParticleSystem muzzle;

            AudioSource _audio;
            [SerializeField, Header("FireSfx")]
            AudioClip _sfx;

            float fireRate = 0.01f;
            float fireTime = 0.0f;

            private void Start()
            {
                _audio = GetComponent<AudioSource>();
            }

            void Update()
            {
                RaycastHit hit;
                int layerMask = 1 << 8;
                layerMask = ~layerMask;

                if (Physics.Raycast(FirePos.position, FirePos.forward, out hit, 500, layerMask))
                {
                    Debug.DrawLine(FirePos.position, hit.point, Color.red);

                    if(hit.transform.tag.Equals("Enemy"))
                    {
                        Fire(hit);                        
                    }
                }
                
            }


            void Fire(RaycastHit hit)
            {
                if(Time.time >= fireTime)
                {
                    fireTime = Time.time + fireRate + Random.Range(0.0f, 0.2f);

                    muzzle.Play();
                    _audio.volume = GameManager.INSTANCE.volume.sfx * 0.7f;
                    _audio.PlayOneShot(_sfx);

                    float dmg = Random.Range(25, 50);

                    if(hit.transform.GetComponent<HitDmg>())
                    {
                        hit.transform.GetComponent<HitDmg>().HitDamage(dmg);
                    }
                }
            }


        }

    }
}
