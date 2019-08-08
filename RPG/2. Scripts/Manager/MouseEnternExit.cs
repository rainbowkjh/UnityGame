using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 특정 버튼에 적용 시킨다
/// 마우스가 버튼에 들어오면
/// 캐릭터 이동 제한
/// 버튼을 클릭해고 캐릭터가 그 방향으로 움직이지 않는다
/// (퀘스트 목록, 상점 이동 버튼 등)
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class MouseEnternExit : MonoBehaviour
        {

            public void OnMouseEnter()
            {
                GameManager.INSTANCE.isMenu = true;
            }

            public void OnMouseExit()
            {
                GameManager.INSTANCE.isMenu = false;
            }


            private void OnDisable()
            {
                GameManager.INSTANCE.isMenu = false;
            }
        }

    }
}
