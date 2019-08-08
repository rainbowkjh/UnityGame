using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 안전 지역에서
/// 창고 인벤
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class ChestManager : MonoBehaviour
        {
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
            GameObject chestBtnObj;

            [SerializeField]
            Characters.PlayerCtrl player;

            [SerializeField]
            PlayerChestAct chest;

            AudioSource _audio;
            [SerializeField]
            AudioClip[] _sfx;

            #region 창고 인벤
            [SerializeField]
            List<GameObject> chestBagInven;

            [SerializeField]
            List<GameObject> chestPartsInven;

            [SerializeField]
            List<GameObject> chestSubInven;
            #endregion

            #region 업그래이드 및 무기 파트 장착 슬롯
            //순서데로 들어가다 보면 중간에 아이템이 없는경우
            //순서가 꼬이기 때문에 개별 관리 함
            [SerializeField]
            List<GameObject> weaponePartsSlot1_1;

            [SerializeField]
            List<GameObject> weaponePartsSlot1_2;

            [SerializeField]
            List<GameObject> weaponePartsSlot1_3;

            [SerializeField]
            List<GameObject> weaponePartsSlot2_1;

            [SerializeField]
            List<GameObject> weaponePartsSlot2_2;

            [SerializeField]
            List<GameObject> weaponePartsSlot2_3;

            [SerializeField]
            List<GameObject> quickSlot_1;
            [SerializeField]
            List<GameObject> quickSlot_2;
            [SerializeField]
            List<GameObject> quickSlot_3;

            [SerializeField]
            List<GameObject> upgradeSlot;

            public List<GameObject> ChestBagInven { get => chestBagInven; set => chestBagInven = value; }
            public List<GameObject> ChestPartsInven { get => chestPartsInven; set => chestPartsInven = value; }
            public List<GameObject> ChestSubInven { get => chestSubInven; set => chestSubInven = value; }           
            public List<GameObject> UpgradeSlot { get => upgradeSlot; set => upgradeSlot = value; }
            public List<GameObject> WeaponePartsSlot1_1 { get => weaponePartsSlot1_1; set => weaponePartsSlot1_1 = value; }
            public List<GameObject> WeaponePartsSlot1_2 { get => weaponePartsSlot1_2; set => weaponePartsSlot1_2 = value; }
            public List<GameObject> WeaponePartsSlot1_3 { get => weaponePartsSlot1_3; set => weaponePartsSlot1_3 = value; }
            public List<GameObject> WeaponePartsSlot2_1 { get => weaponePartsSlot2_1; set => weaponePartsSlot2_1 = value; }
            public List<GameObject> WeaponePartsSlot2_2 { get => weaponePartsSlot2_2; set => weaponePartsSlot2_2 = value; }
            public List<GameObject> WeaponePartsSlot2_3 { get => weaponePartsSlot2_3; set => weaponePartsSlot2_3 = value; }
            public List<GameObject> QuickSlot_1 { get => quickSlot_1; set => quickSlot_1 = value; }
            public List<GameObject> QuickSlot_2 { get => quickSlot_2; set => quickSlot_2 = value; }
            public List<GameObject> QuickSlot_3 { get => quickSlot_3; set => quickSlot_3 = value; }

            #endregion



             private void Start()
            {
                _audio = GetComponent<AudioSource>();

                InvenActDis(bagCg, false);
                InvenActDis(pouchCg1, false);
                InvenActDis(pouchCg2, false);
                chestBtnObj.SetActive(false);

                ItemLoad();
            }

            /// <summary>
            /// 아이템 로드
            /// </summary>
            void ItemLoad()
            {
                #region 창고 및 퀵 슬롯 등 로드
                //창고
                GameManager.INSTANCE.LoadBagItem(chestBagInven,
                   GameManager.INSTANCE.loadPlayerData.chestBagInven);

                GameManager.INSTANCE.LoadPartsItem(chestPartsInven,
                  GameManager.INSTANCE.loadPlayerData.chestPartsInven);

                GameManager.INSTANCE.LoadSubtsItem(chestSubInven,
                  GameManager.INSTANCE.loadPlayerData.chestSubInven);

                //무기
                GameManager.INSTANCE.LoadPartsItem(weaponePartsSlot1_1,
                  GameManager.INSTANCE.loadPlayerData.weaponeParts1_1);

                GameManager.INSTANCE.LoadPartsItem(weaponePartsSlot1_2,
                  GameManager.INSTANCE.loadPlayerData.weaponeParts1_2);

                GameManager.INSTANCE.LoadPartsItem(weaponePartsSlot1_3,
                  GameManager.INSTANCE.loadPlayerData.weaponeParts1_3);

                //무기 2
                GameManager.INSTANCE.LoadPartsItem(weaponePartsSlot2_1,
                  GameManager.INSTANCE.loadPlayerData.weaponeParts2_1);

                GameManager.INSTANCE.LoadPartsItem(weaponePartsSlot2_2,
                  GameManager.INSTANCE.loadPlayerData.weaponeParts2_2);

                GameManager.INSTANCE.LoadPartsItem(weaponePartsSlot2_3,
                  GameManager.INSTANCE.loadPlayerData.weaponeParts2_3);

                //업그래이드
                GameManager.INSTANCE.LoadPartsItem(upgradeSlot,
               GameManager.INSTANCE.loadPlayerData.upgradeSlot);

                //퀵 슬롯
                GameManager.INSTANCE.LoadQuickItem(quickSlot_1,
               GameManager.INSTANCE.loadPlayerData.quickSlot_1);

                GameManager.INSTANCE.LoadQuickItem(quickSlot_2,
               GameManager.INSTANCE.loadPlayerData.quickSlot_2);

                GameManager.INSTANCE.LoadQuickItem(quickSlot_3,
               GameManager.INSTANCE.loadPlayerData.quickSlot_3);

                #endregion
            }

         

            #region 버튼 관리
            /// <summary>
            /// 창 비활성화
            /// </summary>
            /// <param name="canvas"></param>
            /// <param name="act"></param>
            public void InvenActDis(CanvasGroup canvas, bool act)
            {
                canvas.alpha = act ? 1.0f : 0.0f;
                canvas.interactable = act;
                canvas.blocksRaycasts = act;
            }

            /// <summary>
            /// 가방 창고 버튼
            /// </summary>
            public void BagInvenActBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                isBag = !isBag;

                isPouch1 = false;
                isPouch2 = false;

                InvenActDis(pouchCg1, isPouch1);
                InvenActDis(pouchCg2, isPouch2);

                bagCg.alpha = isBag ? 1.0f : 0.0f;
                bagCg.interactable = isBag;
                bagCg.blocksRaycasts = isBag;
            }

            /// <summary>
            /// 파츠 창고 버튼
            /// </summary>
            public void PartsInvenActBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                isPouch1 = !isPouch1;

                isBag = false;
                isPouch2 = false;

                InvenActDis(bagCg, isBag);
                InvenActDis(pouchCg2, isPouch2);

                pouchCg1.alpha = isPouch1 ? 1.0f : 0.0f;
                pouchCg1.interactable = isPouch1;
                pouchCg1.blocksRaycasts = isPouch1;
            }

            /// <summary>
            /// 보조 무기 창고 버튼
            /// </summary>
            public void PouchInvenActBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                isPouch2 = !isPouch2;

                isBag = false;
                isPouch1 = false;

                InvenActDis(bagCg, isBag);
                InvenActDis(pouchCg1, isPouch1);

                pouchCg2.alpha = isPouch2 ? 1.0f : 0.0f;
                pouchCg2.interactable = isPouch2;
                pouchCg2.blocksRaycasts = isPouch2;

            }

            /// <summary>
            /// 창 닫기 버튼
            /// </summary>
            public void CancelBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                isBag = false;
                isPouch1 = false;
                isPouch2 = false;

                InvenActDis(bagCg, false);
                InvenActDis(pouchCg1, false);
                InvenActDis(pouchCg2, false);
                chestBtnObj.SetActive(false);

                player.IsInven = false;
                player.InventoryInit();


                chest.AniChestClose(); //창고 닫기 애니메이션
            }

            #endregion

        }

    }
}
