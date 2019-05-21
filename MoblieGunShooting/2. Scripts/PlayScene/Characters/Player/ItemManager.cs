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

            int nGrenadeCount = 0;
            int nRecoveryCount = 0;

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

            private void Start()
            {
                player = GetComponent<PlayerCtrl>();

                GrenadeCountText();
                RecoveryCountText();
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

            #endregion

            /// <summary>
            /// 수류탄의 개수 출력
            /// </summary>
            public void GrenadeCountText()
            {
                grenadeText.text = NGrenadeCount.ToString();
            }

            /// <summary>
            /// 회복 아이템 개수 출력
            /// </summary>
            public void RecoveryCountText()
            {
                recoveryText.text = NRecoveryCount.ToString();
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

                    greadeColl.GreadeDMG = greadeDMG; //데미지 값 전달
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
                yield return new WaitForSeconds(0.5f);
                greadeColl.IsAttack = false;
            }

            /// <summary>
            /// 회복 아이템 사용
            /// </summary>
            public void UseRecovery()
            {
                if(NRecoveryCount >0)
                {
                    NRecoveryCount--;
                    player.Hp += recoveryHP;
                    RecoveryCountText();
                    player.PlayerUI.CurHP(player.Hp, player.MaxHp);
                }
            }

        }

    }
}
