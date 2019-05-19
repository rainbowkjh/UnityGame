using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Black
{
    namespace Characters
    {
      
        /// <summary>
        /// 사람 적 캐릭터 스킨
        /// </summary>
        [Serializable]
        public enum EnemySkin
        {
            Biohazard, Businessman, Doctor,
            Farmer, FemaleHero, FemalePyro,
            FemaleSoldier, FemaleTrenchcoat, Hazard,
            HoodedMan, InjuredMan, Male_01,
            Male_02, Male_03, Male_04,
            MaleHunter, MaleTrenchcoat, OldMan,
            Prisoner, RoadWorker, Scout,
            Sheriff,            
        }

        public class EnemySkinSetting : MonoBehaviour
        {
    
            [Header("풀링 외 직접 적용 시켜 생성 시킬 적 세팅")]

            [SerializeField, Header("사람 스킨 설정")]
            EnemySkin enemySkin;
                        
            int skinIndex = 0;

          
            [SerializeField]
            GameObject[] enemySkinObj;



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
                for(int i=0;i< enemySkinObj.Length;i++)
                {
                    enemySkinObj[i].SetActive(false);
                }
            }

            /// <summary>
            /// 해당하는 스킨만 켠다
            /// </summary>
            private void SkinSetting()
            {
                switch (enemySkin)
                {
                    case Characters.EnemySkin.Biohazard:
                        skinIndex = 0;
                        break;
                    case Characters.EnemySkin.Businessman:
                        skinIndex = 1;
                        break;
                    case Characters.EnemySkin.Doctor:
                        skinIndex = 2;
                        break;
                    case Characters.EnemySkin.Farmer:
                        skinIndex = 3;
                        break;
                    case Characters.EnemySkin.FemaleHero:
                        skinIndex = 4;
                        break;
                    case Characters.EnemySkin.FemalePyro:
                        skinIndex = 5;
                        break;
                    case Characters.EnemySkin.FemaleSoldier:
                        skinIndex = 6;
                        break;
                    case Characters.EnemySkin.FemaleTrenchcoat:
                        skinIndex = 7;
                        break;
                    case Characters.EnemySkin.Hazard:
                        skinIndex = 8;
                        break;
                    case Characters.EnemySkin.HoodedMan:
                        skinIndex = 9;
                        break;
                    case Characters.EnemySkin.InjuredMan:
                        skinIndex = 10;
                        break;
                    case Characters.EnemySkin.Male_01:
                        skinIndex = 11;
                        break;
                    case Characters.EnemySkin.Male_02:
                        skinIndex = 12;
                        break;
                    case Characters.EnemySkin.Male_03:
                        skinIndex = 13;
                        break;
                    case Characters.EnemySkin.Male_04:
                        skinIndex = 14;
                        break;
                    case Characters.EnemySkin.MaleHunter:
                        skinIndex = 15;
                        break;
                    case Characters.EnemySkin.MaleTrenchcoat:
                        skinIndex = 16;
                        break;
                    case Characters.EnemySkin.OldMan:
                        skinIndex = 17;
                        break;
                    case Characters.EnemySkin.Prisoner:
                        skinIndex = 18;
                        break;
                    case Characters.EnemySkin.RoadWorker:
                        skinIndex = 19;
                        break;
                    case Characters.EnemySkin.Scout:
                        skinIndex = 20;
                        break;
                    case Characters.EnemySkin.Sheriff:
                        skinIndex = 21;
                        break;
                    
                }

                enemySkinObj[skinIndex].SetActive(true);
            }

            /// <summary>
            /// 풀링에서 활성화 시킬때 외형 값을 적용 시킨다
            /// </summary>
            /// <param name="zombie"></param>
            /// <param name="zombieSkin"></param>
            /// <param name="enemySkin"></param>
            public void EnemyTypeSetting(EnemySkin enemySkin)
            {        
                this.enemySkin = enemySkin;
            }

        }

    }
}
