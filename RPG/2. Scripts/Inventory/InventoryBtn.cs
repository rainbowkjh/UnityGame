using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인벤토리 항목 활성화 및 비활성화(버튼)
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class InventoryBtn : MonoBehaviour
        {
            [SerializeField]
            CanvasGroup playerInfoCg; //인벤 창 활성화
            bool isInfo = true; //활성화 상태

            [SerializeField]
            CanvasGroup bagCg;
            bool isBag = true;

            [SerializeField]
            CanvasGroup pouchCg1;
            bool isPouch1 = false;

            [SerializeField]
            CanvasGroup pouchCg2;
            bool isPouch2 = false;

            [SerializeField]
            CanvasGroup weaponeCg;
            bool isWeapone = true;

            [SerializeField]
            CanvasGroup weaponeCg2;
            bool isWeapone2= false;

            AudioSource _audio;
            [SerializeField,Header("버튼 클릭음")]
            AudioClip[] _sfx;

            public CanvasGroup PlayerInfoCg { get => playerInfoCg; set => playerInfoCg = value; }
            public CanvasGroup BagCg { get => bagCg; set => bagCg = value; }
            public CanvasGroup PouchCg1 { get => pouchCg1; set => pouchCg1 = value; }
            public CanvasGroup PouchCg2 { get => pouchCg2; set => pouchCg2 = value; }
            public CanvasGroup WeaponeCg { get => weaponeCg; set => weaponeCg = value; }
            public CanvasGroup WeaponeCg2 { get => weaponeCg2; set => weaponeCg2 = value; }

            private void Start()
            {
                InvenActDis(PlayerInfoCg, isInfo);
                InvenActDis(BagCg, isBag);
                InvenActDis(PouchCg1, isPouch1);
                InvenActDis(PouchCg2, isPouch2);
                InvenActDis(WeaponeCg, isWeapone);
                InvenActDis(WeaponeCg2, isWeapone2);

                _audio = GetComponent<AudioSource>();
            }


            public void InvenActDis(CanvasGroup canvas, bool act)
            {
                canvas.alpha = act ? 1.0f : 0.0f;
                canvas.interactable = act;
                canvas.blocksRaycasts = act;
            }

            /// <summary>
            /// 캐릭터 정보
            /// </summary>
            public void InfoInvenAct()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]); //효과음 재생

                isInfo = !isInfo;

                //playerInfoCg.alpha = isInfo ? 1.0f : 0.0f;
                //playerInfoCg.interactable = isInfo;
                //playerInfoCg.blocksRaycasts = isInfo;

                InvenActDis(PlayerInfoCg, isInfo);
            }

            /// <summary>
            /// Bag 인벤
            /// </summary>
            public void BagInvenAct()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                isBag = !isBag;
                //가방 인벤이 열리면
                //파우치 인벤들은 닫는다
                isPouch1 = false;
                isPouch2 = false;

                //bagCg.alpha = isBag ? 1.0f : 0.0f;
                //bagCg.interactable = isBag;
                //bagCg.blocksRaycasts = isBag;

                InvenActDis(BagCg, isBag);
                InvenActDis(PouchCg1, isPouch1);
                InvenActDis(PouchCg2, isPouch2);
            }

            /// <summary>
            /// Bag 인벤
            /// </summary>
            public void PouchAct1()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                isPouch1 = !isPouch1;

                isBag = false;
                isPouch2 = false;

                //pouchCg1.alpha = isPouch1 ? 1.0f : 0.0f;
                //pouchCg1.interactable = isPouch1;
                //pouchCg1.blocksRaycasts = isPouch1;

                InvenActDis(BagCg, isBag);
                InvenActDis(PouchCg1, isPouch1);
                InvenActDis(PouchCg2, isPouch2);
            }

            /// <summary>
            /// Bag 인벤
            /// </summary>
            public void PouchAct2()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                isPouch2 = !isPouch2;

                isBag = false;
                isPouch1 = false;

                //pouchCg2.alpha = isPouch2 ? 1.0f : 0.0f;
                //pouchCg2.interactable = isPouch2;
                //pouchCg2.blocksRaycasts = isPouch2;

                InvenActDis(BagCg, isBag);
                InvenActDis(PouchCg1, isPouch1);
                InvenActDis(PouchCg2, isPouch2);
            }

            /// <summary>
            /// Bag 인벤
            /// </summary>
            public void WeaponeAct()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                isWeapone = !isWeapone;
                //첫번쨰 무기 정보가 열리면
                //두번째 정보는 닫는다
                isWeapone2 = false;

                //weaponeCg.alpha = isWeapone ? 1.0f : 0.0f;
                //weaponeCg.interactable = isWeapone;
                //weaponeCg.blocksRaycasts = isWeapone;

                InvenActDis(WeaponeCg, isWeapone);
                InvenActDis(WeaponeCg2, isWeapone2);
            }


            /// <summary>
            /// Bag 인벤
            /// </summary>
            public void WeaponeAct2()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                isWeapone2 = !isWeapone2;
                isWeapone = false;

                //weaponeCg2.alpha = isWeapone2 ? 1.0f : 0.0f;
                //weaponeCg2.interactable = isWeapone2;
                //weaponeCg2.blocksRaycasts = isWeapone2;

                InvenActDis(WeaponeCg, isWeapone);
                InvenActDis(WeaponeCg2, isWeapone2);
            }
        }

    }
}
