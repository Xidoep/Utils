using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SerializeScriptableObject : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(SerializeScriptableObject))]
public class ScriptableObject_MostrableDrawer : PropertyDrawer
{
    private Editor editor = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);

        if (property.objectReferenceValue != null)
        {
            //property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, GUIContent.none);
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
        }

        
        if (property.isExpanded)
        {
            EditorGUI.indentLevel++;

            if (!editor)
                Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);

            if (editor)
                editor.OnInspectorGUI();

            EditorGUI.indentLevel--;
        }
        
      
    }
    public override bool CanCacheInspectorGUI(SerializedProperty property)
    {
        return base.CanCacheInspectorGUI(property);
    }
}


[CanEditMultipleObjects]
[CustomEditor(typeof(ScriptableObject),true)]
public class ScriptaObjectEditor : Editor
{

}