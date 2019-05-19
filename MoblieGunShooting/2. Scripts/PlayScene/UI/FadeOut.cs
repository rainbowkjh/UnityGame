﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 쓰러지면
/// 화면을 어둡게 만든다
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class FadeOut : MonoBehaviour
        {
            [SerializeField,Header("화면을 어둡게 만들 UI")]
            CanvasGroup cg;

            [SerializeField, Header("Fade Out 속도"), Range(0.1f, 1f)]
            float fadeOutSpeed;

            [SerializeField, Header("GameOverText, 메인 버튼")]
            GameObject mainBtn;

            private void Start()
            {
                cg.alpha = 0.0f;
                cg.blocksRaycasts = false; //터치를 방해하기 떄문에 끈다
                mainBtn.SetActive(false);
            }

            /// <summary>
            /// 화면을 천천히 보이게 한다
            /// </summary>
            public void FadeInPlay()
            {
                cg.blocksRaycasts = false;

                if (cg.alpha > 0.0f)
                {
                    cg.alpha -= Time.deltaTime * fadeOutSpeed;
                }
            }

            /// <summary>
            /// 이벤트로 플레이어를 쓰러트렸을 때
            /// 상황에 맞게 화면을 어둡게 만들때
            /// </summary>
            public void FadeOutPlay()
            {
                cg.blocksRaycasts = true; // 게임 종료로 다른 기능 버튼을 못 누르게 한다

                if (cg.alpha < 1.0f)
                {
                    cg.alpha += Time.deltaTime * fadeOutSpeed;
                }
            }

            /// <summary>
            /// 게임 종료로 화면을 어둡게 만들떄
            /// 메인 버튼을 생성 시킨다
            /// </summary>
            public void GameOverFade()
            {
                cg.blocksRaycasts = true; // 게임 종료로 다른 기능 버튼을 못 누르게 한다

                if (cg.alpha < 1.0f)
                {
                    cg.alpha += Time.deltaTime * fadeOutSpeed;
                }

                if (cg.alpha == 1)
                {
                    MainBtnAct();
                }
            }

            /// <summary>
            /// 메인 버튼 생성
            /// </summary>
            void MainBtnAct()
            {
                mainBtn.SetActive(true);
            }

            //메인으로 넘기는 버튼 함수



        }

    }
}

