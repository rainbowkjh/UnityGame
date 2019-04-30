using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Characters;

[CustomEditor(typeof(PlayerCtrl))]
public class PlayerCtrlEditor : Editor
{
    PlayerCtrl playerCtrl;

    /// <summary>
    /// 캐릭터 현재 데이터를 본다
    /// </summary>
    bool isStateOn = false;

    private void OnEnable()
    {
        playerCtrl = (PlayerCtrl)target;
    }

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        if(playerCtrl.CharactersData != null)
        {
            using (new ButtonColorEditor(Color.gray))
            {
                if (GUILayout.Button("현재 캐릭터 정보"))
                {
                    isStateOn = !isStateOn;
                }
            }

            CharState();
        }



    }

    private void CharState()
    {
        if(isStateOn)
        {
            EditorGUILayout.HelpBox("현재 적용 중인 캐릭터 데이터", MessageType.Info);
            EditorGUILayout.FloatField("Cur HP", playerCtrl.CharactersData.FHP);
            EditorGUILayout.FloatField("Cur MaxHP", playerCtrl.CharactersData.FMaxHP);
            EditorGUILayout.FloatField("Cur Speed", playerCtrl.CharactersData.FSpeed);
        }
    }

}
