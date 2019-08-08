using Black.Characters;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//아이템 삭제 슬롯
namespace Black
{
    namespace Inventory
    {
        public class DlelteSlotDrop : MonoBehaviour, IDropHandler
        {
            AudioSource _audio;

            [SerializeField]
            AudioClip[] _sfx;

            [SerializeField]
            PlayerCtrl player;

            private void Start()
            {
                _audio = GetComponent<AudioSource>();
            }

            public void OnDrop(PointerEventData eventData)
            {
                GameObject obj = IconDrag.draggingItem;

                if (obj.GetComponent<BagItemData>())
                {
                    BagItemDesory();
                }

                if (obj.GetComponent<PartsItemData>())
                {
                    GetParts(10, 100);
                }

                if (obj.GetComponent<SubItemData>())
                {
                    GetParts(10, 50);
                }

                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                Destroy(obj);
            }

            /// <summary>
            /// 소모 아이템 삭제 시 자원
            /// </summary>
            /// <param name="obj"></param>
            void BagItemDesory()
            {
                player.NMaterial += Random.Range(10, 100);
            }

            /// <summary>
            /// 파츠 또는 수류탄 해체 시
            /// 파츠 얻음
            /// </summary>
            void GetParts(int min, int max)
            {
                player.NPartsMaterial += Random.Range(min, max);
            }

        }

    }
}
