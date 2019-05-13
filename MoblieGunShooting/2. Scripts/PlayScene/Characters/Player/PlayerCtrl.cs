using Black.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Characters
    {
        [RequireComponent(typeof(AniCtrl))]
        public class PlayerCtrl : CharactersData
        {
            
            [SerializeField,Header("에임 (애니메이션 실행)")]
            AimUI aimUI;

            PlayerUI playerUI;

            [SerializeField, Header("발사 시작 지점")]
            Transform firePosTr;

            AniCtrl aniCtrl;
            #region Set,Get


            public AimUI AimUI
            {
                get
                {
                    return aimUI;
                }

                set
                {
                    aimUI = value;
                }
            }

            public PlayerUI PlayerUI
            {
                get
                {
                    return playerUI;
                }

                set
                {
                    playerUI = value;
                }
            }

            public Transform FirePosTr
            {
                get
                {
                    return firePosTr;
                }

                set
                {
                    firePosTr = value;
                }
            }

            public AniCtrl AniCtrl
            {
                get
                {
                    return aniCtrl;
                }

                set
                {
                    aniCtrl = value;
                }
            }
            #endregion

            private void Awake()
            {
                //   Debug.Log("PlayerCtrl");
                AniCtrl = GetComponent<AniCtrl>();
                PlayerUI = GetComponent<PlayerUI>();

                PlayerUI.CurHP(Hp, MaxHp);
            }



            /// <summary>
            /// 에임이 적을 바라보면 정보를 보여준다 
            /// </summary>
            public void EnemyInfoPrint()
            {
                RaycastHit hit;
                int layerMask = 1 << 8;
                layerMask = ~layerMask;

                if (Physics.Raycast(FirePosTr.position, FirePosTr.forward,
                  out hit, 500, layerMask))
                {
                    Debug.DrawLine(FirePosTr.position, hit.point, Color.blue);
                    if (hit.transform.GetComponent<CharactersData>())
                    {
                        PlayerUI.EnemyHpInfoAct();
                        playerUI.EnemyHpInfo(hit.transform.GetComponent<CharactersData>().Hp,
                            hit.transform.GetComponent<CharactersData>().MaxHp);                        
                    }

                    else
                    {
                        PlayerUI.EnmryHpInfoDisable();
                    }
                }
            }

            /// <summary>
            /// 다음 위치로 이동 시킨다
            /// </summary>
            public void PlayerNextMove()
            {
                if(NextMove != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                                                NextMove.position, Speed * Time.deltaTime);
                }                
            }


        }

    }
}
