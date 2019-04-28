using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;
using UnityEngine.UI;

/// <summary>
/// 캐릭터의 HP 등 UI 스크립트
/// </summary>
namespace PlayUI
{
    public class CharHP_UI : MonoBehaviour
    {

        [SerializeField, Header("HP Bar")]
        Image m_HpBar;
        [SerializeField, Header("HP Text")]
        Text m_HpTxt;

        public void HPBarUI(CharactersData data)
        {
            m_HpBar.fillAmount = data.FHP / data.FMaxHP;
            m_HpTxt.text = data.FHP.ToString("N") + " / " + data.FMaxHP.ToString("N");
        }
       
    }

}
