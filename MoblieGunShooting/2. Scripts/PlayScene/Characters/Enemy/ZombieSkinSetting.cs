using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace Characters
    {
        /// <summary>
        /// 좀비 캐릭터 스킨
        /// </summary>
        [Serializable]
        public enum ZombieSkin
        {
            AirportSecurity, AirportWorker, Bellhop,
            Businessman, Cheerleader, Clown,
            Farmer, FarmersDaughter, Firefighter,
            FootballPlayer, Grandma, Hobo,
            Industrial, Mechanic, Mountie,
            Pilot, Pimp, Prisoner,
            Prostitute, Roadworker, Robber,
            Runner, Santa, Shopkeeper,
            Soldier, Streetman, Tourist,
            Trucker,
            
        }


        public class ZombieSkinSetting : MonoBehaviour
        {
            [SerializeField, Header("좀비 스킨 설정")]
            ZombieSkin zombieSkin;

            [SerializeField]
            GameObject[] zombieSkinObj;

            int skinIndex = 0;
            
            /// <summary>
            /// 활성화 될때마다 스킨을 적용 시킨다
            /// </summary>
            private void OnEnable()
            {
                SkinDisable();
                SkinSetting();
            }

            /// <summary>
            /// 모든 스킨을 끈다
            /// </summary>
            void SkinDisable()
            {
                for (int i = 0; i < zombieSkinObj.Length; i++)
                {
                    zombieSkinObj[i].SetActive(false);
                } 
            }

            /// <summary>
            /// 해당하는 스킨만 켠다
            /// </summary>
            private void SkinSetting()
            {
                switch (zombieSkin)
                {
                    case ZombieSkin.AirportSecurity:
                        skinIndex = 0;
                        break;
                    case ZombieSkin.AirportWorker:
                        skinIndex = 1;
                        break;
                    case ZombieSkin.Bellhop:
                        skinIndex = 2;
                        break;
                    case ZombieSkin.Businessman:
                        skinIndex = 3;
                        break;
                    case ZombieSkin.Cheerleader:
                        skinIndex = 4;
                        break;
                    case ZombieSkin.Clown:
                        skinIndex = 5;
                        break;
                    case ZombieSkin.Farmer:
                        skinIndex = 6;
                        break;
                    case ZombieSkin.FarmersDaughter:
                        skinIndex = 7;
                        break;
                    case ZombieSkin.Firefighter:
                        skinIndex = 8;
                        break;
                    case ZombieSkin.FootballPlayer:
                        skinIndex = 9;
                        break;
                    case ZombieSkin.Grandma:
                        skinIndex = 10;
                        break;
                    case ZombieSkin.Hobo:
                        skinIndex = 11;
                        break;
                    case ZombieSkin.Industrial:
                        skinIndex = 12;
                        break;
                    case ZombieSkin.Mechanic:
                        skinIndex = 13;
                        break;
                    case ZombieSkin.Mountie:
                        skinIndex = 14;
                        break;
                    case ZombieSkin.Pilot:
                        skinIndex = 15;
                        break;
                    case ZombieSkin.Pimp:
                        skinIndex = 16;
                        break;
                    case ZombieSkin.Prisoner:
                        skinIndex = 17;
                        break;
                    case ZombieSkin.Prostitute:
                        skinIndex = 18;
                        break;
                    case ZombieSkin.Roadworker:
                        skinIndex = 19;
                        break;
                    case ZombieSkin.Robber:
                        skinIndex = 20;
                        break;
                    case ZombieSkin.Runner:
                        skinIndex = 21;
                        break;
                    case ZombieSkin.Santa:
                        skinIndex = 22;
                        break;
                    case ZombieSkin.Shopkeeper:
                        skinIndex = 23;
                        break;
                    case ZombieSkin.Soldier:
                        skinIndex = 24;
                        break;
                    case ZombieSkin.Streetman:
                        skinIndex = 25;
                        break;
                    case ZombieSkin.Tourist:
                        skinIndex = 26;
                        break;
                    case ZombieSkin.Trucker:
                        skinIndex = 27;
                        break;
                    
                }

                zombieSkinObj[skinIndex].SetActive(true);
            }

            /// <summary>
            /// 풀링에서 활성화 시킬때 외형 값을 적용 시킨다
            /// </summary>
            /// <param name="zombie"></param>
            /// <param name="zombieSkin"></param>
            /// <param name="enemySkin"></param>
            public void EnemyTypeSetting(ZombieSkin zombieSkin)
            {
                this.zombieSkin = zombieSkin;
            }


        }

    }
}
