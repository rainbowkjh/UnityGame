using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum 모음 ㅋㅋㅋ
/// </summary>
namespace Black
{
    namespace Manager
    {
        public enum eWeaponeType
        {
            AR, SG, SR, SMG,
            CharNoWeapone,
        }

        /// <summary>
        /// 탄의 외형을 변경 시켜준다
        /// </summary>
        public enum eProjectilesType
        {
            BubbleBlue, BubbleRose, Feather,
            CometBlue, FireBall, Spark, AttackExplosion,
        }

        /// <summary>
        /// 아이템 타입으로
        /// 각자 지정된 인벤 또는 슬롯에 들어가게 한다
        /// </summary>
        public enum eItemType
        {
            Bag, Pouch1, Pouch2,
        }

        /// <summary>
        /// 아이템 특성
        /// HP 회복, Mana 회복 등 분리
        /// </summary>
        public enum eBagItemType
        {
            HP_Recovery, Satiety_Recovery, Mana_Recovery, Thirst_Recovery,
            HP_Up, Satiety_Up, Mana_Up, Thirst_Up,
            Recovery,
            Dmg_Up,
        }

        /// <summary>
        /// 수류탄 터렛 등 아이템 속성
        /// </summary>
        public enum eSubItemType
        {
            Grenade, Turret,
        }

        /// <summary>
        /// 로드 할 씬 선택
        /// </summary>
        public enum eDialogDataPath
        {
            City,
        }

        /// <summary>
        /// 콜라이더 감지 시 오브젝트 태그 값을
        /// 작성 할 필요 없이 선택 하도록 한다
        /// GameManager에서 태그 값 세팅을 해주는 함수가 있다
        /// </summary>
        public enum eObjTag
        {
            Helicopter, Cop, Swat,
            SoulEx, Player, Enemy,
        }



       
        
    }
}