using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적 캐릭터의 이동경로를 관리 한다
/// 
/// m_MovePosGroup - 이동 시킬 위치를 지정하고 
///                  묶어 놓은 그룹을 지정한다.(각 캐릭터 마다 이동 경로 설정)
/// </summary>
namespace Characters
{
    public class EnemyMovePos : MonoBehaviour
    {
        [SerializeField, Header("이동 시킬 위치의 그룹 오브젝트, 그룹 오브젝트 위치가 최종 위치가 된다")]
        GameObject m_MovePosGroup;

        //이름으로 그룹을 검색하여 그룹안의 트랜스폼들을 가져온다
        Transform[] m_MovePosTr;
        int m_nextPos = 1;

        #region Set,Get
        public Transform[] MovePosTr
        {
            get
            {
                return m_MovePosTr;
            }

            set
            {
                m_MovePosTr = value;
            }
        }

        public int NextPos
        {
            get
            {
                return m_nextPos;
            }

            set
            {
                m_nextPos = value;
            }
        }

        public GameObject MovePosGroup
        {
            get
            {
                return m_MovePosGroup;
            }

            set
            {
                m_MovePosGroup = value;
            }
        }
        #endregion

        private void Start()
        {
            if (MovePosGroup != null)
                MovePosTr = MovePosGroup.GetComponentsInChildren<Transform>();
        }

    }

}
