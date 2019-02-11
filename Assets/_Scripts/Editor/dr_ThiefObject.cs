using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ThiefObject), true)]
public class dr_ThiefObject : Editor
{
    // Draw the property inside the given rect
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ThiefObject myTarget = (ThiefObject)target;
        myTarget.cry = (enumMaterials)EditorGUILayout.EnumPopup("Material", myTarget.cry);
        if (GUI.changed)
            EditorGUIUtility.ExitGUI();
    }

}
