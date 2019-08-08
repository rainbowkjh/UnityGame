using Black.Characters;
using Black.Manager;
using Black.PlayerUI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Black
{
    namespace Inventory
    {
        public class PlayerUpgradeManager : MonoBehaviour
        {
            static public int UPGRADEVALUE = 50;
            static public float WEIGHTUPGRADEVALUE = 3;
            static public int USEMATERAIL = 100;
            static public int USEMONEY = 5000;


            [SerializeField, Header("캐릭터 업그래이드 창")]
            CanvasGroup cg;

            [SerializeField, Header("업그래이드 포인트 출력")]
            Text pointText;

            [SerializeField]
            PlayerCtrl player;

            [SerializeField]
            InfoTextUI playerStatUI;

            AudioSource _audio;
            [SerializeField,Header("0업그래이드 1 취소")]
            AudioClip[] _sfx;

            void Start()
            {
                _audio = GetComponent<AudioSource>();

                cg.alpha = 0.0f;
                cg.interactable = false;
                cg.blocksRaycasts = false;

                UpgradeTipText();
            }

            void UpgradeTipText()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("모든 수치는 ");
                sb.Append(PlayerUpgradeManager.UPGRADEVALUE);
                sb.Append(" 만큼 증가\n");

                sb.Append("(");
                sb.Append("무게는 ");
                sb.Append(PlayerUpgradeManager.WEIGHTUPGRADEVALUE);
                sb.Append(")");

                sb.Append("필요 자원은(재료/돈) ");
                sb.Append(PlayerUpgradeManager.USEMATERAIL);
                sb.Append(" / ");
                sb.Append(PlayerUpgradeManager.USEMONEY);
                sb.Append("\n");

                sb.Append("HP, MANA, THIRST, SATIEY는 ");
                sb.Append("업그래이드로 최대 수치를 증가 시킨후\n");
                sb.Append("수치 증가 아이템을 사용해야 증가가 됩니다\n");

                pointText.text = sb.ToString();
            }

            /// <summary>
            /// 업그래이드 가능한지 확인
            /// </summary>
            /// <returns></returns>
            private bool UpgradeCheck()
            {
                bool isUpgrade = false; 

                if(player.NMoney >= USEMONEY && player.NMaterial >= USEMATERAIL)
                {
                    isUpgrade = true;
                }

                return isUpgrade;
            }

            /// <summary>
            /// HP 업그래이드
            /// </summary>
            public void MaxHpBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                if(UpgradeCheck())
                {
                    player.NMaxUpgradeHp += PlayerUpgradeManager.UPGRADEVALUE;                    
                    playerStatUI.PlayerInfo(); //캐릭터 정보창 출력
                }
                
            }

            /// <summary>
            /// Mana 업그래이드
            /// </summary>
            public void MaxManaBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                if (UpgradeCheck())
                {
                    player.FMaxUpgradeMana += PlayerUpgradeManager.UPGRADEVALUE;
                    playerStatUI.PlayerInfo(); //캐릭터 정보창 출력
                }
                  
            }

            /// <summary>
            /// Thirst 업그래이드
            /// </summary>
            public void MaxThirstBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                if (UpgradeCheck())
                {
                    player.FMaxUpgradeThirst += PlayerUpgradeManager.UPGRADEVALUE;
                    playerStatUI.PlayerInfo(); //캐릭터 정보창 출력
                }
                   
            }

            /// <summary>
            /// Satiety 업그래이드
            /// </summary>
            public void MaxSatietyBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                if (UpgradeCheck())
                {

                    player.FMaxUpgradeSatiety += PlayerUpgradeManager.UPGRADEVALUE;
                    playerStatUI.PlayerInfo(); //캐릭터 정보창 출력
                }
            }

            /// <summary>
            /// 무게 증가
            /// </summary>
            public void MaxWeightBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                if (UpgradeCheck())
                {
                    player.FMaxWeight += PlayerUpgradeManager.WEIGHTUPGRADEVALUE;
                    playerStatUI.PlayerInfo(); //캐릭터 정보창 출력

                    GameObject.FindGameObjectWithTag("INVENTORY").GetComponent<InventoryData>().WeightTextPrint(); //무게 값
                }
                   
            }

            /// <summary>
            /// 무게 증가
            /// </summary>
            public void MaxAmmoBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                if (UpgradeCheck())
                {
                    player.NMaxAmmo += PlayerUpgradeManager.UPGRADEVALUE;
                    playerStatUI.PlayerInfo(); //캐릭터 정보창 출력

                }
                   
            }

            /// <summary>
            /// 창 닫기
            /// </summary>
            public void CloseBtn()
            {
                GameManager.INSTANCE.SFXPlay(_audio, _sfx[1]);

                player.IsInven = false;
                player.InventoryInit();

                cg.alpha = 0.0f;
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
        }

    }
}
