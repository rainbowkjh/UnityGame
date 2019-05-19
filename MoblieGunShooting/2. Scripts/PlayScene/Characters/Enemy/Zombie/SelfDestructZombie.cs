using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 자폭하는 좀비??
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class SelfDestructZombie : ZombieCtrl
        {
            [SerializeField, Header("폭발 이펙트")]
            ParticleSystem explosionPar;

            AudioSource _audio;
            [SerializeField, Header("폭발 효과음")]
            AudioClip[] _sfx;

            
            protected override void AttackDelayTime()
            {
                //Debug.Log("SD Zombie");

                attackBar.LookAtCam();

                //IsFire = false;
                AttackWait = true;

                IsFire = false;

                float Ran = Random.Range(MinAttackSpeed, MaxAttackSpeed);

                delayTime += Time.deltaTime * Ran;

                attackBar.AttackGauge(delayTime);


                if (delayTime >= 1.0f)
                {
                    delayTime = 0;
                    IsFire = true;
                    AttackWait = false;

                    _audio = GetComponent<AudioSource>();
                    _audio.volume = GameManager.INSTANCE.volume.sfx / 4;
                    _audio.PlayOneShot(_sfx[0]);

                    Hp = 0;
                    IsLive = false;

                    //좀비 수 감소
                    GameManager.INSTANCE.NEnemyCount--;

                    explosionPar.Play();
                    StartCoroutine(Disable());
                }


                attackBar.AttackBarAni(AttackWait);
            }

            IEnumerator Disable()
            {
                yield return new WaitForSeconds(1.5f);

                
                gameObject.SetActive(false);
            }

        }

    }
}
