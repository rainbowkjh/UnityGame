using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Characters
{
    public enum CharState
    {
        Idle, Walk, Run, Crouch, Hit, Dead,
    }

    public class CharactersData : MonoBehaviour
    {
        CharState m_State = CharState.Idle;
        int m_nLevel = 1;
        string m_sName = "";
        float m_fHP = 500.0f;
        float m_fMaxHP = 500.0f;
        float m_fMana = 200.0f;
        float m_fMaxMana = 200.0f;
        float m_fExp = 0;
        float m_fNextExp = 500;
        float m_fSpeed = 2.0f;
        float m_fSta = 200.0f;
        float m_fMaxSta = 200.0f;
        bool isJump = false;
        bool isRoll = false;

        #region Set,Get
        public float FHP
        {
            get
            {
                return m_fHP;
            }

            set
            {
                m_fHP = value;

                if (m_fHP <= 0)
                    m_fHP = 0;

                if (m_fHP >= m_fMaxHP)
                    m_fHP = m_fMaxHP;
            }
        }

        public float FMaxHP
        {
            get
            {
                return m_fMaxHP;
            }

            set
            {
                m_fMaxHP = value;
            }
        }

        public float FSpeed
        {
            get
            {
                return m_fSpeed;
            }

            set
            {
                m_fSpeed = value;
            }
        }

        public float FSta
        {
            get
            {
                return m_fSta;
            }

            set
            {
                m_fSta = value;

                if (m_fSta <= 0)
                    m_fSta = 0;

                if (m_fSta >= m_fMaxSta)
                    m_fSta = m_fMaxSta;
            }
        }

        public float FMaxSta
        {
            get
            {
                return m_fMaxSta;
            }

            set
            {
                m_fMaxSta = value;
            }
        }

        public CharState State
        {
            get
            {
                return m_State;
            }

            set
            {
                m_State = value;
            }
        }

        public bool IsJump
        {
            get
            {
                return isJump;
            }

            set
            {
                isJump = value;
            }
        }

        public float FMana
        {
            get
            {
                return m_fMana;
            }

            set
            {
                m_fMana = value;

                if (m_fMana <= 0)
                    m_fMana = 0;

                if (m_fMana >= m_fMaxMana)
                    m_fMana = m_fMaxMana;
            }
        }

        public float FMaxMana
        {
            get
            {
                return m_fMaxMana;
            }

            set
            {
                m_fMaxMana = value;
            }
        }

        public float FExp
        {
            get
            {
                return m_fExp;
            }

            set
            {
                m_fExp = value;
  
            }
        }

        public float FNextExp
        {
            get
            {
                return m_fNextExp;
            }

            set
            {
                m_fNextExp = value;
            }
        }

        public int NLevel
        {
            get
            {
                return m_nLevel;
            }

            set
            {
                m_nLevel = value;
            }
        }

        public bool IsRoll
        {
            get
            {
                return isRoll;
            }

            set
            {
                isRoll = value;
            }
        }

        public string sName
        {
            get
            {
                return m_sName;
            }

            set
            {
                m_sName = value;
            }
        }



        #endregion
    }

}
