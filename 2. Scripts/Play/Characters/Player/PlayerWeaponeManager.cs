using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이 씬으로 넘어오면
/// 장착 무기를 생성 시킨다
/// 실수로 장착무기 없이 들어오면
/// 기본 무기를 생성 시켜준다
/// </summary>
namespace Characters
{
    public class PlayerWeaponeManager : MonoBehaviour
    {
        GameObject m_DefaultWeaponeObj;
        GameObject m_WeaponeObj;

        private void Start()
        {
         
        }

    }
}

