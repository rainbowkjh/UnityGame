using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기 파츠 슬롯 3개를 관리하며
/// 
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class CharWeaponeSlotManager : MonoBehaviour
        {
            [SerializeField]
            CharWeaponeDrop[] weaponeSlots;

            [SerializeField]
            Weapone.CharWeaponeCtrl charWeapone;

            private void Start()
            {
                for (int i = 0; i < weaponeSlots.Length; i++)
                {
                    if (weaponeSlots[i].transform.childCount != 0)
                    {
                        PartsItemData parts = weaponeSlots[i].GetComponentInChildren<PartsItemData>();

                        charWeapone.NMinDmg += parts._PartsItem._DmgUp;
                        charWeapone.NMaxDmg += parts._PartsItem._DmgUp;

                        //장착하는 아이템에 폭발 속성이 있으면
                        if (parts._PartsItem._IsExplosion)
                        {
                            //폭발 속성의 경우 파이어 볼 이펙트
                            if (weaponeSlots[i].IsPartsSkin)
                                charWeapone.BulletType = Manager.eProjectilesType.AttackExplosion;

                            charWeapone.IsExplosion = parts._PartsItem._IsExplosion; //값을 적용 시키고...
                            charWeapone.NExplosionSlot++; //폭발 속성이 하나 적용되었다고 기록한다(0이 되면 false로 변경 시키기 위해서)
                            charWeapone.FExplosionArea += parts._PartsItem._FExplosionArea; //폭발 범위 증가
                        }

                        //위와 같은 원리
                        if (parts._PartsItem._IsStun)
                        {
                            //스턴 속성의 경우 파이어 볼 이펙트
                            if (weaponeSlots[i].IsPartsSkin)
                                charWeapone.BulletType = Manager.eProjectilesType.CometBlue;

                            charWeapone.IsStun = parts._PartsItem._IsStun;
                            charWeapone.FStunPer += parts._PartsItem._FStunPer;
                        }

                        weaponeSlots[i].GetComponentInChildren<IconDrag>().CharWeaponeSlotEquip = true; //캐릭터 무기 파츠 슬롯  장착
                        weaponeSlots[i].InfoUI.CharWeaponeInfo(charWeapone);
                    }
                }


            }

        }

    }
}
