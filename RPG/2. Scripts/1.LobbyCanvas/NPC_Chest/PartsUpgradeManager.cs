using Black.Characters;
using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// partsUpgrade Obj에 적용
/// 파츠 업그래이드 관리 스크립트
/// 버튼 입력도 같이 관리
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class PartsUpgradeManager : MonoBehaviour
        {

            static public float EXPLOSIONRANGEUPGRADE = 0.1f;
            static public float STUNPERCENTUPGRADE = 0.1f;
            static public int DAMAGEUPGRADE = 50;
            static public int USEMATERAIL = 50;
            static public int USEMONEY = 3000;

            [SerializeField,Header("파츠 업그래이드 창")]
            CanvasGroup cg;

            [SerializeField]
            PlayerCtrl player;

            [SerializeField, Header("업그래이드 슬롯")]
            GameObject slot;
                     

            [SerializeField]
            InventoryBtn invenBtn;

            AudioSource _audio;
            [SerializeField,Header("0업그래이드, 1 취소")]
            AudioClip[] _sfx;

            private void Start()
            {               
                cg.alpha = 0.0f;
                cg.interactable = false;
                cg.blocksRaycasts = false;

                _audio = GetComponent<AudioSource>();

                //업그래이드 슬롯에 아이템이 있으면
                if (slot.transform.childCount != 0)
                {
                    PartsItemData data = slot.GetComponentInChildren<PartsItemData>();

                    IconDropEx ex = slot.GetComponent<IconDropEx>();
                    ex.ItemInfoPrint(data);
                }
                //GameObject.Find("GameCanvas/Inventory/InvenBtn").GetComponent<PlayerUI.InfoTextUI>().CharWeaponeInfo();
                //GameObject.Find("GameCanvas/Inventory/InvenBtn").GetComponent<PlayerUI.InfoTextUI>().GunWeaponeInfo();
            }

            
            /// <summary>
            /// 업그래이드 버튼 클릭
            /// 슬롯의 하위 오브젝트 정보를 가져와
            /// 업그래이드를 시켜준다
            /// </summary>
            public void UpgradeBtn()
            {
                PartsItemData data = slot.GetComponentInChildren<PartsItemData>();

                //슬롯에 아이템이 있으면
                if (data !=null)
                {
                    //업르레이드 자원 => 레벨 * 50, 돈 레벨 * 3000
                    if (player.NPartsMaterial >= (data._PartsItem._NLevel * PartsUpgradeManager.USEMATERAIL) &&
                        (player.NMoney >= data._PartsItem._NLevel * PartsUpgradeManager.USEMONEY))
                    {
                        //레벨 최대치보다 낮을때
                        if (data._PartsItem._NLevel < data._PartsItem._NMaxLevel)
                        {
                            GameManager.INSTANCE.SFXPlay(_audio, _sfx[0]);

                            data._PartsItem._NLevel++; //레벨 증가

                            if (data._PartsItem._IsExplosion) //폭발 속성인 경우 범위 0.1 증가
                            {
                                data._PartsItem._FExplosionArea += PartsUpgradeManager.EXPLOSIONRANGEUPGRADE;
                            }

                            if (data._PartsItem._IsStun) //기절 속석의 경우 0.1 확률 증가
                            {
                                data._PartsItem._FStunPer += PartsUpgradeManager.STUNPERCENTUPGRADE;
                            }

                            data._PartsItem._DmgUp += PartsUpgradeManager.DAMAGEUPGRADE;

                            player.NPartsMaterial -= (data._PartsItem._NLevel * PartsUpgradeManager.USEMATERAIL);
                            player.NMoney -= (data._PartsItem._NLevel * PartsUpgradeManager.USEMONEY);

                            data.LevelText();

                            IconDropEx ex = slot.GetComponent<IconDropEx>();
                            ex.ItemInfoPrint(data);
                        }
                    }
                }
                
                               
                
            }

            /// <summary>
            /// 닫기 버튼 입력
            /// </summary>
            public void CancelBtn()
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
