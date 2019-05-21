using Black.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Black
{
    namespace MovePosObj
    {
        public class NextStage : MonoBehaviour
        {
            [SerializeField,Header("다음 스테이지 선택 UI를 가져온다")]
            StageManager stageManager;

            private void OnTriggerEnter(Collider other)
            {
                if(other.tag.Equals("Player"))
                {
                    stageManager.NextStageSelection();
                }
            }
        }

    }
}
