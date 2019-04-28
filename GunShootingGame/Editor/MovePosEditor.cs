using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// MovePos를 작업화면에서
/// 이동 걍로를 연결 시켜준다
/// </summary>

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

    /// <summary>
    /// 작업 화면에 선을 이어주기 때문에
    /// OnSceneGUI에서 코딩한다 
    /// </summary>
    private void OnSceneGUI()
    {
        //타겟 지점에 색을 지정한다
        Handles.color = Color.red;
        Handles.CubeCap(0, movePos.transform.position, Quaternion.identity, 0.5f);

        if (movePos.NextPos!=null)
        {
            //선으로 이어준다
            Handles.color = Color.green;
            Handles.DrawLine(movePos.gameObject.transform.position,
                             movePos.NextPos.position);
        }        

    }

}
