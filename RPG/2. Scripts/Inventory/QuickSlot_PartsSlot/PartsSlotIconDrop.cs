using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기의 속성을 변경 시켜주는
/// 슬롯
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class PartsSlotIconDrop : MonoBehaviour
        {            
           protected eItemType slotType;

            protected virtual void Start()
            {
                slotType = eItemType.Pouch1;
            }

        }

    }
}
