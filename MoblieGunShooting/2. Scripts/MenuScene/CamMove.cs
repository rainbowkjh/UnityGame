using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Main
    {
        public class CamMove : MonoBehaviour
        {
            [SerializeField, Header("MovePos")]
            Transform[] movePosTr;

            int moveIndex = 0;
            bool isEnter = false;

            private void Start()
            {
                transform.position = movePosTr[0].position;
            }

            private void LateUpdate()
            {
                transform.position = Vector3.Slerp(transform.position,
                        movePosTr[moveIndex].position, 0.5f * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    movePosTr[moveIndex].rotation, 1 * Time.deltaTime);
            }

            
            /// <summary>
            /// 카메라를 주변을 돌게 만든다
            /// </summary>
            /// <param name="other"></param>
            private void OnTriggerEnter(Collider other)
            {
                //태그 값 재활용;;;
                if(other.tag.Equals("Excel"))
                {
                    if(!isEnter)
                    {
                        isEnter = true;
                        moveIndex++;

                        if(moveIndex >= movePosTr.Length)
                        {
                            moveIndex = 0;
                        }
                    }
                }
            }

            private void OnTriggerExit(Collider other)
            {
                if (other.tag.Equals("Excel"))
                {
                    if(isEnter)
                    {
                        isEnter = false;
                    }
                }
            }


        }

    }
}
