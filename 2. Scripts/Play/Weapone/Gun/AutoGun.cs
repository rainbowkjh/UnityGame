using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapone
{
    public class AutoGun : GunCtrl
    {
        [SerializeField, Header("연사 속도")]
        float m_fFireDelay = 0.01f;
        float m_Time = 0.0f;


        protected override void Start()
        {
            base.Start();
        }

        void Update()
        {
            if (player.State != Characters.CharState.Dead
           && !player.IsRoll)
            {
                Fire();
                Reload();
            }
                
        }

        protected override void Fire()
        {
            if (Input.GetKey(KeyCode.O))
            {
                if (!isReload && NCurBullet > 0)
                {
                    if (Time.time >= m_Time)
                    {
                        //Debug.Log("연사");
                        m_Time = Time.time + m_fFireDelay + Random.Range(0.0f, 0.03f);

                        BulletSelect();

                        NCurBullet--;

                        player.FireAni();
                        MuzzlePlay();
                        m_WeaponeUI.WeaponeBarUI(this);
                    }
                }
            }
        }

    }

}

