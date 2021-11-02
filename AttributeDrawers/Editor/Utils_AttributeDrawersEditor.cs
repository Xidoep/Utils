using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using XS_Utils;

[CustomPropertyDrawer(typeof(InformacioAttribute))]
public class InformacioDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.SelectableLabel(position,$"{property.name.ToUpper()}: {property.stringValue}" , new GUIStyle(GUI.skin.label) {fontSize = 10, fontStyle = FontStyle.Italic, normal = new GUIStyleState() {textColor = Color.gray } });
    }

}

[CustomPropertyDrawer(typeof(ListToPopupAttribute))]
public class ListToPopupDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ListToPopupAttribute atb = attribute as ListToPopupAttribute;
        List<string> stringList = null;
        //Posar nou sistema.
        if (atb.myType.GetProperty(atb.propertyName) != null)
        {
            stringList = atb.myType.GetProperty(atb.propertyName).GetValue(atb.myType) as List<string>;
        }
        if (stringList != null && stringList.Count != 0)
        {
            int selectedIndex = Mathf.Max(stringList.IndexOf(property.stringValue), 0);
            selectedIndex = EditorGUI.Popup(position, property.name, selectedIndex, stringList.ToArray());
            property.stringValue = stringList[selectedIndex];
        }
        else EditorGUI.PropertyField(position, property, label);
    }
}




