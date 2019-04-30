using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform))]
public class TransformEditor : Editor
{
    Transform transform;
    Vector3 rot;
    Vector3 localRot;
    
    private void OnEnable()
    {
        transform = (Transform)target;        
    }

    
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        #region 월드 좌표
        /*
        using (new ButtonColorEditor(Color.red))
        {
            if (GUI.Button(new Rect(0, 0, 20, 20), "R"))
            {
                transform.position = Vector3.zero;
            }
        }
        transform.position = EditorGUILayout.Vector3Field("  Position", transform.position);



        rot = EditorGUILayout.Vector3Field("  Rotation", transform.eulerAngles);
        transform.rotation = Quaternion.Euler(rot);

        using (new ButtonColorEditor(Color.yellow))
        {
            if (GUI.Button(new Rect(0, 20, 20, 20), "R"))
            {
                rot = Vector3.zero;
                transform.rotation = Quaternion.identity;
            }
        }
        */
        #endregion

        using (new EditorGUILayout.HorizontalScope())
        {
            using (new ButtonColorEditor(Color.red))
            {
                if (GUILayout.Button("R"))
                {
                    transform.localPosition = Vector3.zero;
                }

                transform.localPosition = EditorGUILayout.Vector3Field("  LocolPosition", transform.localPosition);
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            using (new ButtonColorEditor(Color.yellow))
            {
                if (GUILayout.Button("R"))
                {
                    localRot = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                }

                localRot = EditorGUILayout.Vector3Field("  LocolRotation", transform.localEulerAngles);
                transform.localRotation = Quaternion.Euler(localRot);
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            using (new ButtonColorEditor(Color.green))
            {
                if (GUILayout.Button( "R"))
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

                transform.localScale = EditorGUILayout.Vector3Field("  Scale", transform.localScale);
            }

        }


        EditorGUILayout.HelpBox("R 클릭 오브젝트의 위치, 회전, 크기를 초기값으로 변경 해준다", MessageType.Info);

        using (new ButtonColorEditor(Color.grey))
        {
            if (GUILayout.Button("Reset"))
            {
                transform.localPosition = Vector3.zero;
                localRot = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                transform.localScale = new Vector3(1, 1, 1);
            }

        }

     
    }


}
