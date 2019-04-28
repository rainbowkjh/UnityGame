using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 데미지 UI
/// </summary>
public class DmgPrint : MonoBehaviour
{
    [SerializeField, Header("데미지를 출력 시킬 Text")]
    Text m_tDmg;

    float m_fDmg;

    #region Set,Get
    public float FDmg
    {
        get
        {
            return m_fDmg;
        }

        set
        {
            m_fDmg = value;
        }
    }
    #endregion

    private void OnEnable()
    {
        StartCoroutine(DmgScale());
    }

    IEnumerator DmgScale()
    {
        m_tDmg.text = FDmg.ToString("N2");
        m_tDmg.fontSize = 25;

        yield return new WaitForSeconds(1.5f);
        m_tDmg.fontSize = 20;

        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false); //다시 사용 할수 있도록 비활성화 시킨다
    }

}
