using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEditor;
using System.Collections;
using Common.UI;

[CustomEditor(typeof(ToggleButton),true)]
public class ToggleButtonEditor : Editor
{
    //SerializedProperty transition;
 
    private void OnEnable()
    {
        //transition = serializedObject.FindProperty("transition");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //if(transition.enumValueIndex == 2)
        //{
        //    EditorGUILayout.PropertyField()
        //}

    }

}