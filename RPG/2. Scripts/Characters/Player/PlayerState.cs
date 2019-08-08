using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 능력치를 관리
/// 배고품 1 /2이면 Bag 인벤의 무게를 절반
/// 목마름 달리기 제한
/// 마나는 스킬 사용
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class PlayerState : MonoBehaviour
        {
            PlayerCtrl player;

            void Start()
            {
                player = GetComponent<PlayerCtrl>();
                                
            }

            #region 변경 전
            /// <summary>
            /// 마나 감소
            /// 반환 값으로
            /// 스킬을 사용할수 있는지 확인
            /// </summary>
            public bool ManaCtrl(float mana)
            {
                bool isUse = false;

                //플레이어의 마나가
                //사용하려는 마나 수치보다 많으면 사용
                if(player.FMana >= mana
                    && !GameManager.INSTANCE.isSafeArea)
                {
                    isUse = true;
                    player.FMana -= mana; //사용 수치 만큼 감소
                }

                return isUse;
            }

            /// <summary>
            /// 목마름 수치 감소
            /// 반환 값에 따라 달릴수 있는 상태 확인
            /// </summary>
            public bool ThirstCtrl()
            {
                bool isRun = false;

                //마을에 있을때는 감소 하지 않도록 해준다
                if(player.FThirst > 0
                    && !GameManager.INSTANCE.isSafeArea)
                {
                    player.FThirst -= 0.3f * Time.deltaTime;
                    isRun = true;
                }

                else if(player.FThirst <= 0)
                {
                    player.FThirst = 0;
                    isRun = false;
                }

                return isRun;
            }

            /// <summary>
            /// 배고픔 상태
            /// </summary>            
            public void SatietyCtrl()
            {
                if(player.FSatiety > 0
                    && !GameManager.INSTANCE.isSafeArea)
                {
                    player.FSatiety -= 0.1f * Time.deltaTime;

                    if (player.FSatiety >= (player.FSatiety * .5f))
                    {
                        player.FMaxWeight = player.FOriginWeight; //정상 무게
                    }

                    //배고품 수치가 반이하이면 가방의 무게를 반 줄인다
                    if (player.FSatiety < (player.FSatiety * .5f))
                    {
                        player.FMaxWeight = player.FMaxWeight * 0.5f;
                    }

                }

                //수치가 0이면 가방의 무게 0
                else if(player.FSatiety <= 0)
                {
                    player.FSatiety = 0;
                    player.FMaxWeight = 0;
                }

            }
            #endregion

            /// <summary>
            /// 마나 감소
            /// 반환 값으로
            /// 스킬을 사용할수 있는지 확인
            /// </summary>
            public bool UseMana(float mana)
            {
                bool isUse = false;

                //플레이어의 마나가
                //사용하려는 마나 수치보다 많으면 사용
                if (player.FMana >= mana)
                {
                    isUse = true;
                    player.FMana -= mana; //사용 수치 만큼 감소
                }

                return isUse;
            }

            /// <summary>
            /// 마나가 천천히 증가한다
            /// </summary>
            public void ManaIncrease(float increase)
            {
                if(player.FMana < player.FMaxMana)
                {
                    player.FMana += increase * Time.deltaTime;
                }
            }

            /// <summary>
            /// 목마름 수치 감소
            /// 반환 값에 따라 달릴수 있는 상태 확인
            /// </summary>
            public void ThirstDecrease()
            {        
                //마을에 있을때는 감소 하지 않도록 해준다
                if (player.FThirst > 0
                    && !GameManager.INSTANCE.isSafeArea)
                {
                    player.FThirst -= 0.3f * Time.deltaTime;                   
                }

                else if (player.FThirst <= 0)
                {
                    player.FThirst = 0;                    
                }
            }

      
            /// <summary>
            /// 배고픔 상태
            /// 무게 감소 (손 볼때가 많아서 우선 HP 감소 발생으로 변경)
            /// 인벤에서 무게가 줄어 들었을때 경우 코드가 필요함
            /// </summary>            
            public void SatietyDecrease()
            {
                if (player.FSatiety > 0
                    && !GameManager.INSTANCE.isSafeArea)
                {
                    player.FSatiety -= 0.1f * Time.deltaTime;

                    if (player.FSatiety >= (player.FMaxSatiety * 0.5f))
                    {
                        player.FMaxWeight = player.FOriginWeight; //정상 무게
                    }

                    //배고품 수치가 반 이하이면 가방의 무게를 반 줄인다
                    else if (player.FSatiety < (player.FMaxSatiety * 0.5f))
                    {
                        player.FMaxWeight = player.FOriginWeight * 0.5f; //정상 무게에서 절반
                    }

                }

                //수치가 0이면 가방의 무게 0
                else if (player.FSatiety <= 0)
                {
                    player.FSatiety = 0;
                    player.FMaxWeight = 0;
                }

            }

            /// <summary>
            /// Satiety가 0이면 hp 절반으로 감소
            /// </summary>
            public void SatietyDecreaseHP()
            {
                if (player.FSatiety > 0
                    && !GameManager.INSTANCE.isSafeArea)
                {
                    player.NMaxHp = player.NOriginHP;
                    player.FSatiety -= 0.1f * Time.deltaTime;

                }

                else if (player.FSatiety <= 0)
                {
                    player.FSatiety = 0;
                    player.NMaxHp = (int)(player.NOriginHP * 0.5f);
                    player.NHp = player.NMaxHp;
                }

            }

            
        }
        //end
    }
}
