using Black.Characters;
using Black.Weapone;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인벤토리 활성화 시
/// 캐릭터 정보 출력
/// 무기 정보 출력
/// </summary>
namespace Black
{
    namespace PlayerUI
    {
        public class InfoTextUI : MonoBehaviour
        {
            #region PlayerUI 정보 텍스트
            [SerializeField, Header("Player Info Text")]
            Text CharacterInfoText;
            [SerializeField]
            PlayerCtrl player;
            #endregion

            #region Weapone
            [SerializeField, Header("Player Weapone Info Text")]
            Text charWeaponeInfoText;
        
            [SerializeField, Header("Gun Info Text")]
            Text GunInfoText;
        
            #endregion

            #region PlayerUI
            /// <summary>
            /// 인벤토리를 열때
            /// 플레이어의 정보
            /// </summary>
            public void PlayerInfo()
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<color=#00ff00>");
                sb.Append("HP ");
                sb.Append("</color>");
                sb.Append(player.NHp);                
                sb.Append(" / ");
                sb.Append(player.NMaxHp);
                sb.Append("(");
                sb.Append(player.NMaxUpgradeHp);
                sb.Append(")");
                sb.Append("\n");

                sb.Append("<color=#0000ff>");
                sb.Append("Mana ");
                sb.Append("</color>");
                sb.Append(player.FMana.ToString("N0"));
                sb.Append(" / ");
                sb.Append(player.FMaxMana.ToString("N0"));
                sb.Append("(");
                sb.Append(player.FMaxUpgradeMana.ToString("N0"));
                sb.Append(")");
                sb.Append("\n");

                sb.Append("<color=#ffff00>");
                sb.Append("Thirst ");
                sb.Append("</color>");
                sb.Append(player.FThirst.ToString("N0"));
                sb.Append(" / ");
                sb.Append(player.FMaxThirst.ToString("N0"));
                sb.Append("(");
                sb.Append(player.FMaxUpgradeThirst.ToString("N0"));
                sb.Append(")");
                sb.Append("\n");

                sb.Append("<color=#ff0000>");
                sb.Append("Satiety ");
                sb.Append("</color>");
                sb.Append(player.FSatiety.ToString("N0"));
                sb.Append(" / ");
                sb.Append(player.FMaxSatiety.ToString("N0"));
                sb.Append("(");
                sb.Append(player.FMaxUpgradeSatiety.ToString("N0"));
                sb.Append(")");
                sb.Append("\n");

                sb.Append("Ammo ");
                sb.Append(player.NAmmo);
                sb.Append(" / ");
                sb.Append(player.NMaxAmmo);
                sb.Append("\n");

                sb.Append("Bag Weight ");
                sb.Append(player.FWeight.ToString("N2"));
                sb.Append(" / ");
                sb.Append(player.FMaxWeight.ToString("N2"));
                sb.Append("\n");

                sb.Append("Money ");
                sb.Append(player.NMoney);
                sb.Append("\n");

                sb.Append("UPgrade Point ");
                sb.Append(player.NMaterial);
                sb.Append("\n");

                sb.Append("Parts UPgrade Point ");
                sb.Append(player.NPartsMaterial);
                sb.Append("\n");


                //sb.Append("Speed ");
                //sb.Append(player.FSpeed);
                //sb.Append("\n");

                CharacterInfoText.text = sb.ToString();
            }
            #endregion

            #region Weapone
            /// <summary>
            /// 플레이어의 무기 1 정보
            /// </summary>
            /// <param name="attackInfo"></param>
            public  void CharWeaponeInfo(CharWeaponeCtrl attackInfo)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("공격력  ");
                sb.Append(attackInfo.NMinDmg);
                sb.Append(" / ");
                sb.Append(attackInfo.NMaxDmg);
                sb.Append("\n");

                //속성 관련 추가
                //속성 관련 추가
                sb.Append("폭발 속성 ");
                sb.Append(attackInfo.IsExplosion);
                sb.Append("  ");
                sb.Append("폭발 범위  ");
                sb.Append(attackInfo.FExplosionArea.ToString("N2"));
                sb.Append("\n");

                sb.Append("기절 속성 ");
                sb.Append(attackInfo.IsStun);
                sb.Append("  ");
                sb.Append("기절 확률  ");
                sb.Append(attackInfo.FStunPer.ToString("N2"));
                sb.Append("\n");

                charWeaponeInfoText.text = sb.ToString();
            }

            /// <summary>
            /// 플레이어 총 무기 정보
            /// </summary>
            /// <param name="weaponeData"></param>
            public void GunWeaponeInfo(WeaponeData weaponeData)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(weaponeData.WeaponeName);
                sb.Append("\n");

                sb.Append("공격력  ");
                sb.Append(weaponeData.NMinDmg);
                sb.Append(" / ");                
                sb.Append(weaponeData.NMaxDmg);
                sb.Append("\n");

                sb.Append("탄  ");
                sb.Append(weaponeData.NBullet);
                sb.Append(" / ");                
                sb.Append(weaponeData.NMaxMag);
                sb.Append("\n");

                sb.Append("탄 속도  ");
                sb.Append(weaponeData.FBulletSpeed);
                sb.Append("\n");

                //속성 관련 추가
                sb.Append("폭발 속성  ");
                sb.Append(weaponeData.IsExplosion);
                sb.Append("  ");
                sb.Append("(범위  ");
                sb.Append(weaponeData.FExplosionArea.ToString("N2"));
                sb.Append(")");

                sb.Append("\n");
                sb.Append("기절 속성  ");
                sb.Append(weaponeData.IsStun);
                sb.Append("  ");
                sb.Append("(확률  ");
                sb.Append(weaponeData.FStunPer.ToString("N2"));
                sb.Append(")");
                sb.Append("\n");


                GunInfoText.text = sb.ToString();
            }
            #endregion
        }

    }
}

