using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Black
{
    namespace Characters
    {

        public class CharactersData : MonoBehaviour
        {
            /// <summary>
            /// 유니티에서 데이터 수치를 세팅 할수 있도록 함
            /// </summary>
            [SerializeField]
            private float hp;
            [SerializeField]
            private float maxHp;
            [SerializeField]
            private float speed;
            private bool isLive = true;
            private bool isFire = false;
            private bool isReload = false;

            AniCtrl aniCtrl;

            #region Set,Get
            public float Hp
            {
                get
                {
                    return hp;
                }

                set
                {
                    hp = value;
                }
            }

            public float MaxHp
            {
                get
                {
                    return maxHp;
                }

                set
                {
                    maxHp = value;
                }
            }

            public float Speed
            {
                get
                {
                    return speed;
                }

                set
                {
                    speed = value;
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

            public bool IsFire
            {
                get
                {
                    return isFire;
                }

                set
                {
                    isFire = value;
                }
            }

            public bool IsReload
            {
                get
                {
                    return isReload;
                }

                set
                {
                    isReload = value;
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

            protected void CharInit(float hp, float maxHp, float speed)
            {
                this.hp = hp;
                this.maxHp = maxHp;
                this.speed = speed;
            }

        }

    }
}
