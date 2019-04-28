using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform))]
public class TransformEditor : Editor
{
    Transform transform;
    Vector3 rot;
    
    private void OnEnable()
    {
        transform = (Transform)target;        
    }

    
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        transform.position = EditorGUILayout.Vector3Field("  Position", transform.position);

        rot = EditorGUILayout.Vector3Field("  Rotation", transform.eulerAngles);
        transform.rotation = Quaternion.Euler(rot);

        transform.localScale = EditorGUILayout.Vector3Field("  Scale", transform.localScale);

        using (new ButtonColorEditor(Color.red))
        {
            if (GUI.Button(new Rect(0, 0, 20, 20), "R"))
            {
                transform.position = Vector3.zero;
            }
        }

        using (new ButtonColorEditor(Color.yellow))
        {
            if (GUI.Button(new Rect(0, 20, 20, 20), "R"))
            {
                rot = Vector3.zero;
                transform.rotation = Quaternion.identity;
            }
        }
            

        using (new ButtonColorEditor(Color.green))
        {
            if (GUI.Button(new Rect(0, 40, 20, 20), "R"))
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
            

        EditorGUILayout.HelpBox("R 클릭 오브젝트의 위치, 회전, 크기를 초기값으로 변경 해준다", MessageType.Info);

        using (new ButtonColorEditor(Color.grey))
        {
            if (GUILayout.Button("Reset"))
            {
                transform.position = Vector3.zero;
                rot = Vector3.zero;
                transform.rotation = Quaternion.identity;
                transform.localScale = new Vector3(1, 1, 1);
            }

        }

     
    }


}
