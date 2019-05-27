using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// MovePos를 작업화면에서
/// 이동 걍로를 연결 시켜준다
/// </summary>

namespace Black
{
    namespace MovePosObj
    {

        public enum DrawFigure
        {
            Cube, Sphere,
        }

        [CustomEditor(typeof(MovePos))]
        public class MovePosEditor : Editor
        {
            MovePos movePos;


            private void OnEnable()
            {
                movePos = (MovePos)target;
            }

            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                EditorGUILayout.HelpBox("isEvent : 정지 시킨다, isPlayer : 플레이어와 적 캐릭터를 분리해서 감지 시킨다", MessageType.Info);
            }


            /// <summary>
            /// 작업 화면에 선을 이어주기 때문에
            /// OnSceneGUI에서 코딩한다 
            /// </summary>
            private void OnSceneGUI()
            {
                //타겟 지점에 색을 지정한다(플레이어와 적 캐릭터의 이동 경로 색을 다르게 한다)
                if (movePos.IsPlayerPos)
                {
                    if(!movePos.IsEvent)
                    {
                        Handles.color = Color.blue;
                    }
                    else if(movePos.IsEvent)
                    {
                        Handles.color = Color.green;
                    }
                }
                else if (!movePos.IsPlayerPos)
                {
                    Handles.color = Color.red;
                }                    

                Handles.CubeCap(0, movePos.transform.position, Quaternion.identity, 0.5f);

                if (movePos.NextPos != null)
                {
                    //선으로 이어준다
                    Handles.color = Color.green;
                    Handles.DrawLine(movePos.gameObject.transform.position,
                                     movePos.NextPos.position);
                }

            }

        }

    }
}
