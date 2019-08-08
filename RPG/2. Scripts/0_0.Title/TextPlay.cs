using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 타이틀 화면에서
/// 글자를 깜빡이도록 한다
/// 
/// 화면을 클릭하면
/// 로비화면으로 이동 시키는 기능 추가
/// (글자 깜빡이는 기능을 다른데서 사용 안할꺼 같아서
///  다른 기능도 같이 넣었음)
/// </summary>
namespace Black
{
    namespace Manager
    {
        public class TextPlay : MonoBehaviour
        {

            [SerializeField]
            GameObject obj;

            bool isRoof = false;

            private void Update()
            {
                if(!isRoof)
                {
                    StartCoroutine(ObjAct());
                }

                if(Input.GetMouseButtonDown(0))
                {
                    NextScene();
                }

            }


            IEnumerator ObjAct()
            {
                isRoof = true;
                yield return new WaitForSeconds(0.5f);
                obj.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                obj.SetActive(true);
                isRoof = false;
            }

            void NextScene()
            {
                SceneManager.LoadScene("Lobby");
            }

        }

    }
}
