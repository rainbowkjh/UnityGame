using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Black
{
    namespace UI
    {
        public class PlayerUI : MonoBehaviour
        {
            [SerializeField, Header("HP Sprite")]
            Image hpUI;
            [SerializeField, Header("HP Text")]
            Text hpTxt;

            [SerializeField, Header("Weapone")]
            Text weaponeText;

            [SerializeField, Header("Ammo Info")]
            Text ammoTxt;

            [SerializeField, Header("Dmg Info")]
            Text dmgTxt;

            [SerializeField, Header("Enemy Info Obj")]
            GameObject enemyInfoObj;

            [SerializeField, Header("Enemy HP Bar")]
            Image enemyHpBar;

            [SerializeField, Header("Enemy HP Text")]
            Text enemyHpText;

            private void Start()
            {
                enemyInfoObj.SetActive(false);
            }


            public void CurHP(float curHp, float maxHp)
            {
                hpUI.fillAmount = curHp / maxHp;
                hpTxt.text = curHp.ToString("N0") + " / " + maxHp.ToString("N0");

                if(hpUI.fillAmount <= 0.6f && hpUI.fillAmount > 0.3f)
                {
                    hpUI.color = Color.yellow;
                }

                else if((hpUI.fillAmount <= 0.3f))
                {
                    hpUI.color = Color.red;
                }

                else
                {
                    hpUI.color = Color.green;
                }
            }

            /// <summary>
            /// 좌측 상단 캐릭터 정보
            /// </summary>
            /// <param name="name"></param>
            /// <param name="minDmg"></param>
            /// <param name="maxDmg"></param>
            public void CurWeaponeInfo(string name, float minDmg, float maxDmg)
            {
                weaponeText.text = name + "\n"
                            + "MinDmg ~ MaxDmg\n"
                            + minDmg.ToString("N0") + " ~ " + maxDmg.ToString("N0"); ;
            }

            /// <summary>
            /// 에임
            /// 탄 정보 
            /// </summary>
            /// <param name="ammo"></param>
            /// <param name="max"></param>
            public void AmmoInfo(int ammo, int max)
            {
                ammoTxt.text = "Ammo " + ammo + " / " + max;
            }

            /// <summary>
            /// 에임
            /// 데미지 정보
            /// </summary>
            /// <param name="dmg"></param>
            public void DmgInfoprint(float dmg)
            {
                dmgTxt.text = "Hit Dmg " + dmg.ToString("N0");
            }

            /// <summary>
            /// 에임
            /// 데미지 정보 코루틴
            /// </summary>
            /// <param name="dmg"></param>
            /// <returns></returns>
            public IEnumerator DmgInfo(float dmg)
            {
                dmgTxt.text = "Hit Dmg " + dmg.ToString("N0");

                yield return new WaitForSeconds(3.0f);

                dmgTxt.text = "Hit Dmg     ";
            }

            /// <summary>
            /// 적을 공격하면 HP 정보를 보여준다
            /// </summary>
            /// <param name="hp"></param>
            /// <param name="maxHp"></param>
            public void EnemyHpInfo(float hp, float maxHp)
            {                
                enemyHpBar.fillAmount = hp / maxHp;
                enemyHpText.text = hp.ToString("N0") + " / " + maxHp.ToString("N0");
            }

            /// <summary>
            /// 에임이 적을 바라보면
            /// 정보를 보여준다
            /// </summary>
            public void EnemyHpInfoAct()
            {
                enemyInfoObj.SetActive(true);
            }
                 

            /// <summary>
            /// 에임이 적을 벗어나면 정보를 숨긴다
            /// </summary>
            public void EnmryHpInfoDisable()
            {
                enemyInfoObj.SetActive(false);
            }

        }

    }
}
