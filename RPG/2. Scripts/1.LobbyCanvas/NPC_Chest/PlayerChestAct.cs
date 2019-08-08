using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 창고 오브젝트에 적용
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class PlayerChestAct : MonoBehaviour
        {
            [SerializeField, Header("활성화 시킬 기능 창")]
            CanvasGroup cg;

            [SerializeField]
            Characters.PlayerCtrl player;

            [SerializeField]
            InventoryBtn inventoryBtn;

            [SerializeField]
            GameObject chestBtnObj; //창고 인벤 버튼

            Animator ani;

            readonly int hashOpen = Animator.StringToHash("Open");
            readonly int hashClose = Animator.StringToHash("Close");

            [SerializeField]
            GameObject useChestBtn;

            AudioSource _audio;
            [SerializeField, Header("상자 열림 효과음")]
            AudioClip[] _sfx;

            private void Start()
            {
                ani = GetComponent<Animator>();

                _audio = GetComponent<AudioSource>();

                useChestBtn.SetActive(false);
            }


            private void OnTriggerEnter(Collider other)
            {
                if (other.transform.tag.Equals("Player"))
                {

                    useChestBtn.SetActive(true);

                }
            }

            private void OnTriggerExit(Collider other)
            {
                if(other.transform.tag.Equals("Player"))
                {
                    useChestBtn.SetActive(false);
                }
            }

            /// <summary>
            /// 보관함 사용 버튼
            /// </summary>
            public void UseChestBtn()
            {
                Manager.GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                WindowAct();
                player.Stop();
                player.IsInven = true;
                player.InventoryInit();

                inventoryBtn.InvenActDis(inventoryBtn.WeaponeCg, false);
                inventoryBtn.InvenActDis(inventoryBtn.WeaponeCg2, false);

                ani.SetTrigger(hashOpen);

                chestBtnObj.SetActive(true);
            }


            /// <summary>
            /// 해당 기능 창을 연다
            /// </summary>
            private void WindowAct()
            {
                cg.alpha = 1.0f;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }

            /// <summary>
            /// 창고를 닫는 애니메이션
            /// </summary>
            public void AniChestClose()
            {
                ani.SetTrigger(hashClose);
            }

        }

    }
}
