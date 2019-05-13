using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이동 경로의 마지막 지점
/// </summary>
namespace Black
{
    namespace MovePosObj
    {

        public class EndPos : MonoBehaviour
        {
            [SerializeField, Header("Player의 이동 경로인지 구분")]
            bool isPlayerPos = false;

            /// <summary>
            /// 콜라이더에 도착했는지 확인
            /// </summary>
            bool isEnter = false;

            private void OnTriggerEnter(Collider other)
            {
                if (other.transform.tag.Equals("Player"))
                {
                    StartCoroutine(CharStop(other));

                }
            }


            /// <summary>
            /// 콜라이더에 도착하면 정지 시킴
            /// (바로 정지 시키면 원하는 지점보다 전에 정지
            /// 하기 때문에 약간의 시간을 주고 정지 시킴)
            /// </summary>
            /// <param name="coll"></param>
            /// <returns></returns>
            IEnumerator CharStop(Collider coll)
            {
                if (!isEnter)
                {

                    isEnter = true;

                    yield return new WaitForSeconds(0.2f);

                    //해당 캐릭터를 정지 시킴
                    coll.GetComponent<CharactersData>().IsStop = true;

                }
            }


        }

    }
}
