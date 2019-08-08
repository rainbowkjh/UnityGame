using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Inventory
    {
        public class GunWeaponeSlotManager : MonoBehaviour
        {
            [SerializeField]
            GunWeaponeDrop[] weaponeSlots;

            [SerializeField]
            Weapone.WeaponeData gunWeapone;


            private void Start()
            {
                for (int i = 0; i < weaponeSlots.Length; i++)
                {
                    if (weaponeSlots[i].transform.childCount != 0)
                    {
                        PartsItemData parts = weaponeSlots[i].GetComponentInChildren<PartsItemData>();

                        gunWeapone.NMinDmg += parts._PartsItem._DmgUp;
                        gunWeapone.NMaxDmg += parts._PartsItem._DmgUp;

                        //장착하는 아이템에 폭발 속성이 있으면
                        if (parts._PartsItem._IsExplosion)
                        {
                            //폭발 속성의 경우 파이어 볼 이펙트
                            if (weaponeSlots[i].IsPartsSkin)
                                gunWeapone.BulletType = Manager.eProjectilesType.FireBall;

                            gunWeapone.IsExplosion = parts._PartsItem._IsExplosion; //값을 적용 시키고...
                            gunWeapone.NExplosionSlot++; //폭발 속성이 하나 적용되었다고 기록한다(0이 되면 false로 변경 시키기 위해서)
                            gunWeapone.FExplosionArea += parts._PartsItem._FExplosionArea; //폭발 범위 증가
                        }

                        //위와 같은 원리
                        if (parts._PartsItem._IsStun)
                        {
                            //스턴 속성의 경우 파이어 볼 이펙트
                            if (weaponeSlots[i].IsPartsSkin)
                                gunWeapone.BulletType = Manager.eProjectilesType.CometBlue;

                            gunWeapone.IsStun = parts._PartsItem._IsStun;
                            gunWeapone.FStunPer += parts._PartsItem._FStunPer;
                        }

                        weaponeSlots[i].GetComponentInChildren<IconDrag>().GunWeaponeSlotEquip = true; //캐릭터 무기 파츠 슬롯  장착
                        weaponeSlots[i].InfoUI.GunWeaponeInfo(gunWeapone);
                    }
                }


            }


        }
        //class End
    }
}
