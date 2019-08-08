using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Black
{
    namespace Weapone
    {
        public class AutoGun : WeaponeData, IWeaponeCtrl
        {
            [SerializeField, Header("탄 정보 출력(GameCanvas/UI/AmmoText")]
            Text ammoText;

            float fireTime = 0.0f;

            public void Fire()
            {
                if (!player.IsReload && !player.IsInven && !player.IsStun && !player.IsChestOpen
                    && !player.IsSkill)
                {
                    if (Input.GetMouseButton(1) &&
                        NBullet > 0)
                    {
                        if (Time.time >= fireTime)
                        {
                            GameManager.INSTANCE.SFXPlay(_Audio, _Sfx[0]);

                            NBullet--;

                            fireTime = Time.time + FireRate + Random.Range(0.0f, 0.3f);

                            player.FSatiety -= 0.5f; //배고품 수치를 1씩 감소

                            StartCoroutine(player.ShakeCam.ShakeCamAct(0.05f, 0.1f, 0.1f)); //카메라 흔들림
                            player.Stop();
                            aniCtrl.AniFire();
                            Muzzle.Play();
                            BulletSetting(true);
                        }
                    }

                    else if (Input.GetMouseButtonDown(1) && NBullet <= 0)
                    {
                        GameManager.INSTANCE.SFXPlay(_Audio, _Sfx[2]);
                    }
                }

            }

            public void Reload()
            {
                if(!player.IsReload && !player.IsInven && !player.IsStun)
                {
                    if (Input.GetKeyDown(KeyCode.R)
                        && NBullet < NMaxMag //탄창에 한발이라고 사용 했을때
                        && player.NAmmo > 0) //남은 탄이 있을때 재장전
                    {
                        StartCoroutine(ReloadDelay());

                    }
                }
                
            }
            

            private void AmmoInfo()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(NBullet);
                sb.Append(" / ");
                sb.Append(NMaxMag);

                ammoText.text = sb.ToString();
            }

            void Update()
            {
                if(player.IsLive && !player.IsRoll
                    &&!GameManager.INSTANCE.IsEvent)
                {
                    Fire();
                    Reload();

                    AmmoInfo();
                }
                
            }


        }


    }
}
