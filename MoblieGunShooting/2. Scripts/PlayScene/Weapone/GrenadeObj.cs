using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Weapone
    {
        public class GrenadeObj : MonoBehaviour
        {

            /// <summary>
            /// 콜라이더가 겹치기 때문에
            /// 날아오는 폭탄을 막을수가 없어서
            /// 끄고 있다가 터질때 켜짐
            /// </summary>
            [SerializeField, Header("폭발 범위 콜라이더")]
            GreadeHitColl explosionArea;

            [SerializeField, Header("수류탄 터지는 시간")]
            float explosionTime = 1.5f;
            /// <summary>
            /// 풀링으로 사용하면
            /// 재 활성화 될때 초기화를 해주기 위해서
            /// 임시로 폭발 시간을 저장
            /// </summary>
            float tempTime;

            /// <summary>
            /// 폭발 범위 활성화 상태
            /// </summary>
            bool isAct = false;

            /// <summary>
            /// 초기화
            /// </summary>
            private void Awake()
            {
                tempTime = explosionTime;
            }

            /// <summary>
            /// 활성화 될때마다
            /// 시간 초기화, 폭발 범위 비활성화
            /// </summary>
            private void OnEnable()
            {
                explosionArea.gameObject.SetActive(false);
                explosionTime = tempTime;
                isAct = false;
            }

            private void Update()
            {

                if(explosionTime > 0)
                {
                    explosionTime -= Time.deltaTime * 1.0f;
                }

                if(explosionTime <= 0 && !isAct)
                {
                    isAct = true;
                    explosionTime = 0;
                    StartCoroutine(GrenadeExplosion());
                }
            }

            /// <summary>
            /// 수류탄이 활성화 되면 일정 시간 후
            /// 콜라이더가 켜지면서 폭발함
            /// </summary>
            IEnumerator GrenadeExplosion()
            {
                // Debug.Log("Area");
                explosionArea.gameObject.SetActive(true);

                explosionArea.ExplosionPaly(); //이펙트
                explosionArea.IsAttack = true; //공격 데미지 적용

                yield return new WaitForSeconds(3.0f);

                explosionArea.IsAttack = false;
                explosionArea.enabled = false;
                gameObject.SetActive(false);
                
            }

        }

    }
}
