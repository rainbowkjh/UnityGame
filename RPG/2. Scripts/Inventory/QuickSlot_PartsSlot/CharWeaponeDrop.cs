using Black.PlayerUI;
using Black.Weapone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 무기의 파츠 장착 슬롯 중
/// 캐릭터 특성 무기 슬롯
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class CharWeaponeDrop : PartsSlotIconDrop, IDropHandler
        {
            [SerializeField, Header("캐릭터 전용 무기")]
            CharWeaponeCtrl charWeapone;

            [SerializeField, Header("InvenBtn에 있는 UI")]
            InfoTextUI infoUI;

            [SerializeField, Header("해당 슬롯에 파츠 장착 시 탄 외형 변경")]
            bool isPartsSkin = false;

            public bool IsPartsSkin { get => isPartsSkin; set => isPartsSkin = value; }
            public InfoTextUI InfoUI { get => infoUI; set => infoUI = value; }

            protected override void Start()
            {
                base.Start();

                InfoUI.CharWeaponeInfo(charWeapone);
            }

            public void OnDrop(PointerEventData eventData)
            {
                if (transform.childCount == 0)
                {
                    //슬롯과 아이템 타입이 같으면 슬롯에 장착
                    if (slotType == IconDrag.draggingItem.GetComponent<IconDrag>().ItemType)
                    {
                        IconDrag.draggingItem.transform.SetParent(this.transform); //슬롯에 장착
                        IconDrag.draggingItem.GetComponent<IconDrag>().CharWeaponeSlotEquip = true; //캐릭터 무기 파츠 슬롯  장착

                        //데이터 적용
                        PartsItemData parts = IconDrag.draggingItem.GetComponent<PartsItemData>();


                        charWeapone.NMinDmg += parts._PartsItem._DmgUp;
                        charWeapone.NMaxDmg += parts._PartsItem._DmgUp;

                        //장착하는 아이템에 폭발 속성이 있으면
                        if (parts._PartsItem._IsExplosion)
                        {
                            //폭발 속성의 경우 파이어 볼 이펙트
                            if (IsPartsSkin)
                                charWeapone.BulletType = Manager.eProjectilesType.AttackExplosion;

                            charWeapone.IsExplosion = parts._PartsItem._IsExplosion; //값을 적용 시키고...
                            charWeapone.NExplosionSlot++; //폭발 속성이 하나 적용되었다고 기록한다(0이 되면 false로 변경 시키기 위해서)
                            charWeapone.FExplosionArea += parts._PartsItem._FExplosionArea; //폭발 범위 증가
                        }

                        //위와 같은 원리
                        if (parts._PartsItem._IsStun)
                        {
                            //스턴 속성의 경우 파이어 볼 이펙트
                            if (IsPartsSkin)
                                charWeapone.BulletType = Manager.eProjectilesType.CometBlue;

                            charWeapone.IsStun = parts._PartsItem._IsStun;
                            charWeapone.FStunPer += parts._PartsItem._FStunPer;
                        }





                        InfoUI.CharWeaponeInfo(charWeapone);
                    }

                }
            }

            /// <summary>
            /// 파트 해제 시 능력치 수정
            /// </summary>
            /// <param name="min"></param>
            /// <param name="max"></param>
            public void ItemDataSyn(int value, bool isExplosion, bool isStun, float fExplosion, float fStunPer)
            {

                charWeapone.NMinDmg -= value;
                charWeapone.NMaxDmg -= value;

                //폭발 속성이 있는 파트를 제거 시
                if (isExplosion)
                {
                    //첫번째 슬롯의 파츠를 분리하면 원래 이펙트로 변경
                    if (IsPartsSkin)
                        charWeapone.BulletType = charWeapone.BulletTypeOrigin;

                    charWeapone.NExplosionSlot--; //폭발 속성 파츠 하나 제거
                    charWeapone.FExplosionArea -= fExplosion;

                    if (charWeapone.NExplosionSlot <= 0)
                    {
                        charWeapone.IsExplosion = false; //파트가 0개 존재하면 false
                    }

                }

                if (isStun)
                {
                    if (IsPartsSkin)
                        charWeapone.BulletType = charWeapone.BulletTypeOrigin;

                    charWeapone.NStunSlot--;
                    charWeapone.FStunPer -= fStunPer;

                    if (charWeapone.NStunSlot <= 0)
                    {
                        charWeapone.IsStun = false;

                    }
                }


                InfoUI.CharWeaponeInfo(charWeapone);
            }

            /// <summary>
            /// 아이콘이 다시 돌아올때
            /// </summary>
            /// <param name="value"></param>
            public void ItemDataSynRe(int value, bool isExplosion, bool isStun, float fExplosion, float fStunPer)
            {

                charWeapone.NMinDmg += value;
                charWeapone.NMaxDmg += value;

                if (isExplosion)
                {
                    charWeapone.IsExplosion = isExplosion;
                    charWeapone.NExplosionSlot++;
                    charWeapone.FExplosionArea += fExplosion; //폭발 범위 증가

                    //폭발 속성의 경우 파이어 볼 이펙트
                    if (IsPartsSkin)
                        charWeapone.BulletType = Manager.eProjectilesType.AttackExplosion;
                }

                if (isStun)
                {
                    charWeapone.IsStun = isStun;
                    charWeapone.FStunPer += fStunPer;
                    charWeapone.NStunSlot++;

                    if (IsPartsSkin)
                        charWeapone.BulletType = Manager.eProjectilesType.CometBlue;
                }

                InfoUI.CharWeaponeInfo(charWeapone);
            }

        }

    }
}
