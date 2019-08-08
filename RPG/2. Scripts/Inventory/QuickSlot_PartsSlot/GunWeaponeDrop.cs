using Black.PlayerUI;
using Black.Weapone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 무기의 파츠 장착 슬롯 중
/// 캐릭터 총 슬롯
/// </summary>
namespace Black
{
    namespace Inventory
    {
        public class GunWeaponeDrop : PartsSlotIconDrop, IDropHandler
        {
            [SerializeField]
            WeaponeData weaponeData;

            [SerializeField,Header("InvenBtn에 있는 UI")]
            InfoTextUI infoUI;

            [SerializeField, Header("해당 슬롯에 파츠 장착 시 탄 외형 변경")]
            bool isPartsSkin = false;

            public bool IsPartsSkin { get => isPartsSkin; set => isPartsSkin = value; }
            public InfoTextUI InfoUI { get => infoUI; set => infoUI = value; }

            protected override void Start()
            {
                base.Start();

                InfoUI.GunWeaponeInfo(weaponeData);

                //weaponeData = GameObject.FindGameObjectWithTag("WeaponeEquip1").GetComponentInChildren<WeaponeData>();
            }

            public void OnDrop(PointerEventData eventData)
            {
                if (transform.childCount == 0)
                {
                    //슬롯과 아이템 타입이 같으면 슬롯에 장착
                    if (slotType == IconDrag.draggingItem.GetComponent<IconDrag>().ItemType)
                    {
                        IconDrag.draggingItem.transform.SetParent(this.transform); //슬롯 장착
                        IconDrag.draggingItem.GetComponent<IconDrag>().GunWeaponeSlotEquip = true; //총 무기 파츠 슬롯  장착

                        //데이터 적용
                        PartsItemData parts = IconDrag.draggingItem.GetComponent<PartsItemData>();

                        weaponeData.NMinDmg += parts._PartsItem._DmgUp;
                        weaponeData.NMaxDmg += parts._PartsItem._DmgUp;

                        /*
                            Explosion 속성 아이템 장차 후
                            Stun 속성 장착하면 Explosion을 false 시킬수 있기 때문에
                            false의 경우 무시하고 넘긴다
                            (파츠 장착 안했을 경우 기본값이 false이므로 장착하지 않으면 false)
                         */

                        //장착하는 아이템에 폭발 속성이 있으면
                        if (parts._PartsItem._IsExplosion)
                        {
                            //폭발 속성의 경우 파이어 볼 이펙트
                            if (IsPartsSkin)
                                weaponeData.BulletType = Manager.eProjectilesType.FireBall;

                            weaponeData.IsExplosion = parts._PartsItem._IsExplosion; //값을 적용 시키고...
                            weaponeData.NExplosionSlot++; //폭발 속성이 하나 적용되었다고 기록한다(0이 되면 false로 변경 시키기 위해서)
                            weaponeData.FExplosionArea += parts._PartsItem._FExplosionArea; //폭발 범위 증가
                        }
                        
                        //위와 같은 원리
                        if(parts._PartsItem._IsStun)
                        {
                            //기절효과 시 이펙트
                            if (IsPartsSkin)
                                weaponeData.BulletType = Manager.eProjectilesType.CometBlue;

                            weaponeData.IsStun = parts._PartsItem._IsStun;
                            weaponeData.FStunPer += parts._PartsItem._FStunPer;
                        }
                        

                        InfoUI.GunWeaponeInfo(weaponeData);
                    }

                }
            }

            /// <summary>
            /// 파트 해제 시 능력치 수정
            /// </summary>
            /// <param name="min"></param>
            /// <param name="max"></param>
            public void ItemDataSyn(int value, bool isExplosion, bool isStun,float fExplosion, float fStunPer)
            {
                weaponeData.NMinDmg -= value;
                weaponeData.NMaxDmg -= value;

                //첫번째 슬롯의 파츠를 분리하면 원래 이펙트로 변경
                if (IsPartsSkin)
                    weaponeData.BulletType = weaponeData.BulletTypeOrigin;

                //다른 슬롯에도 같은 속성의 파츠가 있을때 예외처리 해줘야함                                
                //폭발 속성이 있는 파트를 제거 시
                if (isExplosion)
                {
                    weaponeData.NExplosionSlot--; //폭발 속성 파츠 하나 제거
                    weaponeData.FExplosionArea -= fExplosion;

                    if (weaponeData.NExplosionSlot <= 0)
                    {
                        weaponeData.IsExplosion = false; //파트가 0개 존재하면 false
                    }

                }
                
                if(isStun)
                {
                    weaponeData.NStunSlot--;
                    weaponeData.FStunPer -= fStunPer;

                    if (weaponeData.NStunSlot <= 0)
                    {
                        weaponeData.IsStun = false;                        
                    }
                }
                     
                InfoUI.GunWeaponeInfo(weaponeData);
            }

            /// <summary>
            /// 아이콘을 파트 장착 슬롯에서
            /// 해제 실패 시 다시 슬롯으로 돌아 갈때
            /// 적용했던 데이터를 다시 적용시킨다
            /// </summary>
            public void ItemDataSynRe(int value, bool isExplosion, bool isStun,float fExplosion, float fStunPer)
            {        

                weaponeData.NMinDmg += value;
                weaponeData.NMaxDmg += value;

                //Explosion 속성 아이템 장차 후
                //Stun 속성 장착하면 Explosion을 false 시킬수 있기 때문에
                //false의 경우 무시하고 넘긴다
                //(파츠 장착 안했을 경우 기본값이 false이므로 장착하지 않으면 false)
                if(isExplosion)
                {
                    weaponeData.IsExplosion = isExplosion;
                    weaponeData.NExplosionSlot++;
                    weaponeData.FExplosionArea += fExplosion; //폭발 범위 증가

                    //폭발 속성의 경우 파이어 볼 이펙트
                    if (IsPartsSkin)
                        weaponeData.BulletType = Manager.eProjectilesType.FireBall;
                }
                
                if(isStun)
                {
                    weaponeData.IsStun = isStun;
                    weaponeData.FStunPer += fStunPer;
                    weaponeData.NStunSlot++;

                    //폭발 속성의 경우 파이어 볼 이펙트
                    if (IsPartsSkin)
                        weaponeData.BulletType = Manager.eProjectilesType.CometBlue;
                }
                

                InfoUI.GunWeaponeInfo(weaponeData);
            }


        }

    }
}
