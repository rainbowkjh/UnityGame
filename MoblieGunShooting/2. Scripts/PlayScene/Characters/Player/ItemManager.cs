using Black.Manager;
using Black.Weapone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어의 아이템을 관리 한다
/// 무기 강화 재료, 수류탄, 회복 아이템
/// 
/// UI 버튼에서 호출하여 사용하는 기능도 같이 제공
/// 아이템 개수 화면에 출력 기능 
/// 
/// 샷건으로 아이템 타격 시 아이템 습득을 못함
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class ItemManager : MonoBehaviour
        {
            PlayerCtrl player;

            //현재 소지량(테스트 후 인스펙터 창에서 닫을것!)
            [SerializeField]
            int nGrenadeCount = 0;
            [SerializeField]
            int nRecoveryCount = 0;

            //최대 소지량
            int nMaxGrenadeCount = 3;
            int nMaxRecorveryCount = 3;

            //업그래이드 레벨(10까지)
            int grenadeLevel = 1;
            int recoveryLevel = 1;

            //데미지와 회복량은 업그래이드로 올릴 수 있다
            [SerializeField, Header("수류탄 데미지")]
            float greadeDMG = 100;

            [SerializeField, Header("회복량")]
            float recoveryHP = 100;


            [SerializeField, Header("수류탄 남은 개수 Text")]
            Text grenadeText;

            [SerializeField, Header("회복 아이템 남은 개수 Text")]
            Text recoveryText;

            [SerializeField, Header("수류탄 타격 지역(coll)")]
            GreadeHitColl greadeColl;

            [SerializeField, Header("업그래이드 포인트")]
            int nUpgradePoint = 0;

            private void Start()
            {
                player = GetComponent<PlayerCtrl>();

                GrenadeCountText();
                RecoveryCountText();

                //데이터 로드
                ItemDataLoad();
            }

            #region Set,Get
            public int NGrenadeCount
            {
                get
                {
                    return nGrenadeCount;
                }

                set
                {
                    nGrenadeCount = value;
                }
            }

            public int NRecoveryCount
            {
                get
                {
                    return nRecoveryCount;
                }

                set
                {
                    nRecoveryCount = value;
                }
            }

            public float GreadeDMG
            {
                get
                {
                    return greadeDMG;
                }

                set
                {
                    greadeDMG = value;
                }
            }

            public float RecoveryHP
            {
                get
                {
                    return recoveryHP;
                }

                set
                {
                    recoveryHP = value;
                }
            }

            public int NMaxGrenadeCount
            {
                get
                {
                    return nMaxGrenadeCount;
                }

                set
                {
                    nMaxGrenadeCount = value;
                }
            }

            public int NMaxRecorveryCount
            {
                get
                {
                    return nMaxRecorveryCount;
                }

                set
                {
                    nMaxRecorveryCount = value;
                }
            }

            public int GrenadeLevel
            {
                get
                {
                    return grenadeLevel;
                }

                set
                {
                    grenadeLevel = value;
                }
            }

            public int RecoveryLevel
            {
                get
                {
                    return recoveryLevel;
                }

                set
                {
                    recoveryLevel = value;
                }
            }

            public int NUpgradePoint
            {
                get
                {
                    return nUpgradePoint;
                }

                set
                {
                    nUpgradePoint = value;
                }
            }

            #endregion

            /// <summary>
            /// 수류탄의 개수 출력
            /// </summary>
            public void GrenadeCountText()
            {
                grenadeText.text = NGrenadeCount + "/<color=#00ff00>" + NMaxGrenadeCount + "</color>";
            }

            /// <summary>
            /// 회복 아이템 개수 출력
            /// </summary>
            public void RecoveryCountText()
            {
                recoveryText.text = NRecoveryCount + "/<color=#00ff00>" + NMaxRecorveryCount + "</color>";
            }

            /// <summary>
            /// 수류탄 사용
            /// </summary>
            public void UseGrenade()
            {
                if (NGrenadeCount > 0)
                {
                    NGrenadeCount--;
                    GrenadeCountText();

                    greadeColl.GreadeDMG = GreadeDMG; //데미지 값 전달
                    greadeColl.IsAttack = true; //사용

                    greadeColl.ExplosionPaly(); //파티클 실행
                    StartCoroutine(GreadeDelay());
                }
                    
            }

            /// <summary>
            /// 바로 false로 하면
            /// 한명만 데미지를 받기 떄문에 약간의 시간을 준다
            /// </summary>
            /// <returns></returns>
            IEnumerator GreadeDelay()
            {
                yield return new WaitForSeconds(0.1f);
                greadeColl.IsAttack = false;
            }

            /// <summary>
            /// 회복 아이템 사용
            /// </summary>
            public void UseRecovery()
            {
                if(NRecoveryCount > 0 &&
                    player.Hp < player.MaxHp)
                {
                    NRecoveryCount--;
                    player.Hp += RecoveryHP;

                    if (player.Hp >= player.MaxHp)
                    {
                        player.Hp = player.MaxHp;
                    }

                    RecoveryCountText();
                    player.PlayerUI.CurHP(player.Hp, player.MaxHp);
                }
            }

            /// <summary>
            /// 로드 데이터 적용
            /// </summary>
            void ItemDataLoad()
            {
                nGrenadeCount = GameManager.INSTANCE.playerData.nGrenadeCount;
                nRecoveryCount = GameManager.INSTANCE.playerData.nRecoveryCount;
                nMaxGrenadeCount = GameManager.INSTANCE.playerData.nMaxGrenadeCount;
                nMaxRecorveryCount = GameManager.INSTANCE.playerData.nMaxRecorveryCount;
                grenadeLevel = GameManager.INSTANCE.playerData.grenadeLevel;
                recoveryLevel = GameManager.INSTANCE.playerData.recoveryLevel;
                greadeDMG = GameManager.INSTANCE.playerData.greadeDMG;
                recoveryHP = GameManager.INSTANCE.playerData.recoveryHP;
                nUpgradePoint = GameManager.INSTANCE.playerData.nUpgradePoint;

                RecoveryCountText();
                GrenadeCountText();
                
            }

        }

    }
}
