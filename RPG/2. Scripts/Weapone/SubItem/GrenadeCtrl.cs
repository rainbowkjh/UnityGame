using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 수류탄 오브젝트
/// 폭발 데미지 속성
/// 다른 속성은 기절, ....
/// </summary>
namespace Black
{
    namespace Weapone
    {
        public class GrenadeCtrl : GrenadeData
        {
            string grenadeName = "Grenade";            

            protected override void Start()
            {
                base.Start();
                DataInit(grenadeName);
                
            }

            private void LateUpdate()
            {
                if (!isExplosion)
                    StartCoroutine(GrenadeExplosion());
            }

            


            /// <summary>
            /// 수류탄 투척 후 잠시 후 폭발하면서
            /// 데미지를 준다
            /// </summary>
            /// <returns></returns>
            IEnumerator GrenadeExplosion()
            {
                isExplosion = true;
                yield return new WaitForSeconds(1.0f);

                ParticleSystem effect = pool.GetParticlePool(pool.GrenadeExplosionCount, pool.GrenadeExplosionList);

                if(effect != null)
                {
                    effect.transform.position = this.transform.position;
                    effect.transform.rotation = this.transform.rotation;
                    effect.gameObject.SetActive(true);
                    effect.Play();

                    GrenadeDmg(); //데미지 처리
                }
                yield return new WaitForSeconds(1.0f);
                isExplosion = false;
                transform.gameObject.SetActive(false);
                //이펙트는 따로 스크립트를 만들어 이펙트에 적용 시킨다(비활성화)
                
            }

        }
                
    }

}
