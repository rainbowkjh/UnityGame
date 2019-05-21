using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 필드의 아이템을 회전 시킨다
/// </summary>
namespace Black
{
    namespace _Item
    {
        public class ItemRot : MonoBehaviour
        {            
            void Update()
            {
                transform.Rotate(Vector3.up, 50 * Time.deltaTime);
            }
        }

    }
}
