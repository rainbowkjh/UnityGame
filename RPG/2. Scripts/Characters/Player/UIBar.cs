using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 캐릭터의 상태를 보여주는 Bar
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class UIBar : MonoBehaviour
        {
            [SerializeField,Header("0:HP, 1:Mana, 2:Thirst, 3:Satiety")]
            Image[] fillBar;

            [SerializeField]
            PlayerCtrl player;

            void Start()
            {
                HpBar();
                ManaBar();
                ThirstBar();
                SatietyBar();
            }

            public void HpBar()
            {
                fillBar[0].fillAmount = (float)player.NHp / (float)player.NMaxHp;
            }

            public void ManaBar()
            {
                fillBar[1].fillAmount = player.FMana / player.FMaxMana;
            }

            public void ThirstBar()
            {
                fillBar[2].fillAmount = player.FThirst / player.FMaxThirst;
            }

            public void SatietyBar()
            {
                fillBar[3].fillAmount = player.FSatiety / player.FMaxSatiety;
            }

        }

    }
}
