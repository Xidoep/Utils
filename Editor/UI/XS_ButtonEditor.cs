using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(XS_Button))]
[CanEditMultipleObjects]
public class XS_ButtonEditor : ButtonEditor
{
    SerializedProperty onEnter;
    SerializedProperty onExit;

    private new void OnEnable()
    {
        onEnter = serializedObject.FindProperty("onEnter");
        onExit = serializedObject.FindProperty("onExit");
        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        XS_Button button = (XS_Button)target;

        EditorGUILayout.PropertyField(onEnter, new GUIContent("On Enter"));
        EditorGUILayout.PropertyField(onExit, new GUIContent("On Exit"));

        serializedObject.ApplyModifiedProperties();
    }
}
