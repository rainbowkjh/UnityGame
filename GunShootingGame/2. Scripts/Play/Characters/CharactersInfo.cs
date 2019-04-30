using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 캐릭터 기본 정보
/// </summary>
namespace Characters
{
    [Serializable]
    public enum CharState
    {
        Idle, Move, 
    }

    [Serializable]
    public class CharactersData
    {
        bool isLive;
        float fHP;
        float fMaxHP;
        float fSpeed;
        CharState eState;

        #region Set,Get
        public float FHP
        {
            get
            {
                return fHP;
            }

            set
            {
                fHP = value;
            }
        }

        public float FMaxHP
        {
            get
            {
                return fMaxHP;
            }

            set
            {
                fMaxHP = value;
            }
        }

        public float FSpeed
        {
            get
            {
                return fSpeed;
            }

            set
            {
                fSpeed = value;
            }
        }

        public CharState EState
        {
            get
            {
                return eState;
            }

            set
            {
                eState = value;
            }
        }

        public bool IsLive
        {
            get
            {
                return isLive;
            }

            set
            {
                isLive = value;
            }
        }
        #endregion

        public CharactersData()
        {
            fHP = 100;
            fMaxHP = 100;
            fSpeed = 2;
        }

       public CharactersData(float hp, float maxhp, float speed)
        {
            isLive = true;
            fHP = hp;
            fMaxHP = maxhp;
            fSpeed = speed;
        }
    }

    public class CharactersInfo : CharactersAniCtrl
    {
        private CharactersData charactersData;

        [SerializeField]
        float fHP;
        [SerializeField]
        float fMaxHP;
        [SerializeField]
        float fSpeed;


        #region Set,Get
        public CharactersData CharactersData
        {
            get
            {
                return charactersData;
            }

            set
            {
                charactersData = value;
            }
        }

        #endregion

        virtual protected void Awake()
        {
            CharactersData = new CharactersData(fHP, fMaxHP, fSpeed);
            Ani = GetComponent<Animator>();
        }


    }

}
