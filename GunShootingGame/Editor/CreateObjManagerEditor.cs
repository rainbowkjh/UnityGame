using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// CreateObjManager를 인스펙터 창을 에디트 한다
/// </summary>
[CustomEditor(typeof(CreateObjManager))]
public class CreateObjManagerEditor : Editor
{
    /// <summary>
    /// 매니져 스크립트를 가져온다
    /// </summary>
    CreateObjManager manager;
    /// <summary>
    /// 생성 시킬 오브젝트를 넣고 Add하면 리스트에 추가
    /// </summary>
    Object obj = null;
    /// <summary>
    /// 삭제 및 Scene에 생성할때 사용할 인덱스 값
    /// </summary>
    int nIndex = 0;
    
    /// <summary>
    /// Create를 눌르면 true가 되고
    /// Scene에 클릭하면 오브젝트 생성 
    /// </summary>
    bool isCreate = false;

    private void OnEnable()
    {
        manager = (CreateObjManager)target;
    }


    /// <summary>
    /// 인스펙터 창 에디트
    /// </summary>
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("Hierarchy에 올리기 전 오브젝트를 추가하고 Hierarchy에서는 오브젝트를 Scene에 추가를 한다", MessageType.Info);

        obj = EditorGUILayout.ObjectField(obj, typeof(Object), false);
        using (new ButtonColorEditor(Color.white))
        {
            if (GUILayout.Button("ADD"))
            {
                if(obj != null)
                {
                    manager.AddObj(obj);
                    obj = null; //추가 시키고 비워둔다
                }                
            }
        }
            

        nIndex = EditorGUILayout.IntField("오브젝트 선택(Index)", nIndex);
        
        if (nIndex >= manager.objList.Count - 1)
            nIndex = manager.objList.Count - 1;
        if (nIndex <= 0)
            nIndex = 0;

        ListCheack();

        //가로로 버튼을 정렬해준다
        using (new EditorGUILayout.HorizontalScope())
        {
            using (new ButtonColorEditor(Color.green))
            {
                if (GUILayout.Button("Crete"))
                {
                    isCreate = true;
                }
            }

            using (new ButtonColorEditor(Color.yellow))
            {
                if (GUILayout.Button("Cancle"))
                {
                    isCreate = false;
                }
            }

            using (new ButtonColorEditor(Color.red))
            {
                if (GUILayout.Button("Delete"))
                {
                    //선택한 인덱스에 오브젝트가 있으면 삭제 
                    if (manager.objList[nIndex] != null)
                        manager.DeleteObj(nIndex);
                }

            }
        }

    }

    /// <summary>
    /// 리스트 내용을 보여준다
    /// </summary>
    private void ListCheack()
    {
        for(int i=0;i<manager.objList.Count;i++)
        {            
            manager.objList[i] = EditorGUILayout.ObjectField(manager.objList[i],typeof(Object), false);
        }
    }


    /// <summary>
    /// Scene
    /// </summary>
    private void OnSceneGUI()
    {
        Event e = Event.current;

        if(isCreate)
        {
            if(e.type == EventType.MouseDown)
            {
                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    manager.CreateObj(nIndex, hit.point);                    
                    isCreate = false;
                }
            }
        }
    }

}
