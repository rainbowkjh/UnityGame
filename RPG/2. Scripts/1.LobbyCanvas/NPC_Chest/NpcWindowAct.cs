using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  NPC 또는 상자에 갔을때
///  해당 기능 창을 연다
///  기능 제어 및 닫기는 
///  해당 스크립트에서 관리
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class NpcWindowAct : MonoBehaviour
        {
            [SerializeField, Header("활성화 시킬 기능 창")]
            CanvasGroup cg;

            [SerializeField]
            PlayerCtrl player;

            [SerializeField]
            InventoryBtn inventoryBtn;
            
            Animator ani;
            //플레이어가 접근하여 메뉴가 열릴때 NPC 행동
            readonly int hashTalk = Animator.StringToHash("Talk");

            /// <summary>
            /// 상점 이용 버튼
            /// </summary>
            [SerializeField]
            GameObject storeBtnObj;

            private void Start()
            {
                ani = GetComponent<Animator>();

                storeBtnObj.SetActive(false);
            }


            private void OnTriggerEnter(Collider other)
            {

                if (other.transform.tag.Equals("Player"))
                {

                    storeBtnObj.SetActive(true);

                    
                }

            }

            private void OnTriggerExit(Collider other)
            {
                if(other.transform.tag.Equals("Player"))
                {
                    storeBtnObj.SetActive(false);
                }
            }

            /// <summary>
            /// 상점 이용 버튼을 누르면
            /// UI가 활성화 된다
            /// </summary>
            public void UseStore()
            {
                WindowAct();
                player.Stop();
                player.IsInven = true;
                player.InventoryInit();

                inventoryBtn.InvenActDis(inventoryBtn.WeaponeCg, false);
                inventoryBtn.InvenActDis(inventoryBtn.WeaponeCg2, false);

                ani.SetTrigger(hashTalk);

                Manager.GameManager.INSTANCE.isMenu = false;
                storeBtnObj.SetActive(false);
            }


            /// <summary>
            /// 해당 기은 창을 연다
            /// </summary>
            private void WindowAct()
            {
                cg.alpha = 1.0f;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
            
        }

    }
}
