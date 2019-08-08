using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지 클리어 조건 달성 시
/// 해당 지경으로 오면 마을로 돌아간다
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class StageClear : MonoBehaviour
        {

            StageManager manager;

            [SerializeField, Header("클리어 조건 미달성 시")]
            GameObject noClearUIObj;

            bool isNextArea = false; //미션 클리어
            bool isNoClear = false; //미 클리어

         
            Characters.PlayerCtrl player;

            [SerializeField]
            MainScene.StageNMenu stageMove;

            void Start()
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Characters.PlayerCtrl>();
                manager = GameObject.Find("StageManager").GetComponent<StageManager>();
                noClearUIObj.SetActive(false);
            }

            private void LateUpdate()
            {
                if(isNextArea)
                {
                    isNextArea = false;

                    GameManager.INSTANCE.isSceneMove = true;
                    GameManager.INSTANCE._DataManager.QuickSave(player);

                    stageMove.SafeAreMove();
                }

                if(isNoClear)
                {
                    isNoClear = false;
                    StartCoroutine(NoClearUIAct());
                }

            }


            IEnumerator NoClearUIAct()
            {
                noClearUIObj.SetActive(true);

                yield return new WaitForSeconds(1.5f);
                noClearUIObj.SetActive(false);
            }

            private void OnTriggerEnter(Collider other)
            {
                if(other.transform.CompareTag("Player"))
                {
                    //클리어 조건 달성 시
                    if(manager.IsClear)
                    {
                        isNextArea = true;
                    }

                    //미달성시 UI 출력
                    else
                    {
                        isNoClear = true;
                    }
                }
            }

        }

    }
}
