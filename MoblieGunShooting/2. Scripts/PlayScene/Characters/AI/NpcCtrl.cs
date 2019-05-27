using Black.Manager;
using Black.Weapone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC 캐릭터 제어
/// 네비 사용 하지 않고
/// 정해진 동선으로 이동 및 공격
/// 연출을 위해 적이 감지 되지 않아도 사격하는 기능
/// 적에게 공격 당하고 쓰러지는 애니메이션 등
/// </summary>
namespace Black
{
    namespace Characters
    {
        [RequireComponent(typeof(NpcAniCtrl))]
        public class NpcCtrl : CharactersData
        {
            NpcAniCtrl aniCtrl;

            [SerializeField, Header("Stage가 시작 되면 제어")]
            StageManager stage;

            [SerializeField, Header("무기 데이터")]
            NPCWeapone weapone;

            [SerializeField, Header("적 감지를 하지 않고 제자리에서 사격 재장전만 함 이벤트 연출용")]
            bool isEvent = false;

            /// <summary>
            /// 공격 자세 전에는 재기 자세로 있는다
            /// </summary>
            [SerializeField]
            bool isAttackReady = false;

            #region Set, Get
            public NpcAniCtrl AniCtrl
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

            void Start()
            {
                AniCtrl = GetComponent<NpcAniCtrl>();

            }

            
            void Update()
            {
                if(stage.IsStart)
                {
                    if (isEvent)
                    {
                        if (isAttackReady)
                        {
                            if (!IsReload)
                            {
                                weapone.Fire(this);
                            }

                            if (weapone.CurBullet <= 0)
                            {
                                if (!IsReload)
                                {
                                    StartCoroutine(weapone.Reload(this));
                                }

                            }
                        }

                    }

                    else if (!isEvent)
                    {

                    }

                }
                AniCtrl.ReadyAni(isAttackReady);
            }

        }

    }
}
