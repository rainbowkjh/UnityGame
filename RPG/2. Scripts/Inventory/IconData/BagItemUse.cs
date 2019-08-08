using Black.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 회복 아이템 사용
/// 슬롯에 있는 아이템을 단툭 키로 사용
/// 또는 인벤에서 마우스로 아이템 아이콘에서
/// 마우스 오른쪽 클릭해서 사용
/// </summary>
/// 
namespace Black
{
    namespace Inventory
    {
        public class BagItemUse : MonoBehaviour
        {
            [SerializeField]
            PlayerCtrl player;

            [SerializeField]
            UIBar uiBar;

            [SerializeField]
            InventoryData inven;

            ParticleSystem effectPlay; //아이템 사용 시 이펙트효과

            public PlayerCtrl _Player { get => player; set => player = value; }

            #region 회복 아이템 적용 부분(아이템 속성 추가시 코드 추가 부분)
            /// <summary>
            /// 아이템의 종류를 확인하여
            /// 데이터 처리
            /// </summary>
            public void UseItemApply(BagItemData item)
            {
                //Debug.Log("item._BagItem._UseType" + item._BagItem._UseType);

                //사용하는 아이템의 타입을 확인하여 해당 수치를 회복 시킨다
                switch (item._BagItem._UseType)
                {
                    #region 회복
                    
                    case Manager.eBagItemType.HP_Recovery:

                        //HP가 Max가 아닐때 사용
                        if(_Player.NHp < _Player.NMaxHp)
                        {
                            item.NCount--;
                            //무게를 줄이고 무게 값 텍스츠를 출력
                            _Player.FWeight -= item._BagItem._Weight;

                            //HP 회복 이펙트를 가져온다
                            effectPlay = _Player.Pool.GetParticlePool(_Player.Pool.hpRevCount, _Player.Pool.hpRecList);

                            _Player.NHp += item._BagItem._Value;
                            if (_Player.NHp >= _Player.NMaxHp)
                            {
                                _Player.NHp = _Player.NMaxHp;
                            }

                        }

                        break;

                    case Manager.eBagItemType.Satiety_Recovery:

                        if(_Player.FSatiety < _Player.FMaxSatiety)
                        {
                            item.NCount--;
                            //무게를 줄이고 무게 값 텍스츠를 출력
                            _Player.FWeight -= item._BagItem._Weight;

                            //Sat 회복 이펙트를 가져온다
                            effectPlay = _Player.Pool.GetParticlePool(_Player.Pool.revCount, _Player.Pool.recList);

                            _Player.FSatiety += item._BagItem._Value;
                            if (_Player.FSatiety >= _Player.FMaxSatiety)
                            {
                                _Player.FSatiety = _Player.FMaxSatiety;
                            }
                        }
                        
                        break;

                    case Manager.eBagItemType.Mana_Recovery:

                        if(_Player.FMana < _Player.FMaxMana)
                        {
                            item.NCount--;
                            //무게를 줄이고 무게 값 텍스츠를 출력
                            _Player.FWeight -= item._BagItem._Weight;
                            //Mana 회복 이펙트를 가져온다
                            effectPlay = _Player.Pool.GetParticlePool(_Player.Pool.revCount, _Player.Pool.recList);

                            _Player.FMana += item._BagItem._Value;
                            if (_Player.FMana >= _Player.FMaxMana)
                            {
                                _Player.FMana = _Player.FMaxMana;
                            }
                        }
                                                
                        break;

                    case Manager.eBagItemType.Thirst_Recovery:

                        if(_Player.FThirst < _Player.FMaxThirst)
                        {
                            item.NCount--;
                            //무게를 줄이고 무게 값 텍스츠를 출력
                            _Player.FWeight -= item._BagItem._Weight;
                            //Thi 회복 이펙트를 가져온다
                            effectPlay = _Player.Pool.GetParticlePool(_Player.Pool.revCount, _Player.Pool.recList);

                            _Player.FThirst += item._BagItem._Value;
                            if (_Player.FThirst >= _Player.FMaxThirst)
                            {
                                _Player.FThirst = _Player.FMaxThirst;
                            }
                        }
                        
                        break;
                    #endregion

                    #region 수치 증가 (Upgrade 수치보다 작아야 가능)
                    case Manager.eBagItemType.HP_Up:

                        if(_Player.NMaxHp < _Player.NMaxUpgradeHp)
                        {
                            item.NCount--;
                            //무게를 줄이고 무게 값 텍스츠를 출력
                            _Player.FWeight -= item._BagItem._Weight;
                            _Player.NOriginHP += item._BagItem._Value;
                            _Player.NMaxHp += item._BagItem._Value;
                        }
                        
                        break;

                    case Manager.eBagItemType.Satiety_Up:

                        if(_Player.FMaxSatiety < _Player.FMaxUpgradeSatiety)
                        {
                            item.NCount--;
                            //무게를 줄이고 무게 값 텍스츠를 출력
                            _Player.FWeight -= item._BagItem._Weight;
                            _Player.FMaxSatiety += item._BagItem._Value;

                        }
                        break;

                    case Manager.eBagItemType.Mana_Up:

                        if(_Player.FMaxMana < _Player.FMaxUpgradeMana)
                        {
                            item.NCount--;
                            //무게를 줄이고 무게 값 텍스츠를 출력
                            _Player.FWeight -= item._BagItem._Weight;
                            _Player.FMaxMana += item._BagItem._Value;

                        }
                        break;

                    case Manager.eBagItemType.Thirst_Up:

                        if(_Player.FMaxThirst < _Player.FMaxUpgradeThirst)
                        {
                            item.NCount--;
                            //무게를 줄이고 무게 값 텍스츠를 출력
                            _Player.FWeight -= item._BagItem._Weight;
                            _Player.FMaxThirst += item._BagItem._Value;

                        }
                        break;
                    #endregion

                        //모든 수치 회복
                    case Manager.eBagItemType.Recovery:

                        if(_Player.NHp < _Player.NMaxHp ||
                           _Player.FSatiety < _Player.FMaxSatiety||
                           _Player.FMana < _Player.FMaxMana||
                           _Player.FThirst < _Player.FMaxThirst)
                        {
                            item.NCount--;

                            _Player.NHp += item._BagItem._Value;
                            _Player.FMana += item._BagItem._Value;
                            _Player.FThirst += item._BagItem._Value;
                            _Player.FSatiety += item._BagItem._Value;

                            effectPlay = _Player.Pool.GetParticlePool(_Player.Pool.revCount, _Player.Pool.recList);

                            //무게를 줄이고 무게 값 텍스츠를 출력
                            _Player.FWeight -= item._BagItem._Weight;
                        }

                        break;

                        //데미지 증가
                    case Manager.eBagItemType.Dmg_Up:
                        item.NCount--;
                        //무게를 줄이고 무게 값 텍스츠를 출력
                        _Player.FWeight -= item._BagItem._Weight;
                        break;
                
                }

                if (effectPlay != null)
                {
                    effectPlay.transform.position = _Player.transform.position; //이펙트 위치를 플레이어로,,,
                    effectPlay.gameObject.SetActive(true); //이펙트 활성화
                    effectPlay.Play(); //이펙트 실행

                    StartCoroutine(_Player.Pool.ParticleFalse(effectPlay, 3)); //이펙트 비활성화
                }

          
                inven.WeightTextPrint();

                uiBar.HpBar();
                uiBar.ManaBar();
                uiBar.ThirstBar();
                uiBar.SatietyBar();
            }
            #endregion

        }

    }
}
