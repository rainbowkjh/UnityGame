using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayUI
{
    public class PlayerUIEx : CharHP_UI
    {
        [SerializeField, Header("Mana Bar")]
        Image m_ManaBar;
        [SerializeField, Header("Mana Text")]
        Text m_ManaTxt;

        [SerializeField, Header("Mana Bar")]
        Image m_ExpBar;
        [SerializeField, Header("Mana Text")]
        Text m_ExpTxt;


        public void ManaBarUI(CharactersData data)
        {
            m_ManaBar.fillAmount = data.FMana / data.FMaxMana;
            m_ManaTxt.text = data.FMana.ToString("N") + " / " + data.FMaxMana.ToString("N");
        }


        public void ExpBarUI(CharactersData data)
        {
            m_ExpBar.fillAmount = data.FExp / data.FNextExp;
            m_ExpTxt.text = data.FExp.ToString("N") + " / " + data.FNextExp.ToString("N");
        }

    }

}
