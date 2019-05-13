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
            private float speed = 5; //이동하면서 상황에 맞게 속도를 변경 시킬 예정
            private bool isLive = true;
            private bool isFire = false;
            private bool isReload = false;
            private bool isStop = true;
            private Transform nextMove; //다음 이동 위치를 받을 변수 

       //     AniCtrl aniCtrl;

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

            //public AniCtrl AniCtrl
            //{
            //    get
            //    {
            //        return aniCtrl;
            //    }

            //    set
            //    {
            //        aniCtrl = value;
            //    }
            //}

            public bool IsStop
            {
                get
                {
                    return isStop;
                }

                set
                {
                    isStop = value;
                }
            }

            public Transform NextMove
            {
                get
                {
                    return nextMove;
                }

                set
                {
                    nextMove = value;
                }
            }
            #endregion

            protected void CharInit(float hp, float maxHp)
            {
                this.hp = hp;
                this.maxHp = maxHp;                
            }

        }

    }
}
