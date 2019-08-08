using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 아이템을 습득하면
/// 머리 위에 이펙트 및 효과음 발생
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class ItemLootPar : MonoBehaviour
        {

            [SerializeField, Header("0 : 자원, 1 : Bag, 2 : PArts, 3 : Grenade")]
            ParticleSystem[] itemLootPar;

            [SerializeField, Header("0 : 자원, 1 : Bag, 2 : PArts, 3 : Grenade")]
            AudioClip[] _sfx;

            AudioSource _audio;

            void Start()
            {
                _audio = GetComponent<AudioSource>();
            }

            /// <summary>
            /// 아이템 습득 시 효과
            /// </summary>
            /// <param name="id"></param>
            public void ItemLoot(int id)
            {
                if (itemLootPar[id].isPlaying == false)
                    itemLootPar[id].Play();

                if (_audio.isPlaying == false)
                    Manager.GameManager.INSTANCE.SFXPlay(_audio, _sfx[id]);
            }

        }

    }
}
