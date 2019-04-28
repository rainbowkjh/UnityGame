using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 무기 정보를 화면에 보여준다
/// 무기 오브젝트에 스크립트를 적용 시키고
/// 쿠기 오브젝트에서 호출하기만 하면 된다
/// </summary>
namespace PlayUI
{
    public class WeaponeAmmoUI : MonoBehaviour
    {
        [SerializeField, Header("Weapone Name")]
        Text m_tName;
        [SerializeField, Header("Ammo Bar")]
        Image m_AmmoBar;
        [SerializeField, Header("Ammo Text")]
        Text m_AmmoTxt;

        public void WeaponeBarUI(Weapone.WeaponeData data)
        {
            GetComponentUI();

            m_tName.text = data.WeaponeName;
            m_AmmoBar.fillAmount = (float)data.NCurBullet / (float)data.NMag;
            m_AmmoTxt.text = data.NCurBullet.ToString("N0") + " / " + data.NMag.ToString("N0");
        }

        void GetComponentUI()
        {
            if(m_tName==null)
            {
                m_tName = GameObject.Find("Canvas/CharInfo/WeaponeData/Name/Text").GetComponent<Text>();
            }

            if(m_AmmoBar == null)
            {
                m_AmmoBar = GameObject.Find("Canvas/CharInfo/WeaponeData/WeaponeAmmo/TheBar").GetComponent<Image>();
            }

            if(m_AmmoTxt == null)
                m_AmmoTxt = GameObject.Find("Canvas/CharInfo/WeaponeData/WeaponeAmmo/BarText").GetComponent<Text>(); 
        }


    }

}
