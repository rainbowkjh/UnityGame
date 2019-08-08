using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC 캐릭터 제어
/// 주로 대화 창을 활성화 시킨다
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class NpcCtrl : MonoBehaviour
        {
            [SerializeField, Header("캐릭터의 이름, 대화 순서를 결정하게 된다")]
            string charName;

            Animator ani;

            readonly int hashTalk = Animator.StringToHash("Talk");
            readonly int hashTalkID = Animator.StringToHash("TalkID");

            public string CharName { get => charName; set => charName = value; }

            private void Start()
            {
                ani = GetComponent<Animator>();
            }

            public void AniTalk(bool talk, int id)
            {
                ani.SetInteger(hashTalkID, id);
                ani.SetBool(hashTalk, talk);
            }

        }

    }
}
