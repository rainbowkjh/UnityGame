using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapone
{
    public class BulletCtrl : MonoBehaviour
    {
        /// <summary>
        /// 탄 발사를 플레이어가 하면 적만 타격을 줄수 있게함(플레이어는 안맞음)
        /// 적이 발사 하면 같은편의 적은 타격을 하지 않는다
        /// </summary>
        bool m_isPlayerBullet = true;
        float m_fSpeed = 10.0f;
        float m_fDmg = 50; //무기에서 랜덤값을 받아와 적용 시킨다.

        #region Set,Get
        public bool IsPlayerBullet
        {
            get
            {
                return m_isPlayerBullet;
            }

            set
            {
                m_isPlayerBullet = value;
            }
        }

        public float FDmg
        {
            get
            {
                return m_fDmg;
            }

            set
            {
                m_fDmg = value;
            }
        }

        public float FSpeed
        {
            get
            {
                return m_fSpeed;
            }

            set
            {
                m_fSpeed = value;
            }
        }
        #endregion
        private void OnEnable()
        {
            StartCoroutine(Disable());
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * FSpeed * Time.deltaTime);
        }

        IEnumerator Disable()
        {
            yield return new WaitForSeconds(2.0f);
            gameObject.SetActive(false);
        }

    }

}

