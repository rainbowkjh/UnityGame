using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 적의 공격 대기 상태를 보여준다.
/// 글씨를 깜빡 거리거나,
/// 크기를 변경하는 등 효과 
/// </summary>
namespace Black
{
    namespace Characters
    {
        public class AttackBar : MonoBehaviour
        {
            [SerializeField, Header("Attack Bar 카메라를 바라보게 하기 위해 Obj")]
            GameObject UIObj;

            [SerializeField,Header("공격 대기 상태를 보여줄 Bar")]
            Image attackUI;

            [SerializeField,Header("danger! 텍스트")]
            GameObject AttackTextMesh;

            /// <summary>
            /// AttackTextMesh안에 있는 애니 컨트롤 
            /// </summary>
            Animator attackAni;

            readonly int hashAttack = Animator.StringToHash("Attack");

            private void Awake()
            {
                attackAni = AttackTextMesh.GetComponent<Animator>();
            }

            private void OnEnable()
            {
                //활성화 될때 마다 0값으로 초기화
                AttackGauge(0);
            }

            public void AttackGauge(float cur)
            {
                attackUI.fillAmount = cur;
            }

            public void AttackGauge(float cur, float max)
            {
                attackUI.fillAmount = cur / max;
            }

            public void AttackBarAni(bool attack)
            {
                //Debug.Log("Text Ani");
                attackAni.SetBool(hashAttack, attack);
            }

            public void LookAtCam()
            {
                UIObj.transform.LookAt(new Vector3( Camera.main.transform.position.x, UIObj.transform.position.y,
                    Camera.main.transform.position.z));
            }

        }

    }
}
