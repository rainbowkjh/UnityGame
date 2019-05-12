using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 버튼의 컬러를 세팅해주는 기능
/// 
/// 사용 법
/// 버튼 코드 위에
/// using (new ButtonColorEditor(Color.적용 색))
/// {
///     버튼 관련 코드
/// }
/// </summary>
public class ButtonColorEditor : GUI.Scope
{
    private readonly Color color;

    /// <summary>
    /// 적용 시킬 컬러 값을 매개변수로 받는다
    /// </summary>
    /// <param name="color"></param>
    public ButtonColorEditor(Color color)
    {
        //원래의 색을 임시 저장
        this.color = GUI.backgroundColor;
        //설정한 값으로 변경
        GUI.backgroundColor = color;
    }

    /// <summary>
    /// 원래 색으로 돌려 놓는 기능
    /// </summary>
    protected override void CloseScope()
    {
        //원래의 색으로 돌려 놓음
        GUI.backgroundColor = this.color;
    }
}
