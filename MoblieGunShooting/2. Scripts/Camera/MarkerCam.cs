using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace CameraUtil
    {
        public class MarkerCam : MonoBehaviour
        {
            [SerializeField, Header("Player Pos")]
            Transform targetTr;


            private void Update()
            {
                transform.position = new Vector3
                    (targetTr.position.x, -5, targetTr.position.z);

                //transform.rotation = Quaternion.AngleAxis(90, Vector3.right);

                //transform.localRotation =
                //    Quaternion.AngleAxis(targetTr.rotation.y, Vector3.up);

                //transform.rotation = Quaternion.Euler(90, targetTr.rotation.y, 0);
               // transform.localRotation = Quaternion.Euler(90, targetTr.localRotation.y, 0);

            }


        }

    }
}
