using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 아이템 아이콘 정보 창 오브젝트에 적용
/// 아이콘에서 마우스가 벗어나면 정보창이 비활성화 되도록 헀는데
/// 마우스가 정보창으로 가면
/// 없어지지 않아 
/// 강제로 비활성화
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class ItemInfoDis : MonoBehaviour, IPointerEnterHandler
        {
            public void OnPointerEnter(PointerEventData eventData)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
