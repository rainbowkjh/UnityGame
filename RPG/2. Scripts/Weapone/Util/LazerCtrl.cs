using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Characters
    {
        public class LazerCtrl : MonoBehaviour
        {
            [SerializeField]
            LineRenderer line;
            [SerializeField]
            Transform lazerPos;

            [SerializeField]
            Light spotLight; //총의 후레쉬
            bool isLight = false;

            private void LateUpdate()
            {
                RaycastHit hit;

                if(Physics.Raycast(lazerPos.position, lazerPos.forward * 5, out hit))
                {
                    line.useWorldSpace = true;
                    line.SetWidth(0.01f, 0.01f);
                    line.SetPosition(0, lazerPos.position);
                    line.SetPosition(1, hit.point);

                }
                else
                {
                    line.useWorldSpace = false;
                    line.SetWidth(0.01f, 0.01f);
                    line.SetPosition(0, lazerPos.localPosition);
                    line.SetPosition(1, lazerPos.localPosition + new Vector3(0,0,5));
                }

                if(Input.GetKeyDown(KeyCode.F))
                {
                    isLight = !isLight;

                    if(isLight)
                    {
                        //Debug.Log("후레쉬 사용");
                        spotLight.enabled = true;
                    }
                    else
                    {
                        spotLight.enabled = false;
                    }
                }

            }
        }

    }
}
