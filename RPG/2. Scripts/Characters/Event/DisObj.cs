using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 해당 오브젝트를 비활성화 시킨다
/// </summary>
namespace Black
{
    namespace EffectCtrl
    {
        public class DisObj : MonoBehaviour
        {
            [SerializeField]
            ParticleSystem effect;

            [SerializeField, Header("활성화 되어 있는 시간")]
            float actTime;

            bool isTimeStart = false;

            private void LateUpdate()
            {
                if(!isTimeStart)
                {
                  //Debug.Log("DisObj");
                    isTimeStart = true;
                    StartCoroutine(UseEffectObjDis(actTime));
                }
            }

            /// <summary>
            /// 이펙트 사용 후 오브젝트를 비활겅화 시킨다
            /// </summary>
            public IEnumerator UseEffectObjDis(float act)
            {
                //Debug.Log("DisObj 코루틴");
                
                yield return new WaitForSeconds(act);
                if (effect != null)
                    effect.Play();

                yield return new WaitForSeconds(3.0f);
                this.gameObject.SetActive(false);
            }
        }

    }
}
