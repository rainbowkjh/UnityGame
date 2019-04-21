using Manager;
using PlayUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  플레이어 행동 관련 함수
///  키보드 입력 컴포넌트에서 호출하여 사용한다
/// </summary>

namespace Characters
{
    public class PlayerCtrl : AniCtrl
    {
        //점프 제어
        Rigidbody mRigid;
        //앉을때..
        CapsuleCollider mColl; //앉을때 콜라이더 크기를 조절한다
        [SerializeField,Header("앉고 서있을때 높이 조절(탄 발사위치)")]
        Transform m_trFirePos;

        PlayerUIEx m_uiBar;

        int m_nParsingIndex = 0; //파싱된 리스트에서 가져올 인덱스 값

        #region Set,Get
        public int NParsingIndex
        {
            get
            {
                return m_nParsingIndex;
            }

            set
            {
                m_nParsingIndex = value;
            }
        }
        #endregion

        protected override void Start()
        {
            base.Start();
            mRigid = GetComponent<Rigidbody>();
            mColl = GetComponent<CapsuleCollider>();
            m_uiBar = GetComponent<PlayerUIEx>();

            UI_Init();
            m_trFirePos.localPosition = new Vector3(0, 1.3f, 0.9f);

            DataInit();
        }

        /// <summary>
        /// 메인 씬에서 게임 시작 시 저장 시킨 데이터를 캐릭터에 적용
        /// </summary>
        void DataInit()
        {
            if (GameManager.INSTANCE.isMale)
            {
                NParsingIndex = 1;
                Debug.Log("Male Load");
            }
            if (!GameManager.INSTANCE.isMale)
            {
                NParsingIndex = 0;
            }


            Manager.GameData.CharactersGameData loadData = GameManager.INSTANCE.gameData.Load(NParsingIndex);

            NLevel = loadData.NLevel;
            sName = loadData.SName;
            FMaxHP = loadData.FMaxHP;
            FHP = loadData.FHP;
            FMaxMana = loadData.FMaxMana;
            FMana = loadData.FMana;            
            FExp = loadData.FExp;
            FNextExp = loadData.FNextExp;
        }

        void UI_Init()
        {
            m_uiBar.HPBarUI(this);
            m_uiBar.ManaBarUI(this);
            m_uiBar.ExpBarUI(this);
        }

     /// <summary>
     /// 키 입력이 있을때 방향 이동
     /// 서있는 상태에서만 이동
     /// </summary>
     /// <param name="h"></param>
        public void PlayerMove(float h, float v)
        {
            if(State != CharState.Crouch
                && !IsRoll)
            {
                //이동 방향(좌 우)
                Vector3 dir = new Vector3(h, 0, v).normalized;
                bool isMove = false;

                if (h != 0 || v != 0)
                {
                    
                    //State = CharState.Walk;
                    State = CharState.Run;
                    isMove = true;

                    transform.rotation = Quaternion.LookRotation(dir);
                }

                else if (h == 0 && v == 0)
                {
                    State = CharState.Idle;
                    isMove = false;
                }

                transform.Translate(dir * FSpeed * Time.deltaTime, Space.World);

                
                //WalkAni(isMove);
                RunAni(isMove);
            }

            
            //  PlayerRot(h, v);
        }

        /// <summary>
        /// 플레이어가 바라보는 방향을 결정한다
        /// </summary>
        /// <param name="h"></param>
        private void PlayerRot(float h, float v)
        {

            if (h > 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }

            if (h < 0)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }


        }

        /// <summary>
        /// 앉는 키 입력 시
        /// true, flase를 변경하여
        /// 행동 실행
        /// </summary>
        /// <param name="isCrouch"></param>
        public void PlayerCrouch(bool isCrouch)
        {
            if (isCrouch)
            {
                State = CharState.Crouch;
                mColl.height = 1.0f;
                mColl.center = new Vector3(0, 0.5f, 0);
                m_trFirePos.localPosition = new Vector3(0, 0.7f, 0.9f);
            }

            else if (!isCrouch)
            {
                StartCoroutine(IdleAniChange());
                mColl.height = 1.5f;
                mColl.center = new Vector3(0, 0.8f, 0);
                m_trFirePos.localPosition = new Vector3(0, 1.3f, 0.9f);
            }

            // Debug.Log("Crouch : " + isCoruch);
            CrouchAni(isCrouch);
        }

        /// <summary>
        /// 일어날때 방향키 입력 시 밀리는 형상이 있어
        /// 상태를 딜레이를 주어 변경 시킴
        /// </summary>
        /// <returns></returns>
        IEnumerator IdleAniChange()
        {
            yield return new WaitForSeconds(1.0f);
            State = CharState.Idle;
        }

        /// <summary>
        /// 점프키 입력 시
        /// </summary>
        public void PlayerJump()
        {
            //캐릭터가 땅에 있는지 확인 후
            if (!IsJump
                && !IsRoll)
            {
               // Debug.Log("IsJump Check 1 : " + IsJump);
                IsJump = true;
                // Debug.Log("IsJump Check 2 : " + IsJump);

                //점프
                mRigid.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
                
                //점프 애니메이션
                GroundAni(false);
                JumpTrigger();
            }           
        }

 /// <summary>
 /// 캐릭터 구르기(무적상태)
 /// </summary>
        public void PlayerRoll()
        {
            if (!IsRoll
                && !IsJump)
                StartCoroutine(RollDelay());
        }

        IEnumerator RollDelay()
        {
            IsRoll = true;
            RollAni();

            yield return new WaitForSeconds(1.5f);
            IsRoll = false;
        }

        public void TestHP()
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
                FHP -= 10;
        }

    }

}
