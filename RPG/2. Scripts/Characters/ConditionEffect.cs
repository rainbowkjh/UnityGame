using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터의 상태 변화에 따라
/// 이펙트를 실행 시킨다
/// HitDmg에서 호출 시킨다
/// (모든 캐릭터에 적용)
/// </summary>
namespace Black
{
    namespace Characters
    {

        public class ConditionEffect : MonoBehaviour
        {
            [SerializeField,Header("기절 상태 시 이펙트")]
            ParticleSystem stunEffect;

            [SerializeField, Header("회피 중 무적 상태 이펙트")]
            ParticleSystem rollEffect;


            CharactersData characters;

            private void Start()
            {
                characters = GetComponent<CharactersData>();
            }

            /// <summary>
            /// 캐릭터가 기절효과로
            /// 행동 불능일때
            /// </summary>
            public void StunEffect()
            {
                //이펙트가 실행이 안되어 있고
                //스턴 상태가 걸리면 실행
                if(!stunEffect.isPlaying
                    && characters.IsStun)
                {
                    stunEffect.Play();
                }                
            }

            /// <summary>
            /// 스터 효과가 끝나면 정지 시킨다
            /// </summary>
            public void StunEffecStop()
            {
                if (!characters.IsStun)
                    stunEffect.Stop();
            }

            /// <summary>
            /// 회피 동작 시 무적 상태일때
            /// </summary>
            public void RollEffect()
            {
                if(!rollEffect.isPlaying && characters.IsRoll)
                {
                    rollEffect.Play();
                }
            }

            public void RollEffectStop()
            {
                if (!characters.IsRoll)
                    rollEffect.Stop();
            }

        }

    }
}
