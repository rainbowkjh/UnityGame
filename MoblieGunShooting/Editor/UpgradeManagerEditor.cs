using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Black
{
    namespace Manager
    {
        [CustomEditor(typeof(UpgradeManager))]
        public class UpgradeManagerEditor : Editor
        {
            UpgradeManager upgrade;

            bool isHelp = false;

            private void OnEnable()
            {
                upgrade = (UpgradeManager)target;
            }

            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if(GUILayout.Button("Help"))
                {
                    isHelp = !isHelp;
                }

                if(isHelp)
                {
                    EditorGUILayout.HelpBox("Text(UI에서 가져온다) : 0 HP, 1 RecoveryItem, 2 GrenadeItem, 3 Pistol, 4 AR, 5 SG", MessageType.Info);
                    EditorGUILayout.HelpBox("Weapone(플레이어의 무기를 가져온다) : 0 Pistol, 1 AR, 2 SG", MessageType.Info);
                }
                
            }

        }

    }
}
